using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float spd;
    Rigidbody2D bod;
    Vector2 input;
    public GameObject ball;
    public float maxHp;
    [SerializeField]
    float hp;
    public Image healthBar;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();

        hp = maxHp;
    }

    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.x != 0)
        {
            bod.AddForce(Vector2.right * spd * input.x * Time.deltaTime);
        }
        if (input.y != 0)
        {
            bod.AddForce(Vector2.up * spd * input.y * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ball, transform.position, Quaternion.identity);
        }

        healthBar.fillAmount = (hp / maxHp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp -= 15;
        }
    }
}
