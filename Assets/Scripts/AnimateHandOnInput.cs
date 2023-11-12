using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationProperty;
    public InputActionProperty gripAnimationProperty;
    public Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationProperty.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationProperty.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
