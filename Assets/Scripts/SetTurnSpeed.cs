using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TurnSpeedSliderController : MonoBehaviour
{
    public ActionBasedContinuousTurnProvider turnProvider;
    public float minTurnSpeed = 10f;
    public float maxTurnSpeed = 180f;

    public void OnTurnSpeedSliderValueChanged(float sliderValue)
    {
        // Map the slider value to the desired turn speed range
        float turnSpeed = Mathf.Lerp(minTurnSpeed, maxTurnSpeed, sliderValue);

        // Update the turn speed in the ContinuousTurnProvider
        turnProvider.turnSpeed = turnSpeed;
    }
}
