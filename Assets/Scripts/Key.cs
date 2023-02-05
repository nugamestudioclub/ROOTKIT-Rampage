using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
       var obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            GameState.Instance.CollectKey();
            Destroy(gameObject);
        }
    }
}