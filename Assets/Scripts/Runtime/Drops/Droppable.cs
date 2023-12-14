using System.Linq;
using BraveBloodMonsterHunt.Utility;
using NaughtyAttributes;
using Timers;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    [System.Serializable]
    internal class DropItem
    {
        public DropBase drop;
        public float probability;
    }
    
    public class Droppable : MonoBehaviour
    {
        [SerializeField, ReorderableList] private DropItem[] dropItemsObjs;
        [SerializeField, Min(0.0f)] private float duration = 0.25f;

        /// <summary>
        /// generation drop
        /// </summary>
        public void GenDrop()
        {
            var probabilities = dropItemsObjs.Select(d => d.probability).ToArray();
            var ind = RandomUtility.RandomArrayIndex(probabilities);

            if (ind != -1 && dropItemsObjs[ind] != null)
            {
                TimersManager.SetTimer(this, duration, () =>
                {
                    Instantiate(dropItemsObjs[ind].drop, transform.position, Quaternion.identity);
                });
            }
        }
    }
}
