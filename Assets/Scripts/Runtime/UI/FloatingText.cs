using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI floatingText;

        private Vector3 m_CurrentScale;

        private void Start()
        {
            m_CurrentScale = floatingText.transform.localScale;
        }

        public void FlyTo(string text, Vector2 position, VertexGradient colorGradient, float duration = 0.25f, float interval = 0.25f)
        {
            floatingText.transform.position = position;
            floatingText.colorGradient = colorGradient;
            floatingText.text = text;
            Sequence seq = DOTween.Sequence();
            seq.Append(floatingText.transform.DOScale(m_CurrentScale, duration));
            seq.AppendInterval(interval);
            seq.Rewind();
        }
        
        private void OnDisable()
        {
            floatingText.transform.DOKill();
        }

        private void OnDestroy()
        {
            floatingText.transform.DOKill();
        }
    }
    
}
