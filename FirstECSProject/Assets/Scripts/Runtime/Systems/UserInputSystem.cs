using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kulikova
{
    public class UserInputSystem : ComponentSystem
    {
        private EntityQuery _inputQuery;

        private InputAction _moveAction;
        private InputAction _shootAction;
        private InputAction _spurtAction;
        private InputAction _effectAction;

        private float2 _moveInput;
        private float _shootInput;
        private float _spurtInput;
        private float _effectInput;

        protected override void OnCreate()
        {
            _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
        }

        protected override void OnStartRunning()
        {
            _moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
            _moveAction.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.Enable();

            _shootAction = new InputAction("shoot", binding: "<Keyboard>/space");
            _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.Enable();

            _shootAction = new InputAction("spurt", binding: "<Keyboard>/e");
            _shootAction.performed += context => { _spurtInput = context.ReadValue<float>(); };
            _shootAction.started += context => { _spurtInput = context.ReadValue<float>(); };
            _shootAction.canceled += context => { _spurtInput = context.ReadValue<float>(); };
            _shootAction.Enable();

            _effectAction = new InputAction("effect", binding: "<Keyboard>/t");
            _effectAction.performed += context => { _effectInput = context.ReadValue<float>(); };
            _effectAction.started += context => { _effectInput = context.ReadValue<float>(); };
            _effectAction.canceled += context => { _effectInput = context.ReadValue<float>(); };
            _effectAction.Enable();
        }

        protected override void OnStopRunning()
        {
            _moveAction?.Disable();
            _shootAction?.Disable();
            _spurtAction?.Disable();
            _effectAction?.Disable();
        }

        protected override void OnUpdate()
        {
            Entities.With(_inputQuery).ForEach(
                (Entity entity, ref InputData inputData) =>
                {
                    inputData.Move = _moveInput;
                    inputData.Shoot = _shootInput;
                    inputData.Spurt = _spurtInput;
                    inputData.Effect = _effectInput;
                });
        }
    }
}
