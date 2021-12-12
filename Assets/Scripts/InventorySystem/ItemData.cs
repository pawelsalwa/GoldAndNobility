﻿using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObject/InventoryItem")]
    public class ItemData : ScriptableObject
    {
        public Sprite sprite;
        public string desc;
        public bool stacks = true;
    }
}