using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Obj2Code
{
    public class Obj2Coder
    {

        private static Dictionary<Type, int> fieldOrdering = new Dictionary<Type, int>() {
            {typeof(int), 1 },
            {typeof(short), 1 },
            {typeof(int?), 1 },
            {typeof(short?), 1 },
            {typeof(string), 2 },
            {typeof(DateTime), 3 },
            {typeof(bool), 4 },
            {typeof(bool?), 4 },
        };


        public static string ToCode<T>(List<T> objectsToConvert, bool useBase = false)
        {
            Type typeToConvert = typeof(T);

            var sites = objectsToConvert.Select(t => Obj2Coder.ToCode(t, true)).ToList();

            return "var result = new List<" + typeToConvert.FullName + ">() {" + Environment.NewLine +
                String.Join("," + Environment.NewLine, sites) + Environment.NewLine +
                "};";
        }

        public static string ToCode(object objectToConvert, bool useBase = false)
        {
            Type typeToConvert = objectToConvert.GetType();
            if (useBase)
            {
                typeToConvert = typeToConvert.BaseType;
            }
            if (HasDefaultConstructor(typeToConvert))
            {
                StringBuilder s = new StringBuilder();

                s.Append("new ");
                s.Append(typeToConvert.FullName);
                s.Append("()");
                s.Append(" ");
                s.Append("{ ");

                var properties = typeToConvert.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var filteredProperties = properties.Where(t => fieldOrdering.Keys.Contains(t.PropertyType)).OrderBy(t => t.Name.Contains("ByID")).ThenBy(t => fieldOrdering[t.PropertyType]);

                foreach (var property in filteredProperties)
                {
                    if (property.CanRead && property.CanWrite)
                    {
                        var mget = property.GetGetMethod(false);
                        var mset = property.GetSetMethod(false);
                        if (mset != null && mget != null)
                        {
                            var propertyName = property.Name + " = ";
                            var value = mget.Invoke(objectToConvert, null);
                            if (value == null)
                            {
                                s.Append(propertyName);
                                s.Append("null, ");
                            }
                            else
                            {
                                var methodType = value.GetType();
                                if (methodType.Equals(typeof(string)))
                                {
                                    s.Append(propertyName);
                                    s.Append("\"");
                                    s.Append(value);
                                    s.Append("\"");
                                    s.Append(", ");
                                }
                                else if (methodType.Equals(typeof(DateTime)))
                                {
                                    s.Append(propertyName);
                                    DateTime dateTime = (DateTime)value;
                                    s.Append("DateTime.Parse(\"");
                                    s.Append(dateTime.ToUniversalTime().ToString("u"));
                                    s.Append("\")");
                                    s.Append(", ");
                                }
                                else
                                {
                                    s.Append(propertyName);
                                    s.Append(value);
                                    s.Append(", ");
                                }
                            }
                        }
                    }
                }

                s.Append("}");
                return s.ToString();
            } else
            {
                throw new Exception("Your class should have a default constructor");
            }
        }


        private static bool HasDefaultConstructor(Type typeToConvert)
        {
            return typeToConvert.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
