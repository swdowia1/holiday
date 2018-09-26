using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taskApi.Core
{
    public static class classExtension
    {
        public static string ToJSON(this object inputObject, bool IsIndented = false)
        {
            if (IsIndented)
            {
                return JsonConvert.SerializeObject(inputObject, Formatting.Indented);
            }
            else
                return JsonConvert.SerializeObject(inputObject);
        }

        public static string TrimText(this string input, int length = 210, string addText = "...")
        {
            if (string.IsNullOrEmpty(input))
                return "";
            else
            {
                if (input.Length > length)
                    return input.Substring(0, length) + addText;
                else
                    return input;
            }
        }

        public static string GetValue(this Object value)
        {
            if (value == null)
                return "";
            var type = value.GetType();

            if (type == null) throw new NullReferenceException();

            if (type == typeof(string))
                return value.ToString();
            else if (type == typeof(int))
                return ((int)value).ToString();
            else if (type == typeof(bool))
                return ((bool)value).ToString();
            else if (type == typeof(DateTimeOffset))
                return ((DateTimeOffset)value).ToString("yyyy-MM-dd");
            return "";
        }

        public static string ToExceptionString(this Exception ex)
        {
            return ex.Message + " " + ex.InnerException ?? "";
        }



        public static IEnumerable<string> SplitKey(this string DecompositionKeys, string separator = "|")
        {
            return DecompositionKeys.Split(new string[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Generowanie nazwa guid
        /// </summary>
        /// <param name="name">przedrostek</param>
        /// <param name="Length">na wypadek gdy w bazie jest ograniczenie</param>
        /// <returns></returns>
        public static string NameGuid(this string name, int Length = 0)
        {
            string result = name + System.Guid.NewGuid().ToString();
            if (Length == 0)
                return result;
            else
            {
                if (result.Length > Length)
                    return result.Substring(0, Length);
                else
                    return result;
            }
        }



        public static int ToId(this Object objectInput)
        {
            if (objectInput == null)
                throw new NullReferenceException();

            Type objectType = objectInput.GetType();

            if (objectType.BaseType == typeof(Enum))
                throw new ArgumentException("Object type can't be enum");

            var id = Convert.ToInt32(objectType.GetProperty("Id").GetValue(objectInput, null));

            return id;
        }

    }
}