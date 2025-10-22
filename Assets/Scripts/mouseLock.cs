using UnityEngine;

public class MouseLock : MonoBehaviour
{

     bool mouseLocked = true;
    

    void Update()
    {
      
		if (Input.GetKey(KeyCode.Escape))
		{
			mouseLocked = !mouseLocked;
		}
		if (!mouseLocked)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}  
    }
}
