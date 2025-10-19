using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class WaveScript : MonoBehaviour
{
    public Material mat;
    public ScriptableRendererData rendererData;
    string featureName = "FullScreenPassRendererFeature";

    private ScriptableRendererFeature _waveRenderFeature;
    private bool wave = false;

    private float _frequency = 4f;
    private float _shift = 0f;
    private float _amplitude = 0.05f;
    private float _shiftSpeed = 5f;
    public int stopWave;
    void Awake()
    {
        _waveRenderFeature = rendererData.rendererFeatures.Find(f => f.name == featureName);
    }

    private void Update()
    {

            stopWave += 1;
            if (stopWave > 600)
        {
            _waveRenderFeature.SetActive(false);
            stopWave = 0;
        }
        
        
    }


    private void SetFrequency(float frequency) { mat.SetFloat("_frequency", frequency); }
    private void SetShift(float shift) { mat.SetFloat("_shift", shift); }
    private void SetAmplitude(float amplitude) { mat.SetFloat("_amplitude", amplitude); }

    public void StartWave()
    {
        if (!wave && _waveRenderFeature != null)
        {
            wave = true;
            _waveRenderFeature.SetActive(true);
            StartCoroutine(WaveCoroutine());
        }
    }

    public void StopWave()
    {
        wave = false;
    }

    private IEnumerator WaveCoroutine()
    {
        SetFrequency(_frequency);
        SetAmplitude(_amplitude);
        _shift = 0f;

        while (wave)
        {
            _shift += _shiftSpeed * Time.deltaTime;
            SetShift(_shift);
            yield return null; 
        }

        _shift = 0f;
        SetShift(_shift); 
        if (_waveRenderFeature != null)
        {
            _waveRenderFeature.SetActive(false); 
        }
    }
    void OnTriggerEnter(Collider touch)
    {
        if (touch.CompareTag("Player") && gameObject.CompareTag("Portal"))
        {
            StartWave();
        }
    }
    void OnApplicationQuit()
    {
        _waveRenderFeature.SetActive(false);
    }
}