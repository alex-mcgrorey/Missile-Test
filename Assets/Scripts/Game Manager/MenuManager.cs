using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public Texture startButton;
    public Texture exitButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, Screen.height - 110, 500, 100), startButton, GUIStyle.none))
            Application.LoadLevel(1);
        if (GUI.Button(new Rect(Screen.width - 250, Screen.height - 110, 500, 100), exitButton, GUIStyle.none))
            Application.Quit();
    }
}
