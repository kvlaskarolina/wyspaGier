using System;

namespace QuickFun.Games.Hangman.WordProviders
{
    public class HardWordProvider : IWordProvider
    {
        public HangmanDifficulty Difficulty => HangmanDifficulty.Hard;

        private static readonly string[] Words = {
            "chocolate",
            "butterfly",
            "basketball",
            "sunflower",
            "watermelon",
            "strawberry",
            "microphone",
            "telescope",
            "calculator",
            "refrigerator"
        };

        public string GetRandomWord()
        {
            Random rnd = new Random();
            return Words[rnd.Next(Words.Length)];
        }
    }
}
