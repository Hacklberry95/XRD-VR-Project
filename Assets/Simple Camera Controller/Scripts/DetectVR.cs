using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class DetectVR : MonoBehaviour
{
    public GameObject xrOrigin;
    public GameObject desktopCharacter;
    public bool startInVR = true;
    void Start()
    {
        if (startInVR)
        {
            var xrSettings = XRGeneralSettings.Instance;
            if (xrSettings == null)
            {
                Console.WriteLine("XRGeneralSettings is null");
                return;
            }
            var xrManager = xrSettings.Manager;
            if (xrManager == null)
            {
                Console.WriteLine("XRManagerSettings is null");
                return;
            }
            var xrLoader = xrManager.activeLoader;
            if (xrLoader == null)
            {
                Console.WriteLine("XRLoader is null");
                xrOrigin.SetActive(false);
                desktopCharacter.SetActive(true);
                return;
            }
            Console.WriteLine("XRLoader is not null");
            xrOrigin.SetActive(true);
            desktopCharacter.SetActive(false);
        }
        else
        {
            xrOrigin.SetActive(false);
            desktopCharacter.SetActive(true);
        }
    }
}
