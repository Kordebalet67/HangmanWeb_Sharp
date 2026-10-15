using System.Collections.Generic;

namespace HangmanWeb.Models
{
    public class HangmanState
    {
        public string Word { get; set; } = string.Empty;
        public List<char> Guessed { get; set; } = new List<char>();
        public List<char> Wrong { get; set; } = new List<char>();
    }
}