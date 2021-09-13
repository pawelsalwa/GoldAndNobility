namespace Common
{
	public interface ISceneLoader
	{

		float Progress { get; }
		bool IsLoading { get; }
		void LoadGameScene();
		void LoadMainMenu();

	}
}