namespace Journey.Web.Infrastructure
{
    using System.Text;

    using Journey.Web.ViewModels.Games;

    public static class ModelExtensions
    {
        public static string GetDetails(this GameBaseViewModel game)
            => game.Title.RemoveSpecialCharacters().Replace(' ', '_');

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
