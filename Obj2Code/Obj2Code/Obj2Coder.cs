using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Obj2Code
{
    public class Obj2Coder
    {
        public static string ToCode(object classToConvert)
        {
            Type typeToConvert = classToConvert.GetType();
            if (HasConstructor(typeToConvert))
            {
                StringBuilder s = new StringBuilder();

                s.Append("new ");
                s.Append(typeToConvert.Name.ToString());
                s.Append("()");
                s.Append(" ");
                s.Append("{ ");

                PropertyInfo[] properties = typeToConvert.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var property in properties)
                {
                    if (property.CanRead && property.CanWrite)
                    {
                        var mget = property.GetGetMethod(false);
                        var mset = property.GetSetMethod(false);
                        if (mset != null && mget != null)
                        {
                            s.Append(property.Name);
                            s.Append(" = ");
                            var value = mget.Invoke(classToConvert, null);
                            var methodType = value.GetType();
                            if (methodType.Equals(typeof(string)))
                            {
                                s.Append("\"");
                                s.Append(value);
                                s.Append("\"");
                            }
                            else if (methodType.Equals(typeof(DateTime)))
                            {
                                DateTime dateTime = (DateTime)value;
                                s.Append("DateTime.Parse(\"");
                                s.Append(dateTime.ToString("u"));
                                s.Append("\")");
                            }
                            else
                            {
                                s.Append(value);
                            }
                            s.Append(", ");
                        }
                    }
                }

                s.Append("}");
                s.Append(";");
                return s.ToString();
            } else
            {
                throw new Exception("Your class should have a default constructor");
            }
        }


        private static bool HasConstructor(Type typeToConvert)
        {
            return typeToConvert.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
