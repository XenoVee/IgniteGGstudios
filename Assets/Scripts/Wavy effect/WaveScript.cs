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
    public float stopWave;
    public float endWave = 3.0f;

    private bool inRange;

    void Awake()
    {
        _waveRenderFeature = rendererData.rendererFeatures.Find(f => f.name == featureName);
        _waveRenderFeature.SetActive(false);
    }

    private void Update()
    {
        if (wave)
        {
            stopWave += Time.deltaTime;
        }
        if (stopWave > endWave)
        {
            _waveRenderFeature.SetActive(false);
            stopWave = 0;
        }
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            StartWave();
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
        if (touch.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Portal"))
            {
                StartWave();
            }
            if (touch.CompareTag("Player") && gameObject.CompareTag("Heart"))
            {
                inRange = true;
            }
        }
      
    }
    void OnApplicationQuit()
    {
        _waveRenderFeature.SetActive(false);
    }
    void OnDisable()
    {
        _waveRenderFeature.SetActive(false);
    }
}