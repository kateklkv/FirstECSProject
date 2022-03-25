using UnityEngine;
using System.Collections;

namespace Kulikova
{
    public class MoveToPlayerBehaviour : MonoBehaviour, IBehaviour
    {
        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private float _moveSpeed = 5f;

        void Start()
        {
            if (_playerHealth == null) 
                _playerHealth = FindObjectOfType<PlayerHealth>();
        }

        public void Behave()
        {
            if (_playerHealth != null) 
                transform.position = Vector3.MoveTowards(transform.position, _playerHealth.transform.position, _moveSpeed / 100f);
        }

        public float Evaluate()
        {
            if (_playerHealth == null) return 0;
            return 1 / (this.gameObject.transform.position - _playerHealth.transform.position).magnitude * 1f;
        }
    }
}