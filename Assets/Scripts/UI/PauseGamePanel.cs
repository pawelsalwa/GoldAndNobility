﻿using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	internal class PauseGamePanel : UiPanelBase
	{

		public Button returnToGameBtn;
		public Button mainMenuBtn;
		public Button quitBtn;

		protected override void Start()
		{
			base.Start();

			returnToGameBtn.onClick.AddListener(PauseGameManager.Resume);
			mainMenuBtn.onClick.AddListener(ReturnToMainMenu);
			quitBtn.onClick.AddListener(Quit);

			PauseGameManager.OnPaused += Open;
			PauseGameManager.OnResumed += Close;
		}

		protected override void OnDestroy()
		{
			PauseGameManager.OnPaused -= Open;
			PauseGameManager.OnResumed -= Close;
		}

		private void ReturnToMainMenu() => ServiceLocator.RequestService<ISceneLoader>().LoadMainMenu();


		private void Quit() { throw new System.NotImplementedException(); }

		private void Update()
		{
			if (!Active) HandlePause();
			else HandleUnpause();
		}

		private void HandlePause()
		{
			if (Input.GetKeyDown(KeyCode.Escape)) PauseGameManager.Pause();
		}

		private void HandleUnpause()
		{
			if (Input.GetKeyDown(KeyCode.Escape)) PauseGameManager.Resume();
		}
	}
}