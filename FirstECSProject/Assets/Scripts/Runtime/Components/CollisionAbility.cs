using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Kulikova
{
    public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
    {
        [HideInInspector] public List<Collider> Collisions;

        [SerializeField]
        private Collider _collider;

        [SerializeField]
        private List<MonoBehaviour> _collisionActions = new List<MonoBehaviour>();

        private List<IAbilityTarget> _collisionActionsAbilitiesTarget = new List<IAbilityTarget>();

        private void Start()
        {
            if (_collider == null) 
                _collider = GetComponent<Collider>();

            foreach (var action in _collisionActions)
            {
                if (action is IAbilityTarget abilityTarget)
                {
                    _collisionActionsAbilitiesTarget.Add(abilityTarget);
                }
                else
                {
                    Debug.LogError("Collision action must drive from IAbility!");
                }
            }
        }

        public void Execute()
        {
            foreach (var actionAbilityTarget in _collisionActionsAbilitiesTarget)
            {
                actionAbilityTarget.Targets = new List<GameObject>();
                Collisions.ForEach(c =>
                {
                    if (c != null) actionAbilityTarget.Targets.Add(c.gameObject);
                });
                actionAbilityTarget.Execute();
            }
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            float3 position = gameObject.transform.position;
            switch (_collider)
            {
                case SphereCollider sphere:
                    sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Sphere,
                        SphereCenter = sphereCenter - position,
                        SphereRadius = sphereRadius,
                        initialTakeOff = true
                    });
                    break;
                case CapsuleCollider capsule:
                    capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Capsule,
                        CapsuleStart = capsuleStart - position,
                        CapsuleEnd = capsuleEnd - position,
                        CapsuleRadius = capsuleRadius,
                        initialTakeOff = true
                    });
                    break;
                case BoxCollider box:
                    box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientaton);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Box,
                        BoxCenter = boxCenter - position,
                        BoxHalfExtents = boxHalfExtents,
                        BoxOrientation = boxOrientaton,
                        initialTakeOff = true
                    });
                    break;
            }

            //Collider.enabled = false;
        }

        public struct ActorColliderData : IComponentData
        {
            public ColliderType ColliderType;
            public float3 SphereCenter;
            public float SphereRadius;
            public float3 CapsuleStart;
            public float3 CapsuleEnd;
            public float CapsuleRadius;
            public float3 BoxCenter;
            public float3 BoxHalfExtents;
            public quaternion BoxOrientation;
            public bool initialTakeOff;
        }

        public enum ColliderType
        {
            Sphere = 0,
            Capsule = 1,
            Box = 2
        }
    }
}