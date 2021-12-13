using NaughtyAttributes;
using UnityEngine;

namespace CharacterSystem
{
	internal class BotController : MonoBehaviour
	{
		private Character chara;

		public Transform InspectorTarget;
		private Vector3 target;

		private bool moving;

		private void Start()
		{
			chara = GetComponent<Character>();
			if (!chara) enabled = false;
			chara.input.LookAtAngle = transform.rotation.eulerAngles.y;
		}

		private void Update()
		{
			if (moving)
			{
				MoveToTarget();
			}
			else
			{
				chara.input.Movement = Vector2.zero;
			}
		}

		private void MoveToTarget()
		{
			chara.input.Movement = Vector2.up;
			LookAt(target);
			if ((target - transform.position).magnitude < 0.5f) moving = false;
		}

		private void LookAt(Vector3 pos)
		{
			var rot = Quaternion.LookRotation(pos - transform.position);
			chara.input.LookAtAngle = rot.eulerAngles.y;
		}

		[Button]
		private void GoToTarget()
		{
			target = InspectorTarget.position;
			moving = true;
		}
	}
}