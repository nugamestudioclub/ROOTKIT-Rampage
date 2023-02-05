using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5f;
    private float remainingLifetime;

    void Start()
    {
        remainingLifetime = lifetime;
        AudioManager.Instance.Barrier();
    }

    void Update()
    {
        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime <= 0)
        {
            Expire();
        }
    }

    void Expire()
    {
        Destroy(gameObject);
    }
}
