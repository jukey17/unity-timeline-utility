using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jukey17.Reflection
{
    public sealed class ReflectionField
    {
        readonly Type type;
        readonly Dictionary<string, FieldInfo> map;

        public ReflectionField(Type type)
        {
            this.type = type;
            this.map = new Dictionary<string, FieldInfo>();
        }
        
        public void Register(string fieldName, BindingFlags bindingFlags)
        {
            map[fieldName] = type.GetField(fieldName, bindingFlags);
        }

        public TValue GetValue<TValue>(string fieldName, object instance)
        {
            return (TValue) map[fieldName].GetValue(instance);
        }

        public TValue GetStaticValue<TValue>(string fieldName)
        {
            return GetValue<TValue>(fieldName, null);
        }

        public void SetValue(string fieldName, object instance, object value)
        {
            map[fieldName].SetValue(instance, value);
        }

        public void SetStaticValue(string fieldName, object value)
        {
            SetValue(fieldName, null, value);
        }
    }
}