using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace BraveBloodMonsterHunt
{
    [ExecuteAlways]
    public class ArmourController : MonoBehaviour
    {
        [SerializeField] private SpriteResolver handLeftEquipment;
        [SerializeField, Dropdown(nameof(GetHandLeftLabels)), OnValueChanged(nameof(SetHandLeftEquipment))] private string currentHandLeftLabel;
        [SerializeField] private SpriteResolver handRightEquipment;
        [SerializeField, Dropdown(nameof(GetHandRightLabels)), OnValueChanged(nameof(SetHandRightEquipment))] private string currentHandRightLabel;

        private string[] GetHandLeftLabels()
        {
            return handLeftEquipment.spriteLibrary.spriteLibraryAsset.GetCategoryLabelNames(handLeftEquipment.GetCategory()).ToArray();
        }

        public void SetHandLeftEquipment()
        {
            handLeftEquipment.SetCategoryAndLabel(handLeftEquipment.GetCategory(), currentHandLeftLabel);
        }

        private string[] GetHandRightLabels()
        {
            return handRightEquipment.spriteLibrary.spriteLibraryAsset.GetCategoryLabelNames(handRightEquipment.GetCategory()).ToArray();
        }
        
        public void SetHandRightEquipment()
        {
            handRightEquipment.SetCategoryAndLabel(handRightEquipment.GetCategory(), currentHandRightLabel);
        }
    }
}