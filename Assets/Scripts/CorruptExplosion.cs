using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptExplosion : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float lifetime = 1.5f;
    private float remainingLifetime;

    private List<Collider2D> hitlist;
    void Awake()
    {
        remainingLifetime = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hitlist.Contains(collision))
        {
            hitlist.Add(collision);
            if (collision.CompareTag("Player"))
            {
                GameState.Instance.DamagePlayer(damage);
            }
        }
    }
}
