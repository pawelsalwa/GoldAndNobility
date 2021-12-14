using CharacterSystem;
using Common;
using GameInput;
using UnityEngine;

namespace GameManagement
{
	internal class PlayerController : MonoBehaviour, IPlayerCharacterSingleton
	{
		private Character chara;

		[field: SerializeField]
		public TradeEntity tradeEntity { get; private set; }
		
		private void Awake()
		{
			ServiceLocator.RegisterService<IPlayerCharacterSingleton>(this);
			GameState.ChangeState(GameStateType.InGame); // player controller shoyld be only one so it could kinda initialize that
		}

		protected virtual void Start()
		{
			chara = GetComponent<Character>();
			if (!chara) enabled = false;
		}

		private void Update()
		{
			chara.input.Movement = GameplayInput.movement;
			chara.input.shift = GameplayInput.shiftDown;
			chara.input.LookAtAngle = Camera.main.transform.eulerAngles.y;
		}

	}

	public interface IPlayerCharacterSingleton
	{
		TradeEntity tradeEntity { get; }
	}
}