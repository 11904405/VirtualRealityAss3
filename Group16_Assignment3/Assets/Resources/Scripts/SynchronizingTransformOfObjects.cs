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

    void Start()
    {
        objectToSync = GameObject.FindGameObjectWithTag(tagOfPositionToSync);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objectToSync != null && this.photonView.IsMine)
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
