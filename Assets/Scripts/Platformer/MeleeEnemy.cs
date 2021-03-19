using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    public override void Damage(float amt)
    {
        hp -= amt;

        if (hp <= 0) Die();
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }

    public override void Chase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (transform.position.x > player.transform.position.x) direction = -1;
        else direction = 1;

        if (distance <= attackRange)
        {
            currentState = enemystates.attack;
        }

        if (distance > chaseRange) currentState = enemystates.move;

        bod.AddForce(Vector2.right * direction * runSpd * Time.deltaTime);
    }

    public override void Attack()
    {
        if (attackCools <= 0)
        {
            anim.SetBool("attack", true);
            Invoke("ResetAttack", 0.1f);
            attackCools = timeBetweenAttacks;
        }
        else currentState = enemystates.chase;
    }

    void ResetAttack()
    {
        anim.SetBool("attack", false);
    }

    public override void Move()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, rayLength, wallLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, (Vector2.right * direction) - Vector2.up, rayLength, wallLayer);

        if (hit.collider != null) direction *= -1;
        if (hit2.collider == null) direction *= -1;

        if (distance <= chaseRange)
        {
            currentState = enemystates.chase;
        }

        bod.AddForce(Vector2.right * direction * spd * Time.deltaTime);
    }
}
