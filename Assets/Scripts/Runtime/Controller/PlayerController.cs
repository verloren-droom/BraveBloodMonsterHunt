using System;
using NaughtyAttributes;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class PlayerController : Player
    {
        public override void NotifyObservers()
        {
            base.NotifyObservers();
            
        }
    }
}