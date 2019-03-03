using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArtificialIntelligenceState { Idle, Patrolling, Angry, Runaway }

public class SomeFunctionalComponent : MonoBehaviour
{

    
    [System.NonSerialized] public float speed;
    [System.NonSerialized] public float rotateSpeed;
    [System.NonSerialized] public AnimationCurve acceleration;
    [System.NonSerialized] public Color color;

    public ArtificialIntelligenceState state = ArtificialIntelligenceState.Idle;
    public Transform self;
    public SomeDataHolder[] controllerConfig;

    [ContextMenu("salut")]
    void MonResetCustom()
    {
        

        self = transform;
    }

    void LoadParameters(SomeDataHolder config)
    {
        speed = config.speed;
        color = config.color;
        rotateSpeed = config.rotateSpeed;
        acceleration = config.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        self.Translate(Vector3.right * speed * Time.deltaTime);

    }

    private void OnGUI()
    {
        GUILayout.Label(speed.ToString());
    }
}
