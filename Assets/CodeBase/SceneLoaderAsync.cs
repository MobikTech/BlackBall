using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackBall
{
    public class SceneLoaderAsync : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen = null!;
        
        public void Load(string sceneName, Action? callback = null)
        {
            StartCoroutine(LoadSceneAsync(sceneName, callback));
        }

        private IEnumerator LoadSceneAsync(string sceneName, Action? callback)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            _loadingScreen.SetActive(true);
            operation.completed += _ =>
            {
                _loadingScreen.SetActive(false);
                callback?.Invoke();
            };
            yield break;
        }
    }
}