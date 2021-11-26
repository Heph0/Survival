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
    private string animationPath = "Animation/MetalonBasicMotions/AnimationControllers/BasicMotions@";

    private float dist;
    private float movementSpeed;

    private GameObject player;

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
        movementSpeed = 3.0F;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().isAlive && !player.GetComponent<Player>().isVictory)
        {
            bool playerDetected = FindTarget("Player").name == "Player" && dist < 100F;

            if (playerDetected)
                heading = FindTarget("Player"); //prioritize player
            else
            {
                if (dest == "") //no programmed destination
                {   
                    //find the closest territory marker
                    float distance = Mathf.Infinity;
                    GameObject[] areas = GameObject.FindGameObjectsWithTag("patrolArea");
                    Vector3 position = transform.position;
                    foreach (GameObject area in areas)
                    {
                        if (area != null)
                        {
                            Vector3 diff = area.transform.position - position;
                            float currDistance = diff.sqrMagnitude;
                            if (currDistance < distance)
                            {
                                dest = area.name;
                                distance = currDistance;
                                dist = distance;
                            }
                        }
                    }
                    if (distance < 3F)  //head to the next area 
                    {
                        int areaNum = int.Parse(dest.Substring(dest.Length - 1));
                        if (areaNum == 3)
                            areaNum = 0;
                        else 
                            areaNum++;
                        dest = "MetalonRedArea" + areaNum;
                    }
                    heading = FindTarget(dest);
                }
                else
                //patrolling around a area
                {
                    FindTarget(dest);
                    if (dist < 3F)  //head to the next area 
                    {
                        int areaNum = int.Parse(dest.Substring(dest.Length - 1));
                        if (areaNum == 3)
                            areaNum = 0;
                        else 
                            areaNum++;
                        dest = "MetalonRedArea" + areaNum;
                    }
                    heading = FindTarget(dest);
                }
            }
            
            if (heading != null)
            {
                Move(heading);
            }
        }
        else
            movementSpeed = 0F;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && !player.GetComponent<Player>().isVictory)
            animator.runtimeAnimatorController = Resources.Load(animationPath + "StabAttack") as RuntimeAnimatorController;
    }

    public void Move(GameObject heading)
    {
        if (dist > heading.transform.localScale.x + 0.5f)
        {
            //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
            Vector3 headingDirection = heading.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(headingDirection, Vector3.up);
            //https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
            transform.position = Vector3.MoveTowards(transform.position, heading.transform.position, movementSpeed * Time.deltaTime);
        }
        animator.runtimeAnimatorController = Resources.Load(animationPath + "WalkForwards") as RuntimeAnimatorController;
    }

    public GameObject FindTarget(string destination)
    {
        GameObject target = null;
        GameObject heading = null;
        Vector3 position = transform.position;
        
        target = GameObject.Find(destination);
        if (target != null)
        {
            Vector3 diff = target.transform.position - position;
            float currDistance = diff.sqrMagnitude;
            dist = currDistance;
            heading = target;
        }
        return heading;
    }
}
