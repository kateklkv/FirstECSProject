using UnityEngine;
using System.Collections;

namespace Kulikova
{
    public class EffectAbility : MonoBehaviour, IAbility
    {
        [SerializeField]
        private SkinnedMeshRenderer _materialGO;
        [SerializeField]
        private Material _changeMaterialTo;
        [SerializeField]
        private float _effectDuration = 5f;

        private Material _currentMaterial;

        void Awake()
        {
            if (_materialGO == null)
            {
                _materialGO = GetComponentInChildren<SkinnedMeshRenderer>();
            }

            _currentMaterial = _materialGO.material;
        }

        public void Execute()
        {
            StartCoroutine(Co_ChangeMaterial());
        }

        IEnumerator Co_ChangeMaterial()
        {
            _materialGO.material = _changeMaterialTo;
            yield return new WaitForSeconds(_effectDuration);
            _materialGO.material = _currentMaterial;
        }
    }
}