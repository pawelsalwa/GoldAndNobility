using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    internal class StartupScript : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("PersistentObjects", LoadSceneMode.Additive);
            // SceneManager.LoadScene("Demo_01", LoadSceneMode.Additive);
            SceneManager.LoadScene("Castle", LoadSceneMode.Additive);
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}
