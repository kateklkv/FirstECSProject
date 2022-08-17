using Unity.Entities;
using UnityEngine;

namespace Kulikova
{
    public class UserMoveSystem : ComponentSystem
    {
        private EntityQuery _moveQuery;

        protected override void OnCreate()
        {
            _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<MoveData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_moveQuery).ForEach(
                (Entity entity, Transform transform, ref InputData inputData, ref MoveData moveData) =>
                {
                    var position = transform.position;
                    position += new Vector3(inputData.Move.x * moveData.Speed * Time.DeltaTime, 0,
                        inputData.Move.y * moveData.Speed * Time.DeltaTime);
                    transform.position = position;

                // OLD ROTATION
                /*if (inputData.Move.x != 0f || inputData.Move.y != 0f) 
                {
                    var moveVector = Vector3.zero;
                    moveVector.x = inputData.Move.x * moveData.Speed * Time.DeltaTime;
                    moveVector.z = inputData.Move.y * moveData.Speed * Time.DeltaTime;

                    Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, moveData.Speed * Time.DeltaTime, 0.0f);
                    transform.rotation = Quaternion.LookRotation(direct);
                }*/

                    var direction = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                    if (direction == Vector3.zero) return;

                    var rotation = transform.rotation;
                    var newRotation = Quaternion.LookRotation(Vector3.Normalize(direction));
                    if (newRotation == rotation) return;
                    transform.rotation = Quaternion.Lerp(rotation, newRotation, Time.DeltaTime * 10f);

                    if (inputData.Spurt > 0f)
                    {
                        Vector3 direct = Vector3.zero;
                        direct = Vector3.RotateTowards(transform.forward, 
                            transform.forward * moveData.Spurt, 0.0f, 0.0f);
                        transform.position += direct;
                    }
                });
        }
    }
}
