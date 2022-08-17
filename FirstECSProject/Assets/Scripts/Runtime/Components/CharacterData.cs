using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kulikova
{
    public class CharacterData : MonoBehaviour
    {
        public GameObject InventoryUIRoot;
        
        [SerializeField]
        private List<MonoBehaviour> _levelUpActions;
        
        [SerializeField]
        private int _score = 0;
        
        private int _currentLevel = 1;
        public int CurrentLevel => _currentLevel;

        private int _scoreToNextLevel = 100;

        private List<IInventoryItem> _inventoryItems;

        public void AddScore(int scoreToAdd)
        {
            _score += scoreToAdd;

            if (scoreToAdd >= _scoreToNextLevel)
                LevelUp();
        }

        private void LevelUp()
        {
            _currentLevel++;
            _scoreToNextLevel *= 2;

            if (_levelUpActions != null)
            {
                foreach (var action in _levelUpActions)
                {
                    if (!(action is ILevelUp levelUp)) return;
                    levelUp.LevelUp(this, _currentLevel);
                }
            }
            else
                throw new Exception("CharacterData.LevelUp(): LevelUpActions is null");
        }
    }
}