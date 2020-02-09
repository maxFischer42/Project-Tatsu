using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prime31;

public class Grappling : MonoBehaviour
{
    //

    public Vector2 ropeDirection;
    private CharacterController2D controller;

    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;

    public bool ropeAttached;

    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    
    public float xLimiter = 4;
    public float yDistance = 7.5f;
    public float maxFuel;
    
    private float currentFuel;
    public float fuelRefreshRate = 0.002f;
    public float fuelDepleteRate = 0.01f;
    
    public Scrollbar fuelUI;
    public Color defUI;
    public Color emptyFuel = Color.red;
    public Image handle;
    private bool empty = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        defUI = fuelUI.colors.normalColor;
        currentFuel = maxFuel;
    }

    private bool distanceSet;

    void Awake()
    {
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    Vector3 GetInputDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (y < 0)
        {
            y = 0;
        }
        x = x / xLimiter;
        return new Vector2(x, y);
    }

    void UpdateUI()
    {
        fuelUI.size = currentFuel / maxFuel;
        if(!ropeAttached)
            currentFuel += fuelRefreshRate;
        if(currentFuel > maxFuel)
        {
            empty = false;
            isActive = true;
            currentFuel = maxFuel;
        }
        if(currentFuel <= 0)
        {
            empty = true;
            ResetRope();
        }
        ColorBlock colors = new ColorBlock();
        if(empty)
        {
            handle.color = emptyFuel;
        }
        else
        {
            handle.color = defUI;
        }
    }

    public bool isActive = true;

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if (empty)
            return;
        UpdateRopePositions();
        if(Input.GetButtonDown("Action3"))
        {
            var facingDirection = GetInputDirection();
            var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }

            var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
            if (facingDirection == Vector3.zero)
                return;
            HandleInput(aimDirection);            
        }
    }

    private void UpdateRopePositions()
    {
        if (!ropeAttached || !isActive)
        {
            return;
        }
        ropeRenderer.positionCount = ropePositions.Count + 1;
        currentFuel -= fuelDepleteRate;
        if(currentFuel <= 0)
        {
            isActive = false;
            ResetRope();
            return;
        }
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }

    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    private List<Vector2> ropePositions = new List<Vector2>();

    private void HandleInput(Vector2 aimDirection)
    {       
        print("handling input");
        if (ropeAttached) {
            print("resetting rope");
            ResetRope();
            return;
        }
        ropeRenderer.enabled = true;
        var hit = Physics2D.Raycast(transform.position, aimDirection, yDistance, ropeLayerMask);
        if (hit.collider != null)
        {
            ropeAttached = true;
            if (!ropePositions.Contains(hit.point))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                ropePositions.Add(hit.point);
                ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                ropeJoint.enabled = true;
                ropeHingeAnchorSprite.enabled = true;
                handleRope(true);
            }
        }
        else
        {
            ropeRenderer.enabled = false;
            ropeAttached = false;
            ropeJoint.enabled = false;
        }        
    }

    public void handleRope(bool state) {
        Rigidbody2D rig = transform.GetComponent<Rigidbody2D>();
        DemoScene dem = transform.GetComponent<DemoScene>();
        dem.enabled = !state;
        switch(state) {
            case true:
                rig.bodyType = RigidbodyType2D.Dynamic;
                break;
            case false:
                rig.bodyType = RigidbodyType2D.Kinematic;
                //rig.velocity = Vector2.zero; take the velocity from the dynamic rigidbody and translate it to a "move" function
                controller.move(rig.velocity);
                rig.velocity = Vector2.zero;
                break;
        }
    }

    private void ResetRope()
    {
        handleRope(false);

        ropeJoint.enabled = false;
        ropeAttached = false;       
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
    }

}
