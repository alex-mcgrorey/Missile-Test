using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
    public Vector3 direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction, Space.World);
	}

    void OnCollisionEnter(Collision collision) //Must have a Rigidbody to work!
    {
        direction = -direction;
    }
}
