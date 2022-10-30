using System.Collections.Generic;
using UnityEngine;

namespace Kulikova.InventoryItemsAbilities
{
    public class ScoreItemAbility : MonoBehaviour, IAbilityTarget
    {
        public List<GameObject> Targets { get; set; } = new List<GameObject>();

        [SerializeField] private int addedScoreCount;

        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<CharacterData>();

                if (character != null)
                    character.AddScore(addedScoreCount);
            }
            Destroy(gameObject);
        }
    }
}