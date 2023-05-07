using UnityEngine;
using Util;

namespace Menu
{
    public class DifficultyMenu : MonoBehaviour
    {

        public void NormalChosen()
        {
            PlayerPrefs.SetString(DifficultyManager.DIFFICULTY_KEY, DifficultyManager.NORMAL);
        }
    
        public void NightmareChosen()
        {
            PlayerPrefs.SetString(DifficultyManager.DIFFICULTY_KEY, DifficultyManager.NIGHTMARE);
        }
    
        public void HellChosen()
        {
            PlayerPrefs.SetString(DifficultyManager.DIFFICULTY_KEY, DifficultyManager.HELL);
        }
    }
}
