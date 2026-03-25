using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public abstract class AiBehaviourScript : MonoBehaviour
{
    protected NavMeshAgent agent;
    public GameObject player;

    public GameObject patrol;
    public List<Transform> patrols = new List<Transform>();
    public int currentPoint;

    protected float moveTime;
    protected float vulnerableTimer;
    protected float lookTime;

    //States
    public bool sleepState = false;
    public bool isVulnerable = true;
    //public bool patroling = true;
    public bool chasing = false;

    //actions
    protected bool moving = false;
    public bool playerSeen = false;
    protected bool lookLeft = true;
    protected bool lookRight = false;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");

        if (patrol.GetComponentInChildren<Transform>() != null) {
            foreach (Transform t in patrol.GetComponentsInChildren<Transform>())
            {
                if (t.gameObject.name != "Patrol")
                {
                    patrols.Add(t);
                    //Debug.Log("append");
                }
            }
        }

        lookTime = 40;
    }

    protected virtual void Update()
    {
        moveTime += Time.deltaTime;
        lookTime += Time.deltaTime;
        vulnerableTimer += Time.deltaTime;

        if (agent.velocity.magnitude < 0.10f && moveTime > 3)
        {
            moveTime = 0;
            moving = false;
        }
        if (playerSeen)
        {
            isVulnerable = false;
            vulnerableTimer = 0;
        }
        if (vulnerableTimer > 2)
        {
            isVulnerable = true;
        }
    }

    protected virtual void OnPatrol()
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

    protected virtual void InChase()
    {
        agent.updateRotation = true;
        lookTime = 0;
        chasing = true;
        agent.SetDestination(player.transform.position);
        moving = true;
    }

    protected virtual void Die()
    {

    }

    public abstract void HeardSound(Transform t, int alertLevel);
}
