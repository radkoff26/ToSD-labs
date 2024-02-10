using System.Text;

namespace Lab1
{
    internal class TypeFormattingUtils
    {
        // Returns data like Dictionary<Previouse Name, Truncated Name>
        public static Dictionary<string, string> TruncateTypesNames(List<string> propertiesNames)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, List<string>> names = new Dictionary<string, List<string>>();
            foreach (var propertyName in propertiesNames)
            {
                string trimmedPropertyName = TrimMethodName(propertyName);
                if (!names.ContainsKey(trimmedPropertyName))
                {
                    names[trimmedPropertyName] = new List<string>();
                }
                names[trimmedPropertyName].Add(propertyName);
            }
            foreach (var group in names)
            {
                List<string> similar = group.Value;
                List<string> truncated;
                if (similar.Count > 1)
                {
                    truncated = TruncateSimilarTypeNames(similar);
                }
                else
                {
                    truncated = similar;
                }
                int i = 0;
                int size = similar.Count;
                if (size == 1)
                {
                    result.Add(similar[0], TrimMethodName(truncated[0]));
                }
                else
                {
                    while (i < size)
                    {
                        result.Add(similar[i], truncated[i]);
                        i++;
                    }
                }
            }
            return result;
        }

        public static string TrimMethodName(string methodName)
        {
            string trimmedMethodName;
            if (methodName.Contains("."))
            {
                int lastDotIndex = methodName.LastIndexOf('.');
                trimmedMethodName = methodName.Substring(lastDotIndex + 1);
            }
            else
            {
                trimmedMethodName = methodName;
            }
            return trimmedMethodName;
        }

        private static List<string> TruncateSimilarTypeNames(List<string> strings)
        {
            List<string[]> splitStrings = new List<string[]>();
            foreach (string s in strings)
            {
                splitStrings.Add(SplitTypesInMethodName(s));
            }
            int[] stringsState = new int[splitStrings.Count];
            Dictionary<string, List<int>> partsCount = new Dictionary<string, List<int>>();
            List<int> leftToTruncate = new List<int>();
            for (int i = 0; i < strings.Count; i++)
            {
                leftToTruncate.Add(i);
            }
            int index = 1;
            while (leftToTruncate.Count > 0)
            {
                partsCount.Clear();
                foreach (var i in leftToTruncate)
                {
                    string[] s = splitStrings[i];
                    int lastIndex = s.Length - 1 - index;
                    if (lastIndex >= 0)
                    {
                        string key = s[lastIndex];
                        List<int> current;
                        if (!partsCount.ContainsKey(key))
                        {
                            current = new List<int>();
                            partsCount.Add(key, current);
                        }
                        else
                        {
                            current = partsCount[key];
                        }
                        current.Add(i);
                        partsCount[key] = current;
                    }
                }
                leftToTruncate.Clear();
                foreach (var item in partsCount)
                {
                    if (item.Value.Count > 1)
                    {
                        foreach (var itemIndex in item.Value)
                        {
                            leftToTruncate.Add(itemIndex);
                        }
                    }
                    else
                    {
                        int i = item.Value[0];
                        stringsState[i] = index;
                    }
                }
                index++;
            }
            string[] truncated = new string[splitStrings.Count];
            for (int i = 0; i < splitStrings.Count; i++)
            {
                int stateIndex = stringsState[i];
                string[] split = splitStrings[i];
                List<string> parts = new List<string>();
                for (int j = stateIndex; j >= 0; j--)
                {
                    parts.Add(split[split.Length - j - 1]);
                }
                truncated[i] = string.Join(".", parts.ToArray());
            }
            return truncated.ToList();
        }

        private static string[] SplitTypesInMethodName(string method)
        {
            List<string> types = new List<string>();
            int i = 0;
            StringBuilder sb = new StringBuilder();
            int genericCount = 0;
            while (i < method.Length)
            {
                char current = method[i];
                bool add = true;
                switch (current)
                {
                    case '.':
                        if (genericCount > 0)
                        {
                            break;
                        }
                        add = false;
                        types.Add(sb.ToString());
                        sb.Clear();
                        break;
                    case '<':
                        genericCount++;
                        break;
                    case '>':
                        genericCount--;
                        break;
                }
                if (add)
                {
                    sb.Append(current);
                }
                i++;
            }
            types.Add(sb.ToString());
            return types.ToArray();
        }
    }
}
