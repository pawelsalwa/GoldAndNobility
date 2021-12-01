using System;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem
{
	internal interface IInteractablesProvider
	{
		List<Interactable> Interactables { get; }
		Transform CameraTransform { get; }
	}
}