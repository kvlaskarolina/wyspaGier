using System;

namespace QuickFun.Games.Hangman.WordProviders
{
    public class MediumWordProvider : IWordProvider
    {
        public HangmanDifficulty Difficulty => HangmanDifficulty.Medium;

        private static readonly string[] Words = {
            "elephant",
            "guitar",
            "puzzle",
            "mountain",
            "penguin",
            "backpack",
            "notebook",
            "umbrella",
            "dolphin",
            "rainbow"
        };

        public string GetRandomWord()
        {
            Random rnd = new Random();
            return Words[rnd.Next(Words.Length)];
        }
    }
}
