using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : AiBehaviourScript
{
    public List<GameObject> SuperVisors = new List<GameObject>();

    protected override void Start()
    {
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
        else if (!moving) Work();
    }

    public void Rat()
    {

    }

    public GameObject FindSupervisor()
    {
        GameObject closeSupervisor = null;

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
