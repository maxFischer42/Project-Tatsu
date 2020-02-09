using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D), typeof(DemoScene))]
public class WallController : MonoBehaviour
{
    public bool facingRight = true;

    public float checkDistance = 0.5f;
    public LayerMask wallLayer;

    public Transform[] rayLocations = new Transform[3];

    private DemoScene inputCheck;
    private CharacterController2D controller2D;

    private Vector2 prevDirection = Vector2.zero;

    private void Start() {
        inputCheck = gameObject.GetComponent<DemoScene>();
        controller2D = gameObject.GetComponent<CharacterController2D>();
    }

    public void SetFacingDirection(bool isRight) {
        facingRight = isRight;
    }

    private Vector2 getDirection() {
        switch(facingRight) {
            case true:
                return new Vector2(1, 0);
            case false:
                return new Vector2(-1, 0);
        }
        return new Vector2();
    }

    public void FixedUpdate() {
        
        Vector2 faceDirection = getDirection();

        //if we hit the ground, set the previously set direction to zero
        if(controller2D.velocity.y == 0f) {
            //print("is grounded");
            prevDirection = Vector2.zero;
        }

        //cast 2 rays to check if there is a wall
        bool upperCheck = DoRaycast(rayLocations[0].position, faceDirection);
        bool lowerCheck = DoRaycast(rayLocations[1].position, faceDirection);

        //cast ray above to check if there is a wall for use of climbing up an edge
        bool edgeCheck = DoRaycast(rayLocations[2].position, faceDirection);
        inputCheck.faceDirection = faceDirection;
        if(upperCheck || lowerCheck && faceDirection != prevDirection) {
            inputCheck._isWall = true;
            //set the previous direction to the faceDirection so that the player cannot wall jump up the same wall
            prevDirection = faceDirection;
        } else {
            inputCheck._isWall = false;
        }
        
        
    }

    public bool DoRaycast(Vector2 position, Vector2 direction) {
        if(Physics2D.Raycast(position, direction, checkDistance, wallLayer)) {
            return true;
        } else {
            return false;
        }
    }

     

}
