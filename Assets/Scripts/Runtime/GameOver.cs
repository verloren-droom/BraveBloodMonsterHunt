using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BraveBloodMonsterHunt
{
    public class GameOver : MonoBehaviour
    {
        [Scene, SerializeField] private string restartScene;
        [SerializeField] private Button restartButton;

        /// <summary>
        /// restart scene
        /// </summary>
        public void Restart()
        {
            SceneManager.LoadScene(restartScene);
        }

        /// <summary>
        /// add restart button
        /// </summary>
        /// <param name="action"></param>
        public void AddRestartEvent(UnityAction action)
        {
            restartButton.onClick.AddListener(action);
        }
        
        /// <summary>
        /// remove restart button
        /// </summary>
        /// <param name="action"></param>
        public void RemoveRestartEvent(UnityAction action)
        {
            restartButton.onClick.RemoveListener(action);
        }
    }
}