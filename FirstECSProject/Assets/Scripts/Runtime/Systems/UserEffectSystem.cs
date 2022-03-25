using Unity.Entities;
using UnityEngine;

namespace Kulikova
{
    public class UserEffectSystem : ComponentSystem
    {
        private EntityQuery _effectQuery;

        protected override void OnCreate()
        {
            _effectQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<EffectData>(),
                ComponentType.ReadOnly<UserInputData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_effectQuery).ForEach(
                (Entity entity, UserInputData inputData, ref InputData input, ref MoveData moveData) =>
                {
                    if (input.Effect > 0f && inputData.EffectAction != null && inputData.EffectAction is IAbility ability)
                    {
                        ability?.Execute();
                    }
                });
        }
    }
}
