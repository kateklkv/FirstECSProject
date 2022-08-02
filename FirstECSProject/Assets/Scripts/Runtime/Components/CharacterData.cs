using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kulikova
{
    public class CharacterData : MonoBehaviour
    {
        [SerializeField]
        private List<MonoBehaviour> levelUpActions;
        
        private int currentLevel = 1;
        public int CurrentLevel => currentLevel;
        
        private int score = 0;
        
        private int scoreToNextLevel = 100;

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;

            if (scoreToAdd >= scoreToNextLevel)
                LevelUp();
        }

        private void LevelUp()
        {
            currentLevel++;
            scoreToNextLevel *= 2;

            if (levelUpActions != null)
            {
                foreach (var action in levelUpActions)
                {
                    if (!(action is ILevelUp levelUp)) return;
                    levelUp.LevelUp(this, currentLevel);
                }
            }
            else
                throw new Exception("CharacterData.LevelUp(): LevelUpActions is null");
        }
    }
}