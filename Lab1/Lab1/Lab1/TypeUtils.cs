using System.Reflection;

namespace Lab1
{
    internal class TypeUtils
    {
        // Тип с наибольшим числом открытых методов
        public static Type GetTypeWithMaxPubMethods(List<Type> types)
        {
            int maxOpenMethods = 0;
            Type? maxMethodsType = null;
            foreach (Type t in types)
            {
                var methods = TypeAnalysisUtils.GetPublicTypeMethods(t);
                if (methods.Count > maxOpenMethods)
                {
                    maxOpenMethods = methods.Count;
                    maxMethodsType = t;
                }
            }
            return maxMethodsType!;
        }

        // Метод с наибольшим количеством символов '_' в названии
        public static MethodInfo GetMethodWithMany_(List<Type> types)
        {
            int maxUnderlineCount = 0;
            MethodInfo? maxUnderlineCountMethod = null;
            foreach(Type t in types)
            {
                var methods = t.GetMethods(
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.NonPublic
                );
                foreach (MethodInfo m in methods)
                {
                    int underlineCount = 0;
                    var methodName = TypeFormattingUtils.TrimMethodName(m.Name);
                    foreach (var c in methodName)
                    {
                        if (c == '_')
                        {
                            underlineCount++;
                        }
                    }
                    if (underlineCount > maxUnderlineCount)
                    {
                        maxUnderlineCount = underlineCount;
                        maxUnderlineCountMethod = m;
                    }
                }
            }
            return maxUnderlineCountMethod!;
        }

        // Доля открытых полей по отношению к общему числу полей (рассматриваются только экземплярные поля)
        public static float GetPortionOfPublicFields(List<Type> types)
        {
            int publicCount = 0;
            int overallCount = 0;
            foreach (Type t in types)
            {
                var publicFields = t.GetFields(
                    BindingFlags.Instance |
                    BindingFlags.Public
                );
                var allFields = t.GetFields(
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.NonPublic
                );
                publicCount += publicFields.Length;
                overallCount += allFields.Length;
            }
            return ((float) publicCount) / overallCount;
        }
    }
}
