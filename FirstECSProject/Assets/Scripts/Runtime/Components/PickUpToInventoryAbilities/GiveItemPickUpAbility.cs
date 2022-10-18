using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Kulikova
{
    public class GiveItemPickUpAbility : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity, IInventoryItem
    {
        public List<GameObject> Targets { get; set; }

        [SerializeField] 
        private GameObject uiItem;

        public GameObject UIItem => uiItem;

        private Entity _entity;
        private EntityManager _dstManager;
        
        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<CharacterData>(); 
                
                if (character != null)
                {
                    var item = Instantiate(uiItem, character.InventoryUIRoot.transform);

                    var ability = item.GetComponent<IAbilityTarget>();
                    ability?.Targets.Add(target);
                    
                    Destroy(gameObject);
                    _dstManager.DestroyEntity(_entity);
                }
            }
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            _entity = entity;
            _dstManager = dstManager;
        }
    }
}