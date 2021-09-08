using System;
using UnityEngine;

namespace Common
{

	[Serializable]
	public struct Optional<T>
	{
		[SerializeField] private bool enabled;
		[SerializeField] private T value;

		public bool Enabled => enabled;
		public T Value => value;
		public T EditorSetValue { set => this.value = value; }
		public bool EditorSetEnabled { set => this.enabled = value; }

		public Optional(T initialValue)
		{
			enabled = true;
			value = initialValue;
		}
	}
}