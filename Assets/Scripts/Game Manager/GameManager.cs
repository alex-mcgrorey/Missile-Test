//  Author:     Alex McGrorey
//  Project:    Missile Pilot
//
//  The GameManager would be a singleton if not for the capabilities of the editor and the MonoBehaviour class.
//  There is one GameManager in the scene, and it should maintain references to all other objects.
//****************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Monobehaviours are called once per frame, and can therefore use the Update() methods
public class GameManager : MonoBehaviour {
    public int length;      //length of the rendered level

    //Semgent difficulty probabilities, set in editor
    public int probCommon;
    public int probUncommon;
    public int probRare;
    public int probLegendary;

    //Creator stores methods and object references useful for level building, set in Start()
    private LevelCreationManager creator;

    //Set in editor
    public Vector3 startPoint;
    public GameObject compass;
    public GameObject missle;
    public float startSpeed;

    private List<GameObject> activeLevel = new List<GameObject>();  //Stores the current, rendered level as references to their GameObjects

    private int score = 0;  //Score counter
    private Vector3 compassRotation;
    private float t_runTime = 0.00f;    //Runtime counter

    //Debug Vars***************************************************     d_ = Debug
    private int d_distanceRendered = 0;


    //Debug Methods************************************************
    public void incrementdDistanceRendered() {
        d_distanceRendered++;
    }

    public int getDistanceRendered() {
        return d_distanceRendered;
    }

    // Accessors***************************************************
    public int getScore() {
        return score;
    }

    public Quaternion getCompassRotation(int i){
        return activeLevel[i+1].transform.rotation; //returns the rotation of the next segment.  This will always be the current segment's end rotation.
    }

    public float getRunTimer() {
        return t_runTime;
    }

    // Mutaters****************************************************
    public void incrementScore(){
        score++;
    }

    public void setCompassRotation(Vector3 nextRotation) {
        compassRotation = nextRotation;
    }

    // Use this for initialization
    void Start () {
        creator = GetComponent<LevelCreationManager>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTimer();
        initializeLevelUpdate();
        UpdateLevelPointer();
    }

    private void UpdateTimer() {
        t_runTime += Time.deltaTime;
    }

    //checks the current position of the player and updates the compass accordingly
    private void UpdateLevelPointer() {
        bool found = false;
        int index = 0;
        for(int i = 0; i < activeLevel.Count - 1; i++) {
            if (!activeLevel[i].GetComponent<SegmentManager>().getContacted() && !found) {
                found = true;
                index = i;
            }
        }
        compass.transform.rotation = activeLevel[index].transform.rotation;
    }

    // This method is called once per frame, and is used to check if the player has progressed enough to progress the course
    private void initializeLevelUpdate() {
        //First, check if the level has been fully rendered, and if the player has entered the third segment.
        if (activeLevel.Count == length && activeLevel[3].GetComponent<SegmentManager>().getContacted()) {
            // If so, destroy the first first segment and clear its position in the linked list
            DestroyObject(activeLevel[0]);
            activeLevel.RemoveAt(0);
        }
        //This is used at the beginning of the game to initialize the level, or after a segment has been deleted.
        //This entire if-statement is a testament to why there should be direct constructor support for GameObjects
        if (activeLevel.Count < length) {
            GameObject segment = Instantiate(creator.ChooseSegment(probCommon, probUncommon, probRare, probLegendary), transform.position, transform.rotation) as GameObject;   //Create a segment object, using the LevelCreationManager
            activeLevel.Add(segment);   // Add to the end of the activeLevel
            d_distanceRendered++;       // Increment debug variable
            segment.GetComponent<SegmentManager>().setLevelIndex(activeLevel.IndexOf(segment)); //Again, wish I could use a proper constructor
            if (activeLevel.Count > 1) {    // This does not apply to the very first segment rendered
                segment.transform.Translate(activeLevel[activeLevel.IndexOf(segment) - 1].GetComponent<SegmentManager>().rearNode.transform.position);  // Move new segment to align with the end-node of the last segment
                segment.transform.Rotate(activeLevel[activeLevel.IndexOf(segment) - 1].transform.rotation.eulerAngles);                                 // Match rotation with the last segment
                segment.transform.Rotate(activeLevel[activeLevel.IndexOf(segment) - 1].GetComponent<SegmentManager>().getNextRotation());               // Apply nextRotation to new segment (necessary for any segments that include a turn) 
            }
        }
    }

    public void resetLevel() {
        Application.LoadLevel(1);
    }
}