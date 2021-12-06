using Common;
using Common.Attributes;
using NaughtyAttributes;
using UnityEngine;

namespace GameManagement
{
    /// <summary> Marks cursor, its fully dependant on GameState class </summary>
    [GameService]
    internal class CursorManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private CursorLockMode cursorState = CursorLockMode.None;
        [SerializeField, ReadOnly] private bool cursorVisible = false;
        
        private void Awake() => GameState.OnChanged += CheckCursorState;

        private void OnDestroy() => GameState.OnChanged -= CheckCursorState;

        private void CheckCursorState(GameStateType obj)
        {
            switch (obj)
            {
                case GameStateType.Loading:
                case GameStateType.InGame:
                    cursorState = CursorLockMode.Locked;
                    cursorVisible = false;
                    break;

                case GameStateType.None: 
                case GameStateType.MainMenu:
                case GameStateType.Paused:
                case GameStateType.InDialogue:
                case GameStateType.BrowsingInventory:
                case GameStateType.AnnouncementTableBrowsing:
                    cursorState = CursorLockMode.None;
                    cursorVisible = true;
                    break;
            }

            UpdateCursorState();
        }

        private void UpdateCursorState()
        {
            Cursor.visible = cursorVisible;
            Cursor.lockState = cursorState;
        }
    }
}