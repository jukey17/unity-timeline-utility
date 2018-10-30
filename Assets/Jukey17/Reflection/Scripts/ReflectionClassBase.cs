using System;

namespace Jukey17.Reflection
{
    public abstract class ReflectionClassBase
    {
        public Type Type { get; private set; }

        protected ReflectionField Field
        {
            get { return field; }
        }

        protected ReflectionProperty Property
        {
            get { return property; }
        }

        protected ReflectionMethod Method
        {
            get { return method; }
        }

        readonly ReflectionField field;
        readonly ReflectionProperty property;
        readonly ReflectionMethod method;

        protected ReflectionClassBase(Type type)
        {
            Type = type;
            field = new ReflectionField(type);
            property = new ReflectionProperty(type);
            method = new ReflectionMethod(type);
        }
    }
}