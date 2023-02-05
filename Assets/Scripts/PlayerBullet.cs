using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Sprite sprite0;
    public Sprite sprite1;
    
    private SpriteRenderer _spriteRenderer;

    private int _spriteMode;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteMode = Random.Range(0, 2);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
