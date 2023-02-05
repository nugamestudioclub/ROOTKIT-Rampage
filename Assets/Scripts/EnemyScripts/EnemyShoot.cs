using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Enemy
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

    // Update is called once per frame
    protected override void EnemyUpdate()
    {
        switch (_targetStyle)
        {
            case TargetStyle.Player:
                TurnToPlayer();
                break;
        }
        _timer += Time.deltaTime;
        if (_timer > _cooldownTime)
        {
            Shoot();
            _timer = 0;
        }
    }

    void Shoot()
    {
        AudioManager.Instance.Balloon();
        GameObject firedBullet;
        switch (_targetStyle)
        {
            case TargetStyle.Player:
                Vector2 playerPos = GetPlayerPosition();
                Vector2 thisPos = transform.position;
                float _targetAngle = Mathf.Atan2(playerPos.y - thisPos.y, playerPos.x - thisPos.x) * Mathf.Rad2Deg - 90;
                firedBullet = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, _targetAngle));
                firedBullet.GetComponent<Rigidbody2D>().velocity = firedBullet.transform.up * _bulletSpeed;
                break;
            case TargetStyle.Forward:
                Quaternion facingDir = transform.rotation;
                firedBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
                firedBullet.GetComponent<Rigidbody2D>().velocity = firedBullet.transform.up * _bulletSpeed;
                break;
        }

    }

    void TurnToPlayer()
    {
        Vector2 thisPos = this.transform.position;
        Vector2 toPlayer = GetPlayerPosition() - thisPos;
        sr.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg - 90);
    }


    Vector2 GetPlayerPosition()
    {
        return GameState.Instance.EnemyTarget.transform.position;
    }
}

enum TargetStyle
{
    Player, Forward
}