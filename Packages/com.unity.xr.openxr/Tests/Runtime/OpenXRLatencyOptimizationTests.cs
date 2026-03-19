using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR.Features.Mock;

namespace UnityEngine.XR.OpenXR.Tests
{
    class OpenXRLatencyOptimizationTests : OpenXRLoaderSetup
    {
        public override void BeforeTest()
        {
            NativeApi.EnableLatencyStats = true;
            base.BeforeTest();
        }

        public override void AfterTest()
        {
            NativeApi.EnableLatencyStats = false;
            base.AfterTest();
        }

        static readonly OpenXRSettings.LatencyOptimization[] s_latencyOptimizations = {
            OpenXRSettings.LatencyOptimization.PrioritizeRendering,
            OpenXRSettings.LatencyOptimization.PrioritizeInputPolling,
        };

        [UnityTest]
        public IEnumerator MeasureLatency([ValueSource(nameof(s_latencyOptimizations))] OpenXRSettings.LatencyOptimization priorityStrategy)
        {
            Initialize();
            // By default, the project's settings are set, replacing with test case settings
            NativeApi.SetLatencyOptimization(priorityStrategy);
            Start();

            yield return new WaitForXrFrame(100); // Warm up

            if (!TryGetFirstDisplaySubsytem(out var displaySubsystem))
            {
                Assert.Inconclusive("No XRDisplaySubsystem instance found during testing. Make sure XR is set up correctly.");
            }
            if (!NativeApi.EnableLatencyStats)
            {
                Assert.Inconclusive("Latency Stat measurement wasn't enabled. Make sure that the test is set up for latency measurements.");
            }

            yield return MeasureLatencyFromStats(displaySubsystem, priorityStrategy);

            StopAndShutdown();
        }

        [UnityTest]
        public IEnumerator CheckFrameSyncFunctionsCalledOncePerFrame(
            [ValueSource(nameof(s_latencyOptimizations))] OpenXRSettings.LatencyOptimization priorityStrategy)
        {
            var waitFrameCallCount = 0;
            var beginFrameCallCount = 0;
            var endFrameCallCount = 0;

            MockRuntime.SetFunctionCallback(
                "xrWaitFrame",
                null,
                (_, _) =>
                {
                    waitFrameCallCount++;
                });
            MockRuntime.SetFunctionCallback(
                "xrBeginFrame",
                null,
                (_, _) =>
                {
                    beginFrameCallCount++;
                });
            MockRuntime.SetFunctionCallback(
                "xrEndFrame",
                null,
                (_, _) =>
                {
                    endFrameCallCount++;
                });

            Initialize();
            NativeApi.SetLatencyOptimization(priorityStrategy);
            Start();

            var testFrames = 500;
            var waitForEndFrame = new WaitForEndOfFrame();
            for (var i = 0; i <= testFrames; i++)
            {
                yield return waitForEndFrame;
            }

            Assert.AreEqual(testFrames, waitFrameCallCount, "xrWaitFrame calls doesn't match the frame count");
            Assert.AreEqual(testFrames, beginFrameCallCount, "xrBeginFrame calls doesn't match the frame count");
            // Coroutine wait is called right before the xrEndFrame call, so reduce one from testFrames
            Assert.AreEqual(testFrames - 1, endFrameCallCount, "xrEndFrame calls doesn't match the expected frame count");

            StopAndShutdown();
        }

        [UnityTest]
        public IEnumerator CheckFrameSynchronizationFunctionsAreCalledInOrder(
            [ValueSource(nameof(s_latencyOptimizations))] OpenXRSettings.LatencyOptimization priorityStrategy)
        {
            bool xrWaitFrameCalled = false;
            bool xrBeginFrameCalled = false;

            bool xrWaitFrameCalledMultipleTimes = false;
            bool xrBeginFrameInWrongOrder = false;
            bool xrEndFrameInWrongOrder = false;

            MockRuntime.SetFunctionCallback(
                "xrWaitFrame",
                (name) =>
                {
                    if (xrWaitFrameCalled)
                    {
                        xrWaitFrameCalledMultipleTimes = true;
                        return NativeTypes.XrResult.RuntimeFailure;
                    }

                    xrWaitFrameCalled = true;
                    return NativeTypes.XrResult.Success;
                });
            MockRuntime.SetFunctionCallback(
                "xrBeginFrame",
                (name) =>
                {
                    if (!xrWaitFrameCalled || xrBeginFrameCalled)
                    {
                        xrBeginFrameInWrongOrder = true;
                        return NativeTypes.XrResult.CallOrderInvalid;
                    }

                    xrBeginFrameCalled = true;
                    xrWaitFrameCalled = false;
                    return NativeTypes.XrResult.Success;
                });
            MockRuntime.SetFunctionCallback(
                "xrEndFrame",
                (name) =>
                {
                    if (!xrBeginFrameCalled)
                    {
                        xrEndFrameInWrongOrder = true;
                        return NativeTypes.XrResult.CallOrderInvalid;
                    }

                    xrBeginFrameCalled = false;
                    return NativeTypes.XrResult.Success;
                });

            Initialize();
            NativeApi.SetLatencyOptimization(priorityStrategy);
            Start();

            var waitFrameCoroutine = new WaitForXrFrame();
            for (int i = 0; i < 500; i++)
            {
                if (xrWaitFrameCalledMultipleTimes || xrBeginFrameInWrongOrder || xrEndFrameInWrongOrder)
                {
                    break;
                }
                yield return waitFrameCoroutine;
            }

            Assert.IsFalse(xrWaitFrameCalledMultipleTimes, "xrWaitFrame was called multiple times in a single frame");
            Assert.IsFalse(xrBeginFrameInWrongOrder, "xrBeginFrame was called out of order");
            Assert.IsFalse(xrEndFrameInWrongOrder, "xrEndFrame was called out of order");

            StopAndShutdown();
        }

        static IEnumerator MeasureLatencyFromStats(XRDisplaySubsystem displaySubsystem, OpenXRSettings.LatencyOptimization priorityStrategy)
        {
            const int frames = 1000;
            var framesFailed = 0;

            float[] inputLatencyMeasurements = new float[frames];
            float[] renderLatencyMeasurements = new float[frames];

            for (var i = 0; i < frames; i++)
            {
                yield return null; // Let a frame be run

                var waitFrameTime = GetTimeFromProfilerMarker(displaySubsystem, "waitFrameTime");
                var inputTickTime = GetTimeFromProfilerMarker(displaySubsystem, "inputTickTime");
                var beginFrameTime = GetTimeFromProfilerMarker(displaySubsystem, "beginFrameTime");

                var inputLatency = Mathf.Abs(inputTickTime - waitFrameTime);
                var renderLatency = Mathf.Abs(beginFrameTime - waitFrameTime);

                inputLatencyMeasurements[i] = inputLatency;
                renderLatencyMeasurements[i] = renderLatency;

                switch (priorityStrategy)
                {
                    case (OpenXRSettings.LatencyOptimization.PrioritizeRendering):
                        framesFailed += renderLatency > inputLatency ? 1 : 0;
                        break;
                    case (OpenXRSettings.LatencyOptimization.PrioritizeInputPolling):
                        framesFailed += inputLatency > renderLatency ? 1 : 0;
                        break;
                    default:
                        Assert.Fail($"Unsupported latency optimization setting used: {priorityStrategy}");
                        break;
                }
            }

            var inputLatencyAvg = inputLatencyMeasurements.Average();
            var renderLatencyAvg = renderLatencyMeasurements.Average();

            var failureRate = framesFailed / (float)frames;
            Assert.Pass($"Latency Optimization measurements for {priorityStrategy}\n" +
                $"Average Input Latency: {inputLatencyAvg} microseconds\n" +
                $"Average Render Latency: {renderLatencyAvg} microseconds\n" +
                $"Optimization fails per frame: {framesFailed}/{frames} ({failureRate * 100}%)");
        }

        static bool TryGetFirstDisplaySubsytem(out XRDisplaySubsystem subsystem)
        {
            var displays = new List<XRDisplaySubsystem>();
            SubsystemManager.GetSubsystems(displays);
            subsystem = displays.Count > 0 ? displays[0] : null;
            return subsystem != null;
        }

        static float GetTimeFromProfilerMarker(XRDisplaySubsystem displaySubsytem, string markerMame)
        {
            if (Provider.XRStats.TryGetStat(displaySubsytem, markerMame, out float time))
            {
                return time;
            }
            return 0;
        }

        static class NativeApi
        {
            const string LibraryName = "UnityOpenXR";

            public static bool EnableLatencyStats
            {
                get => Internal_GetEnableLatencyStats();
                set => Internal_SetEnableLatencyStats(value);
            }

            public static void SetLatencyOptimization(OpenXRSettings.LatencyOptimization latencyOptimization) =>
                Internal_SetLatencyOptimization(latencyOptimization);

            [DllImport(LibraryName, EntryPoint = "NativeConfig_GetEnableLatencyStats")]
            [return: MarshalAs(UnmanagedType.U1)]
            static extern bool Internal_GetEnableLatencyStats();

            [DllImport(LibraryName, EntryPoint = "NativeConfig_SetEnableLatencyStats")]
            static extern void Internal_SetEnableLatencyStats([MarshalAs(UnmanagedType.U1)] bool unityVersion);

            [DllImport(LibraryName, EntryPoint = "NativeConfig_SetLatencyOptimization")]
            static extern void Internal_SetLatencyOptimization(OpenXRSettings.LatencyOptimization latencyOptimzation);
        }
    }
}
