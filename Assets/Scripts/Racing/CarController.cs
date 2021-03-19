using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public int num;
    public float spd;
    public float backSpd;
    Rigidbody2D bod;
    public float rotSpd;
    Vector2 input;
    public string inputXName;
    public string inputYName;
    public GameObject cam;
    int curLap = 1;
    RacingGameController cont;
    //Oil spill
    float cools;
    public float slickTimer;
    public bool slicked = false;
    public float slickRot;
    Vector2 slickDir;
    public float regDrag;
    public float slickDrag;
    float curDrag;
    public float dragLerp;
    //Checkpoint
    public bool hitCheckpoint = false;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
        cont = FindObjectOfType<RacingGameController>();
    }

    private void OnEnable()
    {
        GameObject c = Instantiate(cam, transform.position, Quaternion.identity);
        c.GetComponent<CameraController>().target = transform;
        if (num == 1)
        {
            c.GetComponent<Camera>().rect = new Rect(new Vector2(0f, 0f), new Vector2(0.5f, 1f));
        }
        else
        {
            c.GetComponent<Camera>().rect = new Rect(new Vector2(0.5f, 0f), new Vector2(0.5f, 1f));
        }

        curLap = 0;
        curDrag = regDrag;
    }

    private void Update()
    {
        if (cont.started && !slicked)
        {
            input = new Vector2(Input.GetAxis(inputXName), Input.GetAxis(inputYName));

            if (input.y > 0)
            {
                bod.AddForce(transform.up * input.y * spd * Time.deltaTime);
            }
            if (input.y < 0)
            {
                bod.AddForce(transform.up * input.y * backSpd * Time.deltaTime);
            }

            if (input.x != 0)
            {
                transform.Rotate(0, 0, -rotSpd * Time.deltaTime * input.x);
            }
        }

        if (slicked)
        {
            bod.AddForce(slickDir * backSpd * Time.deltaTime);
            transform.Rotate(0, 0, slickRot * Time.deltaTime);

            if (cools <= 0) slicked = false;
        }

        if (cools > 0)
        {
            cools -= Time.deltaTime;
        }

        curDrag = (slicked) ? slickDrag : regDrag;
        bod.drag = Mathf.Lerp(bod.drag, curDrag, dragLerp * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            if (hitCheckpoint)
            {
                curLap++;

                if (curLap >= cont.laps)
                {
                    cont.EndGame(num);
                }

                hitCheckpoint = false;
            }
        }

        if (collision.CompareTag("Checkpoint"))
        {
            hitCheckpoint = true;
        }

        if (collision.CompareTag("Enemy"))
        {
            slickDir = transform.up;
            cools = slickTimer;
            slicked = true;
        }
    }
}
