using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    internal class StartupScript : MonoBehaviour
    {

        public bool UseLightweightEnviro = false;
        
        private void Start()
        {
            SceneManager.LoadScene("PersistentObjects", LoadSceneMode.Additive);
            SceneManager.LoadScene(UseLightweightEnviro ? "LightweightEnviro" : "Castle", LoadSceneMode.Additive);
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}
