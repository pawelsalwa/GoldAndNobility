using System;
using Common;
using UnityEngine;

namespace CharacterSystem
{
	[Serializable]
	public class Refs
	{
		public bool FsmDebugs = false;
		public TrailRenderer Trail;
		public PlayerInput input;
		public Optional<float> xdd;
	}
}