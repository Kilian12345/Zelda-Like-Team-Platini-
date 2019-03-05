using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbacksOrder : MonoBehaviour
{
    public Feedbacks[] feedbacks;
    public Feedbacks currentFeedback;
    public Feedbacks oldFeedback;

    bool different = false;
    public float bite = 0;
    public float time = 2;

    float startTime = 0f;
    //public Feedbacks transitionFeedback;
    // public Feedbacks oldFeedback;

    public int valueList;
    public int transitionValue;
    public int oldValue;

    public float saturation;
    public float contrast;
    public float brightness;
    public float strength;

    public float friction;
    public float maxAmount;
    public bool ripple;

    private void Start()
    {
        transitionValue = valueList;
        oldValue = valueList;
    }


    private void FixedUpdate()
    {



        if (valueList != transitionValue)
        {
            different = true;

        }
        if (different == true)
        {
            StartCoroutine(Value());
            Values();

            startTime = startTime + (Time.fixedDeltaTime / time);
            startTime = Mathf.Clamp(startTime, 0, 1);
        }
         else
         {
            startTime = 0;
            StopAllCoroutines();
         }





       Debug.Log(startTime);






        ////////////////////////////////////////////////// VALUES
       /* saturation = Mathf.Lerp(oldFeedback.G_saturation, currentFeedback.G_saturation, startTime);
        contrast = Mathf.Lerp(oldFeedback.G_contrast, currentFeedback.G_contrast, startTime);
        brightness = Mathf.Lerp(oldFeedback.G_brightness, currentFeedback.G_brightness, startTime);
        strength = Mathf.Lerp(oldFeedback.G_strength, currentFeedback.G_strength, startTime);
        bite = Mathf.Lerp(bite, 1, startTime);

        friction = currentFeedback.R_friction;
        maxAmount = currentFeedback.R_maxAmount;
        ripple = currentFeedback.R_ripple;*/


    }

    void Values()
    {
        currentFeedback = feedbacks[valueList];
        oldFeedback = feedbacks[oldValue];
        startTime = startTime + (Time.fixedDeltaTime / time);

        saturation = Mathf.Lerp(oldFeedback.G_saturation, currentFeedback.G_saturation, startTime);
        contrast = Mathf.Lerp(oldFeedback.G_contrast, currentFeedback.G_contrast, startTime);
        brightness = Mathf.Lerp(oldFeedback.G_brightness, currentFeedback.G_brightness, startTime);
        strength = Mathf.Lerp(oldFeedback.G_strength, currentFeedback.G_strength, startTime);
        bite = Mathf.Lerp(bite, 1, startTime);

        friction = currentFeedback.R_friction;
        maxAmount = currentFeedback.R_maxAmount;
        ripple = currentFeedback.R_ripple;
    }


    IEnumerator Value()
    {
        yield return new WaitForSeconds(time);
        oldValue = transitionValue;
        transitionValue = valueList;
        different = false;


    }
}

    

