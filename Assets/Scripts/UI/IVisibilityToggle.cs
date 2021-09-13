namespace UI
{
	public interface IVisibilityToggle
	{
		bool Visible { get; }
		void Show();
		void Hide();
	}
}