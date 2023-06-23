using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private float speed;

    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private void Update()
    {
        if(GameManager.Instance.IsState(GameState.GamePlay))
        {
            if (isDead)
            {
                //LevelManager.Instance.OnFinish();
                return;
            }
            if (Input.GetMouseButton(0))
            {
                Move();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Stop();
                GameObject enemy = attackRange.FindNearestEnemy();
                if (enemy != null)
                {
                    Attack(enemy);
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void Move()
    {
        ChangeAnim(AnimationName.run);
        horizontal = variableJoystick.Horizontal;
        vertical = variableJoystick.Vertical;
        direction = new Vector3(horizontal, 0f, vertical);
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);
        transform.position += speed * Time.deltaTime * direction;
    }

    private void Stop()
    {
        ChangeAnim(AnimationName.idle);
    }

    protected override void OnInit()
    {
        base.OnInit();
    }

}
