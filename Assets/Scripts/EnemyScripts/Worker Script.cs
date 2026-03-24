using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : AiBehaviourScript
{
    public List<GameObject> SuperVisors = new List<GameObject>();
    GameObject closeSupervisor;

    Vector3 desk;
    Vector3 playerPosition;
    bool ratting = false;

    protected override void Start()
    {
        desk = transform.position;

        base.Start();

        foreach (GameObject AI in GameObject.FindGameObjectsWithTag("Supervisor"))
        {
            SuperVisors.Add(AI);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (playerSeen) Rat();
        else if (ratting)
        {
            if (Vector3.Distance(transform.position, closeSupervisor.transform.position) < 1)
            {
                closeSupervisor.GetComponent<SupervisorScript>().KnowPlayerLocation(playerPosition);
                ratting = false;
            }
        }
        else if (!moving) Work();
    }

    public void Rat()
    {
        agent.updateRotation = true;
        lookTime = 0;
        chasing = true;
        playerPosition = player.transform.position;
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
        
    }

    public override void HeardSound(Transform t, int alertLevel)
    {

    }
}
