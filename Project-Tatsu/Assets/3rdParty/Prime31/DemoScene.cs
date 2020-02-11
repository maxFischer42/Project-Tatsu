using UnityEngine;
using System.Collections;
using Prime31;
using Tatsu;

[RequireComponent(typeof(WallController))]
[RequireComponent(typeof(TatsuInputController))]
[RequireComponent(typeof(TatsuCombatController))]
public class DemoScene : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	public float wallJumpX = 1.5f;
	public float wallJumpHeight = 2f;
	public bool _isWall = false;

	public Vector2 faceDirection = new Vector2();

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

	private WallController wallController;
	private TatsuCombatController combatController;

	public TatsuAction prevAction;

	void Awake()
	{
		wallController = GetComponent<WallController>();
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		combatController = GetComponent<TatsuCombatController>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion

	#region keybools
		public bool left, right, up, down, aOne, aTwo, aThree, jump;
	#endregion

	public myActions actions;

	public void doActionOne() {
		if(combatController.isAction)
			return;
		TatsuAction action = actions.jab;
		if(prevAction == actions.jab && !combatController.onCooldown) {
//			print("follow up1");
			action = actions.jab2;
		} else if (prevAction == actions.jab2 && !combatController.onCooldown) {
//			print("follow up2");
			action = actions.jab3;
		}
		prevAction = action;
		combatController.Action(action);
	}

	public void doActionTwo() {
		if(combatController.isAction)
			return;
		if(up) {
			combatController.doGrappleLaunch();
		} else {
			TatsuAction action = actions.special1;
			if(prevAction == actions.special1 && !combatController.onCooldown) {
				print("special2");
				action = actions.special2;
			} else if (prevAction == actions.special2 && !combatController.onCooldown && aOne) {
				action = actions.jab;
			}
			prevAction = action;
			combatController.Action(action);
		}
	}

	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		if( _controller.isGrounded )
			_velocity.y = 0;

		if( left )
		{
			wallController.SetFacingDirection(true);
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else if( right )
		{
			wallController.SetFacingDirection(false);
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Idle" ) );
		}

		// we can only jump whilst grounded
		if( jump )
		{
			if(_controller.isGrounded) {
				_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
				_animator.Play( Animator.StringToHash( "Jump" ) );
			} else if (_isWall) {
				_velocity = new Vector2();
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				float _x = (faceDirection.x * -1) * wallJumpX;
				_velocity = new Vector2(_x, Mathf.Sqrt( wallJumpHeight * -gravity));
				_animator.Play( Animator.StringToHash( "Jump" ) );
			}
		}


		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if( _controller.isGrounded && down )
		{
			_velocity.y *= 3f;
			_controller.ignoreOneWayPlatformsThisFrame = true;
		}

		_controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}

	public void PlayAnimation(string _name) {
		_animator.Play( Animator.StringToHash( _name ) );
	}
}

[System.Serializable]
public class myActions {
	public TatsuAction jab;
	public TatsuAction jab2;
	public TatsuAction jab3;
	public TatsuAction special1;
	public TatsuAction special2;
}