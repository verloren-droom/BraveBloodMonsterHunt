using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BraveBloodMonsterHunt
{
    public class GameOver : MonoBehaviour
    {
        [Scene, SerializeField] private string restartScene;

        /// <summary>
        /// 
        /// </summary>
        public void Restart()
        {
            SceneManager.LoadScene(restartScene);
        }
    }
}