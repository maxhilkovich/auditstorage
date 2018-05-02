using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AuditStorageSystem.Framework.Attributes;


namespace AuditStorageSystem.Framework.Extensions
{
    public static class StringExtensions
    {
        public static T ParseAsEnum<T>(this string str) where T : struct, IConvertible
        {
            //if (str.FindEnumsList<T>().Count == 0)
                //Log.TestLog.Error($"There is no string mapping for '{str}' in type '{typeof(T)}'");
            try
            {
                return str.FindEnumsList<T>().First();
            }
            catch (InvalidOperationException)
            {
                //Assert.Fail($"'{str}' doesn't have enum mapping in {typeof(T).Name}");
                return default(T);
            }
        }

        public static IList<T> FindEnumsList<T>(this string str) where T : struct, IConvertible
        {
            Contract.Assert(!string.IsNullOrEmpty(str), "String cannot be empty or null. Cannot parse it as enum");

            var type = typeof(T);
            Contract.Assert(type.IsEnum, "T must be an enum type");

            return type.GetFields()
                .Select(x => new { Field = x, Attribute = x.GetCustomAttribute<StringMappingAttribute>() })
                .Where(x => x.Attribute != null && x.Attribute.StringName.Equals(str, StringComparison.OrdinalIgnoreCase))
                .Select(x => (T)x.Field.GetValue(null)).ToList();
        }
    }
}