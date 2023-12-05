using System.Linq;
using NaughtyAttributes;
using Timers;
using UnityEngine;
using UnityEngine.Pool;

namespace BraveBloodMonsterHunt
{
    public class EnemyCreator : MonoBehaviour
    {
        public class MissileCreator : MonoBehaviour
        {
            [SerializeField, ValidateInput(nameof(CheckSpawnObjsCount)), ReorderableList]
            private GameObject[] spawnObjs;

            [SerializeField, Required] private Transform targetTransform;

            [SerializeField, Min(0)] private int maxCount = 5;
            [SerializeField, Min(0)] private int poolMaxSize = 10;
            [SerializeField, Min(0)] private float interval = 1.0f;

            private ObjectPool<GameObject> m_EnemyPool;
            private int m_ID;
            private int m_LoopID;

            public GameObject Missile { get; private set; }

            private bool CheckSpawnObjsCount()
            {
                return spawnObjs.Length > 0 && spawnObjs.Any(obj => obj != null);
            }

            /// <summary>
            /// Create new enemy
            /// </summary>
            /// <returns>create gameobject</returns>
            private GameObject CreateEnemy()
            {
                var obj = Instantiate(spawnObjs[UnityEngine.Random.Range(0, spawnObjs.Length - 1)],
                    targetTransform.position, Quaternion.identity);
                obj.transform.SetParent(transform);
                obj.name += "_" + m_ID++;

                return obj;
            }

            /// <summary>
            /// Get enemy
            /// </summary>
            /// <param name="obj"></param>
            private void GetEnemy(GameObject obj)
            {
                obj.SetActive(true);
                obj.transform.position = targetTransform.position;
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
                m_LoopID = TimersManager.SetLoopableTimer(this, interval, Get);
            }

            public void Get()
            {
                var missile = m_EnemyPool.Get();

                Missile = missile;
            }

            /// <summary>
            /// Generate enemies
            /// </summary>
            /// <param name="play">true -> play, false -> pause</param>
            public void Generate(bool play = true)
            {
                TimersManager.SetPaused(m_LoopID, !play);
            }
        }
    }
}