using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kulikova
{
    public class WaitBehaviour : MonoBehaviour, IBehaviour
    {
        public void Behave()
        {
            Debug.Log("WAIT!");
        }

        public float Evaluate()
        {
            return 0.5f;
        }
    }
}
