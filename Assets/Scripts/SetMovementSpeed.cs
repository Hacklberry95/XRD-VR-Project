using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetMovementSpeed : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider movementProvider;
    public float minSpeed = 2f;
    public float maxSpeed = 4f;
    public void OnSpeedSliderValueChanged(float sliderValue)
    {
        float speed = Mathf.Lerp(minSpeed, maxSpeed, sliderValue);

        movementProvider.moveSpeed = speed;
    }
}
