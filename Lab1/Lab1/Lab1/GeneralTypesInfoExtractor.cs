using System.Reflection;
using System.Text;

namespace Lab1
{
    internal class GeneralTypesInfoExtractor
    {

        public static void ShowTypesInfo()
        {
            var info = ExtractInfo();
            DisplayGeneralInfo(info);
            DisplaySpecificInfo();
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню...");
            InputUtils.GetCharBlocking();
        }

        private static void DisplayGeneralInfo(GeneralTypesInfo info)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Общая информация по типам:\n");
            sb.Append("Подключенные сборки: ").Append(info.AssembliesCount).Append('\n');
            sb.Append("Всего типов по всем подключенным сборкам: ").Append(info.TypesCount).Append('\n');
            sb.Append("Ссылочные типы (только классы): ").Append(info.RefTypesCount).Append('\n');
            sb.Append("Значимые типы: ").Append(info.SignatureTypesCount).Append('\n');
            Console.WriteLine(sb.ToString());
        }

        private static void DisplaySpecificInfo()
        {
            Console.WriteLine("Информация в соответствии с вариантом V = 9:");
            StringBuilder sb = new StringBuilder();
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> allTypes = new List<Type>();
            foreach (var assembly in allAssemblies)
            {
                allTypes.AddRange(assembly.GetTypes());
            }
            sb.Append("Тип с наибольшим числом открытых методов: ").Append(TypeUtils.GetTypeWithMaxPubMethods(allTypes).Name).Append("\n");
            sb.Append("Метод с наибольшим количеством символов '_' в названии: ").Append(TypeUtils.GetMethodWithMany_(allTypes).Name).Append("\n");
            sb.Append("Доля открытых полей по отношению к общему числу полей: ").Append(Math.Round(TypeUtils.GetPortionOfPublicFields(allTypes) * 100, 4)).Append("%\n");
            Console.WriteLine(sb.ToString());
        }

        private static GeneralTypesInfo ExtractInfo()
        {
            Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            int typesCount = 0;
            int refTypesCount = 0;
            int signatureTypesCount = 0;
            foreach (Assembly assembly in allAssemblies)
            {
                var types = assembly.GetTypes();
                typesCount += types.Length;
                foreach (var type in types)
                {
                    if (type.IsClass)
                    {
                        refTypesCount++;
                    } else if (type.IsValueType)
                    {
                        signatureTypesCount++;
                    }
                }
            }
            return new GeneralTypesInfo(allAssemblies.Length, typesCount, refTypesCount, signatureTypesCount);
        }

        public struct GeneralTypesInfo(int assembliesCount, int typesCount, int refTypesCount, int signatureTypesCount)
        {
            public int AssembliesCount { get; } = assembliesCount;
            public int TypesCount {  get; } = typesCount;
            public int RefTypesCount {  get; } = refTypesCount;
            public int SignatureTypesCount {  get; } = signatureTypesCount;
        }
    }
}
