using Lab1.core;
using System.Reflection;
using System.Text;

namespace Lab1.scenarios.impl.type
{
    internal class TypeSelectionScenario : Scenario
    {
        public ScenarioData ProduceData()
        {
            return new ScenarioData(
                options: [
                    CreateTypeOption('1', "uint", typeof(uint)),
                    CreateTypeOption('2', "int", typeof(int)),
                    CreateTypeOption('3', "long", typeof(long)),
                    CreateTypeOption('4', "float", typeof(float)),
                    CreateTypeOption('5', "double", typeof(double)),
                    CreateTypeOption('6', "char", typeof(char)),
                    CreateTypeOption('7', "string", typeof(string)),
                    CreateTypeOption('8', "record", typeof(DummyRecord)),
                    CreateTypeOption('9', "Tuple<int, string>", typeof(Tuple<int, string>)),
                ],
                exitKey: new ScenarioData.OptionKey('0', "Назад"),
                "Информация по типам\nВыберите тип:"
            );
        }

        private record struct DummyRecord
        {
            // Demonstrational overloading methods
            public void f() { }
            public void f(int a) { }
        }

        private ScenarioData.Option CreateTypeOption(char key, string desc, Type type)
        {
            return new ScenarioData.Option(new ScenarioData.OptionKey(key, desc), new ShowTypeInfoOptionExecutor(type));
        }

        private class ShowTypeInfoOptionExecutor(Type type) : OptionExecutor
        {
            private const string TITLE = "Название";
            private const string OVERLOADS = "Число перегрузок";
            private const string PARAMS = "Число параметров";
            private Type type = type;

            public void Execute()
            {
                Console.WriteLine(GetTypeInfoString());
                Console.WriteLine("Нажмите ‘M’ для вывода дополнительной информации по методам.");
                Console.WriteLine("Нажмите ‘0’ для выхода в главное меню");
                ProcessNextCommandChar();
            }

            private void ProcessNextCommandChar()
            {
                char command = InputUtils.GetCharBlocking();
                while (command != '0' && command != 'm')
                {
                    Console.WriteLine("Некорректная команда!");
                    command = InputUtils.GetCharBlocking();
                }
                if (command == 'm')
                {
                    ShowTypeMethodsInfo();
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                    InputUtils.GetCharBlocking();
                }
            }

            private void ShowTypeMethodsInfo()
            {
                Console.WriteLine("Методы типа: " + GetValidTypeName());
                Dictionary<string, MethodCharacteristics> info = GetTypeMethodsCharacteristics();
                int maxStringLength = TITLE.Length; // Defaults to length of the word "Название"
                foreach (var item in info)
                {
                    maxStringLength = Math.Max(maxStringLength, item.Key.Length);
                }
                ShowHeaderFormatted(maxStringLength);
                foreach (var item in info)
                {
                    ShowCharacteristicsFormatted(item.Key, item.Value, maxStringLength);
                }
            }

            private void ShowHeaderFormatted(int maxStringLength)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(TITLE).Append(' ', maxStringLength - TITLE.Length).Append('\t');
                sb.Append(OVERLOADS).Append('\t');
                sb.Append(PARAMS);
                Console.WriteLine(sb.ToString());
            }

            private void ShowCharacteristicsFormatted(string methodName, MethodCharacteristics characteristics, int maxStringLength) {
                StringBuilder sb = new StringBuilder();
                string overloadsString = characteristics.OverloadsCount.ToString();
                string minParamsString = characteristics.MinParams.ToString();
                string maxParamsString = characteristics.MaxParams.ToString();
                string paramsString = minParamsString + ".." + maxParamsString;

                sb.Append(methodName).Append(' ', maxStringLength - methodName.Length).Append('\t');
                sb.Append(overloadsString).Append(' ', OVERLOADS.Length - overloadsString.Length).Append('\t');
                sb.Append(paramsString).Append(' ', PARAMS.Length - paramsString.Length);
                Console.WriteLine(sb.ToString());
            }

            private Dictionary<string, MethodCharacteristics> GetTypeMethodsCharacteristics()
            {
                Dictionary<string, MethodCharacteristics> dict = new Dictionary<string, MethodCharacteristics>();

                List<MethodInfo> allMethods = TypeAnalysisUtils.GetAllTypeMethods(type);

                allMethods.ForEach(m =>
                {
                    string methodName = m.Name;
                    int paramsCount = m.GetParameters().Length;
                    if (!dict.ContainsKey(methodName))
                    {
                        dict.Add(
                            methodName,
                            new MethodCharacteristics(
                                overloadsCount: 1,
                                minParams: paramsCount,
                                maxParams: paramsCount
                            )
                        );
                    } else
                    {
                        MethodCharacteristics current = dict[methodName];
                        current.OverloadsCount++;
                        if (paramsCount < current.MinParams)
                        {
                            current.MinParams = paramsCount;
                        } else if (paramsCount > current.MaxParams)
                        {
                            current.MaxParams = paramsCount;
                        }
                        dict[methodName] = current;
                    }
                });

                return TruncateMethodNamesInDict(dict);
            }

            private List<string> FormatStringTypesNames(List<string> typesNames)
            {
                return TypeFormattingUtils.TruncateTypesNames(typesNames).Values.ToList();
            }

            private Dictionary<string, MethodCharacteristics> TruncateMethodNamesInDict(Dictionary<string, MethodCharacteristics> dict)
            {
                Dictionary<string, MethodCharacteristics> result = new Dictionary<string, MethodCharacteristics>();
                // Dictionary<Previous Key, Truncated Key>
                Dictionary<string, string> truncatedNames = TypeFormattingUtils.TruncateTypesNames(dict.Keys.ToList());
                foreach (var replacement in truncatedNames)
                {
                    MethodCharacteristics characteristics = dict[replacement.Key];
                    result.Add(replacement.Value, characteristics);
                }
                return result;
            }

            private string GetTypeInfoString()
            {
                StringBuilder sb = new StringBuilder();
                int methodsCount = TypeAnalysisUtils.GetAllTypeMethods(type).Count;
                FullInfo propertiesInfo = GetPropertiesInfo();
                FullInfo fieldsInfo = GetFieldsInfo();
                int generalCount = methodsCount + propertiesInfo.Count + fieldsInfo.Count;
                sb.Append("Информация по типу: ").Append(GetValidTypeName()).Append("\n");
                sb.Append("\t").Append("Значимый тип: ").Append(type.IsValueType ? "+" : "-").Append("\n");
                sb.Append("\t").Append("Пространство имён: ").Append(type.Namespace).Append("\n");
                sb.Append("\t").Append("Сборка: ").Append(type.Assembly.GetName().Name).Append("\n");
                sb.Append("\t").Append("Общее число элементов: ").Append(generalCount).Append("\n");
                sb.Append("\t").Append("Число методов: ").Append(methodsCount).Append("\n");
                sb.Append("\t").Append("Число свойств: ").Append(propertiesInfo.Count).Append("\n");
                sb.Append("\t").Append("Число полей: ").Append(fieldsInfo.Count).Append("\n");
                sb.Append("\t").Append("Список полей: ").Append(fieldsInfo.Representation).Append("\n");
                sb.Append("\t").Append("Список свойств: ").Append(propertiesInfo.Representation);
                return sb.ToString();
            }

            private FullInfo GetPropertiesInfo()
            {
                List<PropertyInfo> allProperties = TypeAnalysisUtils.GetAllTypeProperties(type);
                List<string> allPropertiesNames = new List<string>();
                foreach (var field in allProperties)
                {
                    allPropertiesNames.Add(field.Name);
                }
                allPropertiesNames = FormatStringTypesNames(allPropertiesNames);
                string representation = allPropertiesNames.Count == 0 ? "-" : string.Join(", ", allPropertiesNames);
                return new FullInfo(allPropertiesNames.Count, representation);
            }

            private FullInfo GetFieldsInfo()
            {
                List<FieldInfo> allFields = TypeAnalysisUtils.GetAllTypeFields(type);
                List<string> allFieldsNames = new List<string>();
                foreach (var field in allFields)
                {
                    allFieldsNames.Add(field.Name);
                }
                allFieldsNames = FormatStringTypesNames(allFieldsNames);
                string representation = allFieldsNames.Count == 0 ? "-" : string.Join(", ", allFieldsNames);
                return new FullInfo(allFields.Count, representation);
            }

            private string GetValidTypeName()
            {
                if (type == typeof(DummyRecord))
                {
                    return "record";
                } else if (type == typeof(Tuple<int, string>)) {
                    return "Tuple<int, string>";
                } else
                {
                    return type.Name;
                }
            }

            private struct FullInfo(int count, string representation)
            {
                public int Count { get; } = count;
                public string Representation { get; } = representation;
            }

            private struct MethodCharacteristics(int overloadsCount, int minParams, int maxParams)
            {
                public int OverloadsCount { get; set; } = overloadsCount;
                public int MinParams { get; set; } = minParams;
                public int MaxParams { get; set; } = maxParams;
            }
        }
    }
}
