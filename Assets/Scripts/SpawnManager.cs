using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public string spawnPoint; 
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

   
    public void onLevelLoaded()
    {
        // all objects are loaded, call other methods
        DoorScript[] doors = FindObjectsOfType(typeof(DoorScript)) as DoorScript[];
        foreach (var t in doors)
        {

            if (t.spawnId == spawnPoint)
            {

                changePositiion("Player", t.transform);
                changePositiion("MainCamera", t.transform);
               

            }

        }
    }

    private void changePositiion(string tag, Transform trans)
    {
        GameObject obj = GameObject.FindGameObjectWithTag(tag);
        if (obj == null)
        {
            return;
        }
        Vector3 newPos = new Vector3(trans.position.x, trans.position.y, obj.transform.position.z);
        obj.transform.position = newPos;

    }


}
