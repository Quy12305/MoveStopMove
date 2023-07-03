using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float radius = 40f;
    [SerializeField] private float speed = 10f;

    private IState currentState;

    public Vector3 targetPos;

    private void Update()
    {
        if (currentState != null && GameManager.Instance.IsState(GameState.GamePlay) )
        {
            currentState.OnExecute(this);
        }
    }

    protected override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void RandomMove()
    {
        if (!isMoving)
        {
            targetPos = FindRandomPosition(transform.position, radius);
            isMoving = true;
        }
        Vector3 direction = new Vector3(targetPos.x - transform.position.x, 0f, targetPos.z - transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
        agent.speed = speed;
        ChangeAnim(AnimationName.run);
        agent.SetDestination(targetPos);
        if(Vector3.Distance(transform.position, targetPos) <= 0.01f)
        {
            isMoving = false;
            ChangeState(new IdleState());
        }
    }

    private Vector3 FindRandomPosition(Vector3 center, float radius)
    {
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center;

            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(randomDirection, out navMeshHit, radius, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(navMeshHit.position, agent.destination, NavMesh.AllAreas, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        return navMeshHit.position;
                    }
                }
            }
        }
    }

    public void Stop()
    {
        ChangeAnim(AnimationName.idle);
    }

    public bool IsHaveTargetInRange()
    {
        GameObject enemy = attackRange.GetComponent<AttackRange>().FindNearestEnemy();
        if(enemy != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Attack()
    {
        GameObject enemy = attackRange.GetComponent<AttackRange>().FindNearestEnemy();
        Attack(enemy);
    }

    public override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }
}
