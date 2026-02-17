using System.Text.RegularExpressions;

namespace ParticipacionDigital.Core.Services
{
    public class ContentModerationService
    {
        private readonly List<string> _badWords = new List<string>
        {
            "maldito", "idiota", "estupido", "basura", "mierda", "imbecil", "tonto", "culazo", "pendejo", "coño", "diablo", "estúpido", "estupida", "puta", "puto", "mmg", "singar", "mamagueva", "mamaguevo", "maricon", "maricón", "loco", "loca", "aseroso", "asqueroso"
        };

        public bool ContainsBadWords(string text, out List<string> foundWords)
        {
            foundWords = new List<string>();
            if (string.IsNullOrWhiteSpace(text)) return false;

            var normalizedText = text.ToLower();
            bool found = false;

            foreach (var word in _badWords)
            {
                // Simple check for now to catch embedded insults which is common in Spanish slang (e.g. "comemierda")
                // but avoiding common false positives requires more complex regex. 
                // For "mierda", "comemierda" should be caught.
                // But "computadora" containing "puta" is a classic false positive.
                
                // Strategy: Use word boundaries for short words, and containment for specific compound insults if needed.
                // For this MVP, we will use word boundaries for most.
                
                string pattern = $@"\b{Regex.Escape(word)}\b";
                
                // Exception for specific compound words in DR slang if necessary, but start safe.
                if (Regex.IsMatch(normalizedText, pattern, RegexOptions.IgnoreCase))
                {
                    foundWords.Add(word);
                    found = true;
                }
            }

            return found;
        }
    }
}
