using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class SynchronizingTransformOfObjects : MonoBehaviourPun
{
    // Start is called before the first frame update

    public string tagOfPositionToSync;
    private GameObject objectToSync;
    private bool objectFound;
    

    void Start()
    {
        objectFound = false;
        objectToSync = GameObject.FindGameObjectWithTag(tagOfPositionToSync);
        if(objectToSync != null)
        {
            objectFound = true;
        }
        
    }



    // Update is called once per frame
    void Update()
    {

        if (objectToSync == null)
        {
            objectToSync = GameObject.FindGameObjectWithTag(tagOfPositionToSync);
            if(objectToSync != null)
            {
                objectFound = true;
            }          
        }
        if (objectToSync != null && this.photonView.IsMine)
        {
            this.transform.position = objectToSync.transform.position;
            this.transform.rotation = objectToSync.transform.rotation;
        }
        else
        {
            Debug.Log("Object to sync with tag " + tagOfPositionToSync + " could not be found");
        }
        
    }
}
