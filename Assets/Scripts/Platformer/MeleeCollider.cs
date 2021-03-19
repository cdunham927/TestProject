using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    public SpriteRenderer parentRenderer;
    public float atk;
    Vector3 startPos;

    private void Awake()
    {
        //parentRenderer = transform.parent.GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        transform.localScale = new Vector3((parentRenderer.flipX) ? -1 : 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlatformerPlayerController>().Damage(atk);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlatformerPlayerController>().Damage(atk);
        }
    }
}
