using System;
using System.Collections;
using UnityEngine;

namespace Hell.Display
{
    /// <summary>
    /// All Iplayables are able to be played by the timeline
    /// </summary>
    public interface IPlayable
    {
        bool IsRunning { get; set; }

        void PlayResolution();

        void PlayStart();

        void PlayUpdate(float time);

        void PlayEnd();

        float GetDuration();
    }

    /// <summary>
    /// Some extentions that add some functionalities to all Iplayables
    /// </summary>
    public static class IPlayableExtention
    {
        /// <summary>
        /// Extends iplayables to play a coroutine that plays itself
        /// </summary>
        public static IEnumerator Play(this IPlayable obj, Action preDisplayAction = null)
        {
            obj.IsRunning = true;

            obj.PlayResolution();

            if (preDisplayAction != null)
                preDisplayAction();

            return DisplayCoroutine(obj);
        }

        /// <summary>
        /// Extends iplayables with a coroutine that plays itself
        /// </summary>
        public static IEnumerator DisplayCoroutine(this IPlayable obj)
        {
            obj.PlayStart();

            yield return Stopwatch.PlayUntilReady(obj.GetDuration(), obj.PlayUpdate);

            obj.PlayEnd();

            obj.IsRunning = false;
        }
    }
}
