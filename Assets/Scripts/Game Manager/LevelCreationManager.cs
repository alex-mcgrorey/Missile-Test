using UnityEngine;
using System.Collections;

// This class is a child of Monobehaviour so that the segments can be added via the editor
public class LevelCreationManager : MonoBehaviour {
    public GameObject plain;
    public GameObject end;
    public GameObject[] common;
    public GameObject[] uncommon;
    public GameObject[] rare;
    public GameObject[] legendary;

    private GameObject[] startQueue;

    public GameObject getEnd()
    {
        return end;
    }

    public GameObject ChooseSegment(int chanceCommon, int chanceUncommon, int chanceRare, int chanceLegendary)
    {
        int rand = Random.Range(1,100);
        
        if(rand >= 100 - chanceLegendary)
        {
            return legendary[Random.Range(0, legendary.Length)];
        }
        else if(rand >= 100 - chanceRare)
        {
            return rare[Random.Range(0, rare.Length)];
        }
        else if(rand >= 100 - chanceUncommon)
        {
            return uncommon[Random.Range(0, uncommon.Length)];
        }
        else if(rand >= 100 - chanceCommon)
        {
            return common[Random.Range(0, common.Length)];
        }
        else
        {
            return plain;
        }
        //  Switch statements in C# do not allow dynamic switch cases. lolwat
    }
}
