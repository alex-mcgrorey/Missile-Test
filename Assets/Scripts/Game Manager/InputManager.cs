using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public MissleController missle;
    public GameManager manager;
    public Texture exitButton;
    public Texture resetButton;
    public Texture leftButton;
    public Texture rightButton;
    public Texture boostButton;
    public Texture brakeButton;

    private float yaw;
    private float pitch;
    private float roll;

    private float boost;
    private float brake;
    private bool mobile;

    private float zCal;
    private float yCal;

	// Use this for initialization
	void Start () {
	    if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            mobile = true;
            zCal = CalibrateAccelerometerZ();
            yCal = CalibrateAccelerometerY();
        }
	}
	
	// Update is called once per frame
	void Update () {
        getInput();
	}

    private void getInput()
    {
        if (!mobile)
        {
            yaw = Input.GetAxis("Yaw");
            pitch = Input.GetAxis("Pitch");
            roll = Input.GetAxis("Roll");
            boost = Input.GetAxis("Boost");
            brake = Input.GetAxis("Brake");
            missle.SpeedAdjust(boost, brake);
        }
        else
        {
            yaw = 0;
            pitch = -Input.acceleration.z + zCal;
            roll = Input.acceleration.x;
        }
        missle.Move(yaw, pitch, roll);
        //Mobile: SpeedAdjust is called in the OnGUI method
    }

    void OnGUI()
    {
        //Utility******************** RESET EXIT
        if (GUI.Button(new Rect(10, 10, 250, 50), resetButton, GUIStyle.none))
        {
            Application.UnloadLevel(0);
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect(Screen.width-60, 10, 250, 50), exitButton, GUIStyle.none))
            Application.LoadLevel(0);

        //Movement******************* MOVEMENT
        boost = GUI.RepeatButton(new Rect(Screen.width - Screen.width/2 + 150, Screen.height - 120, 200, 120), boostButton, GUIStyle.none) ? 1 : 0;
        brake = GUI.RepeatButton(new Rect(Screen.width - Screen.width / 2 - 350, Screen.height - 120, 200, 120), brakeButton, GUIStyle.none) ? 1 : 0;
        yaw = GUI.RepeatButton(new Rect(100, Screen.height - 100, 150, 425), leftButton, GUIStyle.none) ? -0.5f : 0;
        yaw = GUI.RepeatButton(new Rect(Screen.width - 250, Screen.height-100, 150, 425), rightButton, GUIStyle.none) ? 0.5f : yaw;
        GUI.color = Color.green;
        GUI.Box(new Rect(Screen.width - Screen.width/2 - 20, 0, 50, 25), manager.getRunTimer().ToString("00:00"));
        GUI.Box(new Rect(Screen.width - Screen.width/2 - 20,25,50,25), manager.getScore().ToString());

        missle.Move(yaw, 0, 0);
        missle.SpeedAdjust(boost, brake);
    }

    private float CalibrateAccelerometerZ()
    {
        return Input.acceleration.z;
    }

    private float CalibrateAccelerometerY()
    {
        return Input.acceleration.y;
    }
}
