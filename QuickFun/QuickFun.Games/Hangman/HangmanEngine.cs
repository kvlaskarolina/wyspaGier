using QuickFun.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickFun.Games.Hangman
{
    public class HangmanEngine : IGameEngine
    {
        public GameType Type => GameType.Hangman;
        public string Name => "Hangman"; 
        public int Score { get; private set; } = 0;

        public string WordToGuess { get; private set; } = string.Empty;
        public char[] CurrentGuess { get; private set; } = Array.Empty<char>();
        public int WrongAttempts { get; private set; }
        public int MaxAttempts { get; private set; } = 7;
        public bool IsGameOver { get; private set; }
        public string Message { get; private set; } = string.Empty;

        private List<char> _guessedLetters = new List<char>();
        public IReadOnlyCollection<char> GuessedLetters => _guessedLetters;

        private static readonly string[] Words = { 
    "elephant", 
    "guitar", 
    "puzzle", 
    "mountain", 
    "chocolate", 
    "penguin", 
    "backpack", 
    "notebook", 
    "umbrella", 
    "dolphin" 
};

        public HangmanEngine()
        {
        }

        public void Reset()
        {
            Random rnd = new Random();
            WordToGuess = Words[rnd.Next(Words.Length)];
            CurrentGuess = new string('_', WordToGuess.Length).ToCharArray();
            WrongAttempts = 0;
            _guessedLetters.Clear();
            IsGameOver = false;
            Score = 0;
            Message = "Let's start! Guess the word:";

        }

        public void MakeMove(char letter)
        {
            if (IsGameOver || _guessedLetters.Contains(letter)) return;

            _guessedLetters.Add(letter);

            if (WordToGuess.Contains(letter))
            {
                for (int i = 0; i < WordToGuess.Length; i++)
                {
                    if (WordToGuess[i] == letter)
                        CurrentGuess[i] = letter;
                }

                if (!CurrentGuess.Contains('_'))
                {
                    IsGameOver = true;
                    Score = 1;
                    Message = "Bravo! You guessed the word! Victory!";

                }
                else
                {
                    Message = $"Nice! Letter found: {new string(CurrentGuess)}";

                }
            }
            else
            {
                WrongAttempts++;
                if (WrongAttempts >= MaxAttempts)
                {
                    IsGameOver = true;
                    Score = 0;
                    Message = $"You lost! The word was: {WordToGuess}";
                }
                else
                {
                    Message = $"Oops! You have {MaxAttempts - WrongAttempts} tries left!";

                }
            }
        }

        public string HangmanPicture => WrongAttempts switch
{
    0 => "      +---+\n      |   |\n          |\n          |\n          |\n          |\n    =========",
    1 => "      +---+\n      |   |\n          |\n          |\n          |\n      ____|\n    =========",
    2 => "      +---+\n      |   |\n      O   |\n          |\n          |\n      ____|\n    =========",
    3 => "      +---+\n      |   |\n      O   |\n      |   |\n          |\n      ____|\n    =========",
    4 => "      +---+\n      |   |\n      O   |\n     /|   |\n          |\n      ____|\n    =========",
    5 => "      +---+\n      |   |\n      O   |\n     /|\\  |\n          |\n      ____|\n    =========",
    6 => "      +---+\n      |   |\n      O   |\n     /|\\  |\n     /    |\n      ____|\n    =========",
    7 => "      +---+\n      |   |\n      O   |\n     /|\\  |\n     / \\  |\n      ____|\n    =========",
    _ => "      +---+\n      |   |\n      O   |\n     /|\\  |\n     / \\  |\n      ____|\n    ========="
};
    }
}