using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private float timer;
    public void OnEnter(Enemy enemy)
    {
        enemy.Stop();
        enemy.Attack();
        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(timer > 2.5f)
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
