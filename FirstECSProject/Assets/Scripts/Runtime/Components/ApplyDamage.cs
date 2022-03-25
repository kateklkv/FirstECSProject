using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kulikova
{
    public class ApplyDamage : MonoBehaviour, IAbilityTarget
    {
        [SerializeField]
        private int _damage = 10;
        [SerializeField]
        private float _damageDelay = 1f;

        public List<GameObject> Targets { get; set; }

        private float _damageTime = float.MinValue;

        public void Execute()
        {
            if (Time.time < _damageTime + _damageDelay) return;

            _damageTime = Time.time;

            foreach (var target in Targets)
            {
                var health = target.GetComponent<PlayerHealth>();
                if (health != null && !health.IsApplyDamage)
                {
                    StartCoroutine(Co_ApplyDamage(health));
                }
            }
        }

        private IEnumerator Co_ApplyDamage(PlayerHealth health)
        {
            health.IsApplyDamage = true;
            health.Health -= _damage;
            yield return new WaitForSeconds(0.5f);
            health.IsApplyDamage = false;
        }
    }
}