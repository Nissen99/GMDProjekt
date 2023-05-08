using UnityEngine;

namespace Util
{
    public class DifficultyManager
    {

        public const string DIFFICULTY_KEY = "Difficulty";
        public const string NORMAL = "Normal";
        public const string NIGHTMARE = "Nightmare";
        public const string HELL = "Hell";
        private static DifficultyManager instance;
        private int _difficultyMultiplier;
        
        public static DifficultyManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DifficultyManager();
            }

            return instance;
        }

        public int GetDifficultyMultiplier()
        {
            switch (PlayerPrefs.GetString(DIFFICULTY_KEY))
            {
                case NORMAL:
                    _difficultyMultiplier = 1;
                    break;
                case NIGHTMARE:
                    _difficultyMultiplier = 4;
                    break;
                case HELL:
                    _difficultyMultiplier = 15;
                    break;
                default:
                    Debug.Log($"No difficulty specified, so will assume {NORMAL}");
                    _difficultyMultiplier = 1;
                    break;
            }
            return _difficultyMultiplier;
        }
    }
}