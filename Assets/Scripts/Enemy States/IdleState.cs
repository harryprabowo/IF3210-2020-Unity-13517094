using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    public void Execute()
    {
        Debug.Log("I'm idling");
    }

    public void Enter(Enemy enemy)
    {

    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }
}
