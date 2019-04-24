﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof(FeedBack_Manager))]
public class FeedBack_Manager_Editor : Editor
{
    private FeedBack_Manager myTarget;
    private SerializedObject soTarget;

    #region Transition
    private SerializedProperty TransitionMaterial;
    private SerializedProperty matList;
    private SerializedProperty cutoff;
    private SerializedProperty transitionTime;
    #endregion

    #region ScreenShake
    [Header("ScreenShake ///////////////////////////////////")]
    private SerializedProperty shakeAmplitude;
    private SerializedProperty shakeDuration;
    private SerializedProperty shakeFrequency;
    private SerializedProperty shakeElapsedTime;

    [Space(10.0f)]
    private SerializedProperty virtualCamera;
    private SerializedProperty virtualCameraNoise;
    #endregion

    #region Camera Shader
    [Header("Camera Shader /////////////////////////////////////")]

    [Header("Bloom")]
    private SerializedProperty timeBloom;
    private SerializedProperty bloom;

    [Header("Vignette")]
    private SerializedProperty timeLens;
    private SerializedProperty vignetteColor;
    private SerializedProperty vignetteOpacity;

    #endregion
    
    #region Shader Effect
    [Header("Shader Effect /////////////////////////////////////")]
    [Header("Ripple")]
    private SerializedProperty ripple;
    private SerializedProperty MaxAmount;
    private SerializedProperty Friction;
    private SerializedProperty Amount;
    private SerializedProperty secondsToWait;

    [Space(10.0f)]
    private SerializedProperty targetCamera;
    private SerializedProperty camera;

    [Header("GrayScale")]
    private SerializedProperty brightnessAmount;
    private SerializedProperty saturationAmount;
    private SerializedProperty contrastAmount;
    private SerializedProperty strength;

    [Header("Rage Visual")]
    private SerializedProperty offsetColor;
    private SerializedProperty mode;
    private SerializedProperty previousMode;

    private SerializedProperty glitchEffect;
    private SerializedProperty glitchSpeed;
    private SerializedProperty colorSwitchSpeed;
    private SerializedProperty glitchPower;
    private SerializedProperty colorSwitch;
    private SerializedProperty colorMaxTime;
    #endregion

    #region Abilities
    [Header("Abilities /////////////////////////////////////")]
    [Header("1st Ability")]

    [Header("2nt Ability")]
    private SerializedProperty doneSecond;
    private SerializedProperty secondActivated;
    private SerializedProperty timeSecond;
    private SerializedProperty timeDeltaSecond;


    [Header("3rd Ability")]
    private SerializedProperty timeThird;

    [Header("Player_Shader /////////////////////////////////////")]
    [Header("Ghost_Effect")]
    private SerializedProperty ghostFadeSpeed;
    private SerializedProperty ghostSpawnRate;
    private SerializedProperty ghostLifetime;
    private SerializedProperty ghostAcivated;

    #endregion
   

    private void OnEnable()
    {
        myTarget = (FeedBack_Manager)target;
        soTarget = new SerializedObject(target);

        TransitionMaterial = soTarget.FindProperty("TransitionMaterial");
        matList = soTarget.FindProperty("matList");
        cutoff = soTarget.FindProperty("cutoff");
        transitionTime = soTarget.FindProperty("transitionTime");

        shakeAmplitude = soTarget.FindProperty("shakeAmplitude");
        shakeDuration = soTarget.FindProperty("shakeDuration");
        shakeFrequency = soTarget.FindProperty("shakeFrequency");
        shakeElapsedTime = soTarget.FindProperty("shakeElapsedTime");
        virtualCamera = soTarget.FindProperty("virtualCamera");
        virtualCameraNoise = soTarget.FindProperty("virtualCameraNoise");

        timeBloom = soTarget.FindProperty("timeBloom");
        bloom = soTarget.FindProperty("bloom");
        timeLens = soTarget.FindProperty("timeLens");
        vignetteColor = soTarget.FindProperty("vignetteColor");
        vignetteOpacity = soTarget.FindProperty("vignetteOpacity");

        ripple = soTarget.FindProperty("ripple");
        MaxAmount = soTarget.FindProperty("MaxAmount");
        Friction = soTarget.FindProperty("Friction");
        Amount = soTarget.FindProperty("Amount");
        secondsToWait = soTarget.FindProperty("secondsToWait");
        targetCamera = soTarget.FindProperty("target");
        camera = soTarget.FindProperty("camera");
        brightnessAmount = soTarget.FindProperty("brightnessAmount");
        saturationAmount = soTarget.FindProperty("saturationAmount");
        contrastAmount = soTarget.FindProperty("contrastAmount");
        strength = soTarget.FindProperty("strength");
        offsetColor = soTarget.FindProperty("offsetColor");
        mode = soTarget.FindProperty("mode");
        previousMode = soTarget.FindProperty("previousMode");
        glitchEffect = soTarget.FindProperty("glitchEffect");
        glitchSpeed = soTarget.FindProperty("glitchSpeed");
        colorSwitchSpeed = soTarget.FindProperty("colorSwitchSpeed");
        glitchPower = soTarget.FindProperty("glitchPower");
        colorSwitch = soTarget.FindProperty("colorSwitch");
        colorMaxTime = soTarget.FindProperty("colorMaxTime");

        doneSecond = soTarget.FindProperty("doneSecond");
        secondActivated = soTarget.FindProperty("secondActivated");
        timeSecond = soTarget.FindProperty("timeSecond");
        timeDeltaSecond = soTarget.FindProperty("timeDeltaSecond");
        timeThird = soTarget.FindProperty("timeThird");
        ghostFadeSpeed = soTarget.FindProperty("ghostFadeSpeed");
        ghostSpawnRate = soTarget.FindProperty("ghostSpawnRate");
        ghostLifetime = soTarget.FindProperty("ghostLifetime");
        ghostAcivated = soTarget.FindProperty("ghostAcivated");

    }

    public override void OnInspectorGUI()
    {
        soTarget.Update();
        EditorGUI.BeginChangeCheck();

        myTarget.toolbarTab = GUILayout.Toolbar(myTarget.toolbarTab, new string[] { "Transition", "Post-Process", "Screen Shake", "Camera Shader", "Abilities"});

        switch (myTarget.toolbarTab)
        {
            case 0:
                myTarget.currentTab = "Transition";
                break;
            case 1:
                myTarget.currentTab = "Post-Process";
                break;
            case 2:
                myTarget.currentTab = "Screen Shake";
                break;
            case 3:
                myTarget.currentTab = "Camera Shade";
                break;
            case 4:
                myTarget.currentTab = "Abilities";
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
            GUI.FocusControl(null);
        }

        EditorGUI.BeginChangeCheck();

        switch (myTarget.currentTab)
        {
            case "Transition":
                EditorGUILayout.PropertyField(TransitionMaterial);
                EditorGUILayout.PropertyField(matList , true);
                EditorGUILayout.PropertyField(cutoff);
                EditorGUILayout.PropertyField(transitionTime);
                break;
            case "Post-Process":
                EditorGUILayout.PropertyField(timeBloom);
                EditorGUILayout.PropertyField(bloom);
                EditorGUILayout.PropertyField(timeLens);
                EditorGUILayout.PropertyField(vignetteColor);
                EditorGUILayout.PropertyField(vignetteOpacity);
                break;
            case "Screen Shake":
                EditorGUILayout.PropertyField(shakeAmplitude);
                EditorGUILayout.PropertyField(shakeDuration);
                EditorGUILayout.PropertyField(shakeFrequency);
                EditorGUILayout.PropertyField(shakeElapsedTime);
                EditorGUILayout.PropertyField(virtualCamera);
                EditorGUILayout.PropertyField(virtualCameraNoise);
                break;
            case "Camera Shade":
                EditorGUILayout.PropertyField(ripple);
                EditorGUILayout.PropertyField(MaxAmount);
                EditorGUILayout.PropertyField(Friction);
                EditorGUILayout.PropertyField(Amount);
                EditorGUILayout.PropertyField(secondsToWait);
                EditorGUILayout.PropertyField(targetCamera);
                EditorGUILayout.PropertyField(camera);
                EditorGUILayout.PropertyField(brightnessAmount);
                EditorGUILayout.PropertyField(saturationAmount);
                EditorGUILayout.PropertyField(contrastAmount);
                EditorGUILayout.PropertyField(strength);

                EditorGUILayout.PropertyField(offsetColor);
                EditorGUILayout.PropertyField(mode , true);
                EditorGUILayout.PropertyField(glitchEffect);

                if (myTarget.glitchEffect == true)
                {
                    EditorGUILayout.PropertyField(glitchSpeed);
                    EditorGUILayout.PropertyField(glitchPower);
                    EditorGUILayout.PropertyField(colorSwitch);
                    EditorGUILayout.PropertyField(colorSwitchSpeed);
                    EditorGUILayout.PropertyField(colorMaxTime);
                }

                break;
            case "Abilities":
                EditorGUILayout.PropertyField(doneSecond);
                EditorGUILayout.PropertyField(secondActivated);
                EditorGUILayout.PropertyField(timeSecond);
                EditorGUILayout.PropertyField(timeDeltaSecond);
                EditorGUILayout.PropertyField(timeThird);
                EditorGUILayout.PropertyField(ghostAcivated);
                EditorGUILayout.PropertyField(ghostFadeSpeed);
                EditorGUILayout.PropertyField(ghostSpawnRate);
                EditorGUILayout.PropertyField(ghostLifetime);
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }       


    }


}