namespace Common.Fsm.States
{
	internal class StartingNewGame : StateBase
	{
		protected override void OnEnter()
		{
			var sceneLoader = ServiceLocator.RequestService<ISceneLoader>();
			sceneLoader.LoadGameScene();
		}
	}
}