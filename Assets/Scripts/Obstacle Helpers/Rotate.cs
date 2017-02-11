using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    public float rotateSpeed;
    private float random;

	// Use this for initialization
	void Start () {
        random = Random.Range(-1, 2);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * rotateSpeed * random);
	}
}
