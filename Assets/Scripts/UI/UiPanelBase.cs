using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	[RequireComponent(typeof(CanvasGroup))]
	internal abstract class UiPanelBase : UIBehaviour, IUIPanel, IVisibilityToggle 
	{

		[SerializeField, HideInInspector] private CanvasGroup cg;

		public bool Active { get; private set; } = false;
		public bool Visible => cg.interactable;
		
		protected virtual bool ShowOnAwake => false;

		public void Open()
		{
			Active = true;
			Show();
			OnOpened();
		}

		public void Close()
		{
			Active = false;
			Hide();
			OnClosed();
		}
		
		[Button] public void Show()
		{
			cg.interactable = cg.blocksRaycasts = true;
			cg.alpha = 1f;
		}

		[Button] public void Hide()
		{
			cg.interactable = cg.blocksRaycasts = false;
			cg.alpha = 0f;
		}

		protected override void Awake()
		{
			if (ShowOnAwake) Open();
			else Hide();
			//else Close(); // make sure this doenst bug anything in inherited panels, like missing refs, maybe this should be in Start() instead?
		}

		protected virtual void OnOpened() { }
		protected virtual void OnClosed() { }

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			cg = GetComponent<CanvasGroup>();
			if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();
		}
#endif
	}
}