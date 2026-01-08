namespace QuickFun.Games.Hangman
{
    public static class HangmanPictures
    {
        public static class Easy
        {
            public static string GetPicture(int wrongAttempts) => wrongAttempts switch
            {
                0 => @"
         
          
          
          
          
    =========",                
                
                1 => @"
      +---+
         
          
          
          
          
    =========",

                2 => @"
      +---+
          |
          |
          |
          |
      
    =========",

                3 => @"
      +---+
      |   |
          |
          |
          |
      
    =========",

                4 => @"
      +---+
      |   |
          |
          |
          |
      ____
    =========",

                5 => @"
      +---+
      |   |
      O   |
          |
          |
      ____
    =========",

                6 => @"
      +---+
      |   |
      O   |
      |   |
          |
      ____
    =========",

                7 => @"
      +---+
      |   |
      O   |
     /|   |
          |
      ____
    =========",

                8 => @"
      +---+
      |   |
      O   |
     /|\  |
          |
      ____
    =========",

                9 => @"
      +---+
      |   |
      O   |
     /|\  |
     /    |
      ____
    =========",

                10 => @"
      +---+
      |   |
      O   |
     /|\  |
     / \  |
      ____
    =========",
                
                _ => @"
      +---+
      |   |
      O   |
     /|\  |
     / \  |
      ____
    ========="
            };
        }

        public static class Medium
        {
            public static string GetPicture(int wrongAttempts) => wrongAttempts switch
            {
                0 => @"
      +---+
      |   |
          |
          |
          |
          |
    =========",

                1 => @"
      +---+
      |   |
          |
          |
          |
      ____
    =========",

                2 => @"
      +---+
      |   |
      O   |
          |
          |
      ____
    =========",

                3 => @"
      +---+
      |   |
      O   |
      |   |
          |
      ____
    =========",

                4 => @"
      +---+
      |   |
      O   |
     /|   |
          |
      ____
    =========",

                5 => @"
      +---+
      |   |
      O   |
     /|\  |
          |
      ____
    =========",

                6 => @"
      +---+
      |   |
      O   |
     /|\  |
     /    |
      ____
    =========",

                7 => @"
      +---+
      |   |
      O   |
     /|\  |
     / \  |
      ____
    =========",

                _ => @"
      +---+
      |   |
      O   |
     /|\  |
     / \  |
      ____
    ========="
            };
        }
        
        public static class Hard
        {
            public static string GetPicture(int wrongAttempts) => wrongAttempts switch
            {
                0 => @"
      +---+
      |   |
      O   |
          |
          |
    =========",

                1 => @"
      +---+
      |   |
      O   |
      |   |
          |
    =========",

                2 => @"
      +---+
      |   |
      O   |
     /|   |
          |
    =========",

                3 => @"
      +---+
      |   |
      O   |
     /|\  |
          |
    =========",

                4 => @"
      +---+
      |   |
      O   |
     /|\  |
     /    |
    =========",

                5 => @"
      +---+
      |   |
      O   |
     /|\  |
     / \  |
    =========",                

                _ => @"
      +---+
      |   |
      O   |
     /|\  |
     / \  |
    ========="
            };
        }
        
        public static string GetPicture(HangmanDifficulty difficulty, int wrongAttempts)
        {
            return difficulty switch
            {
                HangmanDifficulty.Easy => Easy.GetPicture(wrongAttempts),
                HangmanDifficulty.Medium => Medium.GetPicture(wrongAttempts),
                HangmanDifficulty.Hard => Hard.GetPicture(wrongAttempts),
                _ => Medium.GetPicture(wrongAttempts)
            };
        }
    }
}
