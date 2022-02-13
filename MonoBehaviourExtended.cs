using System;
using System.Collections;
using System.Threading.Tasks;
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
        protected void Log(string _string, LogType type = LogType.Log, UnityEngine.Object obj = null)
        {
            Log(_string, obj, type);
        }

        /// <summary>
        /// Logs debug string if debugMode is set to Debug or Verbose
        /// </summary>
        protected void Log(string _string, UnityEngine.Object _obj = null, LogType _type = LogType.Log)
        {
            if(_obj == null)
            {
                _obj = this;
            }

            if (debugMode > DebugMode.Off)
            {
                switch(_type)
                {
                    case LogType.Log:
                        Debug.Log(_string, _obj);
                        break;
                    case LogType.Warning:
                        Debug.LogWarning(_string, _obj);
                        break;
                    case LogType.Error:
                        Debug.LogError(_string, _obj);
                        break;
                    case LogType.Assertion:
                        Debug.LogAssertion(_string, _obj);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Logs debug string if debugMode is set to Verbose
        /// </summary>
        protected void LogVerbose(string _string, UnityEngine.Object obj = null, LogType type = LogType.Log)
        {
            if (obj == null)
            {
                obj = this;
            }

            if (debugMode > DebugMode.Debug)
            {
                switch(type)
                {
                    case LogType.Log:
                        Debug.Log(_string, obj);
                        break;
                    case LogType.Warning:
                        Debug.LogWarning(_string, obj);
                        break;
                    case LogType.Error:
                        Debug.LogError(_string, obj);
                        break;
                    case LogType.Assertion:
                        Debug.LogAssertion(_string, obj);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion Logging

        #region Do Things Later

        /// <summary>
        /// Invokes provided action at the end of the frame
        /// </summary>
        protected void DoEndOfFrame(Action _action)
        {
            StartCoroutine(DoEndOfFrameCoroutine(_action));
        }

        IEnumerator DoEndOfFrameCoroutine(Action _action)
        {
            yield return new WaitForEndOfFrame();
            _action?.Invoke();
        }

        /// <summary>
        /// Invokes provided action on the next frame (coroutine)
        /// </summary>
        protected void DoNextFrame(Action _action)
        {
            DoLater(_action, 1);
        }

        /// <summary>
        /// Invoke an action after a specified number of frames (coroutine)
        /// </summary>
        /// <param name="_frameDelay">The number of frames to wait before performing action</param>
        protected void DoLater(Action _action, int _frameDelay)
        {
            StartCoroutine(DoAfterFrameDelay(_action, _frameDelay));
        }

        /// <summary>
        /// Invoke an action after a time delay (Coroutine)
        /// </summary>
        /// <param name="_secondsDelay">The amount of time to delay the action in seconds</param
        /// <param name="_unscaledTime">Use Time.unscaledDeltaTime?</param>
        protected void DoLater(Action _action, float _secondsDelay, bool _unscaledTime = false)
        {
            if (_unscaledTime)
            {
                StartCoroutine(DoAfterUnscaledDelay(_action, _secondsDelay));
            }
            else
            {
                StartCoroutine(DoAfterDelay(_action, _secondsDelay));
            }
        }

        protected async void DoNextFrameAsync(Action _action)
        {
            await Task.Yield();
            _action?.Invoke();
        }

        protected async void DoLaterAsync(Action _action, float _secondsDelay, bool _unscaledTime = false)
        {
            if (_unscaledTime)
            {
                for (float timer = 0f; timer < _secondsDelay; timer += Time.unscaledDeltaTime)
                {
                    await Task.Yield();
                }
            }
            else
            {
                for (float timer = 0f; timer < _secondsDelay; timer += Time.deltaTime)
                {
                    await Task.Yield();
                }
            }

            _action.Invoke();
        }

        protected async void DoLaterAsync(Action _action, int _frames)
        {
            for (int i = 0; i < _frames; i++)
            {
                await Task.Yield();
            }

            _action.Invoke();
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
        IEnumerator DoAfterUnscaledDelay(Action _action, float _delay)
        {
            for (float timer = 0f; timer < _delay; timer += Time.unscaledDeltaTime)
            {
                yield return null;
            }

            _action.Invoke();
        }
        #endregion Do Things Later

    }
}