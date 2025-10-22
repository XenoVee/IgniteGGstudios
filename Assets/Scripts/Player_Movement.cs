// This first example shows how to move using Input System Package (New)

using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
	[Header("Player Control variables")]
	public float	playerSpeed = 5.0f;
	public float	jumpHeight = 1.5f;
	public float	gravityValue = -9.81f;
	public float	mouseSensitivity = 1;
	public float	jumpBoost;
	public float	CoyoteTime;
	public float	rotX;
	public float	rotY;
	public float	JumpTimingLeniency;

	[Header("Components")]
	[SerializeField] private CharacterController	controller;
	[SerializeField] private Collider				collide;
	[SerializeField] private Transform				cameraTransform;
	[SerializeField] private InputActionReference	moveAction;
	[SerializeField] private InputActionReference	jumpAction;

	// Private Variables (AFBLIJVEN!) 
	private Vector3	playerVelocity;	
	private float	timeSinceLastJump;
	public float	originalJumpHeight;
	private float	timeSinceLastJumpInput;
	private bool	grounded;
	private float	airTime;
	private bool	canJump;

	private void Start()
	{
		originalJumpHeight = jumpHeight;
		airTime = 0;
	}

	private void OnEnable()
	{
		moveAction.action.Enable();
		jumpAction.action.Enable();
	}

	private void OnDisable()
	{
		moveAction.action.Disable();
		jumpAction.action.Disable();
	}

	void Update()
	{
		timeSinceLastJump += Time.deltaTime;
		if (timeSinceLastJumpInput <= JumpTimingLeniency)
		{
			timeSinceLastJumpInput += Time.deltaTime;
		}
		if (jumpAction.action.triggered)
		{
			timeSinceLastJumpInput = 0;
		}
		grounded = isGrounded();
		if (grounded)
		{
			airTime = 0;
			if (timeSinceLastJump > 0.2)
			{
				canJump = true;
			}
			if (playerVelocity.y < 0)
			{
				playerVelocity.y = 0;
			}
		}
		else
		{
			airTime += Time.deltaTime;
			if (airTime > CoyoteTime)
			{
				canJump = false;
			}
			//Apply gravity
			playerVelocity.y += gravityValue * Time.deltaTime;
		}

		// Read input and move player
		if (timeSinceLastJumpInput < JumpTimingLeniency && canJump)
		{
			playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
			canJump = false;
			timeSinceLastJumpInput += JumpTimingLeniency;
		}
		if (hitHead() && playerVelocity.y > 0)
		{
			playerVelocity.y = 0f;
		}

		Vector2 input = moveAction.action.ReadValue<Vector2>();
		float moveX = input.x;
		float moveY = input.y;
		Vector3 move = transform.right * moveX + transform.forward * moveY;
		move = Vector3.ClampMagnitude(move, 1f);

		Vector3 finalMove = ((move * playerSpeed) + (playerVelocity.y * Vector3.up)) * Time.deltaTime;
		if (timeSinceLastJump > 0.2)
		{
			finalMove = StickToGround(finalMove);
		}
		controller.Move(finalMove);

		// Read mouse movement and rotate camera
		rotX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		rotY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
		rotY = Mathf.Clamp(rotY, -90, 90);
		transform.eulerAngles = new(0, rotX, 0);
		cameraTransform.eulerAngles = new Vector3(rotY, rotX, 0f);

	}

	Vector3 StickToGround(Vector3 move)
	{
		RaycastHit	hit;
		Vector3		newLocation;

		newLocation = transform.position + move;
		Physics.Raycast(newLocation - Vector3.up, -Vector3.up, out hit, 1f);
		Debug.DrawLine(newLocation - Vector3.up, newLocation - Vector3.up - (Vector3.up * 1f));
		if (hit.distance < 0.3 && hit.distance > 0)
		{
			move.y -= hit.distance;
		}
		return (move);
	}
	bool hitHead()
	{
		RaycastHit	hit;
		bool ret = Physics.SphereCast(transform.position, 1, Vector3.up, out hit, collide.bounds.extents.y - 1 + 0.3f);
		Vector3 test = new(0, collide.bounds.extents.y - 1 + 0.3f, 0);
		//Debug.DrawLine(transform.position, transform.position + test);
		//Debug.Log(ret);
		return (ret);
	}
	bool isGrounded()
	{
		RaycastHit	hit;
		bool ret = Physics.SphereCast(transform.position, 1, -Vector3.up, out hit, collide.bounds.extents.y - 1 + 0.3f);
		//Debug.DrawLine(transform.position, hit.point);
		return (ret);
	}

}
