using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public Rigidbody2D rb;
    public Vector2 MoveVector;
    public float speed = 2f;
    public float jumpForce = 7f;
    public bool isGrounded;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;
    private bool _jumpControl;
    private int jumpIteration = 0;
    public int jumpValueIteration = 60;
    private float jumpTime = 0;
  



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckingGround();
        Lunge();
        Reflect();
    }

    private void FixedUpdate()
    {
        walk();
        Jump();
    }

    void walk()
    {
        MoveVector.x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(MoveVector.x * speed, rb.velocity.y);

        //rb.AddForce(MoveVector*speed);
    }

    public bool faceRight = true;
    void Reflect()
    {
        if((MoveVector.x > 0 && !faceRight) || (MoveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    private bool jumpControl;
    public float jumpControlTime = 0.7f;

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(7, 8, true);
            Invoke("IgnoreLayerOFF", 0.5f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (isGrounded) { jumpControl = true; }
        }
        else { jumpControl = false; }
        if (jumpControl)
        {
            if ((jumpTime += Time.fixedDeltaTime) < jumpControlTime)
            {
                rb.AddForce(Vector2.up * jumpForce / (jumpTime * 10));
            }
        }
        else { jumpTime = 0; }
    }
    void CheckingGround()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }

    public int lungeImpulce = 5000;
    public float LungeLockeTime = 2f;

    void Lunge()
    {
        if(Input.GetKey(KeyCode.LeftControl) && !lockLunge)
        {
            lockLunge = true;
            Invoke("LungeLock", LungeLockeTime);
            rb.velocity = new Vector2(0, 0);

            if (!faceRight) { rb.AddForce(Vector2.left * lungeImpulce); }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.right * lungeImpulce);

            }
        }

    }

    private bool lockLunge = false;

    void LungeLock()
    {
        lockLunge = false;
    }

    void IgnoreLayerOFF()
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}
