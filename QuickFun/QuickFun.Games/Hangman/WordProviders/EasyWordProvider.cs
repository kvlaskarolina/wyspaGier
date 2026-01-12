using System;

namespace QuickFun.Games.Hangman.WordProviders
{
    public class EasyWordProvider : IWordProvider
    {
        public HangmanDifficulty Difficulty => HangmanDifficulty.Easy;

        private static readonly string[] Words = {
            "book",
            "tree",
            "fish",
            "bird",
            "moon",
            "star",
            "bear",
            "lion",
            "duck",
            "frog"
        };

        public string GetRandomWord()
        {
            Random rnd = new Random();
            return Words[rnd.Next(Words.Length)];
        }
    }
}
