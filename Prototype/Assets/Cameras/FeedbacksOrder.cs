using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbacksOrder : MonoBehaviour
{
    public Feedbacks[] feedbacks;
    public Feedbacks currentFeedback;
    public int valueList;


    [System.NonSerialized] public float saturation;
    [System.NonSerialized] public float contrast;
    [System.NonSerialized] public float brightness;
    [System.NonSerialized] public float strength;

    [System.NonSerialized] public float friction;
    [System.NonSerialized] public float maxAmount;




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

}


}
