using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour
{
    public NavMeshAgent player;
    public Transform target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetDestination(target.position);
        }
    }
}
