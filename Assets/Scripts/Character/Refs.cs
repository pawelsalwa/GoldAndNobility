using System;
using Common;
using UnityEngine;

namespace Character
{
	[Serializable]
	public class Refs
	{
		public bool FsmDebugs = false;
		public TrailRenderer Trail;
		public bool overrideInput = false;
		public PlayerInput debugPlayerInput;
		public Optional<float> xdd;
	}
}