using UnityEngine;

namespace BlackBall
{
    public class DontDestroyOnLoadSystems : MonoBehaviour
    {
        private static bool s_exists;
       
        private void Awake()
        {
            if (!s_exists)
            {
                DontDestroyOnLoad(gameObject);
                s_exists = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}