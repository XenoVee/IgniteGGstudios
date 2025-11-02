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
			Debug.Log("DO SOMETHING!!!");
			movementScript.test();
			//GameManager.Instance.player.GetComponent<Player_Movement>().respawnPos = respawnPos;
			//GameManager.Instance.player.GetComponent<Player_Movement>().isRespawning = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.LogWarning("FUCKING TELEPORT");
			//GameManager.Instance.player.GetComponent<Player_Movement>().respawnPos = respawnPos;
			//GameManager.Instance.player.GetComponent<Player_Movement>().isRespawning = true;
		}
	}
}
