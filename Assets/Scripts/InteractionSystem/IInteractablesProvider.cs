using System;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem
{
	internal interface IInteractablesProvider
	{
		List<InteractableBase> Interactables { get; }
		Transform CameraTransform { get; }
	}
}