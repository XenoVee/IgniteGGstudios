using UnityEngine;

public class waveyScript : MonoBehaviour
{

    public Material mat;
    public float speed;


    void Update()
    {if (mat != null)
        {
            mat.SetFloat("_shift", Time.time * speed);
            Debug.Log("Script is updating! Shift is: " + (Time.time * speed));
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}