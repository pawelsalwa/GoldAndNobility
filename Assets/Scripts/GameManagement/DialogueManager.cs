using Common;
using Common.Attributes;
using DialogueSystem;
using GameInput;
using UnityEngine;

namespace GameManagement
{
    /// <summary> I got no idea if thats a good approachm basically this class wraps Dialogue system functionality providing the same interface that system itself to service locator.
    /// Thanks to that, DialogueSystem doesnt need to have any project related dependancies</summary>
    [PersistentComponent()]
    public class DialogueManager : MonoBehaviour
    {

        private readonly IDialogueController controller = new DialogueController();

        private void Awake()
        {
            ServiceLocator.RegisterService(controller);
            controller.OnDialogueStarted += OnDialogueStarted;
            controller.OnDialogueEnded += OnDialogueEnded;
            controller.OnQuoteStarted += CheckDialogueAction;
        }

        private void OnDestroy()
        {
            controller.OnDialogueStarted -= OnDialogueStarted;
            controller.OnDialogueEnded -= OnDialogueEnded;
            controller.OnQuoteStarted -= CheckDialogueAction;
        }

        private void CheckDialogueAction(Quote obj)
        {
            if (!obj.isDialogueAction) return;
            if (obj.text == "selling_ui_action")
                GameState.ChangeState(GameStateType.Trading);
        }

        private void OnDialogueStarted(DialogueData obj) => GameState.ChangeState(GameStateType.InDialogue);

        private void OnDialogueEnded() => GameState.CancelState(GameStateType.InDialogue);
        private void Update()
        {
            if (DialogueInput.advanceDialogue) controller.Skip();
        }
    }
}