using NaughtyAttributes;
using UnityEngine;

namespace Tools
{
	public class GizmoSphere : MonoBehaviour
	{
		[SerializeField] private float radius = 0.5f;
		public Color color = Color.blue;
		[Range(0.1f, 1f)]
		public float alpha = 0.25f;

		private void Start()
		{
			//just for enabling in inspector....
		}

		private void OnDrawGizmos()
		{
			if (!enabled) return;
			color.a = alpha;
			Gizmos.color = color;
			Gizmos.DrawSphere(transform.position, radius);
		}

		[Button]
		private void ToggleInstancesEnabled()
		{
			var en = enabled;
			foreach (var gs in FindObjectsOfType<GizmoSphere>()) gs.enabled = !en;
		}
	}
}