using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField]
    private float _cooldownTime = 3;
    [SerializeField]
    private TargetStyle _targetStyle = TargetStyle.Forward;
    public GameObject bullet;

    [SerializeField]
    private float _timer = 0;
    [SerializeField]
    private float _targetAngle = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _cooldownTime)
        {
            Shoot();
            _timer = 0;
        }
    }

    void Shoot()
    {
        switch(_targetStyle)
        {
            case TargetStyle.Player:
                Vector2 playerPos = GetPlayerPosition();
                Vector2 thisPos = transform.position;
                _targetAngle = Mathf.Atan2(playerPos.y - thisPos.y, playerPos.x - thisPos.x) * Mathf.Rad2Deg;
                break;
            case TargetStyle.Forward:
                // TODO Figure out how the direction you're facing actually works
                Quaternion facingDir = transform.rotation;
                _targetAngle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg;
                break;
        }
        GameObject firedBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
    }

    Vector2 GetPlayerPosition()
    {
        // TODO actually get player position
        return new Vector2();
    }
}

enum TargetStyle
{
    Player, Forward
}