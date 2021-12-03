using System;

namespace Common
{
	public interface ISceneLoader
	{
		event Action<string> OnLoadingStarted;
		event Action<float> OnProgressChanged;
		event Action<string> OnSceneLoaded;

		void LoadGameScene();
		void LoadMainMenu();
		void LoadPersistentObjects();
	}
}