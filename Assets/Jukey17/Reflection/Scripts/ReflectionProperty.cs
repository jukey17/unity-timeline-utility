using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jukey17.Reflection
{
    public sealed class ReflectionProperty
    {
        readonly Type type;
        readonly Dictionary<string, PropertyInfo> map;

        public ReflectionProperty(Type type)
        {
            this.type = type;
            this.map = new Dictionary<string, PropertyInfo>();
        }
        
        public void Register(string propertyName, BindingFlags bindingFlags)
        {
            map[propertyName] = type.GetProperty(propertyName, bindingFlags);
        }

        public TValue GetValue<TValue>(string propertyName, object instance)
        {
            return (TValue) map[propertyName].GetValue(instance, null);
        }

        public TValue GetStaticValue<TValue>(string propertyName)
        {
            return GetValue<TValue>(propertyName, null);
        }

        public void SetValue(string propertyName, object instance, object value)
        {
            map[propertyName].SetValue(instance, value, null);
        }

        public void SetStaticValue(string propertyName, object value)
        {
            SetValue(propertyName, null, value);
        }
    }
}
