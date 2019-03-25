﻿using System;
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
    public float transitionTime = 1;
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

    [Header("Bloom")]
    [SerializeField] float timeBloom;
    Bloom bloomLayer = null;
    public float bloom = 0.15f;
    bool doneBloom = false;
    float bloomTime;

    [NonSerialized] PostProcessVolume volume;

    [Header("Vignette")]
    [SerializeField] float timeLens;
    [SerializeField] Color vignetteColor;
    Vignette vignette = null;
    public float vignetteOpacity = 0;
    bool doneVignette = false;
    float vignetteTime;

    #endregion

    #region Shader Effect
    [Header("Shader Effect /////////////////////////////////////")]
    [Header("Ripple")]
    public bool ripple;
    public float MaxAmount = 50f;
    public float Friction = .9f;
    public float Amount = 0f;
    public float secondsToWait = 0.2f;

    [Space (10.0f)]
    public Transform target;
    public Camera camera;

    [Header("GrayScale")]
    public float brightnessAmount = 1;
    public float saturationAmount = 1;
    public float contrastAmount = 1;
    public float strength = 0;
    #endregion

    #region Abilities
    [Header("Abilities /////////////////////////////////////")]
    [Header("1st Ability")]

    [Header("2nt Ability")]
    [SerializeField] float timeSecond;

    [Header("3rd Ability")]
    [SerializeField] float timeThird;
    #endregion

    void Start()
    {
        // VirtualCam
        if (virtualCamera != null)
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        // PostProcess
        volume = GetComponentInChildren<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out vignette);
        //renderTexture = cam.activeTexture;
    }


    void Update()
    {
        CameraShake();
        Bloom();
        Vignette();
        if (Input.GetKey(KeyCode.Space)) StartCoroutine(secondAbility());
    }

    void Bloom()
    {

        bloomLayer.intensity.value = Mathf.Lerp(0, bloom, bloomTime);

        if (Input.GetKey(KeyCode.Space))
        {
            bloomTime += Time.deltaTime * timeBloom;
            doneBloom = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            doneBloom = true;
        }

        if (doneBloom)
        {
            bloomTime = Mathf.Lerp(bloomTime, 0, Time.deltaTime / bloomTime);
        }

        if (bloomTime > 1) bloomTime = 1;
    }

    void Vignette()
    {



        vignette.opacity.value = Mathf.Lerp(0, vignetteOpacity, vignetteTime);
        vignette.color.value = vignetteColor;

        if (Input.GetKey(KeyCode.Space))
        {
            vignetteTime += Time.deltaTime * timeLens;
            doneVignette = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            doneVignette = true;
        }

        if (doneVignette)
        {
            vignetteTime = Mathf.Lerp(vignetteTime, 0, Time.deltaTime / vignetteTime);
        }

        if (vignetteTime > 1) vignetteTime = 1;
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

    IEnumerator secondAbility()
    {
        while (saturationAmount > 0)
        {
            ripple = true;
            saturationAmount -= 0.00000000000001f;
        }


        yield return new WaitForSeconds(timeSecond);

        saturationAmount = Mathf.Lerp(0, 1, Time.deltaTime);
        ripple = false;

    }


}