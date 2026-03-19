using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEditor.XR.OpenXR.Tests
{
    class PollEventRouterTests : MockRuntimeEditorTestBase
    {
        List<XrEventDataSpatialDiscoveryRecommendedEXT> m_EventsReceived = new();

        [TearDown]
        public override unsafe void TearDown()
        {
            PollEventRouter.TryUnsubscribeFromAllEvents(OnEventReceived);
            PollEventRouter.TryUnsubscribeFromEventType(
                XrStructureType.EventDataSpatialDiscoveryRecommendedEXT, OnEventReceived);

            base.TearDown();
            m_EventsReceived.Clear();
        }

        [Test]
        public unsafe void TrySubscribeToAllEvents_InvokesCorrectly()
        {
            m_Environment.Start();
            m_Environment.ProcessEventQueue();
            var isSuccess = PollEventRouter.TrySubscribeToAllEvents(OnEventReceived);

            Assert.IsTrue(isSuccess);

            isSuccess = PollEventRouter.TrySubscribeToAllEvents(OnEventReceived);

            Assert.IsFalse(isSuccess);

            var discoveryRecommended = new XrEventDataSpatialDiscoveryRecommendedEXT(123);
            var discoveryRecommendedPtr = new IntPtr(&discoveryRecommended);
            m_Environment.EnqueueMockEventData(discoveryRecommendedPtr);
            m_Environment.ProcessEventQueue();

            Assert.AreEqual(1, m_EventsReceived.Count);
            Assert.AreEqual(123, m_EventsReceived[0].spatialContext);

            m_EventsReceived.Clear();
            m_Environment.ProcessEventQueue();

            Assert.AreEqual(0, m_EventsReceived.Count);

            isSuccess = PollEventRouter.TryUnsubscribeFromAllEvents(OnEventReceived);

            Assert.IsTrue(isSuccess);

            m_Environment.EnqueueMockEventData(discoveryRecommendedPtr);
            m_Environment.ProcessEventQueue();

            Assert.AreEqual(0, m_EventsReceived.Count);
        }

        [Test]
        public unsafe void TrySubscribeToEventType_InvokesCorrectly()
        {
            m_Environment.Start();
            m_Environment.ProcessEventQueue();
            var isSuccess = PollEventRouter.TrySubscribeToEventType(
                XrStructureType.EventDataSpatialDiscoveryRecommendedEXT, OnEventReceived);

            Assert.AreEqual(true, isSuccess);

            isSuccess = PollEventRouter.TrySubscribeToEventType(
                XrStructureType.EventDataSpatialDiscoveryRecommendedEXT, OnEventReceived);

            Assert.IsFalse(isSuccess);

            var discoveryRecommended = new XrEventDataSpatialDiscoveryRecommendedEXT(123);
            var discoveryRecommendedPtr = new IntPtr(&discoveryRecommended);
            m_Environment.EnqueueMockEventData(discoveryRecommendedPtr);
            m_Environment.ProcessEventQueue();

            Assert.AreEqual(1, m_EventsReceived.Count);
            Assert.AreEqual(123, m_EventsReceived[0].spatialContext);

            m_EventsReceived.Clear();
            var eventWeArentListeningFor = new XrEventDataBaseHeader(XrStructureType.EventDataEyeCalibrationChangedML);
            m_Environment.EnqueueMockEventData(new IntPtr(&eventWeArentListeningFor));
            m_Environment.ProcessEventQueue();

            Assert.AreEqual(0, m_EventsReceived.Count);

            isSuccess = PollEventRouter.TryUnsubscribeFromEventType(
                XrStructureType.EventDataSpatialDiscoveryRecommendedEXT, OnEventReceived);

            Assert.AreEqual(true, isSuccess);

            m_Environment.EnqueueMockEventData(discoveryRecommendedPtr);
            m_Environment.ProcessEventQueue();

            Assert.AreEqual(0, m_EventsReceived.Count);
        }

        unsafe void OnEventReceived(XrEventDataBaseHeader* eventPtr)
        {
            Assert.AreEqual(XrStructureType.EventDataSpatialDiscoveryRecommendedEXT, eventPtr->type);
            m_EventsReceived.Add(*(XrEventDataSpatialDiscoveryRecommendedEXT*)eventPtr);
        }
    }
}
