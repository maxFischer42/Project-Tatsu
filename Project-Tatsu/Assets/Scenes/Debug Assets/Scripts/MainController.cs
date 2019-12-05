using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody2D rig;
    private SpriteRenderer sprt;

    //debug stuff
    private Color startcolor;
    public Color walljumpColor;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sprt = GetComponent<SpriteRenderer>();
        startcolor = sprt.color;
    }

    // Update is called once per frame
    void Update()
    {
        SetInputs();
        if (direction != Vector2.zero)
            MoveCharacter();
        CheckGround();
        CheckWall();
        Jump();
        GetComponent<SpriteRenderer>().flipX = isFlipped;
    }

    public Vector2 direction;
    public bool jump = false;
    public bool isAI = false;

    private void SetInputs()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Action1");
        direction = new Vector2(x, y);
        if(x > 0)
        {
            isFlipped = false;
        } else if (x < 0)
        {
            isFlipped = true;
        }
    }

    public float groundSpeed = 1.0f;
    public float airSpeed = 1.0f;

    public void MoveCharacter()
    {
        if (!isGrounded && rig.velocity.y > 0)
            return;
        if (isGrounded)
        {
            rig.AddForce(new Vector2(direction.x * groundSpeed, 0f));
        }
        else if (!isGrounded && rig.velocity.y < 0)
        {
            rig.AddForce(new Vector2(direction.x * airSpeed, 0f));
        }
    }

    public float distanceDown = 0.2f;
    public float distanceSide = 0.5f;
    public LayerMask groundCheckLayermask;

    private void CheckGround()
    {
        Vector2 dir = Vector2.down;
        var hit = Physics2D.Raycast(transform.position, dir, distanceDown, groundCheckLayermask);
        if(hit.collider)
        {
            this.isGrounded = true;
            this.walljumpDetect = false;
        }
        else
        {
            this.isGrounded = false;
        }
        
    }

    private void CheckWall()
    {
        Vector2 leftDir = Vector2.left;
        Vector2 rightDir = Vector2.right;
        RaycastHit2D[] rays = new RaycastHit2D[2];
        rays[0] = doRay(leftDir);
        rays[1] = doRay(rightDir);
        if((rays[0].collider || rays[1].collider))
        {
            StartCoroutine(delayWalljump(walljumpColor));
        }
        if(rays[0].collider && !rays[1].collider)
        {
            wallLeft = true;
            wallRight = false;
        } else if (rays[1].collider && !rays[0].collider)
        {
            wallLeft = false;
            wallRight = true;
        }       
    }

    RaycastHit2D doRay(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, distanceSide, groundCheckLayermask);
    }

    public float jumpForce = 25f;
    public float walljumpTimeframe = 0.2f;
    public bool canWalljump = false;
    private bool walljumpDetect = false;
    public float multiplier;
    public float multiplierLimit;
    private float multiplierCurrent;
    public float wallJumpHorizontalMultiplier = 1.5f;
    private Vector2 oldVelocity;
    private bool wallLeft;
    private bool wallRight;

    public bool isFlipped;

    private void Jump()
    {      
        if(this.jump && this.isGrounded || this.jump && this.walljumpDetect)
        {
            if(this.walljumpDetect && multiplierCurrent < multiplierLimit)
            {
                float xDir = 0;
                if(wallRight)
                {
                    xDir = -1;
                }
                else if(wallLeft)
                {
                    xDir = 1;
                }
                rig.velocity = (new Vector2(xDir * wallJumpHorizontalMultiplier, jumpForce + multiplierCurrent));
                multiplierCurrent += (multiplier);
            }
            else
            {
                rig.velocity = (new Vector2(rig.velocity.x, jumpForce + multiplierCurrent));
                multiplierCurrent = multiplier;
                oldVelocity = rig.velocity;
            }
            
            this.walljumpDetect = false;
            this.isGrounded = false;
        }
    }

    IEnumerator delayWalljump(Color c)
    {
        sprt.color = c;
        this.walljumpDetect = true;      
        yield return new WaitForSeconds(walljumpTimeframe);
        sprt.color = startcolor;
        this.walljumpDetect = false;
    }
}
