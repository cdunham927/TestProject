using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDGameController : MonoBehaviour
{
    public GameObject tower;
    [HideInInspector]
    public float currentTowerCost;
    public Transform[] waypoints;
    public float maxHp;
    float hp;
    public Image healthImage;
    public float lerpSpd;
    public float money;
    public Text moneyText;

    private void Awake()
    {
        hp = maxHp;
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, hp / maxHp, lerpSpd * Time.deltaTime);

        UpdateTower(100f);
    }

    public void UpdateTower(float amt)
    {
        currentTowerCost = amt;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale *= 2f;
            }
        }

        moneyText.text = "Money: " + money.ToString();
    }

    public void GiveMoney(float amt)
    {
        money += amt;
    }

    public void TakeDamage(float amt)
    {
        hp -= amt;
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, hp / maxHp, lerpSpd * Time.deltaTime);
    }
}
