using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Timers;
using UnityEngine;
using UnityEngine.Pool;

namespace BraveBloodMonsterHunt
{
    public enum DestroyMode
    {
        None = 0,
        Distance = 1,
        Time = 2,
    }
    public class MissileCreator : MonoBehaviour
    {
        [SerializeField, ValidateInput(nameof(CheckSpawnObjsCount)), ReorderableList] private GameObject[] spawnObjs;
        [SerializeField, Required] private Transform targetTransform;
        
        [SerializeField, Min(0), EnableIf(nameof(IsPlayingEnable))] private int maxCount = 5;
        [SerializeField, Min(0), EnableIf(nameof(IsPlayingEnable))] private int poolMaxSize = 10;
        [SerializeField, Min(0), EnableIf(nameof(IsPlayingEnable))] private float interval = 1.0f;

        [SerializeField] private DestroyMode destroyMode;
        [SerializeField, Min(0), BoxGroup(nameof(DestroyMode.Distance)), ShowIf(nameof(IsDestroyForDistance))] private float maxDistance = 10.0f;
        [SerializeField, Min(0), BoxGroup(nameof(DestroyMode.Time)), ShowIf(nameof(IsDestroyForTime))] private float holdTime = 5.0f;

        private ObjectPool<GameObject> m_MissilePool;
        private int m_ID;
        private int m_LoopID;

        public GameObject Missile { get; private set; }

        private bool IsPlayingEnable() => !Application.isPlaying;
        private bool IsDestroyForDistance() => destroyMode == DestroyMode.Distance;
        private bool IsDestroyForTime() => destroyMode == DestroyMode.Time;

        private bool CheckSpawnObjsCount()
        {
            return spawnObjs.Length > 0 && spawnObjs.Any(obj => obj != null);
        }
        
        /// <summary>
        /// Create new missile
        /// </summary>
        /// <returns>create gameobject</returns>
        private GameObject CreateMissile()
        {
            var obj = Instantiate(spawnObjs[UnityEngine.Random.Range(0, spawnObjs.Length - 1)], targetTransform.position, Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.name += "_" + (m_ID++);

            return obj;
        }
        
        /// <summary>
        /// Get missile
        /// </summary>
        /// <param name="obj"></param>
        private void GetMissile(GameObject obj)
        {
            obj.SetActive(true);
            obj.transform.position = targetTransform.position;
        }
        private void ReleaseMissile(GameObject obj)
        {
            obj.SetActive(false);
        }
        private void DestroyMissile(GameObject obj)
        {
            m_ID--;
            Destroy(obj);
        }

        private void Awake()
        {
            m_MissilePool = new ObjectPool<GameObject>(CreateMissile, GetMissile, ReleaseMissile, DestroyMissile, true, maxCount, poolMaxSize);
        }
        
        private void Start()
        {
            m_LoopID = TimersManager.SetLoopableTimer(this, interval, Get);
        }

        public void Get()
        {
            var missile = m_MissilePool.Get();
            if (destroyMode == DestroyMode.Time)
            {
                TimersManager.SetTimer(this, holdTime, (() => { m_MissilePool.Release(missile); }));
            } else if (destroyMode == DestroyMode.Distance)
            {
                // TODO: Distance
            }

            Missile = missile;
        }

        /// <summary>
        /// Launch missile
        /// </summary>
        /// <param name="play">true -> play, false -> pause</param>
        public void Launch(bool play = true)
        {
            TimersManager.SetPaused(m_LoopID, !play);
        }
    }
}