using System.Collections.Generic;
using UnityEngine;

namespace Kulikova.InventoryItemsAbilities
{
    public class StarItemAbility : MonoBehaviour, IAbilityTarget
    {
        public List<GameObject> Targets { get; set; } = new List<GameObject>();

        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<PlayerHealth>();

                if (character != null)
                    character.Health += 5;
            }
            Destroy(gameObject);
        }
    }
}