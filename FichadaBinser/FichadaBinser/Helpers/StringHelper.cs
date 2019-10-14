namespace FichadaBinser.Helpers
{
    public class StringHelper
    {
        public static string FirstUpper(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return text.Substring(0, 1).ToUpper() + text.Substring(1);
        }
    }
}
