using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Kulikova
{
    public class GiveScorePickUpAbility : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity
    {
        public List<GameObject> Targets { get; set; }

        private Entity _entity;
        private EntityManager _dstManager;
        
        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<CharacterData>();
                if (character != null)
                {
                    character.AddScore(3);
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