using System.Collections.Generic;
using UnityEngine;

namespace Kulikova.InventoryItemsAbilities
{
    public class HealthItemAbility : MonoBehaviour, IAbilityTarget, ICraftable
    {
        public List<GameObject> Targets { get; set; } = new List<GameObject>();
        
        [SerializeField] private int addedHealthCount;

        [SerializeField] private string _name;
        public string Name => _name;

        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<PlayerHealth>();

                if (character != null)
                    character.Health += addedHealthCount;
            }
            //Destroy(gameObject);
        }
    }
}