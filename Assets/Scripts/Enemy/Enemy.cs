using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    private string animationPath = "";

    // Start is called before the first frame update
    void Start()
    {
        setData();
    }
    
    //sets the data when the enemy starts
    public void setData()
    { 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        
    }
}
