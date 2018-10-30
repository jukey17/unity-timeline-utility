using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jukey17.Reflection
{
    public sealed class ReflectionMethod
    {
        readonly Type type;
        readonly Dictionary<string, MethodInfo> map;

        public ReflectionMethod(Type type)
        {
            this.type = type;
            this.map = new Dictionary<string, MethodInfo>();
        }

        public void Register(string methodName, BindingFlags bindingFlags, params Type[] argumentTypes)
        {
            var methodInfo = type.GetMethod(methodName, bindingFlags, null, argumentTypes, null);
            var key = MakeMethodKey(methodName, argumentTypes);
            map[key] = methodInfo;
        }

        public void Register(string methodName, BindingFlags bindingFlags, Type[] genericTypes, Type[] argumentTypes)
        {
            var methodInfo = type.GetMethod(methodName, bindingFlags, null, argumentTypes, null);
            var key = MakeMethodKey(methodName, genericTypes, argumentTypes);
            map[key] = methodInfo.MakeGenericMethod(genericTypes);
        }

        public void Invoke(string methodName, object instance, params object[] arguments)
        {
            var key = MakeMethodKey(methodName, arguments);
            map[key].Invoke(instance, arguments);
        }

        public void Invoke(string methodName, object instance, Type[] genericTypes, object[] arguments)
        {
            var key = MakeMethodKey(methodName, genericTypes, arguments);
            map[key].Invoke(instance, arguments);
        }

        public TResult Invoke<TResult>(string methodName, object instance, params object[] arguments)
        {
            var key = MakeMethodKey(methodName, arguments);
            return (TResult) map[key].Invoke(instance, arguments);
        }

        public TResult Invoke<TResult>(string methodName, object instance, Type[] genericTypes, object[] arguments)
        {
            var key = MakeMethodKey(methodName, genericTypes, arguments);
            return (TResult) map[key].Invoke(instance, arguments);
        }

        public void InvokeStatic(string methodName, params object[] arguments)
        {
            Invoke(methodName, null, arguments);
        }

        public void InvokeStatic(string methodName, Type[] genericTypes, object[] arguments)
        {
            Invoke(methodName, null, genericTypes, arguments);
        }

        public TResult InvokeStatic<TResult>(string methodName, params object[] arguments)
        {
            return Invoke<TResult>(methodName, null, arguments);
        }

        public TResult InvokeStatic<TResult>(string methodName, Type[] genericTypes, object[] arguments)
        {
            return Invoke<TResult>(methodName, genericTypes, arguments);
        }

        static string MakeMethodKey(string methodName, params Type[] argumentTypes)
        {
            var argumentNames = string.Join("+", argumentTypes.Select(type => type.FullName).ToArray());
            // methodName#type0+type1+type2...
            return methodName + "#" + argumentNames;
        }
               
        static string MakeMethodKey(string methodName, params object[] arguments)
        {
            var argumentNames = string.Join("+", arguments.Select(argument => argument.GetType().FullName).ToArray());
            // methodName#type0+type1+type2...
            return methodName + "#" + argumentNames;
        }
        
        static string MakeMethodKey(string methodName, Type[] genericTypes, Type[] argumentTypes)
        {
            var genericNames = string.Join("+", genericTypes.Select(type => type.FullName).ToArray());
            var argumentNames = string.Join("+", argumentTypes.Select(type => type.FullName).ToArray());
            // methodName@T1+T2+T3+T4#type0+type1+type2...
            return methodName + "@" + genericNames + "#" + argumentNames;
        }

        static string MakeMethodKey(string methodName, Type[] genericTypes, object[] arguments)
        {
            var genericNames = string.Join("+", genericTypes.Select(type => type.FullName).ToArray());
            var argumentNames = string.Join("+", arguments.Select(argument => argument.GetType().FullName).ToArray());
            // methodName@T1+T2+T3+T4#type0+type1+type2...
            return methodName + "@" + genericNames + "#" + argumentNames;
        }
    }
}