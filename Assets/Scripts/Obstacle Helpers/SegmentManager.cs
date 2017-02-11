using UnityEngine;
using System.Collections;

public class SegmentManager : MonoBehaviour {
    public bool debug = false;
    public GameObject frontNode;
    public GameObject rearNode;
    public Vector3 nextRotation;

    private GameManager gameManager;
    private bool contacted = false;
    private int levelPos;
    private int levelIndex = 0;


    //Accessors***************************************************
    public bool getContacted()
    {
        return contacted;
    }

    public Vector3 getNextRotation() {
        return nextRotation;
    }

    public int getLevelIndex() {
        return levelIndex;
    }

    //Mutators****************************************************
    public void setLevelIndex(int index) {
        levelIndex = index;
    }

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        levelPos = gameManager.getDistanceRendered();
    }
	
	// Update is called once per frame
	void Update () {
        if (debug) {
            GetComponent<TextMesh>().text = levelIndex.ToString();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!contacted) {
                contacted = true;
                gameManager.incrementScore();
            }
        }
    }
}