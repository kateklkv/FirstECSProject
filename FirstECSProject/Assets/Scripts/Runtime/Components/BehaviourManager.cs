using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

namespace Kulikova
{
    public class BehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField]
        private List<MonoBehaviour> _behaviours;
        public List<MonoBehaviour> Behaviours { get => _behaviours; }

        public IBehaviour ActiveBehaviour { get; set; }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<AIAgent>(entity);
        }
    }

    public struct AIAgent : IComponentData
    {

    }
}