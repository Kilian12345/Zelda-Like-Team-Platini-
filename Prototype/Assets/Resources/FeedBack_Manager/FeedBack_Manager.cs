using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;
using UnityEngine;

public class FeedBack_Manager : MonoBehaviour
{
    #region Transition
    [Header("Transition ///////////////////////////////////")]
    [Header("Blit")]
    public Material TransitionMaterial;
    [SerializeField] Material[] matList;
    public float cutoff;
    #endregion

    #region ScreenShake
    [Header("ScreenShake ///////////////////////////////////")]
    [SerializeField] private float shakeAmplitude = 0f;
    [SerializeField] private float shakeDuration = 0f;
    [SerializeField] private float shakeFrequency = 0f;
    [SerializeField] private float shakeElapsedTime = 0f;

    [Space (10.0f)]
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [NonSerialized] CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    #endregion

    #region Camera Shader
    [Header("Camera Shader /////////////////////////////////////")]

    [Header("PostProcess")]
    PostProcessVolume volume;

    [Header("Bloom")]
    [SerializeField] float time;
    float timos;
    public float bloom = 0.15f;
    bool doneBloom = false;
    Bloom bloomLayer = null;

    [Header("Lens")]
    LensDistortion lens = null;
    public float lensOpacity = 0;
    bool doneLens = false;

    #endregion

    #region Shader Effect
    [Header("Shader Effect /////////////////////////////////////")]
    [Header("Ripple")]
    public bool ripple;
    public float MaxAmount = 50f;
    public float Friction = .9f;
    public float Amount = 0f;
    public float secondsToWait = 0.2f;
    public Transform target;
    public Camera camera;

    [Header("GrayScale")]
    public float brightnessAmount = 1;
    public float saturationAmount = 1;
    public float contrastAmount = 1;
    public float strength = 0;
    #endregion



    void Start()
    {
        // VirtualCam
        if (virtualCamera != null)
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        // PostProcess
        volume = GetComponentInChildren<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out lens);
        //renderTexture = cam.activeTexture;
    }


    void Update()
    {
        CameraShake();
        Bloom();
        LensDistorted();
    }

    void Bloom()
    {

        bloomLayer.intensity.value = Mathf.Lerp(0, bloom, timos);

        if (Input.GetKey(KeyCode.Space))
        {

            timos += Time.deltaTime * time;
            doneBloom = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            doneBloom = true;
        }

        if (doneBloom)
        {
            timos = Mathf.Lerp(timos, 0, Time.deltaTime / 0.5f);
        }
    }

    void LensDistorted()
    {
        lens.intensity.value = Mathf.Lerp(0, bloom, timos);

        if (Input.GetKey(KeyCode.Space))
        {

            timos += Time.deltaTime * time;
            doneLens = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            doneLens = true;
        }

        if (doneLens)
        {
            timos = Mathf.Lerp(timos, 0, Time.deltaTime / 0.5f);
        }
    }

    void CameraShake()
    {
        // TODO: Replace with your trigger
        if (Input.GetButtonDown("Jump"))
        {
            shakeElapsedTime = shakeDuration;
        }
        else
        {

        }

        // If the Cinemachine componet is not set, avoid update
        if (virtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (shakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                // Update Shake Timer
                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }

}