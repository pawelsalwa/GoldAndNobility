using System;
using System.Collections;
using Common;
using Common.Fsm;
using Common.GameInput;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Tests
{
	public class IntegrationTest
	{
		private const float maxLoadingDuration = 10; // secs
		
		private float timer;

		[SetUp]
		public void Setup() 
		{
		}

		[Test]
		public void CheckPersistentObjectLoaded()
		{
			// this should be initialized on InitializeOnLoad or some Awake()
			Assert.IsTrue(Helpers.IsSceneLoaded("PersistentObjects")); 
		}


		[UnityTest]
		public IEnumerator EnterCastleScene()
		{
			SceneManager.LoadScene("Castle");
			Assert.IsTrue(GameplayInput.enabled);
			yield break;
		}

		[UnityTest]
		public IEnumerator PassThroughApp()
		{
			SceneManager.LoadScene("MainMenu");
			// ServiceLocator.RequestService<ISceneLoader>().OnSceneLoaded += 
			timer = Time.time;
			yield return null;
			GameObject.Find("StartBtn").GetComponent<Button>().onClick.Invoke();
			yield return new WaitUntil(IsLoadingCompleted);
			Assert.IsTrue(GameplayInput.enabled);
			PauseGameManager.Pause();
			Assert.IsFalse(GameplayInput.enabled);
			GameObject.Find("MainMenuBtn").GetComponent<Button>().onClick.Invoke();
			// yield return new WaitUntil(IsLoadingCompleted);

			// Debug.Log($"<color=white>Enter game from main menu test... </color>");
			// SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
			// Assert.AreEqual(gameState.Current, gameState.StartingApp);
			// gameFsm.EnterMainMenu();
			// Assert.AreEqual(gameState.Current, gameState.MainMenu);
			// gameFsm.StartNewGame();
			// Assert.AreEqual(gameState.Current, gameState.Loading);
			//
			// timer = Time.time;
			// Assert.AreEqual(gameState.Current, gameState.LoadedAndWaiting);
			// Assert.IsTrue(Helpers.IsSceneLoaded("Castle"));
		}

		[TearDown]
		public void TearDown() { }

		private bool IsLoadingCompleted()
		{
			if (Helpers.IsSceneLoaded("Castle")) return true;
			if (Time.time - timer > maxLoadingDuration)
				throw new TimeoutException("Loading takes longer than defined.");
			return false;
		}
	}
}