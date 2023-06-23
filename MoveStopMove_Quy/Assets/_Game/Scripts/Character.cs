﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SkinnedMeshRenderer characterMesh;
    [SerializeField] private SkinnedMeshRenderer PantMesh;
    [SerializeField] private Transform weaponTransform;

    [SerializeField] protected GameObject weaponPrefab;
    [SerializeField] protected LayerMask characterLayer;
    [SerializeField] protected AttackRange attackRange;

    [SerializeField] Transform attachIndicatorPoint;
    public Transform AttachIndicatorPoint => attachIndicatorPoint;

    public List<Material> pantMaterials;

    protected WeaponData weaponData;
    protected bool isMoving = false;
    protected bool isAttacking = false;
    protected bool isDead = false;
    protected float delayAttacktime = 0.333f;
    protected float cooldownAttacktime = 1f;

    protected enum AnimationName
    {
        idle,
        run,
        attack,
        win,
        dead
    }

    protected AnimationName currentAninName = AnimationName.idle;

    protected void Start()
    {
        OnInit();
    }


    protected void ChangeAnim(AnimationName animationName)
    {
        if (currentAninName != animationName)
        {
            anim.ResetTrigger(currentAninName.ToString());
            currentAninName = animationName;
            anim.SetTrigger(currentAninName.ToString());
        }
    }

    public void ChangeSkin(int colorID)
    {
        if (colorID < pantMaterials.Count)
        {
            characterMesh.material = pantMaterials[colorID];
            PantMesh.material = pantMaterials[colorID];
        }
    }
    protected virtual void Attack(GameObject enemy)
    {
        if (!isAttacking && !isDead)
        {
            Vector3 direction = new Vector3(enemy.transform.position.x - transform.position.x, 0f, enemy.transform.position.z - transform.position.z);
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            ChangeAnim(AnimationName.attack);
            isAttacking = true;
            StartCoroutine(SpawnWeapon(delayAttacktime, enemy));
        }
    }

    protected IEnumerator SpawnWeapon(float delayTime, GameObject enemy)
    {
        yield return new WaitForSeconds(delayTime);
        weaponTransform.gameObject.SetActive(false);
        //Direction of weapon
        Vector3 enemyDirection = new Vector3(enemy.transform.position.x - weaponTransform.position.x, 0f, enemy.transform.position.z - weaponTransform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(enemyDirection, Vector3.up);
        targetRotation.eulerAngles = new Vector3(-90f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

        GameObject weaponInstance = Instantiate(weaponPrefab, weaponTransform.position, targetRotation);
        weaponInstance.GetComponent<WeaponController>().setAttackRange(attackRange);
        weaponInstance.GetComponent<WeaponController>().enabled = true;
        weaponInstance.GetComponent<WeaponController>().Attack(enemyDirection);
        StartCoroutine(ResetAttack(cooldownAttacktime));
    }

    protected IEnumerator ResetAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!isDead)
        {
            ChangeAnim(AnimationName.idle);
            weaponTransform.gameObject.SetActive(true);
            isAttacking = false;
        }
    }


    protected virtual void OnInit()
    {
        weaponPrefab.GetComponent<WeaponController>().enabled = false;
        int temp = Random.Range(0, 8);
        ChangeSkin(temp);
    }


    public void OnDeath()
    {
        isDead = true;
        ChangeAnim(AnimationName.dead);
        StartCoroutine(DeactiveSelf());
    }

    private IEnumerator DeactiveSelf()
    {
        yield return new WaitForSeconds(1.2f);
        IndicatorManager.Instance.RemoveIndicator(this);
        Destroy(gameObject);
        BotManager.Instance.getList().Remove(gameObject);
    }
}