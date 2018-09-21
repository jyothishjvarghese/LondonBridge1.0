using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    protected Player playerUnit;
    
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected Rigidbody2D rigid;

    private bool _switch;
    [SerializeField]
    private float speed;

    public void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
	void Start ()
    {
        Init();
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
        Movement();
	}

    public void Movement()
    {

        rigid.velocity = new Vector2(transform.localScale.x * speed, rigid.velocity.y);
        
    }
}
