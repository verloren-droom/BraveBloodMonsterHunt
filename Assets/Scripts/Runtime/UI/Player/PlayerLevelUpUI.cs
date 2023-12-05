using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BraveBloodMonsterHunt
{
    public class PlayerLevelUpUI : MonoBehaviour
    {
        [SerializeField, Required] private Button btn1, btn2, btn3;

        public void AddBtn1ClickEvent(UnityAction action)
        {
            btn1.onClick.AddListener(action);
        }
        public void AddBtn2ClickEvent(UnityAction action)
        {
            btn2.onClick.AddListener(action);
        }
        public void AddBtn3ClickEvent(UnityAction action)
        {
            btn3.onClick.AddListener(action);
        }
    }
}