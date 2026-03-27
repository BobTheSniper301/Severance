using UnityEngine;

public class SupervisorScript : AiBehaviourScript
{
    protected override void Start()
    {
        base.Start();

        agent.SetDestination(patrols[currentPoint].position);
        moving = true;
    }

    protected override void Update()
    {
        base.Update();

        if (Vector3.Distance(player.transform.position, transform.position) < 2) PlayerScript.instance.Die();

        if (playerSeen) InChase();
        else if (!moving) OnPatrol();
    }

    public void KnowPlayerLocation()
    {
        Debug.Log("rat");

        InChase();
    }
}
