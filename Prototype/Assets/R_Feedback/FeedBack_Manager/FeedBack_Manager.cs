using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;
using UnityEngine;

public class FeedBack_Manager : MonoBehaviour
{
    Player plScript;
    #region Transition
    [Header("Transition ///////////////////////////////////")]
    [Header("Blit")]
    public Material TransitionMaterial;
    public Material[] matList;
    public float cutoff;
    public float transitionTime = 1;
    #endregion

    #region ScreenShake
    [Header("ScreenShake ///////////////////////////////////")]
    public float shakeAmplitude = 0f;
    public float shakeDuration = 0f;
    public float shakeFrequency = 0f;
    public float shakeElapsedTime = 0f;

    [Space (10.0f)]
    public CinemachineVirtualCamera virtualCamera;
    [HideInInspector]
    public CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    #endregion

    #region Camera Shader
    [Header("Camera Shader /////////////////////////////////////")]

    [Header("Bloom")]
    public float timeBloom;
    Bloom bloomLayer = null;
    public float bloom = 0.15f;
    bool doneBloom = false;
    float bloomTime;

    [NonSerialized] PostProcessVolume volume;

    [Header("Vignette")]
    public float timeLens;
    public Color vignetteColor;
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

    [Header("Rage Visual")]
    [Range(0, 0.05f)] public float offsetColor;

    [Space(10.0f)]
    public bool glitchEffect;
    [HideInInspector] [Range(-0.05f, 0.05f)] public float colorGlitch;
    [Range(0f, 0.01f)] public float glitchSpeed;
    [Range(0.0001f, 0.001f)] public float colorSwitchSpeed;
    [Range(0f, 0.05f)] public float glitchPower;
    public bool colorSwitch;
    public float colorActual;
    [Range(0, 1f)] public float colorMaxTime;

    public Rage_Visual_Enum mode = Rage_Visual_Enum.Normal;
    [HideInInspector]public Rage_Visual_Enum previousMode = Rage_Visual_Enum.Normal;

    [Header("Glitch")]
    public bool glitchRageEnabled;

    #endregion

    #region Abilities
    [Header("Abilities /////////////////////////////////////")]
    [Header("1st Ability")]
    public Material playerMat;
    public bool firstActivated = false;
    public Color opaqueColor;
    public float timeFirstAbility;

    [Header("2nt Ability")]
    public bool secondActivated = false;
    bool doneSecond = false;
    public float timeSecondAbility;
    public float timeDeltaSecond;


    [Header("3rd Ability")]
    public bool thirdActivated = false;
    public float timeThird;

    [Header("Player_Shader /////////////////////////////////////")]
    [Header("Ghost_Effect")]
    Ghost ghostScript;
    public bool ghostAcivated;
    public float ghostLifetimeFirst;
    [Range(0,1)]public float ghostFadeSpeedFirst;
    public float ghostLifetimeSecond;
    [Range(0,1)]public float ghostFadeSpeedSecond;
    public float ghostSpawnRateFirst;
    public float ghostSpawnRateSecond;
    
    #endregion


    #region GUI LAYOUT

    [HideInInspector]
    public int toolbarTab;
    public string currentTab;
    #endregion

    void Start()
    {
        // VirtualCam
        if (virtualCamera != null)
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        // PostProcess
        volume = GetComponentInChildren<PostProcessVolume>();
        plScript = FindObjectOfType<Player>();
        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out vignette);
        //renderTexture = cam.activeTexture;
    }


    void Update()
    {
        //if (Input.GetKey(KeyCode.Space)) secondActivated = true;

        CameraShake();
        Bloom();
        Vignette();
        if (firstActivated == true) StartCoroutine(firstAbility());
        else {StopCoroutine(firstAbility());}
        if (secondActivated == true) StartCoroutine(secondAbility());
        else {StopCoroutine(secondAbility());}
        if (thirdActivated == true) StartCoroutine(thirdAbility());
        else {StopCoroutine(thirdAbility());}
        
        if(plScript.health >= 100) {Berserker();}
        else {/*saturationAmount = 1;*/ glitchRageEnabled = false;}

    }

    void Bloom()
    {

        bloomLayer.intensity.value = Mathf.Lerp(0, bloom, bloomTime);

        if (Input.GetButton("Jump"))
        {
            bloomTime += Time.deltaTime * timeBloom;
            doneBloom = false;
        }
        else if (Input.GetButtonUp("Jump"))
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



        vignette.opacity.value = Mathf.Lerp(0, vignetteOpacity, (plScript.health *0.01f) * 0.75f);
        vignette.color.value = vignetteColor;

        /* if (Input.GetButton("Jump")) ////////////// Vignette Input
        {
            vignetteTime += Time.deltaTime * timeLens;
            doneVignette = false;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            doneVignette = true;
        }*/

        if (doneVignette)
        {
            vignetteTime = Mathf.Lerp(vignetteTime, 0, Time.deltaTime / vignetteTime);
        }

        if (vignetteTime > 1) vignetteTime = 1;
    }

    void Berserker()
    {
        vignette.opacity.value = 1;
        //saturationAmount = (Mathf.Sin(Time.time * 8) * 0.5f) +2;
        glitchRageEnabled = true;
    }

    void CameraShake()
    {
        // TODO: Replace with your trigger
        if (Input.GetButton("Jump"))
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

    IEnumerator firstAbility()
    {
        ghostAcivated = true;
        
        yield return new WaitForSeconds(timeFirstAbility);

        ghostAcivated = false;
        firstActivated = false;
    }

    IEnumerator secondAbility()
    {
        float t = Mathf.Clamp(timeDeltaSecond, 0, 1);

        saturationAmount = Mathf.Lerp(1, 0, t);
        if (saturationAmount <= 0) doneSecond = true;


        if (saturationAmount > 0 && doneSecond == false)
        {
            ghostAcivated = true;
            ripple = true;
            timeDeltaSecond += Time.deltaTime;


        }

        if (doneSecond == true)
        {
            yield return new WaitForSeconds(timeSecondAbility);


            timeDeltaSecond -= Time.deltaTime;

            if (saturationAmount >= 1)
            {
                timeDeltaSecond = 0;
                doneSecond = false;
                ripple = false;
                ghostAcivated = false;
                secondActivated = false;
                saturationAmount = 1;
            }
        }



    }

    IEnumerator thirdAbility()
    {
        yield return null;
    }


}