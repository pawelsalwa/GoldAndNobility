using System;
using System.Collections;
using Common;
using Common.Fsm;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
	public class IntegrationTest
	{
		private const float maxLoadingDuration = 10; // secs
		
		private IGameFsm gameFsm;
		private IGameStates gameState;
		private float timer;

		[SetUp]
		public void Setup() 
		{
			Debug.Log($"<color=orange>seereetup</color>");
			gameFsm = ServiceLocator.RequestService<IGameFsm>();
			gameState = ServiceLocator.RequestService<IGameStates>();
		}

		[UnityTest]
		public IEnumerator EnterGameFromMainMenu()
		{
			Debug.Log($"<color=white>Enter game from main menu test... </color>");
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
			Assert.AreEqual(gameState.Current, gameState.StartingApp);
			gameFsm.EnterMainMenu();
			Assert.AreEqual(gameState.Current, gameState.MainMenu);
			gameFsm.StartNewGame();
			Assert.AreEqual(gameState.Current, gameState.Loading);

			timer = Time.time;
			yield return new WaitUntil(IsLoadingCompleted);
			Assert.AreEqual(gameState.Current, gameState.LoadedAndWaiting);
			Assert.IsTrue(Helpers.IsSceneLoaded("Castle"));
		}
		
		// [Test]
		// public void EnterGameFromGameScene()
		// {
		// 	Debug.Log($"<color=white>Enter game scene immediately test... </color>");
		// 	SceneManager.LoadScene("Castle", LoadSceneMode.Additive);
		// 	Assert.AreEqual(gameState.Current, gameState.InGame);
		// }

		[TearDown]
		public void TearDown() { }

		private bool IsLoadingCompleted()
		{
			if (gameState.Current != gameState.Loading) return true;
			if (Time.time - timer > maxLoadingDuration)
				throw new TimeoutException("Loading takes longer than defined.");
			return false;
		}
	}
}