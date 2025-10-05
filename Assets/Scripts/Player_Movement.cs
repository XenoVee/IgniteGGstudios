// This first example shows how to move using Input System Package (New)

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Example : MonoBehaviour
{
	public float playerSpeed = 5.0f;
	public float jumpHeight = 1.5f;
	public float gravityValue = -9.81f;
	public float mouseSensitivity = 1;
	public float jumpBoost;
	float originalJumpHeight;

	[SerializeField] private CharacterController controller;
	[SerializeField] private Collider collide;
	[SerializeField] Transform cameraTransform;
	[SerializeField] private InputActionReference moveAction;
	[SerializeField] private InputActionReference jumpAction;

	private Vector3 playerVelocity;
	private float playerBoundExtent;
	private bool grounded;
	private float rotY;
	private float rotX;


	private void Start()
	{
		playerBoundExtent = collide.bounds.extents.y;
		
	 originalJumpHeight = jumpHeight;
	}

	void Update()
	{
		grounded = isGrounded();

		if (grounded)
		{
			// Jump (or not)
			if (jumpAction.action.triggered)
			{
				playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
			}
			else if (playerVelocity.y < 0)
			{
				playerVelocity.y = 0;
			}
		}
		else
		{
			//Apply gravity
			playerVelocity.y += gravityValue * Time.deltaTime;
		}

		// Read input
		Vector2 input = moveAction.action.ReadValue<Vector2>();

		float moveX = input.x;
		float moveY = input.y;
		Vector3 move = transform.right * moveX + transform.forward * moveY;
		move = Vector3.ClampMagnitude(move, 1f);

		// Combine horizontal and vertical movement
		Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
		controller.Move(finalMove * Time.deltaTime);

		rotX += Input.GetAxis("Mouse X") * mouseSensitivity;
		rotY -= (Input.GetAxis("Mouse Y") * mouseSensitivity);

		rotY = Mathf.Clamp(rotY, -90, 90);


		transform.eulerAngles = new(0, rotX, 0);
		cameraTransform.eulerAngles = new Vector3(rotY, rotX, 0f);
	}

	bool isGrounded()
	{
		return (Physics.Raycast(transform.position, -Vector3.up, collide.bounds.extents.y + 0.2f));
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Jumppad"))
		{
			jumpHeight += jumpBoost;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Jumppad"))
		{
			jumpHeight = originalJumpHeight;
		}
	}
}
