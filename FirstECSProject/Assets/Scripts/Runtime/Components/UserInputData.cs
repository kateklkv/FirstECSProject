using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kulikova
{
    public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _spurt;

        [SerializeField]
        private string _walkAnimationHash;
        public string WalkAnimationHash { get => _walkAnimationHash; }

        [SerializeField]
        private string _runAnimationHash;
        public string RunAnimationHash { get => _runAnimationHash; }

        [SerializeField]
        private string _attackAnimationHash;
        public string AttackAnimationHash { get => _attackAnimationHash; }

        [SerializeField]
        private string _getHitAnimationHash;
        public string GetHitAnimationHash { get => _getHitAnimationHash; }

        [SerializeField]
        private string _dieAnimationHash;
        public string DieAnimationHash { get => _dieAnimationHash; }

        [SerializeField]
        private MonoBehaviour _shootAction;
        public MonoBehaviour ShootAction { get => _shootAction; }

        [SerializeField]
        private MonoBehaviour _effectAction;
        public MonoBehaviour EffectAction { get => _effectAction; }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new InputData());

            dstManager.AddComponentData(entity, new MoveData
            {
                Speed = _speed,
                Spurt = _spurt / 100
            });

            if (ShootAction != null && ShootAction is ShootAbility)
            {
                dstManager.AddComponentData(entity, new ShootData());
            }

            if (EffectAction != null && EffectAction is EffectAbility)
            {
                dstManager.AddComponentData(entity, new EffectData());
            }

            if (_walkAnimationHash != String.Empty || _runAnimationHash != String.Empty || _attackAnimationHash != String.Empty || _getHitAnimationHash != String.Empty || _dieAnimationHash != String.Empty)
            {
                dstManager.AddComponentData(entity, new AnimatorData());
            }
        }
    }

    public struct InputData : IComponentData
    {
        public float2 Move;
        public float Shoot;
        public float Spurt;
        public float Effect;
    }

    public struct MoveData : IComponentData
    {
        public float Speed;
        public float Spurt;
    }

    public struct ShootData : IComponentData
    {

    }

    public struct EffectData : IComponentData
    {

    }

    public struct AnimatorData : IComponentData
    {

    }
}
