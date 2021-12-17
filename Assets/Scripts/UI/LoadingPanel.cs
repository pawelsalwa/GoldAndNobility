using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	/// <summary>
	/// opens up on loading state, i think dependancies are messed up since it has dependancy
	/// on both loading game state and scen loader service, think about it.
	/// </summary>
	internal class LoadingPanel : UiPanelBase
	{

		public GameObject loadingEndHint;
		public Image loadingBar;
		
		private ISceneLoader sceneLoader;

		protected override void Start()
		{
			base.Start();
			
			sceneLoader = ServiceLocator.RequestService<ISceneLoader>();
			sceneLoader.OnSceneLoaded += OnSceneLoaded;
			sceneLoader.OnLoadingStarted += OnLoadingStarted;
			sceneLoader.OnProgressChanged += UpdateProgress;
		}

		protected override void OnDestroy()
		{
			sceneLoader.OnSceneLoaded -= OnSceneLoaded;
			sceneLoader.OnLoadingStarted -= OnLoadingStarted;
			sceneLoader.OnProgressChanged -= UpdateProgress;
		}

		private void Update()
		{
			if (!Active) return;
			if (loadingEndHint.activeSelf && Input.anyKeyDown) Close();
		}

		protected override void OnOpened() => loadingEndHint?.SetActive(false);

		private void UpdateProgress(float obj) => loadingBar.fillAmount = obj;
		private void OnLoadingStarted(string scene) => Open();

		private void OnSceneLoaded(string scene)
		{
			switch (scene)
			{
				case "Castle": loadingEndHint.SetActive(true); break;
				case "MainMenu": Close(); break;
			}
		}
	}
}
