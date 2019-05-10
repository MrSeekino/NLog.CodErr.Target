using System;
using System.Collections.Generic;
using System.Dynamic;

namespace NLog.CodErr.Target
{
    public static class Extensions
    {
        public static void SetProperty(this ExpandoObject input, string propertyName, object propertyValue)
        {
            var expandoDict = input as IDictionary<string, object>;

            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        public static string ToUniversalISO8601(this DateTime input)
        {
            return input.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
