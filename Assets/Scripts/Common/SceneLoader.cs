using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
	/// <summary> Api for loading scenes and monitoring current progress. </summary>
	[PersistentComponent]
	internal class SceneLoader : MonoBehaviour, ISceneLoader
	{
		
		private readonly List<AsyncOperation> operations = new List<AsyncOperation>();

		public event Action<string> OnSceneLoaded;
		public event Action<string> OnLoadingStarted;
		public event Action<float> OnProgressChanged;
		
		private SceneLoader() => ServiceLocator.RegisterService<ISceneLoader>(this);

		private void Awake() => LoadPersistentScene(); // could be called wherever..

		public void LoadGameScene()
		{
			GameState.Current = GameStateType.Loading;
			var scene = "Castle";
			OnLoadingStarted?.Invoke("Castle");
			
			var unloadOp = SceneManager.UnloadSceneAsync("MainMenu");
			var loadOp = SceneManager.LoadSceneAsync("Castle", LoadSceneMode.Additive);
			
			operations.Clear();

			if (unloadOp != null) operations.Add(unloadOp);
			if (loadOp != null) operations.Add(loadOp);

			StartCoroutine(CheckLoading(scene));
		}

		public void LoadMainMenu()
		{
			GameState.Current = GameStateType.Loading;
			var scene = "MainMenu";
			OnLoadingStarted?.Invoke(scene);
			
			var unloadOp = SceneManager.UnloadSceneAsync("Castle");
			var loadOp = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
			operations.Clear();

			if (unloadOp != null) operations.Add(unloadOp);
			if (loadOp != null) operations.Add(loadOp);

			StartCoroutine(CheckLoading(scene));
		}

		private void LoadPersistentScene() 
		{
			if (Helpers.IsSceneLoaded("PersistentObjects")) return;
			SceneManager.LoadScene("PersistentObjects", LoadSceneMode.Additive);
		}

		private IEnumerator CheckLoading(string scene)
		{
			yield return new WaitUntil(IsLoadingFinished);
			OnLoadingFinished(scene);
		}

		private bool IsLoadingFinished()
		{
			var progress = operations.Average(x => x.progress);
			OnProgressChanged?.Invoke(progress);
			var isFinished = operations.All(x => x.isDone);
			return isFinished;
		}

		private void OnLoadingFinished(string scene) => OnSceneLoaded?.Invoke(scene);
	}
}