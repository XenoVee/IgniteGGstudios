using UnityEngine;

public class Deathbox : MonoBehaviour
{
    [SerializeField] Vector3 respawnPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("DO SOMETHING!!!");
            GameManager.Instance.player.transform.position = respawnPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("FUCKING TELEPORT");
            GameManager.Instance.player.transform.position = respawnPos;
        }
    }
}
