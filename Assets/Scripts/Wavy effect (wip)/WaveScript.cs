using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class WaveScript : MonoBehaviour
{
    public Material mat;
    // VERWIJZING NAAR DE RENDERER DATA ASSET
    public ScriptableRendererData rendererData;
    // DE NAAM VAN JE FEATURE ZOALS DIE IN DE RENDERER DATA STAAT
    string featureName = "FullScreenPassRendererFeature"; // Pas deze naam eventueel aan!

    private ScriptableRendererFeature _waveRenderFeature;
    private bool wave = false; // Start als false

    // Parameters voor het effect
    private float _frequency = 4f;
    private float _shift = 0f;
    private float _amplitude = 0.05f;
    private float _shiftSpeed = 5f;
    public int stopWave;
    // Awake wordt aangeroepen voordat Start wordt uitgevoerd
    void Awake()
    {
        // Zoek de render feature op basis van de naam die je hebt ingevuld
        _waveRenderFeature = rendererData.rendererFeatures.Find(f => f.name == featureName);

        if (_waveRenderFeature == null)
        {
            Debug.LogError($"Render Feature met de naam '{featureName}' niet gevonden in {rendererData.name}. Controleer de naam!");
        }
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
            wave = true; // Belangrijk: zet 'wave' op true
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
        // Initialiseer de waarden aan het begin van het effect
        SetFrequency(_frequency);
        SetAmplitude(_amplitude);
        _shift = 0f;

        // Blijf de animatie updaten zolang 'wave' true is
        while (wave)
        {
            _shift += _shiftSpeed * Time.deltaTime;
            SetShift(_shift);
            yield return null; // Wacht tot de volgende frame
        }

        // --- OPruimen NADAT de loop is gestopt ---
        _shift = 0f;
        SetShift(_shift); // Reset de shader property
        if (_waveRenderFeature != null)
        {
            _waveRenderFeature.SetActive(false); // Schakel de feature uit
        }
    }
    void OnTriggerEnter(Collider touch)
    {
        if (touch.CompareTag("Player") && gameObject.CompareTag("Portal"))
        {
            StartWave();
        }
    }
}