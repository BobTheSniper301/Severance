using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public AiBehaviourScript AiScript;

    public float radius;
    private float increasedAngle;
    [Range(0f, 360f)]
    public float angle;

    public GameObject player;

    public LayerMask targetMask;
    public LayerMask obsticle;

    private void Start()
    {
        increasedAngle = angle * 2;

        player = GameObject.Find("Player");

        StartCoroutine(FovRoutine());
    }

    private void Update()
    {

        if (AiScript.chasing) angle = increasedAngle;
        else angle = increasedAngle / 2;
    }

    //Slows down FOV calculations
    private IEnumerator FovRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (!AiScript.sleepState)
        {
            yield return wait;

            FieldOfViewCheck();
        }
    }

    //Actually checks for player
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0) 
        { 
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTagert = (target.position - transform.position).normalized;


            //Checks view angle
            if (Vector3.Angle(transform.forward, directionToTagert) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //Checks for Obsticle
                if (!Physics.Raycast(transform.position, directionToTagert, distanceToTarget, obsticle)) AiScript.playerSeen = true;
                else AiScript.playerSeen = false;
            }
            else AiScript.playerSeen = false;
        }
        else if (AiScript.playerSeen) AiScript.playerSeen = false;
    }
}
