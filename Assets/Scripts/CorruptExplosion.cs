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

    private List<Collider2D> hitlist = new List<Collider2D>();
    void Awake()
    {
        remainingLifetime = lifetime;
        AudioManager.Instance.Explosion();
    }

    // Update is called once per frame
    void Update()
    {
        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime <= 0)
        {
            enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hitlist.Contains(collision))
        {
            hitlist.Add(collision);
            Enemy enemy;
            if (collision.CompareTag("Player"))
            {
                GameState.Instance.DamagePlayer(damage);
            }
            else if (collision.TryGetComponent(out enemy))
            {
                enemy.Die();
            }
        }
    }
}
