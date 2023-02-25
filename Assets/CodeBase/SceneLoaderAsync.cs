using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackBall
{
    public class SceneLoaderAsync : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen = null!;
        
        public void Load(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            _loadingScreen.SetActive(true);
            operation.completed += _ => _loadingScreen.SetActive(false);
            yield break;
        }
    }
}