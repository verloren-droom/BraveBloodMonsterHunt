using System;
using System.Linq;
using BraveBloodMonsterHunt.Utility;
using NaughtyAttributes;
using Timers;
using UnityEngine;
using UnityEngine.Pool;

namespace BraveBloodMonsterHunt
{
    public partial class EnemyCreator : MonoBehaviour
    {
        [SerializeField, ValidateInput(nameof(CheckSpawnObjsLength)), ReorderableList]
        private GameObject[] spawnObjs;

        [SerializeField, Required] private CastBox2DMono spawnBound;

        [SerializeField, Min(0)] private int maxCount = 50;
        [SerializeField, Min(0)] private int poolMaxSize = 500;
        
        [SerializeField, Min(0)] private float interval = 2.0f;
        private float m_IntervalTimer;

        [SerializeField] private bool infiniteCount;
        [SerializeField, Min(0), ShowIf(nameof(IsLimitedCount))] private int spawnCount = 10;
        private int m_CurrentSpawnCount;
        private bool m_IsStop;

        [SerializeField, ProgressBar("Elapsed",nameof(interval), EColor.Yellow)]
        private float elapsedTime;

        private ObjectPool<GameObject> m_EnemyPool;
        private int m_ID;
        
        // private int m_LoopID;

        public GameObject Enemy { get; private set; }
        
        private Vector2 GetSpawnPos()
        {
            return RandomUtility.InsideRectangle(spawnBound.Center, spawnBound.Size);
        }
        
        /// <summary>
        /// create new enemy
        /// </summary>
        /// <returns>create gameobject</returns>
        private GameObject CreateEnemy()
        {
            var obj = Instantiate(spawnObjs[UnityEngine.Random.Range(0, spawnObjs.Length - 1)],
                GetSpawnPos(), Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.name += "_" + m_ID++;

            if (obj.TryGetComponent<Portal>(out var portal))
            {
                portal.AddOnObjectGenerateEvent((() =>
                {
                    TimersManager.SetTimer(this, interval, (() => m_EnemyPool.Release(obj)));
                }));
            }
            
            return obj;
        }

        /// <summary>
        /// get enemy
        /// </summary>
        /// <param name="obj"></param>
        private void GetEnemy(GameObject obj)
        {
            obj.SetActive(true);
            obj.transform.position = GetSpawnPos();
        }

        private void ReleaseEnemy(GameObject obj)
        {
            obj.SetActive(false);
        }
        
        private void DestroyEnemy(GameObject obj)
        {
            m_ID--;
            Destroy(obj);
        }

        private void Awake()
        {
            m_EnemyPool = new ObjectPool<GameObject>(CreateEnemy, GetEnemy, ReleaseEnemy, DestroyEnemy,
                true, maxCount, poolMaxSize);
        }

        private void Start()
        {
            // m_LoopID = TimersManager.SetLoopableTimer(this, interval, Get);
        }

        private void Update()
        {
            // elapsedTime = TimersManager.ElapsedTime(m_LoopID) % interval;

            if (m_IsStop) return;
            
            m_IntervalTimer += Time.deltaTime;
            elapsedTime = m_IntervalTimer;
            if (m_IntervalTimer >= interval)
            {
                if (m_CurrentSpawnCount++ < spawnCount || infiniteCount)
                {
                    m_IntervalTimer %= interval;
                    Enemy = m_EnemyPool.Get();
                }
                else
                {
                    m_IsStop = true;
                }
            }
        }

        // public void Get()
        // {
        //
        //     TimersManager.SetTimer(this, interval, () => { m_EnemyPool.Release(Enemy); });
        //     m_EnemyPool.Release(Enemy);
        // }

        /// <summary>
        /// Generate enemies
        /// </summary>
        /// <param name="play">true -> play, false -> pause</param>
        // public void Generate(bool play = true)
        // { 
        //     TimersManager.SetPaused(m_LoopID, !play); 
        // }

        private void OnDrawGizmosSelected()
        {
            
        }
    }

    public partial class EnemyCreator
    {
#if UNITY_EDITOR
        private bool CheckSpawnObjsLength()
        {
            return spawnObjs.IsNotNullAndLengthGreaterZero();
        }

        private bool IsLimitedCount()
        {
            return !infiniteCount;
        }
#endif
    }
}