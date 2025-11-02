using UnityEngine;

public class Deathbox : MonoBehaviour
{
	[SerializeField] Vector3 respawnPos;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log("DO SOMETHING!!!");
			GameManager.Instance.player.GetComponent<Player_Movement>().respawnPos = respawnPos;
			GameManager.Instance.player.GetComponent<Player_Movement>().isRespawning = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.LogWarning("FUCKING TELEPORT");
			GameManager.Instance.player.GetComponent<Player_Movement>().respawnPos = respawnPos;
			GameManager.Instance.player.GetComponent<Player_Movement>().isRespawning = true;
		}
	}
}
