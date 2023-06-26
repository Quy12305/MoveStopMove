using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IState
{
    private float timer;
    private float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.Stop();
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.IsHaveTargetInRange())
        {
            enemy.Attack();
        }
        else if (timer > randomTime)
        {
            if(enemy.dead() == false)
            {
                enemy.ChangeState(new PatrolState());
            }
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
