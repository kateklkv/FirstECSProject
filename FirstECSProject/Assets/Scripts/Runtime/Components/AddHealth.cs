using UnityEngine;
using System.Collections.Generic;
using Unity.Entities;

namespace Kulikova
{
    public class AddHealth : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity
    {
        [SerializeField]
        private int _health = 50;

        public List<GameObject> Targets { get; set; }

        private Entity _entity;
        private EntityManager _dstManager;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            _entity = entity;
            _dstManager = dstManager;
        }

        public void Execute()
        {
            foreach (var target in Targets)
            {
                var health = target.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.Health += _health;
                    _dstManager.DestroyEntity(_entity);
                    Destroy(gameObject);
                }
            }
        }
    }
}