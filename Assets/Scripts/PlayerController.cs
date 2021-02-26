using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float spd;
    Rigidbody2D bod;
    Vector2 input;
    public GameObject ball;
    public int maxHp;
    [SerializeField]
    int hp;
    public Image healthBar;

    public TextMeshProUGUI tmText;
    public Text moneyText;
    public int money;
    GameController cont;
    public float lerpSpd;

    public GameObject healthParent;
    public GameObject hpImage;

    public float iframeTime = 0.3f;
    float iframes;

    public ParticleSystem bloodParticles;
    public int burstAmt;

    public float timeBetweenShots = 0.1f;
    float shootCools;
    public GameObject bulSpawn;
    Animator anim;

    private void Awake()
    {
        money = PlayerPrefs.GetInt("money", 0);
        cont = FindObjectOfType<GameController>();
        bod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        hp = maxHp;
        for (int i = 0; i < hp; i++)
        {
            AddHeart();
        }
    }

    void AddHeart()
    {
        GameObject h = Instantiate(hpImage);
        h.transform.SetParent(healthParent.transform);
    }

    void RemoveHeart()
    {
        Destroy(healthParent.transform.GetChild(0).gameObject);
    }

    void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * lerpSpd);

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.x != 0)
        {
            bod.AddForce(Vector2.right * spd * input.x * Time.deltaTime);
        }
        if (input.y != 0)
        {
            bod.AddForce(Vector2.up * spd * input.y * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space) && shootCools <= 0)
        {
            Shoot();
        }

        if (Input.GetMouseButton(0) && shootCools <= 0)
        {
            ShootSpread();
        }

        if (Input.GetMouseButton(1) && shootCools <= 0)
        {
            SwordAttack();
        }

        healthBar.fillAmount = (hp / maxHp);

        moneyText.text = "x" + money.ToString();
        tmText.text = "x" + money.ToString();

        if (iframes > 0) iframes -= Time.deltaTime;
        if (shootCools > 0) shootCools -= Time.deltaTime;
    }

    public void SwordAttack()
    {
        anim.Play("PlayerAttack");

        shootCools = timeBetweenShots;
    }

    public void Shoot()
    {
        Instantiate(ball, bulSpawn.transform.position, transform.rotation);
        shootCools = timeBetweenShots;
    }

    public void ShootSpread()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(ball, bulSpawn.transform.position, transform.rotation * Quaternion.Euler(0, 0, -20f + (10f * i)));
        }

        shootCools = timeBetweenShots;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (iframes <= 0)
        {
            for (int i = 0; i < dmg; i++)
            {
                hp -= dmg;
                RemoveHeart();

                if (hp <= 0) Die();
            }

            bloodParticles.Emit(burstAmt);
            iframes = iframeTime;
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            money++;
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
