using UnityEngine;

namespace Kulikova
{
    public class TrapAbility : CollisionAbility
    {
        [SerializeField]
        private int damage = 10;

        public new void Execute()
        {
            foreach (var target in Collisions)
            {
                var targetHealth = target?.gameObject?.GetComponent<PlayerHealth>();

                if (targetHealth != null)
                {
                    targetHealth.Health -= damage;
                }
            }
        }
    }
}
