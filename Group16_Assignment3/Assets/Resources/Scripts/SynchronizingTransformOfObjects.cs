using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizingTransformOfObjects : MonoBehaviour
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
        this.transform.position = objectToSync.transform.position;
        this.transform.rotation = objectToSync.transform.rotation;
    }
}
