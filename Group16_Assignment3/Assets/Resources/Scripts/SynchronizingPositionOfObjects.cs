using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizingTransformOfObjects : MonoBehaviour
{
    public Transform transformToSynchronize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = transformToSynchronize.position;
        this.transform.rotation = transformToSynchronize.rotation;

        
    }
}
