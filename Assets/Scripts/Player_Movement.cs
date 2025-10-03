// This first example shows how to move using Input System Package (New)

using UnityEngine;
using UnityEngine.InputSystem;

public class Example : MonoBehaviour
{
	[SerializeField] private float	playerSpeed = 5.0f;
	[SerializeField] private float	jumpHeight = 1.5f;
	[SerializeField] private float	gravityValue = -9.81f;
	
	[SerializeField]
	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;

	public InputActionReference moveAction; // expects Vector2
	public InputActionReference jumpAction; // expects Button

	private void Start()
	{

	}

	void Update()
	{
		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
		}

		// Read input
		Vector2 input = moveAction.action.ReadValue<Vector2>();
		Vector3 move = new Vector3(input.x, 0, input.y);
		move = Vector3.ClampMagnitude(move, 1f);

		if (move != Vector3.zero)
		{
			transform.forward = move;
		}

		// Jump
		if (jumpAction.action.triggered && groundedPlayer)
		{
			playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
		}

		// Apply gravity
		playerVelocity.y += gravityValue * Time.deltaTime;

		// Combine horizontal and vertical movement
		Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
		controller.Move(finalMove * Time.deltaTime);
	}
}
