using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floatingText;
    [SerializeField] private Vector2 offset = new Vector2(1.0f, 5.0f);

    public void FlyTo(string text, Vector2 position, float duration = 0.25f, float interval = 0.25f)
    {
        floatingText.text = text;
        Sequence seq = DOTween.Sequence();
        seq.Append(floatingText.transform.DOMove(new Vector3(position.x + offset.x, position.y + offset.y, floatingText.transform.position.z),
            duration));
        seq.AppendInterval(interval);
        seq.Rewind();
    } 
}
