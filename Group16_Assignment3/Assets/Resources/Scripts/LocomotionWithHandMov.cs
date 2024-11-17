using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionWithHandMov : MonoBehaviour
{

    public XRBaseController leftHand;
    public Transform headTransform;

    private float heightMax = 1.5f;
    private float heightMin = 1.2f;

    private bool startUpMovement;
    private bool startDownMovement;
    private bool inMovement;
    private CharacterController charController;
    private Vector3 velocity;
    private float gravity = -9.81f;

    private float speed;

    float timeForMovement;
    float timeForLastMovement;

    float timeCharacterMoving;

    private float timeUntilSlowDown;

    private float timeSlowDown;


    // Start is called before the first frame update
    void Start()
    {
        startUpMovement = false;
        startDownMovement = false;
        inMovement = false;

        timeForMovement = 0;

        velocity = new Vector3(0, 0, 0);

        charController = this.GetComponent<CharacterController>();
        speed = 0;

        timeUntilSlowDown = 0;
        timeSlowDown = 0;
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
                timeForLastMovement = timeForMovement;
                timeForMovement = 0;
                Debug.Log("Camera viewing Direction: " + headTransform.forward);
                speed += 0.3f;
                timeSlowDown = 0;
                timeUntilSlowDown = 3 - speed;
                Debug.Log(speed);
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
                timeForLastMovement = timeForMovement;
                timeForMovement = 0;
                Debug.Log("Camera viewing Direction: " + headTransform.forward);
                speed += 0.4f;
                timeSlowDown = 0;
                timeUntilSlowDown = 2 - speed;
                Debug.Log(speed);
            }
        }

        Vector3 stepDirection = headTransform.forward;
        stepDirection = new Vector3(stepDirection.x, 0, stepDirection.z);
        stepDirection = stepDirection.normalized;
        timeCharacterMoving += Time.deltaTime;
        charController.Move(stepDirection * Time.deltaTime * speed);

        if (!charController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0; // Reset gravity effect when grounded
        }

        charController.Move(velocity * Time.deltaTime);

        if(speed > 0)
        {
            timeSlowDown += Time.deltaTime;
            if(timeSlowDown >= timeUntilSlowDown)
            {
                speed -= 0.4f;
                timeSlowDown = 0;
                timeUntilSlowDown = 2 - speed * 0.4f;
            }
        }
        

    }

    public void resetMoveSpeed()
    {
        speed = 0;
        timeSlowDown = 0;
        timeUntilSlowDown = 0;
        startUpMovement = false;
        startDownMovement = false;
        timeCharacterMoving = 0;
        timeForMovement = 0;
    }
}