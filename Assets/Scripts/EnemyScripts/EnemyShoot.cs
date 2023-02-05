using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    private TargetStyle _targetStyle = TargetStyle.Forward;
    [SerializeField]
    private float _bulletSpeed = 5;
    [SerializeField]
    private float _cooldownTime = 3;
    public GameObject bullet;

    [SerializeField]
    private float _timer = 0;
    private Vector2 _bulletVelocity;

    

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
        GameObject firedBullet;
        switch (_targetStyle)
        {
            case TargetStyle.Player:
                Vector2 playerPos = GetPlayerPosition();
                Vector2 thisPos = transform.position;
                float _targetAngle = Mathf.Atan2(playerPos.y - thisPos.y, playerPos.x - thisPos.x) * Mathf.Rad2Deg - 90;
                //_bulletVelocity = (thisPos - playerPos).normalized;
                firedBullet = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, _targetAngle));
                firedBullet.GetComponent<Rigidbody2D>().velocity = firedBullet.transform.up * _bulletSpeed;
                break;
            case TargetStyle.Forward:
                // TODO Figure out how the direction you're facing actually works
                Quaternion facingDir = transform.rotation;
                firedBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
                firedBullet.GetComponent<Rigidbody2D>().velocity = firedBullet.transform.up;
                break;
        }
    }

    Vector2 GetPlayerPosition()
    {
        return new Vector2();
    }
}

enum TargetStyle
{
    Player, Forward
}