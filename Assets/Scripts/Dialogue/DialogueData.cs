using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "NewConversation", menuName = "ScriptableObject/DialogueData")]
    public class DialogueData : ScriptableObject
    {
        public List<Quote> quotes;
    }

}
