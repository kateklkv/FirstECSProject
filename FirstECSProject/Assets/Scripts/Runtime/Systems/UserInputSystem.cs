using System.Threading.Tasks;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kulikova
{
    public class UserInputSystem : ComponentSystem
    {
        private EntityQuery _inputQuery;

        private InputActionAsset _inputActionAsset;
        private InputActionMap _inputActionMap;

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
            _inputActionAsset = (InputActionAsset)AssetDatabase
                .LoadAssetAtPath("Assets/Resources/PlayerInput.inputactions", typeof(InputActionAsset));
            _inputActionMap = _inputActionAsset.FindActionMap("Gameplay");
        }

        protected override void OnStartRunning()
        {
            _moveAction = _inputActionMap.FindAction("Movement");
            _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.Enable();

            _shootAction = _inputActionMap.FindAction("Shoot");
            _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.Enable();

            _spurtAction = _inputActionMap.FindAction("Spurt");
            _spurtAction.performed += context => { _spurtInput = context.ReadValue<float>(); };
            _spurtAction.started += context => { _spurtInput = context.ReadValue<float>(); };
            _spurtAction.canceled += context => { _spurtInput = context.ReadValue<float>(); };
            _spurtAction.Enable();

            _effectAction = _inputActionMap.FindAction("DissolveEffect");
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
