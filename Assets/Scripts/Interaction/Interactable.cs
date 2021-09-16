using System;
using UnityEngine;

namespace Interaction
{
	public class Interactable : MonoBehaviour
	{

		private void OnValidate()
		{
			if (LayerMask.LayerToName( gameObject.layer) == "Interactable" ) return;
			Debug.Log($"<color=red> Interactable component doesnt have Interactable layer! {gameObject}</color>", gameObject);
		}
	}
}