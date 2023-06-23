using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private LayerMask characterLayer;
    [SerializeField] private float radius;
    [SerializeField] GameObject cylinder;
    private bool value = true;



    private void Update()
    {
        if (value)
        {
            Cylinder();
            value = false;
        }
    }

    public void Cylinder()
    {
        cylinder.transform.localScale = new Vector3(radius, cylinder.transform.localScale.y, radius);
    }

    public GameObject FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius / 2, characterLayer);
        if (colliders != null)
        {
            GameObject nearestEnemy = null;
            float closestDistance = 100f;
            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance && distance != 0)
                {
                    closestDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }
            return nearestEnemy;
        }
        else
        {
            return null;
        }
    }

    public void ChangeAttacRange()
    {
        if(radius < 12)
        {
            radius += 1f;
        }
        value = true;
    }

}
