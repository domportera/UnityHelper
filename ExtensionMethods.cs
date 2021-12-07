using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace DomsUnityHelper
{
    public static class ExtensionMethods
    {
        #region Finding Things
        /// <summary>
        /// Can get component on a disabled game object. This will enable then re-disable a disabled game object - so beware any OnEnabled or OnDisabled functions will be called on the target.
        /// Type provided must be a monobehaviour of course
        /// </summary>
        /// <returns>Component of type requested, or default value (null)</returns>
        public static T GetComponentHandleDisabled<T>(this GameObject _obj)
        {
            bool active = _obj.activeSelf;
            if(!active)
            {
                _obj.SetActive(true);
            }

            T component = _obj.GetComponent<T>();

            if(!active)
            {
                _obj.SetActive(false);
            }

            return component;
        }
        #endregion Finding Things

        #region Vector Operations

        #region Divide Self
        public static void DivideSelf(ref this Vector2 _numerator, Vector2 _denominator)
        {
            _numerator = _numerator / _denominator;
        }

        public static void DivideSelf(ref this Vector3 _numerator, Vector3 _denominator)
        {
            _numerator = _numerator.Divide(_denominator);
        }

        public static void DivideSelf(ref this Vector4 _numerator, Vector4 _denominator)
        {
            _numerator = _numerator.Divide(_denominator);
        }
        #endregion Divide Self

        #region Division

        public static Vector3 Divide(this Vector3 _numerator, Vector3 _denominator)
        {
            return new Vector3(_numerator.x / _denominator.x, _numerator.y / _denominator.y, _numerator.z / _denominator.z);
        }

        public static Vector4 Divide(this Vector4 _numerator, Vector4 _denominator)
        {
            return new Vector4(_numerator.x / _denominator.x, _numerator.y / _denominator.y, _numerator.z / _denominator.z, _numerator.w / _denominator.w);
        }
        #endregion Division

        #region Vector Components
        public static Vector2 YX(this Vector2 _vec)
        {
            return new Vector2(_vec.y, _vec.x);
        }
        public static Vector2 XY(this Vector3 _vec)
        {
            return new Vector2(_vec.x, _vec.y);
        }
        public static Vector2 YZ(this Vector3 _vec)
        {
            return new Vector2(_vec.y, _vec.z);
        }
        public static Vector2 XZ(this Vector3 _vec)
        {
            return new Vector2(_vec.y, _vec.z);
        }
        public static Vector2 YX(this Vector3 _vec)
        {
            return new Vector2(_vec.y, _vec.x);
        }
        public static Vector2 ZX(this Vector3 _vec)
        {
            return new Vector2(_vec.z, _vec.x);
        }
        public static Vector2 ZY(this Vector3 _vec)
        {
            return new Vector2(_vec.z, _vec.y);
        }
        #endregion Vector Components

        #region Vector to Color Conversion
        public static Color ToColor(this Vector4 _vector)
        {
            return (Color)_vector;
        }
        public static Color ToColor(this Vector3 _vector, float _alpha = 1f)
        {
            return new Color(_vector.x, _vector.y, _vector.z, _alpha);
        }
        #endregion Vector to Color Conversion

        #endregion Vector Operataions

        #region Mapping
        public static Vector2 Map(this Vector2 x, Vector2 in_min, Vector2 in_max, Vector2 out_min, Vector2 out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static Vector3 Map(this Vector3 x, Vector3 in_min, Vector3 in_max, Vector3 out_min, Vector3 out_max)
        {
            Vector3 a = x - in_min;
            Vector3 b = out_max - out_min;
            Vector3 numerator = Vector3.Scale(a, b);
            Vector3 denominator = (in_max - in_min);
            Vector3 offset = out_min;
            Vector3 result = numerator.Divide(denominator) + offset;
            return result;
        }

        public static Vector4 Map(this Vector4 x, Vector4 in_min, Vector4 in_max, Vector4 out_min, Vector4 out_max)
        {
            Vector4 a = x - in_min;
            Vector4 b = out_max - out_min;
            Vector4 numerator = Vector4.Scale(a, b);
            Vector4 denominator = (in_max - in_min);
            Vector4 offset = out_min;
            Vector4 result = numerator.Divide(denominator) + offset;
            return result;
        }

        public static double Map(this double x, double in_min, double in_max, double out_min, double out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static float Map(this float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static int Map(this int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (int)((x - in_min) * (out_max - out_min) / (float)(in_max - in_min) + out_min);
        }

        public static void MapSelf(ref this double x, double in_min, double in_max, double out_min, double out_max)
        {
            x = x.Map(in_min, in_max, out_min, out_max);
        }

        public static void MapSelf(ref this float x, float in_min, float in_max, float out_min, float out_max)
        {
            x = x.Map(in_min, in_max, out_min, out_max);
        }

        public static void MapSelf(ref this int x, int in_min, int in_max, int out_min, int out_max)
        {
            x = x.Map(in_min, in_max, out_min, out_max);
        }
        #endregion Mapping

        #region Averages

        public static float Average(this float x, float floatToAverageWith)
        {
            return (x + floatToAverageWith) / 2f;
        }
        public static Vector2 Average(this Vector2 x, Vector2 vectorToAverageWith)
        {
            return (x + vectorToAverageWith) / 2f;
        }
        public static Vector3 Average(this Vector3 x, Vector3 vectorToAverageWith)
        {
            return (x + vectorToAverageWith) / 2f;
        }
        public static Vector4 Average(this Vector4 x, Vector4 vectorToAverageWith)
        {
            return (x + vectorToAverageWith) / 2f;
        }

        public static float Average(this float[] floatsToAverage)
        {
            float sum = 0f;

            foreach(float f in floatsToAverage)
            {
                sum += f;
            }

            return sum / floatsToAverage.Length;
        }

        public static float Average(this int[] intsToAverage)
        {
            float sum = 0f;

            foreach(int f in intsToAverage)
            {
                sum += f;
            }

            return sum / intsToAverage.Length;
        }

        public static Vector2 Average(this Vector2[] vecsToAverage)
        {
            Vector2 sum = Vector2.zero;

            foreach(Vector2 f in vecsToAverage)
            {
                sum += f;
            }

            return sum / vecsToAverage.Length;
        }

        public static Vector3 Average(this Vector3[] vecsToAverage)
        {
            Vector3 sum = Vector3.zero;

            foreach(Vector3 f in vecsToAverage)
            {
                sum += f;
            }

            return sum / vecsToAverage.Length;
        }

        public static Vector4 Average(this Vector4[] vecsToAverage)
        {
            Vector4 sum = Vector4.zero;

            foreach(Vector4 f in vecsToAverage)
            {
                sum += f;
            }

            return sum / vecsToAverage.Length;
        }

        #endregion Averages

        #region Enums
        /// <summary>
        /// Gets the description using the [Description] tag of the provided enum. A nice way to have prettier and  easily updated enum names within an application UI
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if(name != null)
            {
                FieldInfo field = type.GetField(name);
                if(field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if(attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the enum value cast to an int
        /// </summary>
        /// <returns></returns>
        public static int GetInt(this Enum value)
        {
            return (int)(object)value;
        }

        #endregion Enums
    }
}