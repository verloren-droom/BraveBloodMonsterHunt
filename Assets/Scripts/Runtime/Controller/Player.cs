using System.Collections.Generic;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    public abstract class Player : MonoBehaviour
    {
        private List<IObserver> m_Observers;

        public void AddObserver(IObserver observer)
        {
            m_Observers ??= new List<IObserver>();
            m_Observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            if (m_Observers != null && m_Observers.Contains(observer))
                m_Observers.Remove(observer);
        }

        public virtual void NotifyObservers()
        {
            foreach (var observer in m_Observers)
                observer.UpdateNotify();
        }
    }
}