using System;
using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;
using UnityEngine.Pool;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent, RequireComponent(typeof(Canvas))]
    public class FloatingTextManager : BaseMonoManager<FloatingTextManager>
    {
        [SerializeField] private GameObject floatingTextPrefab;

        [SerializeField, Min(0)] private int maxCount = 10;
        [SerializeField, Min(0)] private int maxPoolCount = 20;

        [SerializeField, Min(0)] private float duration = 0.05f;
        [SerializeField, Min(0)] private float interval = 0.05f;

        private ObjectPool<GameObject> m_FloatingTextPool;

        protected override void Awake()
        {
            base.Awake();

            m_FloatingTextPool = new ObjectPool<GameObject>(CreateText, GetText, ReleaseText, DestroyText, true,
                maxCount, maxPoolCount);
        }

        private GameObject CreateText()
        {
            var obj = Instantiate(floatingTextPrefab, transform, true);
            return obj;
        }
        
        private void GetText(GameObject obj)
        {
            obj.SetActive(true);
        }
        private void ReleaseText(GameObject obj)
        {
            obj.SetActive(false);
        }
        private void DestroyText(GameObject obj)
        {
            Destroy(obj);
        }

        public void GetFloatingText(string text, Vector2 targetPosition)
        {
            var textObj = m_FloatingTextPool.Get();
            textObj.transform.position = targetPosition;

            if (textObj.TryGetComponent<FloatingText>(out var ft))
            {
                ft.FlyTo(text, targetPosition, duration, interval);
            }

            TimersManager.SetTimer(this, duration * 2 + interval, () =>
            {
                m_FloatingTextPool.Release(textObj);
            });
        }
    }
}
