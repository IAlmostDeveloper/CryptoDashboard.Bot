using System.Text.RegularExpressions;

namespace CryptoDashboard.Bot.Extensions
{
    public static class StringExtensions
    {
        public static string EscapeText(this string source)
        {
            if (source != null)
            {
                var regex = new Regex(@"<(\w*|\/\w*)>");
                source = regex.Replace(source, string.Empty);

                var charactersToEscape = new[] { "_", "*", "[", "]", "(", ")", "~", "`", "<", ">", "#", "+", "-", "=", "|", "{", "}", ".", "!" };
                foreach (var item in charactersToEscape)
                {
                    source = source.Replace(item, $@"\{item}");
                }
            }
            return source;
        }

        public static string EscapeUnknownHtml(this string text)
        {
            var supportedTags = new List<string> { "b", "i", "strong", "em", "u", "s", "pre", "code", "ins", "strike", "del", "a" };

            var escapedMessage = text.Replace("<", "&lt;").Replace(">", "&gt;");

            foreach (var tag in supportedTags)
            {
                var openingTag = $"<{tag} $3>";
                escapedMessage = (new Regex($"&lt;{tag}(((((?!&gt;).)*)&gt;)| &gt)")).Replace(escapedMessage, openingTag);
                escapedMessage = new Regex($"&lt;[ ]*[/]{tag}[ ]*&gt;").Replace(escapedMessage, $"</{tag}>");
            }

            return escapedMessage;
        }
    }
}
