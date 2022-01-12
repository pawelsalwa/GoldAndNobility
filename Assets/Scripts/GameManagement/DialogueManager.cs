using System;
using System.Collections.Generic;
using Common;
using Common.Attributes;
using DialogueSystem;
using GameInput;
using UnityEngine;

namespace GameManagement
{
    /// <summary> I got no idea if thats a good approachm basically this class wraps DialogueSystem functionality providing similar interface that system itself to service locator.
    /// Thanks to that, DialogueSystem doesnt need to have any project related dependancies</summary>
    [GameService()]
    internal class DialogueManager : MonoBehaviour
    {

        private IDialogueController controller;

        private void DialogueStarted(DialogueData obj) => GameState.ChangeState(GameStateType.InDialogue);

        private void DialogueEnded() => GameState.CancelState(GameStateType.InDialogue);

        private void Awake()
        {
            controller = DialogueController.instance;
            controller.OnDialogueStarted += DialogueStarted;
            controller.OnDialogueEnded += DialogueEnded;
        }

        private void OnDestroy()
        {
            controller.OnDialogueStarted -= DialogueStarted;
            controller.OnDialogueEnded -= DialogueEnded;
        }

        private void Update()
        {
            if (DialogueInput.advanceDialogue) controller.Skip();
        }
    }
}