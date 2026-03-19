using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR.Features.Mock;
using ProviderXRStats = UnityEngine.XR.Provider.XRStats;

namespace UnityEngine.XR.OpenXR.Tests
{
    class MetaPerformanceMetricsTests : OpenXRLoaderSetup
    {
        static readonly string kAppCPUFrametimeStr = "/perfmetrics_meta/app/cpu_frametime";
        static readonly string kAppGPUFrametimeStr = "/perfmetrics_meta/app/gpu_frametime";
        static readonly string kAppMotionToPhotonLatencyStr =
            "/perfmetrics_meta/app/motion_to_photon_latency";
        static readonly string kCompositorCPUFrametimeStr =
            "/perfmetrics_meta/compositor/cpu_frametime";
        static readonly string kCompositorGPUFrametimeStr =
            "/perfmetrics_meta/compositor/gpu_frametime";
        static readonly string kCompositorDroppedFrameCountStr =
            "/perfmetrics_meta/compositor/dropped_frame_count";
        static readonly string kDeviceCPUUtilizationAvgStr =
            "/perfmetrics_meta/device/cpu_utilization_average";
        static readonly string kDeviceCPUUtilizationWorstStr =
            "/perfmetrics_meta/device/cpu_utilization_worst";
        static readonly string kDeviceGPUUtilizationStr =
            "/perfmetrics_meta/device/gpu_utilization";

        readonly List<string> xrPathStrings = new()
        {
            kAppCPUFrametimeStr,
            kAppGPUFrametimeStr,
            kAppGPUFrametimeStr, // because we have 2 consumers
            kAppMotionToPhotonLatencyStr,
            kCompositorCPUFrametimeStr,
            kCompositorGPUFrametimeStr,
            kCompositorGPUFrametimeStr,  // because we have 2 consumers
            kCompositorDroppedFrameCountStr,
            kCompositorDroppedFrameCountStr, // because we have 2 consumers
            kDeviceCPUUtilizationAvgStr,
            kDeviceCPUUtilizationWorstStr,
            kDeviceGPUUtilizationStr
        };

        static readonly string kUnityStatsAppCpuTime = "perfmetrics.appcputime";
        static readonly string kUnityStatsAppGpuTime = "perfmetrics.appgputime";
        static readonly string kUnityStatsAppGpuTimeFunc = "GPUAppLastFrameTime";
        static readonly string kUnityStatsCompositorCpuTime =
            "perfmetrics.compositorcputime";
        static readonly string kUnityStatsCompositorGpuTime =
            "perfmetrics.compositorgputime";
        static readonly string kUnityStatsCompositorGpuTimeFunc =
            "GPUCompositorLastFrameTime";
        static readonly string kUnityStatsGpuUtil = "perfmetrics.gpuutil";
        static readonly string kUnityStatsCpuUtilAvg = "perfmetrics.cpuutilavg";
        static readonly string kUnityStatsCpuUtilWorst = "perfmetrics.cpuutilworst";
        static readonly string kUnityStatsDroppedFrameCount = "appstats.compositordroppedframes";
        static readonly string kUnityStatsDroppedFrameCountFunc = "droppedFrameCount";
        static readonly string kUnityStatsMotionToPhotonFunc = "motionToPhoton";

        readonly List<string> unityStatStrings = new()
        {
            kUnityStatsAppCpuTime,
            kUnityStatsAppGpuTime,
            kUnityStatsAppGpuTimeFunc,
            kUnityStatsCompositorCpuTime,
            kUnityStatsCompositorGpuTime,
            kUnityStatsCompositorGpuTimeFunc,
            kUnityStatsGpuUtil,
            kUnityStatsCpuUtilAvg,
            kUnityStatsCpuUtilWorst,
            kUnityStatsDroppedFrameCount,
            kUnityStatsDroppedFrameCountFunc,
            kUnityStatsMotionToPhotonFunc
        };

        readonly Dictionary<string, XrPerformanceMetricsCounterUnitMETA> unitMap = new()
        {
            {
                kAppCPUFrametimeStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_MILLISECONDS_META
            },
            {
                kAppGPUFrametimeStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_MILLISECONDS_META
            },
            {
                kAppMotionToPhotonLatencyStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_MILLISECONDS_META
            },
            {
                kCompositorCPUFrametimeStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_MILLISECONDS_META
            },
            {
                kCompositorGPUFrametimeStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_MILLISECONDS_META
            },
            {
                kCompositorDroppedFrameCountStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_GENERIC_META
            },
            {
                kDeviceCPUUtilizationAvgStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_PERCENTAGE_META
            },
            {
                kDeviceCPUUtilizationWorstStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_PERCENTAGE_META
            },
            {
                kDeviceGPUUtilizationStr,
                XrPerformanceMetricsCounterUnitMETA.XR_PERFORMANCE_METRICS_COUNTER_UNIT_PERCENTAGE_META
            }
        };

        enum XrPerformanceMetricsCounterUnitMETA
        {
            XR_PERFORMANCE_METRICS_COUNTER_UNIT_GENERIC_META = 0,
            XR_PERFORMANCE_METRICS_COUNTER_UNIT_PERCENTAGE_META = 1,
            XR_PERFORMANCE_METRICS_COUNTER_UNIT_MILLISECONDS_META = 2,
            XR_PERFORMANCE_METRICS_COUNTER_UNIT_BYTES_META = 3,
            XR_PERFORMANCE_METRICS_COUNTER_UNIT_HERTZ_META = 4,
            XR_PERFORMANCE_METRICS_COUNTER_UNIT_MAX_ENUM_META = 0x7FFFFFFF
        }

        [UnityTest]
        public IEnumerator TestAllMetrics()
        {
            base.InitializeAndStart();
            yield return new WaitForXrFrame(2);

            List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();
            SubsystemManager.GetSubsystems(displays);

            Assert.That(displays.Count, Is.EqualTo(1));

            // insert a -1 dummy value to ensure we're ready when we start querying
            for (int i = -1; i < 5; i++)
            {
                foreach (string xrPathString in xrPathStrings)
                {
                    MockRuntime.MetaPerformanceMetrics_SeedCounterOnce_Float(
                        xrPathString,
                        (float)i,
                        (uint)unitMap[xrPathString]
                    );
                }
            }

            // wait for results to come available.
            bool didSync = false;
            for (int i = 0; i < 60; i++)
            {
                if (ProviderXRStats.TryGetStat(displays[0], kUnityStatsAppCpuTime, out var sync))
                {
                    if (sync < 0)
                    {
                        didSync = true;
                        break;
                    }
                    if (sync > 0)
                        Assert.Fail("did not receive -1.f sync stat");
                }
                yield return new WaitForXrFrame();
            }
            Assert.True(didSync, "did not receive -1.f sync stat");

            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForXrFrame();
                foreach (string unityStatString in unityStatStrings)
                {
                    Assert.True(
                        ProviderXRStats.TryGetStat(displays[0], unityStatString, out var stat),
                        "did not get stat for " + unityStatString
                    );
                    Assert.That(stat, Is.EqualTo((float)i).Within(0.001));
                }
            }
        }
    }
}
