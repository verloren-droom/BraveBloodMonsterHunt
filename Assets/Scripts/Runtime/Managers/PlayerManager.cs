using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class PlayerManager : BaseMonoManager<PlayerManager>
    {
        [SerializeField, Required] private PlayerController player;
        public Transform PlayerTransform => player.transform;
    }
}
