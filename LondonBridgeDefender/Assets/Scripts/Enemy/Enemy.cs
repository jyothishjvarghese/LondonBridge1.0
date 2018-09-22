using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {

    protected Player playerUnit;
    
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected Rigidbody2D rigid;
    public int Health { get; set; }
    
    protected int health = 2;

    private bool _canDamage;
    private bool _switch;
    [SerializeField]
    private float speed;
    private float distanceBetweenPlayerAndEnemy;

    public void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Health = health;

    }
	void Start ()
    {
        Init();
        Destroy(this.gameObject, 10.0f);
	}
	
	
	public virtual void Update ()
    {
        Movement();
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            speed = 0;
        }
        
	}

    public void Damage()
    {
        health--;
                
        if(health < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void Movement()
    {

        rigid.velocity = new Vector2(transform.localScale.x * speed, rigid.velocity.y);
        distanceBetweenPlayerAndEnemy = Vector3.Distance(transform.localPosition, playerUnit.transform.localPosition);
        if(distanceBetweenPlayerAndEnemy < 2.0f)
        {
           
            anim.SetBool("Attack", true);
            StopAndAttack();
        }
        else
        {
            anim.SetBool("Attack", false);
        }
        
    }
    void StopAndAttack()
    {
        Vector3 direction = playerUnit.transform.position - transform.position;
        if (direction.x > 0)
        {
            sprite.flipX = false;
            
        }
        else if (direction.x < 0)
        {
            sprite.flipX = true;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if (other.tag == "Player")
        {
            if (_canDamage == true)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamage());
            }
        }
    }
    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(1.0f);
        _canDamage = true;
    }
}
