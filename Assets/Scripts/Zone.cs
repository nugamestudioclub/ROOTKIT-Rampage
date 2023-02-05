using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private readonly HashSet<Enemy> enemies = new HashSet<Enemy>();

    void Start()
    {
        StartCoroutine(DoStart());
    }

    private IEnumerator DoStart()
    {
        yield return new WaitForSeconds(1.5f);
        PutEnemiesToSleep(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Player"))
            PutEnemiesToSleep(false);
        else if (obj.TryGetComponent<Enemy>(out var enemy))
            enemies.Add(enemy);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Player"))
            PutEnemiesToSleep(true);
    }

    private void PutEnemiesToSleep(bool value)
    {
        foreach (var enemy in enemies)
            enemy.Asleep = value;
    }
}