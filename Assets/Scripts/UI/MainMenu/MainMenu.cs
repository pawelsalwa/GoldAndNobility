using System;
using Common;
using Common.Fsm;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {

        public Button startBtn;
        public Button loadBtn;
        public Button quitBtn;
        
        private IGameFsm gameFsm;

        private void Start()
        {
            gameFsm = ServiceLocator.RequestService<IGameFsm>();
            startBtn.onClick.AddListener(OnStartBtn);
        }

        private void OnStartBtn()
        {
            gameFsm.StartNewGame();
        }

    }
}
