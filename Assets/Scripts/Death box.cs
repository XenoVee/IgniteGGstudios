using UnityEngine;

public class Deathbox : MonoBehaviour
{
	[SerializeField] Vector3 respawnPos;
	Player_Movement movementScript;
	private void Start()
	{
		movementScript = GameManager.Instance.player.GetComponent<Player_Movement>();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			movementScript.respawnPos = respawnPos;
			movementScript.respawn();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{

			movementScript.respawnPos = respawnPos;
			movementScript.respawn();
		}
	}
}
