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

        if (playerSeen) InChase();
        else if (!moving) OnPatrol();
    }

    public override void HeardSound(Transform t, int alertLevel)
    {

    }

    public void KnowPlayerLocation(Vector3 PlayerPosition)
    {
        agent.updateRotation = true;
        lookTime = 0;
        chasing = true;
        agent.SetDestination(PlayerPosition);
        moving = true;
    }
}
