using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ManagerScript : AiBehaviourScript
{
    public GameObject keycard;

    protected override void Start()
    {
        base.Start();

        agent.SetDestination(patrols[currentPoint].position);
        moving = true;
    }

    protected override void Update()
    {
        base.Update();

        if (playerSeen) InChase();
        else if (!moving) OnPatrol();
    }

    protected override void InChase()
    {
        agent.updateRotation = true;
        lookTime = 0;
        chasing = true;
        Debug.Log(FindPoint(player.transform.position, patrols).position);
        agent.SetDestination(FindPoint(player.transform.position, patrols).position);
        moving = true;
    }

    public override void HeardSound(Transform t, int alertLevel)
    {
        ItemInventoryManager.instance.SpawnObject(keycard, this.transform.position);
        Destroy(this.gameObject);
    }

    public Transform FindPoint(Vector3 playerPosition, List<Transform> patrol)
    {
        Transform bestPoint = null;

        foreach (Transform t in patrol) 
        {
            if (bestPoint == null)
            {
                bestPoint = t;
            }
            else
            {
                float currentDistance = Vector3.Distance(playerPosition, bestPoint.position);
                float nextDistance = Vector3.Distance(playerPosition, t.position);
                if (currentDistance < nextDistance) bestPoint = t;
            }
        }

        return bestPoint;
    }
}
