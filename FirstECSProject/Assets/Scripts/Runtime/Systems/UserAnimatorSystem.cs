using Unity.Entities;
using UnityEngine;

namespace Kulikova
{
    public class UserAnimatorSystem : ComponentSystem
    {
        private EntityQuery _animatorQuery;

        protected override void OnCreate()
        {
            _animatorQuery = GetEntityQuery(ComponentType.ReadOnly<AnimatorData>(),
                ComponentType.ReadOnly<Animator>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_animatorQuery).ForEach(
                (Entity entity, ref InputData input, Animator animator, UserInputData inputData, PlayerHealth health) =>
                {
                    animator.SetBool(inputData.WalkAnimationHash, ((Mathf.Abs(input.Move.x) > 0.1f && (Mathf.Abs(input.Move.x) < 0.2f)) || (Mathf.Abs(input.Move.y) > 0.1f && Mathf.Abs(input.Move.y) < 0.2f)));

                    animator.SetBool(inputData.RunAnimationHash, (Mathf.Abs(input.Move.x) >= 0.2f || Mathf.Abs(input.Move.y) >= 0.2f));

                    if (input.Shoot > 0f && inputData.ShootAction != null && inputData.ShootAction is IAbility ability)
                    {
                        animator.SetBool(inputData.AttackAnimationHash, true);
                    }
                    else
                    {
                        animator.SetBool(inputData.AttackAnimationHash, false);
                    }

                    if (health.IsApplyDamage)
                    {
                        animator.SetBool(inputData.GetHitAnimationHash, true);
                    }
                    else
                    {
                        animator.SetBool(inputData.GetHitAnimationHash, false);
                    }

                    if (health.Health <= 0)
                        animator.SetBool(inputData.DieAnimationHash, true);
                });
        }
    }
}
