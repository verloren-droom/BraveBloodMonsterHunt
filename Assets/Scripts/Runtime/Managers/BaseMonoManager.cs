using UnityEngine;

namespace BraveBloodMonsterHunt
{
    public class BaseMonoManager<T> : MonoBehaviour where T : BaseMonoManager<T>
    {
        private static T m_Instance;
        public static T Instance => m_Instance;

        protected virtual void Awake()
        {
            if (m_Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                m_Instance = (T)this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (m_Instance == this)
            {
                m_Instance = null;
            }
        }
    }
}