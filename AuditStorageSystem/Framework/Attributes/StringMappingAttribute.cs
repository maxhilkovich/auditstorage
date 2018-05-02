using System;

namespace AuditStorageSystem.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringMappingAttribute : Attribute
    {
        private readonly string stringName;
        private readonly string[] stringNames;

        public string StringName { get { return stringName; } }
        public string[] StringNames { get { return stringNames; } }

        public StringMappingAttribute(string stringName)
        {
            this.stringName = stringName;
        }

        public StringMappingAttribute(string[] stringNames)
        {
            this.stringNames = stringNames;
        }
    }
}