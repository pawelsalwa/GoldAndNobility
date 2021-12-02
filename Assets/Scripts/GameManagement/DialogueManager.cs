using Common;
using Common.Attributes;
using Dialogue;
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
        }

        private void OnDestroy()
        {
            controller.OnDialogueStarted -= OnDialogueStarted;
            controller.OnDialogueEnded -= OnDialogueEnded;
        }

        private void OnDialogueStarted(DialogueData obj) => GameState.ChangeState(GameStateType.InDialogue);

        private void OnDialogueEnded() => GameState.CancelState(GameStateType.InDialogue);

        // public event Action<DialogueData> OnDialogueStarted;
        // public event Action OnDialogueEnded;
        // public event Action<Quote> OnQuoteStarted;
        // public event Action<List<Quote>> OnPlayerQuotesAppear;

        // public void StartDialogue(DialogueData data)
        // {
        //     GameState.ChangeState(GameStateType.InDialogue);
        //     controller.StartDialogue(data);
        // }

        // public void ChoosePlayerQuote(Quote quote) => controller.ChoosePlayerQuote(quote);
        //
        // public void Skip() => throw new Exception("Dont call this method, skip is made in update instead on its own.");

        private void Update()
        {
            if (DialogueInput.advanceDialogue) controller.Skip();
        }

        // private void Start()
        // {
        //     controller.OnDialogueStarted += OnDialogueStarted.Invoke;
        //     controller.OnDialogueEnded += OnDialogueEndedCallback;
        //     controller.OnQuoteStarted += OnQuoteStarted.Invoke;
        //     controller.OnPlayerQuotesAppear += OnPlayerQuotesAppear.Invoke;
        // }
        //
        // private void OnDestroy()
        // {
        //     controller.OnDialogueStarted -= OnDialogueStarted.Invoke;
        //     controller.OnDialogueEnded -= OnDialogueEndedCallback;
        //     controller.OnQuoteStarted -= OnQuoteStarted.Invoke;
        //     controller.OnPlayerQuotesAppear -= OnPlayerQuotesAppear.Invoke;
        //     
        // }
        //
        // private void OnDialogueEndedCallback()
        // {
        //     GameState.
        //     OnDialogueEnded?.Invoke();
        // }
    }
}