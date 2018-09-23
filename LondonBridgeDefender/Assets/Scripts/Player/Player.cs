using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int jumpForce;

    public int score;
    public GameObject fireSwingPrefab;

    private Rigidbody2D _rigid;
    private PlayerAnimation _anim;

    private bool isDead;
    private bool isGrounded;
    private bool _canDamage = true;
    private bool resetJumpNeeded;
    public int Health { get; set; }
    [SerializeField]
    protected int health;
    

    public void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        Health = health;
    }

    public void Update()
    {
        if (isDead == false)
        {
            Movement();
        }
        if (Input.GetKeyDown(KeyCode.L) && isGrounded == true)
        {
            _anim.Swing();
            Instantiate(fireSwingPrefab, transform.position, Quaternion.identity);
        }
    }
    public void Damage() { }
    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            if(health > 1f)
            {
                if (_canDamage == true)
                {
                    TakeDamage(col.transform);
                    _canDamage = false;
                    StartCoroutine(ResetDamage());
                }
                
            }
            else
            {
                _anim.Death();
                isDead = true;
                StartCoroutine(DestroyObject());
            }
        }

    }

    void TakeDamage(Transform enemy)
    {
        health--;
        UIManager.Instance.UpdateLives(health);
    }
    public void Movement()
    {
        float transition = Input.GetAxis("Horizontal");
        _rigid.velocity = new Vector2(transition * speed, _rigid.velocity.y);
        _anim.Run(transition);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.green);

        if (hitInfo.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                isGrounded = true;

            }
            else
                isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            _rigid.velocity = new Vector2(transition * speed, jumpForce);
            isGrounded = false;
            resetJumpNeeded = true;
            _anim.Jump();
            StartCoroutine(ResetJumpRoutine());

        }

    }
    IEnumerator ResetJumpRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
       
    }
    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
    public void AddScore()
    {
        UIManager.Instance.UpdateScore();
    }

}
