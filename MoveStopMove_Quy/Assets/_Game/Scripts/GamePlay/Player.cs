using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private float speed;

    private int isKilled = 0;
    private float horizontal;
    private float vertical;
    private Vector3 direction;
    

    private void Update()
    {
        if(GameManager.Instance.IsState(GameState.GamePlay) && !isDead)
        {

            if (isKilled >= 7)
            {
                isKilled = 0;
                LevelManager.Instance.OnFinish();
            } 

            if (variableJoystick.Horizontal !=0 || variableJoystick.Vertical !=0)
            {
                Move();
            }
            
            else if( !isAttacking )
            {
                ChangeAnim(AnimationName.idle);
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
        //ChangeAnim(AnimationName.run);
        //horizontal = variableJoystick.Horizontal;
        //vertical = variableJoystick.Vertical;
        //direction = new Vector3(horizontal, 0f, vertical);
        //Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);
        //transform.position += speed * Time.deltaTime * direction;


        this.transform.LookAt(new Vector3(this.transform.position.x + variableJoystick.Horizontal * speed * Time.deltaTime, this.transform.position.y, this.transform.position.z + variableJoystick.Vertical * speed * Time.deltaTime));
        ChangeAnim(AnimationName.run);
        this.transform.position = new Vector3(this.transform.position.x + variableJoystick.Horizontal * speed * Time.deltaTime, this.transform.position.y, this.transform.position.z + variableJoystick.Vertical * speed * Time.deltaTime);
        
    }

    protected override void OnInit()
    {
        base.OnInit();
    }

    public void EnemyIsKilled()
    {
        isKilled += 1;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        isKilled = 0;
        LevelManager.Instance.OnLose();
    }
}
