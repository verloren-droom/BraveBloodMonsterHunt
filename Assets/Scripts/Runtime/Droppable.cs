using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    public class Droppable : MonoBehaviour
    {
        [SerializeField, ReorderableList] private DropItem[] dropItemsObjs;

        public void GenDrop()
        {
            var probabilities = dropItemsObjs.Select(d => d.probability).ToArray();
            float totalProbability = probabilities.Sum();

            float ranValue = UnityEngine.Random.Range(0.0f, totalProbability);
            
            float cumulativeProbability = 0f;
            for (int i = 0; i < probabilities.Length; i++)
            {
                cumulativeProbability += probabilities[i];
                if (ranValue <= cumulativeProbability && dropItemsObjs[i].DropItemType != DropType.None)
                {
                     Instantiate(dropItemsObjs[i].drop, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
