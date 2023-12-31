using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    public float intensity;
    public float duration;

    public void TriggerHaptic(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }
    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}
public class HapticInteractable : MonoBehaviour
{
    public Haptic hapticOnActivated;
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();

        if(hapticOnActivated != null && interactable != null) { 
        interactable.activated.AddListener(hapticOnActivated.TriggerHaptic);
        }
    }

}
