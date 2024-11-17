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

    float timeCharacterMoving;

    private float stopMovementTime;

    private float timeUntilSlowDown;

    private float timeSlowDown;

    public Transform mainCamera;

    private bool firstMotion;
    private bool moving; 


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
        stopMovementTime = 0;
        moving = false;
        firstMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        heightMax = mainCamera.position.y * 0.7f;
        heightMin = mainCamera.position.y * 0.55f;

        if (leftHand.transform.position.y > heightMax)
        {
            if (startDownMovement)
            {
                timeForMovement = 0;
            }
            else
            {
                stopMovementTime = 0;
            }
            startDownMovement = true;
        }

        if (leftHand.transform.position.y < heightMin)
        {
            if (startUpMovement)
            {
                timeForMovement = 0;
            }
            else
            {
                stopMovementTime = 0;
            }
            startUpMovement = true;
        }

        if (startUpMovement == true)
        {
            timeForMovement += Time.deltaTime;
            if (timeForMovement > 1.4f)
            {
                startUpMovement = false;
                timeForMovement = 0;
                speed = 0;
                moving = false;
                firstMotion = false;
            }
            if (leftHand.transform.position.y > heightMax)
            {
                if (moving)
                {
                    startUpMovement = false;
                    Debug.Log("Correct Up Movement, Move one step foward");
                    timeForMovement = 0;
                    stopMovementTime = 0;
                    //Debug.Log("Camera viewing Direction: " + headTransform.forward);              
                    speed += 0.3f;                
                    //timeSlowDown = 0;
                    timeUntilSlowDown = 2.1f - speed;
                    Debug.Log(speed);
                }
                else if (!firstMotion)
                {
                    startUpMovement = false;
                    speed = 0;
                    firstMotion = true;
                    Debug.Log("firstMotionTrue up");
                }
                else
                {
                    moving = true;
                    firstMotion = false;
                    Debug.Log("movingTrue up");
                }
            }
        }

        if (startDownMovement == true)
        {
            timeForMovement += Time.deltaTime;
            if (timeForMovement > 1.4f)
            {
                startDownMovement = false;
                timeForMovement = 0;
                speed = 0;
                moving = false;
                firstMotion = false;
            }
            if (leftHand.transform.position.y < heightMin)
            {
                if (moving)
                {
                    startDownMovement = false;
                    Debug.Log("Correct Down Movement, Move one step foward");
                    timeForMovement = 0;
                    //Debug.Log("Camera viewing Direction: " + headTransform.forward);
                    speed += 0.3f;
                    //timeSlowDown = 0;
                    timeUntilSlowDown = 2.1f - speed;
                    Debug.Log(speed);
                }
                else if (!firstMotion) {
                    startDownMovement = false;
                    speed = 0;
                    firstMotion = true;
                    Debug.Log("firstMotionTrue down");
                }
                else
                {
                    moving = true;
                    firstMotion = false;
                    Debug.Log("moving true down");
                }
            }
        }

        if (stopMovementTime >= 1.8f)
        {
            startDownMovement = false;
            startUpMovement = false;
            timeForMovement = 0;
            speed = 0;
            moving = false;
            firstMotion = false;
        }


        if (moving)
        {
            Vector3 stepDirection = headTransform.forward;
            stepDirection = new Vector3(stepDirection.x, 0, stepDirection.z);
            stepDirection = stepDirection.normalized;
            timeCharacterMoving += Time.deltaTime;
            charController.Move(stepDirection * Time.deltaTime * speed);
            
        }
        stopMovementTime += Time.deltaTime;



        if (!charController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0; // Reset gravity effect when grounded
        }

        if (moving)
        {
            charController.Move(velocity * Time.deltaTime);          
        }

        if (speed > 0)
        {
            timeSlowDown += Time.deltaTime;
            if (timeSlowDown >= timeUntilSlowDown)
            {
                speed -= 0.3f;
                timeSlowDown = 0;
                timeUntilSlowDown = 2.1f - speed;
                if(speed <= 0)
                {
                    moving = false;
                    firstMotion = false;
                }
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
        moving = false;
        firstMotion = false;
    }
}