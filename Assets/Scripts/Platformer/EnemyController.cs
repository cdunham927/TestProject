using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float maxHp;
    protected float hp;
    public float spd;
    public float runSpd;
    public Image healthImage;
    public float chaseRange;
    public float attackRange;
    public enum enemystates { move, chase, attack }
    public enemystates currentState = enemystates.move;

    protected Rigidbody2D bod;
    public LayerMask wallLayer;
    public float rayLength;
    public int direction;
    protected SpriteRenderer rend;
    protected float distance;
    protected PlatformerPlayerController player;
    protected Animator anim;

    public float timeBetweenAttacks;
    protected float attackCools;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlatformerPlayerController>();
    }

    private void OnEnable()
    {
        hp = maxHp;
        direction = (Random.value > 0.5f) ? 1 : -1;
    }

    private void LateUpdate()
    {
        healthImage.gameObject.transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        rend.flipX = (direction == -1);

        switch(currentState)
        {
            case enemystates.move:
                Move();
                break;
            case enemystates.chase:
                Chase();
                break;
            case enemystates.attack:
                Attack();
                break;
        }

        if (attackCools > 0) attackCools -= Time.deltaTime;

        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, hp / maxHp, 10 * Time.deltaTime);

        Debug.DrawRay(transform.position, Vector2.right * direction * rayLength);
        Debug.DrawRay(transform.position, (Vector2.right * direction) - Vector2.up * rayLength);

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                direction *= -1;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Damage(5f);
            }
        }
    }

    public virtual void Move() { }
    public virtual void Chase() { }
    public virtual void Attack() { }
    public virtual void Damage(float amt) { }
    public virtual void Die() { }
}
