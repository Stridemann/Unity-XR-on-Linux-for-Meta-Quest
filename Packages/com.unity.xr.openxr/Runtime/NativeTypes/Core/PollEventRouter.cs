using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Delegate type for a method that handles an event received from `xrPollEvent`.
    /// </summary>
    /// <param name="eventData">Pointer to the event data struct. Read `eventData.type` to determine the structure
    /// type of the event, then cast `eventData` to the corresponding struct type to read any additional information.
    /// </param>
    public unsafe delegate void XrPollEventCallback(XrEventDataBaseHeader* eventData);

    /// <summary>
    /// Manages subscriptions to events received from
    /// [xrPollEvent](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#xrPollEvent).
    ///
    /// The OpenXR Plug-in calls `xrPollEvent` every frame during <see cref="Application.onBeforeRender"/>.
    /// </summary>
    public static class PollEventRouter
    {
        static readonly HashSet<XrPollEventCallback> s_SubscribersToAllEvents = new();
        static readonly Dictionary<XrStructureType, HashSet<XrPollEventCallback>> s_TypedEventSubscribers = new();
        static int numSubscribersToEventReceived => s_SubscribersToAllEvents.Count + s_TypedEventSubscribers.Count;

        static void RegisterNativeCallback()
        {
            OpenXRLoaderBase.deinitializedInternal += ClearAllState;
            Internal_RegisterPollEventCallback(s_XrPollEventCallback);
        }

        static void UnregisterNativeCallback()
        {
            OpenXRLoaderBase.deinitializedInternal -= ClearAllState;
            Internal_UnregisterPollEventCallback();
        }

        static void ClearAllState(OpenXRLoaderBase _)
        {
            OpenXRLoaderBase.deinitializedInternal -= ClearAllState;
            s_SubscribersToAllEvents.Clear();
            s_TypedEventSubscribers.Clear();
            Internal_UnregisterPollEventCallback();
        }

        static readonly unsafe IntPtr s_XrPollEventCallback =
            Marshal.GetFunctionPointerForDelegate((XrPollEventCallback)OnXrPollEvent);

        [MonoPInvokeCallback(typeof(XrPollEventCallback))]
        static unsafe void OnXrPollEvent(XrEventDataBaseHeader* eventData)
        {
            foreach (var callback in s_SubscribersToAllEvents)
            {
                callback.Invoke(eventData);
            }

            if (!s_TypedEventSubscribers.ContainsKey(eventData->type))
                return;

            foreach (var callback in s_TypedEventSubscribers[eventData->type])
            {
                callback.Invoke(eventData);
            }
        }

        /// <summary>
        /// Subscribe to all events. <paramref name="callback"/> will be invoked whenever any event is
        /// received by `xrPollEvent`.
        /// </summary>
        /// <param name="callback">The delegate to invoke when any event is received.</param>
        /// <returns>`true` if the subscription attempt succeeded.
        /// `false` if <paramref name="callback"/> was already subscribed.</returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > The pointer argument passed by <paramref name="callback"/> is only valid in the scope of the callback.
        /// > If you require longer-term access to event data, cast the pointer to the corresponding struct pointer
        /// > type and dereference it to make a copy of the struct. Don't save the pointer itself.
        ///
        /// > [!NOTE]
        /// > Your callback is automatically unsubscribed whenever the OpenXR loader is deinitialized.
        /// </remarks>
        public static bool TrySubscribeToAllEvents(XrPollEventCallback callback)
        {
            var isSuccess = s_SubscribersToAllEvents.Add(callback);
            if (!isSuccess)
                return false;

            if (numSubscribersToEventReceived == 1) // the subscriber we just added is the first subscriber
                RegisterNativeCallback();

            return true;
        }

        /// <summary>
        /// Subscribe to a specific event type. <paramref name="callback"/> will be invoked whenever the given
        /// event type is received by `xrPollEvent`.
        /// </summary>
        /// <param name="eventType">The event type.</param>
        /// <param name="callback">The delegate to invoke when an event of type <paramref name="eventType"/>
        /// is received.</param>
        /// <returns>`true` if the subscription attempt succeeded.
        /// `false` if <paramref name="callback"/> was already subscribed.</returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > The pointer argument passed by <paramref name="callback"/> is only valid in the scope of the callback.
        /// > If you require longer-term access to event data, cast the pointer to the corresponding struct pointer
        /// > type and dereference it to make a copy of the struct. Don't save the pointer itself.
        ///
        /// > [!NOTE]
        /// > Your callback is automatically unsubscribed whenever the OpenXR loader is deinitialized.
        /// </remarks>
        public static bool TrySubscribeToEventType(XrStructureType eventType, XrPollEventCallback callback)
        {
            if (!s_TypedEventSubscribers.ContainsKey(eventType))
                s_TypedEventSubscribers.Add(eventType, new HashSet<XrPollEventCallback>());

            var isSuccess = s_TypedEventSubscribers[eventType].Add(callback);
            if (!isSuccess)
                return false;

            if (numSubscribersToEventReceived == 1) // the subscriber we just added is the first subscriber
                RegisterNativeCallback();

            return true;
        }

        /// <summary>
        /// Unsubscribe from all events.
        /// </summary>
        /// <param name="callback">The delegate to unsubscribe.</param>
        /// <returns>`true` if <paramref name="callback"/> was unsubscribed.
        /// `false` if <paramref name="callback"/> wasn't subscribed in the first place.</returns>
        /// <remarks>
        /// > [!NOTE]
        /// > All callbacks are automatically unsubscribed whenever the OpenXR loader is deinitialized.
        /// </remarks>
        public static bool TryUnsubscribeFromAllEvents(XrPollEventCallback callback)
        {
            var isSuccess = s_SubscribersToAllEvents.Remove(callback);
            if (!isSuccess)
                return false;

            if (numSubscribersToEventReceived == 0)
                UnregisterNativeCallback();

            return true;
        }

        /// <summary>
        /// Unsubscribe from a specific event type.
        /// </summary>
        /// <param name="eventType">The event type.</param>
        /// <param name="callback">The delegate to unsubscribe.</param>
        /// <returns>`true` if <paramref name="callback"/> was unsubscribed.
        /// `false` if <paramref name="callback"/> wasn't subscribed in the first place.</returns>
        /// <remarks>
        /// > [!NOTE]
        /// > All callbacks are automatically unsubscribed whenever the OpenXR loader is deinitialized.
        /// </remarks>
        public static bool TryUnsubscribeFromEventType(XrStructureType eventType, XrPollEventCallback callback)
        {
            if (!s_TypedEventSubscribers.TryGetValue(eventType, out var subscribedSet))
                return false;

            var isSuccess = subscribedSet.Remove(callback);
            if (!isSuccess)
                return false;

            if (subscribedSet.Count == 0)
                s_TypedEventSubscribers.Remove(eventType);

            if (numSubscribersToEventReceived == 0)
                UnregisterNativeCallback();

            return true;
        }

        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "messagepump_RegisterPollEventCallback")]
        static extern void Internal_RegisterPollEventCallback(IntPtr callback);

        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "messagepump_UnregisterPollEventCallback")]
        static extern void Internal_UnregisterPollEventCallback();
    }
}
