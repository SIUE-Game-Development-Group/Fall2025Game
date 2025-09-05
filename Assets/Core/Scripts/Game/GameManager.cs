using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Core.Scripts.Utility;

namespace Core.Scripts.Game
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private List<string> teamScenesToLoad = new List<string>();

        protected override void Awake()
        {
            base.Awake();
            LoadTeamScenes();
        }

        private void LoadTeamScenes()
        {
            foreach (string sceneName in teamScenesToLoad)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}