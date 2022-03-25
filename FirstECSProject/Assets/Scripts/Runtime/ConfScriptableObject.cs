using UnityEngine;

namespace Kulikova
{
    public class ConfScriptableObject : IConfiguration
    {
        public int Health()
        {
            var scriptableObject = Resources.Load<PlayerData>("PlayerData");

            return scriptableObject.health;
        }
    }
}