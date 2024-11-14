using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionWithHandMov : MonoBehaviour
{

    public XRBaseController leftHand;
    public GameObject origin;
    public Transform headTransform;

    private float heightMax = 1.5f;
    private float heightMin = 1.2f;

    private bool startUpMovement;
    private bool startDownMovement;
    private bool inMovement;

    float timeForMovement;
    float timeForLastMovement;

    float timeCharacterMoving;


    // Start is called before the first frame update
    void Start()
    {
        startUpMovement = false;
        startDownMovement = false;
        inMovement = false;

        timeForMovement = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHand.transform.position.y < heightMax + 0.1 && leftHand.transform.position.y > heightMax - 0.1)
        {
            //Debug.Log("Y Posititon of left hand controller: " + leftHand.transform.position.y);
            Debug.Log("In High Interval");
            //leftHand.SendHapticImpulse(0.5f, 3);
            if (startDownMovement)
            {
                timeForMovement = 0;
            }
            startDownMovement = true;
            //startUpMovement = false;
            //timeForMovement = 0;
        }

        if (leftHand.transform.position.y < heightMin + 0.1 && leftHand.transform.position.y > heightMin - 0.1)
        {
            //Debug.Log("Y Posititon of left hand controller: " + leftHand.transform.position.y);
            Debug.Log("In Low Interval");
            //leftHand.SendHapticImpulse(0.5f, 3);
            if (startUpMovement)
            {
                timeForMovement = 0;
            }
            startUpMovement = true;
            //startDownMovement = false;
            //timeForMovement = 0;
        }

        if (startUpMovement == true)
        {
            timeForMovement += Time.deltaTime;
            if (timeForMovement > 3)
            {
                startUpMovement = false;
                timeForMovement = 0;
            }
            if (leftHand.transform.position.y > heightMax - 0.1)
            {
                startUpMovement = false;
                Debug.Log("Correct Up Movement, Move one step foward");
                Vector3 stepDirection = headTransform.forward;
                stepDirection = new Vector3(stepDirection.x, 0, stepDirection.z);
                stepDirection = stepDirection.normalized;
                //origin.transform.position = origin.transform.position + stepDirection/3;
                timeForLastMovement = timeForMovement;
                timeForMovement = 0;
                Debug.Log("Camera viewing Direction: " + headTransform.forward);
            }
        }

        if (startDownMovement == true)
        {
            timeForMovement += Time.deltaTime;
            if (timeForMovement > 3)
            {
                startDownMovement = false;
                timeForMovement = 0;
            }
            if (leftHand.transform.position.y < heightMin + 0.1)
            {
                startDownMovement = false;
                Debug.Log("Correct Down Movement, Move one step foward");
                Vector3 stepDirection = headTransform.forward;
                stepDirection = new Vector3(stepDirection.x, 0, stepDirection.z);
                stepDirection = stepDirection.normalized;
                //origin.transform.position = origin.transform.position + stepDirection/3;
                timeForLastMovement = timeForMovement;
                timeForMovement = 0;
                Debug.Log("Camera viewing Direction: " + headTransform.forward);
            }
        }

        if (timeCharacterMoving < timeForLastMovement)
        {
            Vector3 stepDirection = headTransform.forward;
            stepDirection = new Vector3(stepDirection.x, 0, stepDirection.z);
            stepDirection = stepDirection.normalized;
            timeCharacterMoving += Time.deltaTime;
            origin.transform.position = origin.transform.position + stepDirection * Time.deltaTime / (timeForLastMovement * 2);
        }
        else
        {
            timeCharacterMoving = 0;
            timeForLastMovement = 0;
        }


    }
}