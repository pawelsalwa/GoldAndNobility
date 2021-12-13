using System;
using UnityEngine;

namespace DialogueSystem
{
    /// <summary>
    /// used as scriptable object so we can stop dialogue and put some custom action instead of dialogue Quote
    /// call FinishDialogueAction() from derived class when dialogue should proceed
    /// </summary>
    public abstract class DialogueAction : ScriptableObject
    {
        internal event Action OnFinished;
        
        public abstract void BeginDialogueAction();
        
        protected void FinishDialogueAction() => OnFinished?.Invoke();
    }
}