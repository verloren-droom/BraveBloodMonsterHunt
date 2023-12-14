using System;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    using UnityEngine.Events;

    [Serializable]
    public class TargetFoundEvent : UnityEvent<GameObject> { }
}