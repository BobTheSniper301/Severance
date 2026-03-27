using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : AiBehaviourScript
{
    public List<GameObject> SuperVisors = new List<GameObject>();
    GameObject closeSupervisor;

    Vector3 desk;
    public bool ratting = false;
    bool working = false;

    private void Awake()
    {
        playerSeen = false;
        sleepState = true;
    }

    protected override void Start()
    {
        desk = transform.position;

        base.Start();

        Work();

        foreach (GameObject AI in GameObject.FindGameObjectsWithTag("Supervisor"))
        {
            SuperVisors.Add(AI);
        }
    }

    protected override void Update()
    {
        base.Update();

        //if (!working)
        if (ratting)
        {
            agent.SetDestination(FindSupervisor().transform.position);
            if (Vector3.Distance(transform.position, closeSupervisor.transform.position) < 2)
            {
                closeSupervisor.GetComponent<SupervisorScript>().KnowPlayerLocation();
                ratting = false;
                Work();
            }
        }
        else if (playerSeen) Rat();
        else if (!moving && !ratting && !working) Work();
        else if (!moving && !ratting && working && Vector3.Distance(desk, transform.position) < 1) sleepState = true;
    }

    public void Rat()
    {
        Debug.Log("rat");
        ratting = true;
        working = false;
        agent.updateRotation = true;
        lookTime = 0;
        chasing = true;
        agent.SetDestination(FindSupervisor().transform.position);
        moving = true;
    }

    public GameObject FindSupervisor()
    {
        foreach (GameObject AI in SuperVisors)
        { 
            if (closeSupervisor == null) closeSupervisor = AI;
            else
            {
                float currentDistance = Vector3.Distance(gameObject.transform.position, closeSupervisor.transform.position);
                float NextDistance = Vector3.Distance(gameObject.transform.position, AI.transform.position);

                if (currentDistance > NextDistance) closeSupervisor = AI;
            }
        }

        return closeSupervisor;
    }

    public void Work()
    {
        Debug.Log("work");

        working = true;
        agent.SetDestination(desk);
    }

    public override void HeardSound(Transform t, int alertLevel)
    {
        sleepState = false;
        working = false;
        
        base.HeardSound(t, alertLevel);
    }
}
