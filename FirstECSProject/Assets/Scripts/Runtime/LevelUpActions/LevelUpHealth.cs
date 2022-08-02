using System;
using UnityEngine;

namespace Kulikova.LevelUpActions
{
    public class LevelUpHealth : MonoBehaviour, ILevelUp
    {
        private int minLevel = 2;
        public int MinLevel => minLevel;

        private PlayerHealth playerHealth;

        private void Awake()
        {
            playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth == null)
                throw new Exception("LevelUpHealth: PlayerHealth is null");
        }

        public void LevelUp(CharacterData characterData, int level)
        {
            if (characterData.CurrentLevel >= minLevel)
                playerHealth.Health += 100;
        }
    }
}