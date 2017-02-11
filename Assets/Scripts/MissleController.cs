using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {
    public float startSpeed;
    public float acceleration;
    public float maxSpeed;
    private float effectiveSpeed;

    public Vector3 startPoint;
    private Quaternion startRotation;
    public GameManager manager;

    public float smooth = 2.0f;
    public float tiltAngle = 30.0f;

    private float tiltAroundZ;
    private float tiltAroundX;
    private float tiltAroundY;

    // Use this for initialization
    void Start () {
        startRotation = transform.rotation;
        effectiveSpeed = startSpeed;
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
        WaitOnLoad();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Moving at "+effectiveSpeed);
        transform.Translate(-Vector3.up * Time.deltaTime*effectiveSpeed, Space.Self);
        if(this.GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
            this.GetComponent<Rigidbody>().AddForce(effectiveSpeed * -transform.up);
	}

    public void Move(float yaw, float pitch, float roll)
    {
        tiltAroundZ = yaw/2;
        tiltAroundX = pitch;
        tiltAroundY = roll;

        transform.Rotate(-tiltAroundX, tiltAroundY, -tiltAroundZ);
    }

    public void SpeedAdjust(float boost, float brake)
    {
        if (boost > 0.1f)
            effectiveSpeed = startSpeed * 2;
        if (brake > 0.1f)
            effectiveSpeed = startSpeed / 2;
        if (brake == 0 && boost == 0)
            effectiveSpeed = startSpeed;

        //Debug.Log("Adjusting speed to " + effectiveSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision: "+ collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Finish":
                //manager.ResetLevel(true);
                transform.position = startPoint;
                break;
            case "Obstacle":
                //manager.ResetLevel(false);
                transform.position = startPoint;
                transform.rotation = startRotation;
                manager.resetLevel();
                break;
            default:
                break;
        }
    }

    IEnumerator WaitOnLoad()
    {
        yield return new WaitForSeconds(3);
    }
}