using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kulikova
{
    public class ShootAbility : MonoBehaviour, IAbility
    {
        [SerializeField]
        private GameObject _bullet;
        [SerializeField]
        private Transform _gun;
        [SerializeField]
        private float _shootDelay;
        [SerializeField]
        private float _shotForce;

        private float _shootTime = float.MinValue;

        public void Execute()
        {
            if (Time.time < _shootTime + _shootDelay) return;

            _shootTime = Time.time;

            if (_bullet != null)
            {
                GameObject newBullet = Instantiate(_bullet, _gun.transform.position, Quaternion.Euler(90, 0, 0));
                StartCoroutine(Co_Shot(newBullet));
            }
            else
            {
                Debug.LogError("[SHOOT ABILITY] No bullet prefab link!");
            }
        }

        IEnumerator Co_Shot(GameObject bullet)
        {
            bullet.GetComponent<Rigidbody>().AddForce(_gun.forward * _shotForce, ForceMode.Impulse);
            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
