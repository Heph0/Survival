using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private Vector3 randPos; //territory of the monster
    public GameObject territory; //center of the territory
    private GameObject heading;
    
    private string dest = "";
    private string animationPath = "";

    private float dist;
    private float moveSpeed;

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
        randPos = territory.GetComponent<Transform>().position;
        dist = Mathf.Infinity;
        moveSpeed = 3.0F;
        dest = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        //patrolling around a area
        heading = FindTarget(dest);
        if (heading != null)
        {
            Move(heading);
        }
    }

    public void Move(GameObject heading)
    {
        if (heading.name == "Player" && dist < 100F)
            moveSpeed = 3.0F;
        
        if (dist > heading.transform.localScale.x + 0.5f)
        {
            //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
            Vector3 headingDirection = heading.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(headingDirection, Vector3.up);
            //https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
            transform.position = Vector3.MoveTowards(transform.position, heading.transform.position, moveSpeed * Time.deltaTime);
        }
        else
            moveSpeed = 0.0F;
    }

    public GameObject FindTarget(string destination)
    {
        //https://docs.unity3d.com/ScriptReference/GameObject.Find.html
        GameObject target = null;
        GameObject heading = null;
        Vector3 position = transform.position;
        
        //targets =  
        if (destination == "Player" && dist < 100F)
            target = GameObject.Find(destination);
        else
            target = territory;

        if (target != null)
        {
            Vector3 diff = target.transform.position - position;
            float currDistance = diff.sqrMagnitude;
            dist = currDistance;
            if (destination == "Player" && dist < 100F)  //if the player is too far then patrol in territory
                heading = target;
            else
                heading = territory;
        }
        return heading;
    }
}
