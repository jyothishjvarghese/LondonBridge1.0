using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int jumpForce;


    private Rigidbody2D _rigid;
    private PlayerAnimation _anim;

    private bool isGrounded;
    private bool resetJumpNeeded;

    public void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
    }

    public void Update()
    {
        float transition = Input.GetAxis("Horizontal");
        _rigid.velocity = new Vector2(transition * speed, _rigid.velocity.y);
        _anim.Run(transition);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);

        if(hitInfo != null)
        {
            isGrounded = true;
            resetJumpNeeded = true;

            if (resetJumpNeeded == false && isGrounded == true)
            {
                _rigid.velocity = new Vector2(transition * speed, jumpForce);
                isGrounded = false; 
            }
            StartCoroutine(ResetJumpRoutine());


        }
        
    }
    IEnumerator ResetJumpRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        resetJumpNeeded = false;
    }
    
}
