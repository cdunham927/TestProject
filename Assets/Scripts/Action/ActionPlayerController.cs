using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPlayerController : MonoBehaviour, IDamageable<float>, IKillable
{
    [Header("Stats")]
    public float maxHp;
    float hp;
    public float lerpSpd = 10f;
    public Image healthUI;
    public Text moneyText;
    public Text expText;
    public float startMoney;
    float curMoney;
    public float money;
    public float atk;
    public float def;

    [Range(0, 1)]
    public float chanceToAtk;
    [Range(0, 1)]
    public float chanceToDef;
    [Range(0, 1)]
    public float chanceToSpd;
    [Range(0, 1)]
    public float chanceToHp;

    //Experience
    [Header("Experience/Leveling Up")]
    public int level = 1;
    public float experience;
    public float expToNext;
    public AnimationCurve expCurve = new AnimationCurve();
    bool statsAdded = false;

    //Movement
    [Header("Movement")]
    Vector2 input;
    Rigidbody2D bod;
    public float spd;
    public bool rotate = false;

    Animator anim;
    SpriteRenderer rend;
    //0 = down, 1 = sideways, 2 = up
    int lookDir = 0;
    bool moving = false;
    public GameObject meleeObj;

    public float iframeTime;
    float iframes;
    
    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        expToNext = CalculateExp(level);

        for (int i = 1; i < 30; i++)
        {
            expCurve.AddKey(i, CalculateExp(i));
        }
    }

    private void OnEnable()
    {
        hp = maxHp;
        money = startMoney;
    }

    public void Damage(float amt)
    {
        if (iframes <= 0)
        {
            hp -= amt;
            if (hp <= 0) Die();
            iframes = iframeTime;
        }
    }

    public float CalculateExp(int lvl)
    {
        float expNeeded = 0;

        //expNeeded = lvl * 100f;
        expNeeded = Mathf.RoundToInt(Mathf.Pow(8 * (lvl + 1), 1.6f));

        return expNeeded;
    }

    public void Heal(float amt)
    {
        float newHp = hp + amt;
        if (newHp > maxHp) newHp = maxHp;
        hp = newHp;
    }

    public void AddExp(float amt)
    {
        experience += amt;

        if (experience >= expToNext)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        experience -= expToNext;

        if (!statsAdded)
        {
            if (Random.value <= chanceToAtk)
            {
                atk++;
                statsAdded = true;
            }
            if (Random.value <= chanceToDef)
            {
                def++;
                statsAdded = true;
            }
            if (Random.value <= chanceToSpd)
            {
                spd += 200f;
                statsAdded = true;
            }
            if(Random.value <= chanceToHp)
            {
                maxHp += 10;
                statsAdded = true;
            }

            if (!statsAdded)
            {
                int r = Random.Range(0, 4);
                if (r == 0) atk++;
                else if (r == 1) def++;
                else if (r == 2) spd += 200f;
                else maxHp += 10;

                statsAdded = true;
            }
        }

        Invoke("ResetAdded", 0.2f);
        Heal(9999);
        expToNext = CalculateExp(level);
    }

    void ResetAdded()
    {
        statsAdded = false;
    }

    public void AddMoney(float amt)
    {
        money += amt;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        healthUI.fillAmount = Mathf.Lerp(healthUI.fillAmount, hp / maxHp, lerpSpd * Time.deltaTime);
        curMoney = Mathf.RoundToInt(Mathf.Lerp(curMoney, money, Time.deltaTime * lerpSpd));
        moneyText.text = "x" + curMoney.ToString();
        expText.text = "Level " + level.ToString() + "     Exp: " + experience.ToString() + "/" + expToNext.ToString();

        if (rotate)
        {
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * lerpSpd);
        }

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.y != 0)
        {
            bod.AddForce(Vector2.up * spd * input.y * Time.deltaTime);
        }
        if (input.x != 0)
        {
            bod.AddForce(Vector2.right * spd * input.x * Time.deltaTime);
        }

        moving = (input.x != 0 || input.y != 0);

        if (input.y < 0)
        {
            lookDir = 0;
            meleeObj.transform.localPosition = new Vector3(0, -1, 0);
            meleeObj.transform.localScale = new Vector3(1.25f, 1, 1);
        }
        else if (input.x > 0)
        {
            lookDir = 1;
            meleeObj.transform.localPosition = new Vector3(1, 0, 0);
            meleeObj.transform.localScale = new Vector3(1, 1.25f, 1);
        }
        else if (input.x < 0)
        {
            lookDir = 1;
            meleeObj.transform.localPosition = new Vector3(-1, 0, 0);
            meleeObj.transform.localScale = new Vector3(1, 1.25f, 1);
        }
        else if (input.y > 0)
        {
            lookDir = 2;
            meleeObj.transform.localPosition = new Vector3(0, 1, 0);
            meleeObj.transform.localScale = new Vector3(1.25f, 1, 1);
        }

        if (input.x < 0)
        {
            rend.flipX = true;
        }
        else if (input.x > 0)
        {
            rend.flipX = false;
        }

        anim.SetInteger("dir", lookDir);
        anim.SetBool("moving", moving);
        if (iframes > 0) iframes -= Time.deltaTime;

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Damage(15f);
            }

            if (Input.GetKey(KeyCode.M))
            {
                AddMoney(10f);
            }

            if (Input.GetKey(KeyCode.Alpha9))
            {
                AddExp(5);
            }
        }
    }

    public void SwingAttack()
    {
        anim.SetBool("attacking", true);
        Invoke("ResetAttack", 0.1f);
    }

    void ResetAttack()
    {
        anim.SetBool("attacking", false);
    }
}
