using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float rotateSpeed;
    public float movementSpeed;
    public float jumpAmount;

    // Start is called before the first frame update
    void Start()
    {
        setData();
    }
    
    //sets the data when the player starts
    public void setData()
    {   
        rb = GetComponent<Rigidbody>();
        rotateSpeed = 3.0F;
        movementSpeed = 3.0F;
        jumpAmount = 100.0F;
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
            //Player moves relative to the direction they are facing
            if (Input.GetKey(KeyCode.A))
                transform.position -= transform.right * Time.deltaTime * movementSpeed;
            if (Input.GetKey(KeyCode.D))
                transform.position += transform.right * Time.deltaTime * movementSpeed;
            if (Input.GetKey(KeyCode.W))
                transform.position += transform.forward * Time.deltaTime * movementSpeed;
            if (Input.GetKey(KeyCode.S))
                transform.position -= transform.forward * Time.deltaTime * movementSpeed;

            //Player rotates left/right
            if (Input.GetKey(KeyCode.Q))
                transform.Rotate(0, -1 * rotateSpeed, 0);
            if (Input.GetKey(KeyCode.E))
                transform.Rotate(0, 1 * rotateSpeed, 0);

            //Player jumps
            if (Input.GetKey(KeyCode.Space))
                transform.position += transform.up * Time.deltaTime * movementSpeed;

            //Player sprints
            if (Input.GetKeyDown(KeyCode.LeftShift))
                movementSpeed = 6.0f;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                movementSpeed = 3.0f;
            
        }
    }
}
