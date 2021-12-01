using UnityEngine;

namespace RuntimeData
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObject/InventoryItem")]
    public class Item : ScriptableObject
    {
        public Sprite sprite;
        public string Name => name;
    }
}