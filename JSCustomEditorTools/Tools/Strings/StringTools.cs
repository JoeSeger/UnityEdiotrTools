using System;
using System.Linq;
using System.Text;

namespace CCZ.Code.Tools.Strings
{
    public static class StringTools
    {

        public enum StringConstructionType
        {
            Default,
            Path,
            UnderScore
            
        }
        
        public const string UnderScoreString = "_";
        public const char  UnderScoreChar = '_';
        public const string DashString = "/";
        public const char DashChar = '/';



        public static string  ConstructedString(StringConstructionType constructionType = StringConstructionType.Default,
            params string[] stringsToBeBuilt)
        {
            switch (constructionType)
            {
                case StringConstructionType.Default:
                    return  ConstructedString(stringsToBeBuilt);
                case StringConstructionType.Path:
                    return ConstructedStringAsPath(stringsToBeBuilt);
                case StringConstructionType.UnderScore:
                    return ConstructedStringByUnderScore(stringsToBeBuilt);
                default:
                   return ConstructedString(stringsToBeBuilt);
            }
            
        }


        private static string ConstructedStringAsPath(params string[] stringsToBeBuilt)
        {
            return ConstructedSeparatedString(DashString,stringsToBeBuilt);
        }
        private static string ConstructedStringByUnderScore(params string[] stringsToBeBuilt)
        {
            return ConstructedSeparatedString(UnderScoreString,stringsToBeBuilt);
        }
        private static string ConstructedSeparatedString(string separatingString , params string[] stringsToBeBuilt)
        { 
            var currentStringBuilder = new StringBuilder();

            foreach (var st in stringsToBeBuilt)
            {
                currentStringBuilder.Append(ConstructedString(st, separatingString));
              
            }
      
            return RemoveLastChar(currentStringBuilder.ToString());
        }
        public static string RemoveLastChar(string s) {
            return string.IsNullOrEmpty(s)
                ? null
                : (s.Substring(0, s.Length - 1));
        }
        private static string ConstructedString(params string[] stringsToBeBuilt)
        {
            var currentStringBuilder = new StringBuilder();
            foreach (var st in stringsToBeBuilt)
            {
                currentStringBuilder.Append(st);
            }
            return currentStringBuilder.ToString();
        }
           
        
        public static string RemoveString(string sourceString, string splitter)
        {
            if (!sourceString.Contains(splitter))
            {
                return sourceString;
            }

            var startIndex = sourceString.IndexOf(splitter, StringComparison.Ordinal);
            var result = sourceString.Remove(startIndex, splitter.Length);

            return result;
        }


        public static string TrimStartOfAllCharacters(string sourceString, char trimmed)
        {
            while (sourceString[0] == trimmed)
            {
                sourceString = sourceString.TrimStart(trimmed);
            }

            return sourceString;
        }


        public static string GetStringAfterChar(string sourceString, char delemenator)
        {
            while (sourceString[0] != delemenator)
            {
                sourceString = sourceString.TrimStart(sourceString[0]);
            }

            return sourceString;
        }


        public static string TrimStartCharacter(string sourceString, char trimmed)
        {
            sourceString = sourceString.TrimStart(trimmed);
            return sourceString;
        }


        public static string TrimStartString(string sourceString, string trimmed)
        {
            var trimmedChars = trimmed.ToCharArray();
            return trimmedChars.Aggregate(sourceString, (current, trimmedChar) => current.TrimStart(trimmedChar));
        }


        public static string GetBeforeNextChar(string sourceString, char trimmed)
        {
            var splits = sourceString.Split(trimmed);
            return splits[0];
        }

        public static string GetParentPathFromString(string sourceString)
        {
            var endOfParentPath = sourceString.Length - 1;
            for (; endOfParentPath >= 0; --endOfParentPath)
            {
                if (sourceString[endOfParentPath] == DashChar
                    || sourceString[endOfParentPath] == '\\')
                {
                    break;
                }
            }

            //string parentPath = "";
            var  parentPathBuffer = new char[endOfParentPath];
            sourceString.CopyTo(0, parentPathBuffer, 0, endOfParentPath);
            var parentPath = new string(parentPathBuffer);

            return parentPath;
        }

        public static string BuildReadableString(string source)
        {
            var readableString = string.Empty;
            var lastCharWasSpace = false;
            foreach (var c in source)
            {
                if (c >= 'A' && c <= 'Z' && (!lastCharWasSpace))
                {
                    readableString += " " + c;
                    lastCharWasSpace = true;
                    continue;
                }
                else if (c == DashChar)
                {
                    if (!lastCharWasSpace)
                    {
                        readableString += " ";
                    }

                    lastCharWasSpace = true;
                    continue;
                }
                else
                {
                    readableString += c;
                }

                lastCharWasSpace = false;
            }

            return readableString;
        }


        public static string[] PathToArray(string path)
        {
            return path.Split(DashChar);
        }


        public static bool AreValidStrings(params string[] checkingStrings)
        {
            return checkingStrings.All(IsValidString);
        }

        public static bool IsValidString(string checkingString)
        {
            return string.IsNullOrEmpty(checkingString);
        }
    }
}