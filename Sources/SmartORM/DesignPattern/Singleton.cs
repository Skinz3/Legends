using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartORM.DesignPattern
{
    public abstract class Singleton<T> where T : class
    {
        internal static class SingletonAllocator
        {
            internal static T instance;

            static SingletonAllocator()
            {
                Singleton<T>.SingletonAllocator.CreateInstance(typeof(T));
            }

            public static T CreateInstance(Type type)
            {
                ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
                T result;
                if (constructors.Length > 0)
                {
                    result = (Singleton<T>.SingletonAllocator.instance = (T)((object)Activator.CreateInstance(type)));
                }
                else
                {
                    ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, new ParameterModifier[0]);
                    if (constructor == null)
                    {
                        throw new Exception(type.FullName + " doesn't have a private/protected constructor so the property cannot be enforced.");
                    }
                    try
                    {
                        result = (Singleton<T>.SingletonAllocator.instance = (T)((object)constructor.Invoke(new object[0])));
                    }
                    catch (Exception innerException)
                    {
                        throw new Exception("The Singleton couldnt be constructed, check if " + type.FullName + " has a default constructor", innerException);
                    }
                }
                return result;
            }
        }

        public static T Instance
        {
            get
            {
                return Singleton<T>.SingletonAllocator.instance;
            }
            protected set
            {
                Singleton<T>.SingletonAllocator.instance = value;
            }
        }
    }
}
