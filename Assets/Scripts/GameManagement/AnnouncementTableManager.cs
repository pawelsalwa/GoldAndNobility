using System;
using Common;
using Common.Attributes;
using UnityEngine;

namespace GameManagement
{
    [GameService(typeof(AnnouncementTableManager))]
    public class AnnouncementTableManager : MonoBehaviour
    {

        public event Action OnTableOpened;
        public event Action OnTableClosed;

        public void ShowTable()
        {
            GameState.ChangeState(GameStateType.AnnouncementTableBrowsing);
            OnTableOpened?.Invoke();
        }

        public void HideTable()
        {
            GameState.CancelState(GameStateType.AnnouncementTableBrowsing);
            OnTableClosed?.Invoke();
        }
    }
}