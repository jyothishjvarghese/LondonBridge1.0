using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_effect : MonoBehaviour
{
    private Rigidbody2D _fireSprite;
    private float speed = 10.0f;
    private float transition;
    private bool resetTurn;

    private void Start()
    {
        Destroy(this.gameObject, 3.0f);
        _fireSprite = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (resetTurn == false)
        {
            if (isRight(transition) == false)
                _fireSprite.velocity = new Vector2(speed, 0);
            else if (isRight(transition) == true)
                _fireSprite.velocity = new Vector2(-speed, 0);

            StartCoroutine(ResetTransitionRoutine());
        }

    }

    private bool isRight(float transition)
    {
        transition = Input.GetAxis("Horizontal");
        resetTurn = true;

        if (transition < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
      
    }

    IEnumerator ResetTransitionRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        resetTurn = false;
       
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.tag == "Enemy")
    //    {
            
    //    }
    //}
}
