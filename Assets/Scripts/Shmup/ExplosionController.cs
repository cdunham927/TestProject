using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AnimationClip clip;
    public float animationSpd;

    private void OnEnable()
    {
        Invoke("Disable", clip.length / animationSpd);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
