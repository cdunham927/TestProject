using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformerPlayerController : MonoBehaviour
{
    public float maxHp;
    float hp;
    float iframes;
    public float spd;
    Rigidbody2D bod;
    float inputX;
    Animator anim;
    bool canJump;
    public LayerMask wallLayer;
    public float rayLength;
    public float jumpHeight;
    bool hurt;
    SpriteRenderer rend;
    public Image healthImage;
    public Text moneyText;
    int money;

    // Start is called before the first frame update
    void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, wallLayer);

        inputX = Input.GetAxisRaw("Horizontal");

        if (inputX != 0)
        {
            bod.AddForce(Vector2.right * inputX * spd * Time.deltaTime);
        }

        if (hit.collider != null)
        {
            canJump = true;
        }

        if (canJump && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
            bod.AddForce(Vector2.up * jumpHeight);
            canJump = false;
        }
        Debug.DrawRay(transform.position, Vector2.down * rayLength);

        if (iframes > 0) iframes -= Time.deltaTime;

        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, hp / maxHp, Time.deltaTime * 10f);
        moneyText.text = "x" + money.ToString();

        rend.flipX = (inputX < 0);
        anim.SetBool("moving", inputX != 0);
        anim.SetBool("canJump", canJump);
        anim.SetBool("hurt", hurt);
    }

    public void Damage(float amt)
    {
        if (iframes <= 0)
        {
            hp -= amt;
            hurt = true;
            Invoke("ResetHurt", 0.1f);

            if (hp <= 0) Die();

            iframes = 0.3f;
        }
    }

    void ResetHurt()
    {
        hurt = false;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && bod.velocity.y < 0)
        {
            float boundsY = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            if (transform.position.y > collision.transform.position.y + boundsY)
            {
                //bod.AddForce(Vector2.up * jumpHeight / 2f);
                bod.AddForceAtPosition(-bod.velocity.normalized * jumpHeight / 2f, bod.position);
                collision.gameObject.GetComponent<EnemyController>().Damage(20f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            money++;
            collision.gameObject.SetActive(false);
        }
    }
}
