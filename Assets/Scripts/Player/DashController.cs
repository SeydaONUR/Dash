using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [Header("Simple Movement")]
    public Rigidbody2D rb;
    public float moveSpeed;
    public Transform groundPoint;
    public LayerMask ground;
    public float jumpForce;
    private bool onGround;

    [Header("Dash")]
    private bool canDash;
    private bool isDashing;
    [SerializeField] private float dashPower;
    public float dashTime;
    private int counter;
    public TrailRenderer tr;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        simpleMovement();
        Flip();
        onGround = Physics2D.OverlapCircle(groundPoint.position, .2f, ground);
        if (!onGround)
        {
            canDash = true; // you can dash
        }
        else
        {
            canDash = false;
            isDashing = false;
            counter = 0;
        }
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            
        }
        if (canDash==true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
            
        }
        
    }
    public void simpleMovement()
    {
        if (isDashing==false)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*moveSpeed,rb.velocity.y);
            rb.gravityScale = 1f;
        }
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x,jumpForce);
    }
    public void Dash()
    {
        isDashing = true;
        if (isDashing && counter <1)  // dash can be once
        {
            if (rb.velocity.x!=0)
            {
                rb.velocity = new Vector2(transform.localScale.x * dashPower, Input.GetAxisRaw("Vertical") * dashPower * .3f);
                counter++;

            }else if (rb.velocity.x ==0)
            {
                rb.velocity = new Vector2(0f, Input.GetAxisRaw("Vertical") * dashPower * .3f);
                counter++;

            }
            tr.emitting = true;
            rb.gravityScale = 0f;
            Invoke("cancelDash",dashTime); // call cancelDash when dashTime end
            
        }
        

    }
    public void cancelDash()
    {
        isDashing = false;
        tr.emitting = false;
        rb.gravityScale = 1f; // original  gravity
    }
    public void Flip()
    {
        if (rb.velocity.x> 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (rb.velocity.x<0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
}
