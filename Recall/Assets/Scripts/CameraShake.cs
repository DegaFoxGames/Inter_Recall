using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{

    public float ShakeDuration = 0.3f;          // Tempo que o efeito do noise da câmera vai durar
    public float ShakeAmplitude = 1.2f;         // Amplitude do noise
    public float ShakeFrequency = 2.0f;         // Frequência do noise

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start()
    {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        // Acionamento
        if (Input.GetButton("Fire1"))
        {
            ShakeElapsedTime = ShakeDuration;
        }

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // Se o efeito estiver sendo acionado
            if (ShakeElapsedTime > 0)
            {
                // Setando os parâmetros na cinemachine
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // Reseta as variáveis caso o efeito não esteja acionado
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }
}