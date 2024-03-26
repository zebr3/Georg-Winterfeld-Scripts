using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;

    Vector2 rightAttackOffset;

    public Collider2D swordCollider;

    private void Start()
    {
       
        rightAttackOffset = transform.position;
    }

  

    public void AttackRight()
    {
        //print("attack right");
       swordCollider.enabled = true;
       transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        //print("attack left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();

            if(enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}
