using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private readonly HashSet<Enemy> enemies = new HashSet<Enemy>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Player"))
            foreach (var enemy in enemies)
                enemy.Asleep = false;
        else if (obj.TryGetComponent<Enemy>(out var enemy))
            enemies.Add(enemy);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Player"))
            foreach (var enemy in enemies)
                enemy.Asleep = true;
    }
}