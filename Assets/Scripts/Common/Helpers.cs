using UnityEngine.SceneManagement;

namespace Common
{
    public static class Helpers 
    {

        public static bool IsSceneLoaded(string name)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var sceneAt = SceneManager.GetSceneAt(i);
                if (sceneAt.isLoaded && sceneAt.name == name)
                    return true;
            }
            return false;
        }
    }
}
