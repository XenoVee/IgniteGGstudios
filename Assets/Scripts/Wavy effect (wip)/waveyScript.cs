using UnityEngine;

public class testscript : MonoBehaviour
{

    public Material mat;

    public float waveNumber;
    public float waveLength;

    void Update()
    {
        Shader.SetGlobalFloat("_waveLength", waveNumber);
        Shader.SetGlobalFloat("_waveNumber", waveLength);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}