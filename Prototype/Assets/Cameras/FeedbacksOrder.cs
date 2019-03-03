using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbacksOrder : MonoBehaviour
{
    public Feedbacks[] feedbacks;
    public Feedbacks currentFeedback;
    public int valueList;


    public float saturation;
    public float contrast;
    public float brightness;
    public float strength;

    public float friction;
    public float maxAmount;
    public bool ripple;




    private void Update()
    {
        currentFeedback = feedbacks[valueList];
        Debug.Log(currentFeedback.G_saturation);

        ////////////////////////////////////////////////// VALUES
        saturation = currentFeedback.G_saturation;
        contrast = currentFeedback.G_contrast;
        brightness = currentFeedback.G_brightness;
        strength = currentFeedback.G_strength;

        friction = currentFeedback.R_friction;
        maxAmount = currentFeedback.R_maxAmount;
        ripple = currentFeedback.R_ripple;
}

    
}
