// This first example shows how to move using Input System Package (New)

using UnityEngine;
using UnityEngine.InputSystem;

public class Example : MonoBehaviour
{
	[SerializeField] private float	playerSpeed = 5.0f;
	[SerializeField] private float	jumpHeight = 1.5f;
	[SerializeField] private float	gravityValue = -9.81f;
	
	[SerializeField] private CharacterController controller;
	[SerializeField] private Collider collide;

	[SerializeField] bool grounded;
	[SerializeField] private Vector3 playerVelocity;
	public InputActionReference moveAction; // expects Vector2
	public InputActionReference jumpAction; // expects Button

	private float	playerBoundExtent;

	private void Start()
	{
		playerBoundExtent = collide.bounds.extents.y;
	}

	void Update()
	{
		grounded = isGrounded();
		
		if (grounded)
		{
			playerVelocity.y = 0;
		}
		else if (!grounded)
		{
			Debug.Log("in the air");
			//Apply gravity
			playerVelocity.y += gravityValue * Time.deltaTime;
		}

		// Read input
		Vector2 input = moveAction.action.ReadValue<Vector2>();
		Vector3 move = new Vector3(input.x, 0, input.y);
		move = Vector3.ClampMagnitude(move, 1f);

		// Jump
		if (jumpAction.action.triggered && grounded)
		{
			playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
		}



		// Combine horizontal and vertical movement
		Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
		controller.Move(finalMove * Time.deltaTime);
	}

	bool isGrounded()
	{
		return (Physics.Raycast(transform.position, -Vector3.up, collide.bounds.extents.y + 0.2f));
	}
}
