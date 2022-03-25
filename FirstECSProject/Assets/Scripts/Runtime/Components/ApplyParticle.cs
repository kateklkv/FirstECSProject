using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Kulikova
{
    public class ApplyParticle : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity
    {
        [SerializeField]
        private LayerMask _layerExecute;
        [SerializeField]
        private GameObject _particleSystem;
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
                if (((1 << target.layer) & _layerExecute.value) != 0)
                {
                    StartCoroutine(Co_Particle());
                }
            }
        }

        IEnumerator Co_Particle()
        {
            _particleSystem.SetActive(true);
            yield return new WaitForSeconds(_particleSystem.GetComponent<ParticleSystem>().main.duration);
            _dstManager.DestroyEntity(_entity);
            Destroy(gameObject);
        }
    }
}
