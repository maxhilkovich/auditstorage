using AuditStorageSystem.Framework.Attributes;
using System;
using System.Linq;

namespace AuditStorageSystem.Framework.Extensions
{
public static class EnumExtensions
    {
        public static string GetStringMapping(this System.Enum obj)
        {
           return GetAttribute<StringMappingAttribute>(obj).StringName;
        }

        public static int GetValue(this System.Enum value)
        {
            return (int)System.Enum.ToObject(value.GetType(), value);
        }

        public static TAttribute GetAttribute<TAttribute>(this System.Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = System.Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
    }
}