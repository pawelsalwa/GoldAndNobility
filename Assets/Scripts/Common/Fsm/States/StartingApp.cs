using UnityEngine.SceneManagement;

namespace Common.Fsm.States
{
	internal class StartingApp : StateBase
	{

		/// <summary> Checks what scene should be currently loaded. Handles editor enter playmode with arbitrary scenes as well as build. </summary>
		protected override void OnEnter()
		{
			if (!Helpers.IsSceneLoaded("PersistentObjects")) 
				SceneManager.LoadScene("PersistentObjects", LoadSceneMode.Additive);

			if (Helpers.IsSceneLoaded("Castle"))
			{
				ServiceLocator.RequestService<IGameFsm>().EnterGame();
			}
			else
			{
				if (!Helpers.IsSceneLoaded("MainMenu"))  
					SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
			}
		} 
	}
}