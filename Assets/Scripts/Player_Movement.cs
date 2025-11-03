using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
	[Header("Player Control variables")]
	[SerializeField] private float	playerSpeed				= 5.0f;
	[SerializeField] public float	jumpHeight				= 1.5f;
	[SerializeField] private float	gravityValue			= -9.81f;
	[SerializeField] private float	mouseSensitivity		= 1;
	[SerializeField] private float	rotX					= 0;
	[SerializeField] private float	rotY					= 0;
	[SerializeField] private float sprintSpeed				= 8.0f;
	//[SerializeField] private float	jumpBoost				= 2;
	[SerializeField] private float	CoyoteTime				= 0.1f;
	[SerializeField] private float	JumpTimingLeniency		= 0.1f;
	[SerializeField] private float	respawnMovementLockout	= 1;

	[Header("Components")]
	[SerializeField] private CharacterController	controller;
	[SerializeField] private Collider				collide;
	[SerializeField] private Transform				cameraTransform;
	[SerializeField] private InputActionReference	moveAction;
	[SerializeField] private InputActionReference	jumpAction;

	// Private Variables (AFBLIJVEN!) 
	private Vector3						playerVelocity;	
	public float						originalJumpHeight;
	private float						timeSinceLastJumpInput;
	private float						timeSinceRespawn;
	private float						speedSave;
	[HideInInspector] public Vector3	respawnPos;
	[HideInInspector] public bool		isRespawning;
	[SerializeField] private bool		grounded;
	[SerializeField] private float		airTime;
	[SerializeField] private bool		canjump;

	private void Start()
	{
		originalJumpHeight = jumpHeight;
		timeSinceLastJumpInput = JumpTimingLeniency;
		speedSave = playerSpeed;
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
		grounded = isGrounded();
		if (timeSinceLastJumpInput <= JumpTimingLeniency)
		{
			timeSinceLastJumpInput += Time.deltaTime;
		}
		if (jumpAction.action.triggered)
		{
			timeSinceLastJumpInput = 0;
		}
		if (grounded)
		{
			if (playerVelocity.y < 0)
			{
				playerVelocity.y = 0;
			}
			if (grounded)
			airTime = 0;
		}
		else
		{
			playerVelocity.y += gravityValue * Time.deltaTime;
			airTime += Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			playerSpeed = sprintSpeed;
		}
		else
		{
			playerSpeed = speedSave;
		}
		// Read input and move player
		if (timeSinceLastJumpInput < JumpTimingLeniency && airTime <= CoyoteTime)
		{
			playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
			timeSinceLastJumpInput += JumpTimingLeniency;
			airTime += CoyoteTime;
		}
		if (hitHead() && playerVelocity.y > 0)
		{
			playerVelocity.y = 0f;
		}
		if (isRespawning)
		{
			transform.position = respawnPos;
			timeSinceRespawn += Time.deltaTime;
			if (timeSinceRespawn >= respawnMovementLockout)
			{
				isRespawning = false;
			}
		}
		else
		{
			Vector2 input = moveAction.action.ReadValue<Vector2>();
			float moveX = input.x;
			float moveZ = input.y;
			Vector3 move = transform.right * moveX + transform.forward * moveZ;
			move = Vector3.ClampMagnitude(move, 1f);

			Vector3 finalMove = ((move * playerSpeed) + (playerVelocity.y * Vector3.up)) * Time.deltaTime;
			finalMove = StickToGround(finalMove);
			controller.Move(finalMove);
		}

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
		Physics.Raycast(newLocation - Vector3.up, -Vector3.up, out hit, 0.1f);
		//Debug.DrawLine(newLocation - Vector3.up, newLocation - Vector3.up - (Vector3.up * 0.1f));
		if (hit.distance < 0.1 && hit.distance > 0)
		{
			move.y -= hit.distance;
		}
		return (move);
	}

	public void respawn()
	{
		isRespawning = true;
		timeSinceRespawn = 0;
	}

	bool hitHead()
	{
		RaycastHit	hit;
		bool ret = Physics.SphereCast(transform.position, 1, Vector3.up, out hit, collide.bounds.extents.y - 1 + 0.2f);
		Vector3 test = new(0, collide.bounds.extents.y - 1 + 0.3f, 0);
		//Debug.DrawLine(transform.position, transform.position + test);
		//Debug.Log(ret);
		return (ret);
	}
	bool isGrounded()
	{
		RaycastHit	hit;
		bool ret = Physics.SphereCast(transform.position, 1, -Vector3.up, out hit, collide.bounds.extents.y - 1 + 0.2f);
		//Debug.DrawLine(transform.position, hit.point);
		return (ret);
	}

}
