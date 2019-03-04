using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbacksOrder : MonoBehaviour
{
    public Feedbacks[] feedbacks;
    public Feedbacks currentFeedback;
    public Feedbacks oldFeedback;

    bool different = false;
    public float bite;
    public float time = 0;
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


    private void Update()
    {
        if (valueList != transitionValue)
        {
            different = true;
        }
        if (different == true)
        {
           // StartCoroutine(Value());
        }
        else
        {
            StopAllCoroutines();
        }


        currentFeedback = feedbacks[valueList];
        oldFeedback = feedbacks[oldValue];
        time += 0.5f * Time.deltaTime;

       // Debug.Log(oldFeedback.G_saturation);
      //  Debug.Log(currentFeedback.G_saturation);
       // Debug.Log(saturation);


        ////////////////////////////////////////////////// VALUES
        saturation = Mathf.Lerp(oldFeedback.G_saturation, currentFeedback.G_saturation, time);
        contrast = Mathf.Lerp(oldFeedback.G_contrast, currentFeedback.G_contrast, time);
        brightness = Mathf.Lerp(oldFeedback.G_brightness, currentFeedback.G_brightness, time);
        strength = Mathf.Lerp(oldFeedback.G_strength, currentFeedback.G_strength, time);
        bite = Mathf.Lerp(0, 1, time);

        friction = currentFeedback.R_friction;
        maxAmount = currentFeedback.R_maxAmount;
        ripple = currentFeedback.R_ripple;


    }

    /*IEnumerator Value()
    {
        oldValue = transitionValue;
        transitionValue = valueList;
        different = false;
        yield return new WaitForEndOfFrame();
    }*/
}

    

