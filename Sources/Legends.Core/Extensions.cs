using Legends.Core.IO;
using Legends.Core.Time;
using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Legends.Core
{
    public static class Extensions
    {
        /// <summary>
        /// T is Enum.
        /// </summary>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        public static T2 GetValueOrDefault<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key,T2 @default = default(T2))
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return @default;
            }
        }
        public static float GetValuePrct(this float value, float percentage)
        {
            return (value * (percentage / 100f));
        }
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            int count = enumerable.Count();

            if (count <= 0)
                return default(T);

            return enumerable.ElementAt(new AsyncRandom().Next(count));
        }
        public static Vector3 DeserializeVector3(LittleEndianReader reader)
        {
            float x = reader.ReadFloat();
            float z = reader.ReadFloat();
            float y = reader.ReadFloat();
            return new Vector3(x, y, z);
        }
        public static void Serialize(this Vector3 vector3, LittleEndianWriter writer)
        {
            writer.WriteFloat(vector3.X);
            writer.WriteFloat(vector3.Z);
            writer.WriteFloat(vector3.Y);
        }
        public static T[] Random<T>(this IEnumerable<T> enumerable, int count)
        {
            T[] array = new T[count];

            int lenght = enumerable.Count();

            if (lenght <= 0)
                return new T[0];

            var random = new AsyncRandom();

            for (int i = 0; i < count; i++)
            {
                array[i] = enumerable.ElementAt(random.Next(lenght));
            }

            return array;
        }
        public static T CreateDelegate<T>(this ConstructorInfo ctor)
        {
            List<ParameterExpression> list =
                Enumerable.ToList<ParameterExpression>(Enumerable.Select<ParameterInfo, ParameterExpression>((IEnumerable<ParameterInfo>)ctor.GetParameters(),
                (Func<ParameterInfo, ParameterExpression>)(param => Expression.Parameter(param.ParameterType, string.Empty))));

            var list2 = list.ConvertAll<Expression>(x => (Expression)x);
            return Expression.Lambda<T>((Expression)Expression.New(ctor, list2), list).Compile();
        }
        public static Delegate CreateDelegate(this MethodInfo method, params Type[] delegParams)
        {
            Type[] array = (
                from p in method.GetParameters()
                select p.ParameterType).ToArray<Type>();
            if (delegParams.Length != array.Length)
            {
                throw new Exception("Method parameters count != delegParams.Length");
            }
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, null, new Type[]
            {
                typeof(object)
            }.Concat(delegParams).ToArray<Type>(), true);
            ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
            if (!method.IsStatic)
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(method.DeclaringType.IsClass ? OpCodes.Castclass : OpCodes.Unbox, method.DeclaringType);
            }
            for (int i = 0; i < delegParams.Length; i++)
            {
                iLGenerator.Emit(OpCodes.Ldarg, i + 1);
                if (delegParams[i] != array[i])
                {
                    if (!array[i].IsSubclassOf(delegParams[i]) && !HasInterface(array[i], delegParams[i]))
                    {
                        throw new Exception(string.Format("Cannot cast {0} to {1}", array[i].Name, delegParams[i].Name + " check your parameters order."));
                    }
                    iLGenerator.Emit(array[i].IsClass ? OpCodes.Castclass : OpCodes.Unbox, array[i]);
                }
            }
            iLGenerator.Emit(OpCodes.Call, method);
            iLGenerator.Emit(OpCodes.Ret);
            return dynamicMethod.CreateDelegate(Expression.GetActionType(new Type[]
            {
                typeof(object)
            }.Concat(delegParams).ToArray<Type>()));
        }
        public static bool HasInterface(this Type type, Type interfaceType)
        {
            return type.FindInterfaces(new TypeFilter(FilterByName), interfaceType).Length > 0;
        }
        private static bool FilterByName(Type typeObj, object criteriaObj)
        {
            return typeObj.ToString() == criteriaObj.ToString();
        }
        public static object GetCustomAttribute(this MethodInfo methodInfo, Type attributeType)
        {
            return methodInfo.GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == attributeType);
        }
        public static string XMLSerialize(this object obj)
        {
            YAXSerializer serializer = new YAXSerializer(obj.GetType());
            return serializer.Serialize(obj);
        }
        public static object XMLDeserialize(this string content, Type type)
        {
            if (content == string.Empty)
                return Activator.CreateInstance(type);

            YAXSerializer serializer = new YAXSerializer(type);
            return Convert.ChangeType(serializer.Deserialize(content), type);
        }
        /// <summary>
        /// Less Faster then XMLDeserialize(this string content,Type type)
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static object XMLDeserialize(this string content, Assembly assembly)
        {
            string typeAsString = new string(content.Split('>')[0].Skip(1).ToArray());
            var type = assembly.GetTypes().FirstOrDefault(x => x.Name == typeAsString);
            return XMLDeserialize(content, type);
        }
        public static T XMLDeserialize<T>(this string content)
        {
            return (T)XMLDeserialize(content, typeof(T));
        }
        public static bool RandomAssertion(float percentage)
        {
            if (percentage <= 0)
            {
                return false;
            }
            if (percentage >= 1)
            {
                return true;
            }
            return new AsyncRandom().NextDouble(0, 1) <= percentage;
        }
    }
}
