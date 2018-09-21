using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Run(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
        Flip(move);
    }
    private void Flip(float move)
    {
        if (move < 0)
            _sprite.flipX = true;
        else if (move > 0)
            _sprite.flipX = false;
    }
}
