using System;
using System.Collections;
using UnityEngine;

namespace DomsUnityHelper
{
    public abstract class MonoBehaviourExtended : MonoBehaviour
    {
        #region Logging
        protected enum DebugMode { Off, Debug, Verbose };
        [SerializeField] DebugMode debugMode = DebugMode.Off;
        protected enum LogType { Log, Warning, Error, Assertion };

        /// <summary>
        /// Logs debug string if debugMode is set to Debug or Verbose
        /// </summary>
        protected void LogDebug(string _string, LogType type = LogType.Log)
        {
            if(debugMode > DebugMode.Off)
            {
                switch(type)
                {
                    case LogType.Log:
                        Debug.Log(_string, this);
                        break;
                    case LogType.Warning:
                        Debug.LogWarning(_string, this);
                        break;
                    case LogType.Error:
                        Debug.LogError(_string, this);
                        break;
                    case LogType.Assertion:
                        Debug.LogAssertion(_string, this);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Logs debug string if debugMode is set to Verbose
        /// </summary>
        protected void LogDebugVerbose(string _string, LogType type = LogType.Log)
        {
            if(debugMode > DebugMode.Debug)
            {
                switch(type)
                {
                    case LogType.Log:
                        Debug.Log(_string, this);
                        break;
                    case LogType.Warning:
                        Debug.LogWarning(_string, this);
                        break;
                    case LogType.Error:
                        Debug.LogError(_string, this);
                        break;
                    case LogType.Assertion:
                        Debug.LogAssertion(_string, this);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion Logging

        #region Do Things Later

        /// <summary>
        /// Invokes provided action on the next frame
        /// </summary>
        protected void DoNextFrame(Action _action)
        {
            DoLater(_action, 1);
        }

        /// <summary>
        /// Invoke an action after a specified number of frames
        /// </summary>
        /// <param name="_frameDelay">The number of frames to wait before performing action</param>
        protected void DoLater(Action _action, int _frameDelay)
        {
            StartCoroutine(DoAfterFrameDelay(_action, _frameDelay));
        }

        /// <summary>
        /// Invoke an action after a time delay
        /// </summary>
        /// <param name="_secondsDelay">The amount of time to delay the action in seconds</param>
        protected void DoLater(Action _action, float _secondsDelay)
        {
            StartCoroutine(DoAfterDelay(_action, _secondsDelay));
        }

        IEnumerator DoAfterFrameDelay(Action _action, int _frames)
        {
            for(int i = 0; i < _frames; i++)
            {
                yield return null;
            }

            _action.Invoke();
        }

        IEnumerator DoAfterDelay(Action _action, float _delay)
        {
            for(float timer = 0f; timer < _delay; timer += Time.deltaTime)
            {
                yield return null;
            }

            _action.Invoke();
        }
        #endregion Do Things Later

    }
}