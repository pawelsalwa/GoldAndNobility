using UnityEngine;

namespace DialogueSystem
{
	
	/// <summary> used so we can reuse dialogue data for multiple characters. It also instantiates the dialogue so it can be modified at runtime for quests purposes for example. </summary>
	public class DialogueEntity : MonoBehaviour
	{

		[SerializeField] private DialogueData dialogueData;
		[field: SerializeField]
		public DialogueData RuntimeDialogue { get; private set; }

		public void StartDialogue() => DialogueController.instance.StartDialogue(RuntimeDialogue);

		public void AddRuntimeDialogueBranch(DialogueData dialogueBranch)
		{
			foreach (var quote in dialogueBranch.quotes) RuntimeDialogue.AddQuote(quote);
			foreach (var connection in dialogueBranch.edges) RuntimeDialogue.AddEdge(dialogueBranch.quotes[connection.outputIdx], dialogueBranch.quotes[connection.inputIdx]);
			// make ending quotes of quest dialogue point to entry quote of runtime NPC dialogue
			foreach (var endQuote in dialogueBranch.EndingQuotes) RuntimeDialogue.AddEdge(endQuote, RuntimeDialogue.entryQuote);
			RuntimeDialogue.AddEdge(RuntimeDialogue.entryQuote, dialogueBranch.entryQuote); // dont forget to add it to root choices :)
		}

		public void RemoveRuntimeDialogueBranch(DialogueData dialogueBranch)
		{
			foreach (var quote in dialogueBranch.quotes) RuntimeDialogue.RemoveQuote(quote);
		}

		private void Awake()
		{
			RuntimeDialogue = Instantiate(dialogueData);
		}
	}
}