using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Attributes;
using Common.Fsm;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
	/// <summary> Api for loading scenes and monitoring current progress. </summary>
	[PersistentComponent]
	internal class SceneLoader : MonoBehaviour, ISceneLoader
	{
		private readonly List<AsyncOperation> operations = new List<AsyncOperation>();
		private float progress = 0f;
		private bool isLoading = false;
		private IGameFsm gameFsm;

		private SceneLoader() => ServiceLocator.RegisterService<ISceneLoader>(this);

		public float Progress => progress;
		public bool IsLoading => isLoading;

		private void Start()
		{
			gameFsm = ServiceLocator.RequestService<IGameFsm>();
		}

		public void LoadGameScene()
		{
			ServiceLocator.RequestService<IGameFsm>().EnterLoading();

			var unloadOp = SceneManager.UnloadSceneAsync("MainMenu");
			var loadOp = SceneManager.LoadSceneAsync("Castle", LoadSceneMode.Additive);

			progress = 0f;
			isLoading = true;
			operations.Clear();

			if (unloadOp != null) operations.Add(unloadOp);
			if (loadOp != null) operations.Add(loadOp);

			StartCoroutine(CheckLoading());
		}

		public void LoadMainMenu()
		{
			ServiceLocator.RequestService<IGameFsm>().EnterLoading();

			var unloadOp = SceneManager.UnloadSceneAsync("Castle");
			var loadOp = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);

			progress = 0f;
			isLoading = true;
			operations.Clear();

			if (unloadOp != null) operations.Add(unloadOp);
			if (loadOp != null) operations.Add(loadOp);

			StartCoroutine(CheckLoading());
		}

		private IEnumerator CheckLoading()
		{
			yield return new WaitUntil(IsLoadingFinished);
			OnLoadingFinished();
		}

		private bool IsLoadingFinished()
		{
			progress = operations.Average(x => x.progress);
			var isFinished = operations.All(x => x.isDone);
			if (isFinished && progress < 1f) Debug.Log($"<color=red>Progress is lower than 1, wtf?</color>");
			return isFinished;
		}

		private void OnLoadingFinished()
		{
			isLoading = false;

			if (Helpers.IsSceneLoaded("MainMenu")) gameFsm.EnterMainMenu();
			else gameFsm.FinishLoading();

		}
	}
}