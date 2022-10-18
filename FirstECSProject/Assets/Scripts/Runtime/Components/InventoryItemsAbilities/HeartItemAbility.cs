using System.Collections.Generic;
using UnityEngine;

namespace Kulikova.InventoryItemsAbilities
{
    public class HeartItemAbility : MonoBehaviour, IAbilityTarget, ICraftable
    {
        public List<GameObject> Targets { get; set; } = new List<GameObject>();

        public string Name => _name;

        [SerializeField] private string _name;

        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<PlayerHealth>();

                if (character != null)
                    character.Health += 12;
            }
            //Destroy(gameObject);
        }
    }
}