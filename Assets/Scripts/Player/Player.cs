using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public float rotateSpeed;
    public float movementSpeed;
    public float jumpAmount;

    private float walkSpeed;
    private float sprintSpeed;

    private string animationPath = "Animation/BasicMotions/AnimationControllers/BasicMotions@";

    // Start is called before the first frame update
    void Start()
    {
        setData();
    }
    
    //sets the data when the player starts
    public void setData()
    {   
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load(animationPath + "Idle") as RuntimeAnimatorController;
        rb = GetComponent<Rigidbody>();
        walkSpeed = 3.0F;
        sprintSpeed = 6.0F;
        rotateSpeed = 3.0F;
        movementSpeed = walkSpeed;
        jumpAmount = 10.0F;
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
    }
 
    public void Move()
    {
        //player must have transform and rigidbody to move
        if (transform != null && rb != null)
        {
            //Player sprints
            if (Input.GetKeyDown(KeyCode.LeftShift))
                movementSpeed = sprintSpeed;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                movementSpeed = walkSpeed;

            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                if (!Input.GetKey(KeyCode.S))
                {
                    if (movementSpeed != sprintSpeed)
                        animator.runtimeAnimatorController = Resources.Load(animationPath+"Run") as RuntimeAnimatorController;
                    else
                        animator.runtimeAnimatorController = Resources.Load(animationPath+"Sprint") as RuntimeAnimatorController;
                }
                else
                {
                    animator.runtimeAnimatorController = Resources.Load(animationPath+"RunBackwards") as RuntimeAnimatorController;
                    movementSpeed = walkSpeed;
                }

                //Player moves relative to the direction they are facing
                if (Input.GetKey(KeyCode.A))
                    transform.position -= transform.right * Time.deltaTime * movementSpeed;
                if (Input.GetKey(KeyCode.D))
                    transform.position += transform.right * Time.deltaTime * movementSpeed;
                if (Input.GetKey(KeyCode.W))
                    transform.position += transform.forward * Time.deltaTime * movementSpeed;
                if (Input.GetKey(KeyCode.S))
                    transform.position -= transform.forward * Time.deltaTime * movementSpeed;
            }

            //Player rotates left/right
            if (Input.GetKey(KeyCode.Q))
                transform.Rotate(0, -1 * rotateSpeed, 0);
            else if (Input.GetKey(KeyCode.E))
                transform.Rotate(0, 1 * rotateSpeed, 0);

            //Player jumps
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.runtimeAnimatorController = Resources.Load(animationPath+"Jump") as RuntimeAnimatorController;
                transform.position += transform.up * Time.deltaTime * movementSpeed;
            }

            //Player idling
            if (!Input.anyKey)
                animator.runtimeAnimatorController = Resources.Load(animationPath+"Idle") as RuntimeAnimatorController;
        }
    }
}
