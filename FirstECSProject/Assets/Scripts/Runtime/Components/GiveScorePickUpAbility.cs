using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Kulikova
{
    public class GiveScorePickUpAbility : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity
    {
        public List<GameObject> Targets { get; set; }

        private Entity entity;
        private EntityManager dstManager;
        
        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<CharacterData>();
                if (character != null)
                {
                    character.AddScore(3);
                    Destroy(gameObject);
                    dstManager.DestroyEntity(entity);
                }
            }
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            this.entity = entity;
            this.dstManager = dstManager;
        }
    }
}