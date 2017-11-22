using Code.BaseClasses;
using Data.Lists;

public class MainMenuController : AppMonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
