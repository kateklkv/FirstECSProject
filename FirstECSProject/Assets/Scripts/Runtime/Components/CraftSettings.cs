using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kulikova
{
    [CreateAssetMenu(fileName = "CraftSettings", menuName = "Create Craft Data")]
    public class CraftSettings : ScriptableObject
    {
        public List<CraftCombination> combinations;
    }

    [Serializable]
    public class CraftCombination
    {
        public List<string> sources;
        public GameObject result;
    }
}