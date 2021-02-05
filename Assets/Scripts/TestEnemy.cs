using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f);
    }
}
