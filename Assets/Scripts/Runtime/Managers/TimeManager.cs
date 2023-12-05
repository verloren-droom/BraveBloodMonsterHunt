using System;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    public class TimeManager : MonoBehaviour
    {
        public void Stop()
        {
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            Time.timeScale = 1.0f;
        }
    }
}