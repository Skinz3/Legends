using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.DesignPattern
{
    public class DevelopmentManager
    {
        public static BindingFlags BindingFlags = BindingFlags.NonPublic | BindingFlags.Public
          | BindingFlags.Default | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance | BindingFlags.Static;

        public const string TypeLog = "Type: {0} State: {1} Comment: {2}";
        public const string MethodLog = "Method: {0} Type: {1} State: {2} Comment: {3}";
        public const string FieldLog = "Field: {0} Type: {1} State: {2} Comment: {3}";

        public static string Analyse(Assembly assembly)
        {
            string result = string.Empty;
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var type in assembly.GetTypes())
            {
                InDevelopmentAttribute attribute = type.GetCustomAttributes(false).OfType<InDevelopmentAttribute>().FirstOrDefault();

                if (attribute != null)
                {
                    result += string.Format(TypeLog, type.Name, attribute.State, attribute.Comment);
                    result += Environment.NewLine;
                }

                foreach (var method in type.GetMethods(BindingFlags))
                {
                    attribute = method.GetCustomAttributes(false).OfType<InDevelopmentAttribute>().FirstOrDefault();

                    if (attribute != null)
                    {
                        result += string.Format(MethodLog, method.Name, type.Name, attribute.State, attribute.Comment);
                        result += Environment.NewLine;
                    }
                }
                foreach (var field in type.GetFields(BindingFlags))
                {
                    attribute = field.GetCustomAttributes(false).OfType<InDevelopmentAttribute>().FirstOrDefault();

                    if (attribute != null)
                    {
                        result += string.Format(FieldLog, field.Name, type.Name, attribute.State, attribute.Comment);
                        result += Environment.NewLine;
                    }
                }
            }
            result += "Analysis executed in " + stopwatch.ElapsedMilliseconds + "ms";
            return result;
        }
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class InDevelopmentAttribute : Attribute
    {
        public InDevelopmentState State
        {
            get;
            private set;
        }
        public string Comment
        {
            get;
            private set;
        }
        public InDevelopmentAttribute(InDevelopmentState state = InDevelopmentState.TODO, string comment = null)
        {
            this.State = state;
            this.Comment = comment;
        }
    }
    public enum InDevelopmentState
    {
        TODO,
        STARTED,
        NOT_VERIFIED,
        OPTIMIZATION,
        HAS_BUG,
        THINK_ABOUT_IT,
        BAD_SPELLING,
        TEMPORARY,

    }
}
