using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaveScript : MonoBehaviour
{
    public Material mat;
    public bool wave = false;
    private float _frequency = 4f;
    private float _shift = 0f;
    private float _amplitude = 0.05f;
    private float _shiftSpeed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void SetFrequency(float frequency)
    {
        mat.SetFloat("_frequency", frequency);
    }

    private void SetShift(float shift)
    {
        mat.SetFloat("_shift", shift);
    }

    private void SetAmplitude(float amplitude)
    {
        mat.SetFloat("_amplitude", amplitude);
    }

    public void StartWave()
    {
        wave = true;
        StartCoroutine(WaveCoroutine());  
    }
    public void StopWave()
    {
        wave = false;
    }

    private IEnumerator WaveCoroutine()
    {
        SetFrequency(_frequency);
        SetShift(_shift);
        SetAmplitude(_amplitude);

        while (wave)
        {
            _shift += _shiftSpeed * Time.deltaTime;
            SetShift(_shift);
            yield return null;
        _shift = 0f;
        enabled = false;

    }
}
}
