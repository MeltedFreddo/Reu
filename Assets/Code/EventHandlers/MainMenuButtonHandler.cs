using Assets.Code.Controllers;
using Assets.Code.Data.Lists;
using UnityEngine;

namespace Assets.Code.EventHandlers
{
    public class MainMenuButtonHandler : MonoBehaviour {

        public void StartGame()
        {
            App.Instance.LoadScene(SceneNames.SystemView);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        App.Instance.Quit();
#endif
        }
    }
}
