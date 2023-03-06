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
            _loadingScreen.SetActive(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
            while (!operation.isDone) yield return null;
            callback?.Invoke();
            
            yield return new WaitForSeconds(0.2f);
            _loadingScreen.SetActive(false);
        }
    }
}