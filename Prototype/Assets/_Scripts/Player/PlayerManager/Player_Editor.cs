using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR 
[CustomEditor(typeof(Player))]
public class Player_Editor : Editor
{
    private Player myTarget;
    private SerializedObject soTarget;

    #region Rage

    private SerializedProperty health;
    private SerializedProperty CalciumAmount;
    private SerializedProperty CalciumCapacity;
    private SerializedProperty curDropChanceRate;
    private SerializedProperty DropChanceRate;

    private SerializedProperty rageDamage;
    private SerializedProperty rageTimer;
    private SerializedProperty rageVel;
    private SerializedProperty curTime;

    private SerializedProperty isDead;
    private SerializedProperty isInRage;

    #endregion

    #region Movement

    private SerializedProperty lastHor, lastVer;
    private SerializedProperty vel;
    private SerializedProperty maxVel;

    #endregion

    #region Combat

    private SerializedProperty gun;
    private SerializedProperty gunSprite;
    private SerializedProperty shootPoint;
    private SerializedProperty carryPoint;
    private SerializedProperty centrePoint;

    private SerializedProperty PlayerScore;
    private SerializedProperty EnemiesFollowing;
    private SerializedProperty enemyFollowLimit;
    private SerializedProperty enemyBulletDamage;
    private SerializedProperty toPunch;
    private SerializedProperty damage;
    private SerializedProperty baseDamage;
    private SerializedProperty attackSpeed;
    private SerializedProperty attackRange;
    private SerializedProperty attackPushForce;

    #endregion

    #region Ability

    private SerializedProperty cooldownTime;
    private SerializedProperty curcooldownTime;
    private SerializedProperty abilityIsToCooldown;
    private SerializedProperty activatedAbility;
    private SerializedProperty selector;
    private SerializedProperty abilityMeters;

    private SerializedProperty dashDistance;
    private SerializedProperty thrust;

    private SerializedProperty slowDownFactor;
    private SerializedProperty slowDownLast;

    #endregion

    #region Audio Visuals

    private SerializedProperty hit, died;
    private SerializedProperty particles;

    #endregion


    private void OnEnable()
    {
        myTarget = (Player)target;
        soTarget = new SerializedObject(target);

        health = soTarget.FindProperty("health");
        CalciumAmount = soTarget.FindProperty("CalciumAmount");
        CalciumCapacity = soTarget.FindProperty("CalciumCapacity");
        curDropChanceRate = soTarget.FindProperty("curDropChanceRate");
        DropChanceRate = soTarget.FindProperty("DropChanceRate");
        rageDamage = soTarget.FindProperty("rageDamage");
        rageTimer = soTarget.FindProperty("rageTimer");
        rageVel = soTarget.FindProperty("rageVel");
        curTime = soTarget.FindProperty("curTime");
        isDead = soTarget.FindProperty("isDead");
        isInRage = soTarget.FindProperty("isInRage");

        lastHor = soTarget.FindProperty("lastHor");
        lastVer = soTarget.FindProperty("lastVer");
        vel = soTarget.FindProperty("vel");
        maxVel = soTarget.FindProperty("maxVel");

        gun = soTarget.FindProperty("gun");
        gunSprite = soTarget.FindProperty("gunSprite");
        shootPoint = soTarget.FindProperty("shootPoint");
        carryPoint = soTarget.FindProperty("carryPoint");
        centrePoint= soTarget.FindProperty("centrePoint");
        PlayerScore = soTarget.FindProperty("PlayerScore");
        EnemiesFollowing = soTarget.FindProperty("EnemiesFollowing");
        enemyFollowLimit = soTarget.FindProperty("enemyFollowLimit");
        enemyBulletDamage = soTarget.FindProperty("enemyBulletDamage");
        toPunch = soTarget.FindProperty("toPunch");
        damage = soTarget.FindProperty("damage");
        baseDamage = soTarget.FindProperty("baseDamage");
        attackSpeed = soTarget.FindProperty("attackSpeed");
        attackRange = soTarget.FindProperty("attackRange");
        attackPushForce = soTarget.FindProperty("attackPushForce");

        cooldownTime = soTarget.FindProperty("cooldownTime");
        curcooldownTime = soTarget.FindProperty("curcooldownTime");
        abilityIsToCooldown = soTarget.FindProperty("abilityIsToCooldown");
        activatedAbility = soTarget.FindProperty("activatedAbility");
        selector = soTarget.FindProperty("selector");
        abilityMeters = soTarget.FindProperty("abilityMeters");
        dashDistance = soTarget.FindProperty("dashDistance");
        thrust = soTarget.FindProperty("thrust");
        slowDownFactor = soTarget.FindProperty("slowDownFactor");
        slowDownLast = soTarget.FindProperty("slowDownLast");

        hit = soTarget.FindProperty("hit");
        died = soTarget.FindProperty("died");
        particles = soTarget.FindProperty("particles");


    }


    public override void OnInspectorGUI()
    {
        soTarget.Update();
        EditorGUI.BeginChangeCheck();

        myTarget.toolbarTab = GUILayout.Toolbar(myTarget.toolbarTab, new string[] { "Rage", "Movement", "Combat", "Ability", "Audio Visuals" });

        switch (myTarget.toolbarTab)
        {
            case 0:
                myTarget.currentTab = "Rage";
                break;
            case 1:
                myTarget.currentTab = "Movement";
                break;
            case 2:
                myTarget.currentTab = "Combat";
                break;
            case 3:
                myTarget.currentTab = "Ability";
                break;
            case 4:
                myTarget.currentTab = "Audio Visuals";
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
            case "Rage":
                EditorGUILayout.PropertyField(health);
                EditorGUILayout.PropertyField(CalciumAmount);
                EditorGUILayout.PropertyField(CalciumCapacity);
                EditorGUILayout.PropertyField(curDropChanceRate);
                EditorGUILayout.PropertyField(DropChanceRate);
                EditorGUILayout.PropertyField(rageDamage);
                EditorGUILayout.PropertyField(rageTimer);
                EditorGUILayout.PropertyField(rageVel);
                EditorGUILayout.PropertyField(curTime);
                EditorGUILayout.PropertyField(isDead);
                EditorGUILayout.PropertyField(isInRage);
                break;
            case "Movement":
                EditorGUILayout.PropertyField(lastHor);
                EditorGUILayout.PropertyField(lastVer);
                EditorGUILayout.PropertyField(vel);
                EditorGUILayout.PropertyField(maxVel);
                break;
            case "Combat":
                EditorGUILayout.PropertyField(gun);
                EditorGUILayout.PropertyField(gunSprite);
                EditorGUILayout.PropertyField(shootPoint);
                EditorGUILayout.PropertyField(carryPoint);
                EditorGUILayout.PropertyField(centrePoint);
                EditorGUILayout.PropertyField(PlayerScore);
                EditorGUILayout.PropertyField(EnemiesFollowing);
                EditorGUILayout.PropertyField(enemyFollowLimit);
                EditorGUILayout.PropertyField(enemyBulletDamage);
                EditorGUILayout.PropertyField(toPunch);
                EditorGUILayout.PropertyField(damage);
                EditorGUILayout.PropertyField(baseDamage);
                EditorGUILayout.PropertyField(attackSpeed);
                EditorGUILayout.PropertyField(attackRange);
                EditorGUILayout.PropertyField(attackPushForce);
                break;
            case "Ability":
                EditorGUILayout.PropertyField(cooldownTime,true);
                EditorGUILayout.PropertyField(curcooldownTime, true);
                EditorGUILayout.PropertyField(abilityIsToCooldown);
                EditorGUILayout.PropertyField(activatedAbility);
                EditorGUILayout.PropertyField(selector);
                EditorGUILayout.PropertyField(abilityMeters, true);
                EditorGUILayout.PropertyField(dashDistance);
                EditorGUILayout.PropertyField(thrust);
                EditorGUILayout.PropertyField(slowDownFactor);
                EditorGUILayout.PropertyField(slowDownLast);
                break;
            case "Audio Visuals":
                EditorGUILayout.PropertyField(hit);
                EditorGUILayout.PropertyField(died);
                EditorGUILayout.PropertyField(particles);
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }
    }
}
#endif
