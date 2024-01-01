using System.Text;

namespace CryptoDashboard.Bot.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToArrayString<T>(this IEnumerable<T> sequence)
        {
            var sb = new StringBuilder('[');
            foreach (var item in sequence)
            {
                sb.Append($"\"{item}\",");
            }
            sb.Replace(",", "]", sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
