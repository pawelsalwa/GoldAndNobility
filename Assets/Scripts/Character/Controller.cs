using UnityEngine;

namespace Character
{
	[RequireComponent(typeof(Character))]
	public class Controller : MonoBehaviour
	{

		// protected virtual void Start() => GetComponent<Character>().Controller = this;

		public virtual void UpdateInput(PlayerInput playerInput)
		{
		}
	}
}