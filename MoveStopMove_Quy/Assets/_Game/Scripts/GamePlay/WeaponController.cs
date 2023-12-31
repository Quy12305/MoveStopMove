using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timetoLive;
    [SerializeField] AttackRange attackrange;
    [SerializeField] Character character;

    public Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Attack(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
        StartCoroutine(SelfDestroy(timetoLive));
    }

    public IEnumerator SelfDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("characterLayer") && other.GetComponent<Character>() != character)
        {
            Destroy(gameObject);  
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Character>().OnDeath();
            character.changeScale();
            attackrange.ChangeAttacRange();

            if(character.GetComponent<Player>() != null)
            {
                character.GetComponent<Player>().EnemyIsKilled();
            }
        }
    }

    public void setAttackRange(AttackRange atr)
    {
        attackrange = atr;
    }

    public void setCharacter(Character chrt)
    {
        character = chrt;
    }
}