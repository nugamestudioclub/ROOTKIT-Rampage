using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int playerLayer;
    
    public Sprite sprite0;
    public Sprite sprite1;

    [SerializeField]
    private int damage = 1;
    
    private SpriteRenderer _spriteRenderer;

    private int _spriteMode;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteMode = Random.Range(0, 2);
        if (_spriteMode == 0)
        {
            _spriteRenderer.sprite = sprite0;
        }
        else if (_spriteMode == 1)
        {
            _spriteRenderer.sprite = sprite1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_spriteMode == 0)
        {
            _spriteRenderer.sprite = sprite0;
        }
        else if(_spriteMode == 1)
        {
            _spriteRenderer.sprite = sprite1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            GameState.Instance.DamagePlayer(damage);
        }
        Destroy(gameObject);
    }



}
