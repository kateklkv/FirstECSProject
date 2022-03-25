using Unity.Entities;

namespace Kulikova
{
    public class AIBehaveSystem : ComponentSystem
    {
        private EntityQuery _behaveQuery;

        protected override void OnCreate()
        {
            _behaveQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_behaveQuery).ForEach(
                (Entity entity, BehaviourManager manager) =>
                {
                    manager.ActiveBehaviour?.Behave();
                });
        }
    }
}