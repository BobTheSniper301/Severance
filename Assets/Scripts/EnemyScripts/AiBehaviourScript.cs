using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class AiBehaviourScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;

    public GameObject patrol;
    public List<Transform> patrols = new List<Transform>();
    public int currentPoint;

    private float moveTime;
    private float lookTime;

    //States
    public bool sleepState = false;
    //public bool patroling = true;
    public bool chasing = false;

    //actions
    private bool moving = false;
    public bool playerSeen = false;
    bool lookLeft = true;
    bool lookRight = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        foreach (Transform t in patrol.GetComponentsInChildren<Transform>()) 
        { 
            if (t.gameObject.name != "Patrol")
            {
                patrols.Add(t);
                Debug.Log("append");
            }
        }

        lookTime = 40;
        currentPoint = 0;
        agent.SetDestination(patrols[currentPoint].position);
        moving = true;
    }

    void Update()
    {
        moveTime += Time.deltaTime;
        lookTime += Time.deltaTime;

        if (agent.velocity.magnitude < 0.10f && moveTime > 3)
        {
            moveTime = 0;
            moving = false;
        }

        if (playerSeen) InChase();
        else if (!moving) OnPatrol();
    }

    void OnPatrol()
    {
        if (lookTime > 8)
        {
            chasing = false;
            agent.updateRotation = true;
            currentPoint++;
            if (currentPoint == patrols.Count) currentPoint = 0;
            agent.SetDestination(patrols[currentPoint].position);
            moving = true;
            lookLeft = true;
        }
        else
        {
            agent.updateRotation = false;
            if (lookLeft)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * new Quaternion(0, -35, 0, 1), 1);
                lookLeft = false;
                lookRight = true;
            }
            if (lookRight && lookTime >= 4)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * new Quaternion(0, 35, 0, 1), 1);
                lookRight = false;
            }
        }
    }

    void InChase()
    {
        agent.updateRotation = true;
        lookTime = 0;
        chasing = true;
        agent.SetDestination(player.transform.position);
        moving = true;
    }
}
