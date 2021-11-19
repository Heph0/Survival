using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private float rotateSpeed;
    private float movementSpeed;
    private float jumpAmount;
    private float walkSpeed;
    private float sprintSpeed;
    public bool isGrounded;
    public bool isAlive;
    public bool isVictory;

    private string animationPath = "Animation/HumanoidBasicMotions/AnimationControllers/BasicMotions@";

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
        jumpAmount = 5.0F;
        isAlive = true;
        isVictory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && !isVictory)
            Move();
        else
            movementSpeed = 0F;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayArea")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            isAlive = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "PlayArea")
        {
            isGrounded = false;
        }
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
                string movement0 = movementSpeed != sprintSpeed ? "Walk" : "Run";
                string movement1 = "";
                string movement2 = "";

                //Player moves relative to the direction they are facing
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position -= transform.right * Time.deltaTime * movementSpeed;
                    movement2 = "Left";
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += transform.right * Time.deltaTime * movementSpeed;
                    movement2 = "Right";
                }
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += transform.forward * Time.deltaTime * movementSpeed;
                    movement1 = "Forwards";
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position -= transform.forward * Time.deltaTime * movementSpeed;
                    movement1 = "Backwards";
                }
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    rb.AddForce(transform.up * jumpAmount, ForceMode.Impulse);
                }
                animator.runtimeAnimatorController = Resources.Load(animationPath + movement0 + movement1 + movement2) as RuntimeAnimatorController;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                animator.runtimeAnimatorController = Resources.Load(animationPath+"Jump") as RuntimeAnimatorController;
                rb.AddForce(transform.up * jumpAmount, ForceMode.Impulse);
            }

            //Player rotates left/right
            if (Input.GetKey(KeyCode.Q))
                transform.Rotate(0, -1 * rotateSpeed, 0);
            else if (Input.GetKey(KeyCode.E))
                transform.Rotate(0, 1 * rotateSpeed, 0);

            //Player idling
            if (isGrounded && this.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle01") ||
                    Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                animator.runtimeAnimatorController = Resources.Load(animationPath+"Idle") as RuntimeAnimatorController;
            
        }
    }
}
