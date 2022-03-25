using UnityEngine;
using System.Collections.Generic;

namespace Kulikova
{
    public interface IAbilityTarget : IAbility
    {
        public List<GameObject> Targets { get; set; }
    }
}