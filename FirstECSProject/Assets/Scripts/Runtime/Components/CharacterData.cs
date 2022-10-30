using System;
using System.Collections.Generic;
using Kulikova.UI;
using UnityEditor.Profiling;
using UnityEngine;
using Zenject;

namespace Kulikova
{
    public class CharacterData : MonoBehaviour
    {
        public GameObject inventoryUIRoot;
        
        [SerializeField]
        private List<MonoBehaviour> levelUpActions;
        
        [SerializeField]
        private int score = 0;
        
        private int _currentLevel = 1;
        public int CurrentLevel => _currentLevel;

        private int _scoreToNextLevel = 100;

        private CanvasViewModel _canvasViewModel;

        [Inject]
        private void Construct(CanvasViewModel canvasViewModel) => _canvasViewModel = canvasViewModel;

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            
            if (_canvasViewModel != null) _canvasViewModel.Score = string.Format("Score: {0}", score.ToString());

            if (scoreToAdd >= _scoreToNextLevel)
                LevelUp();
        }

        private void LevelUp()
        {
            _currentLevel++;
            _scoreToNextLevel *= 2;

            if (levelUpActions != null)
            {
                foreach (var action in levelUpActions)
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