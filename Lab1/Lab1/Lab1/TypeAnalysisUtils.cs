using System.Reflection;

namespace Lab1
{
    internal class TypeAnalysisUtils
    {

        public static List<MethodInfo> GetAllTypeMethods(Type type)
        {
            BindingFlags flags = BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.NonPublic;
            return GetAllTypeMethods(type, flags, true);
        }

        public static List<MethodInfo> GetPublicTypeMethods(Type type)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            return GetAllTypeMethods(type, flags, true);
        }

        public static List<FieldInfo> GetAllTypeFields(Type type)
        {
            BindingFlags flags = BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.NonPublic;
            return type.GetFields(flags).ToList();
        }

        public static List<PropertyInfo> GetAllTypeProperties(Type type)
        {
            BindingFlags flags = BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.NonPublic;
            return type.GetProperties(flags).ToList();
        }

        private static List<MethodInfo> GetAllTypeMethods(Type type, BindingFlags flags, bool isRootType)
        {
            List<MethodInfo> methods = type.GetMethods(flags).ToList();
            Type? baseType = type.BaseType;
            if (baseType != null) { 
                List<MethodInfo> inheritedMethods = GetAllTypeMethods(baseType, BindingFlags.Public, false);
                methods.AddRange(inheritedMethods);
            }
            return methods;
        }
    }
}
