using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    // Update is called once per frame
    void Update()
    {
        Vector2 thisPos = this.transform.position;
        Vector2 nextPos = Vector2.MoveTowards(thisPos, GetPlayerPosition(), _speed * Time.deltaTime);
        this.transform.Translate(nextPos - thisPos);
    }

    Vector2 GetPlayerPosition()
    {
        // TODO actually get player position
        return new Vector2();
    }
}
