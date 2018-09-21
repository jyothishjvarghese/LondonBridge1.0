using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int jumpForce;

    public GameObject fireSwingPrefab;

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
        Movement();
        if (Input.GetKeyDown(KeyCode.L) && isGrounded == true)
        {
            _anim.Swing();
            Instantiate(fireSwingPrefab, transform.position, Quaternion.identity);
}
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

   
}
