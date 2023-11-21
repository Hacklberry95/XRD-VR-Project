using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    [Header("Raycast Settings")]
    [SerializeField] XRGrabInteractable interactable;
    [SerializeField] Transform raycastOrigin;
    [SerializeField] LayerMask targetLayer;

    [Header("Magazine")]
    public XRBaseInteractor socketInteractor;
    public Magazine magazine;
    public bool hasSlide = true;

    [Header("Audio Settings")]
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reload;
    public AudioClip emptyMagazine;

    [Header("Target Hit Graphic")]
    [SerializeField] GameObject hitGraphic;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;
        Debug.Log(barrelLocation);
        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
        Debug.Log(gunAnimator);

        socketInteractor.selectEntered.AddListener(AddMagazine);
        socketInteractor.selectExited.AddListener(RemoveMagazine);
    }

    public void PullTheTrigger()
     {
        if (magazine && magazine.numberOfBullets > 0 && hasSlide)
        {
        Debug.Log("TriggerPulled");
        gunAnimator.SetTrigger("Fire");
        FireRaycastIntoScene();

        }
        else
        {
        source.PlayOneShot(emptyMagazine);
        }
     }

    private void CreateHitGraphicOnTarget(Vector3 hitLocation)
    {
        Debug.Log("created hit graphic");
        GameObject hitMarker = Instantiate(hitGraphic, hitLocation, Quaternion.identity);
    }

    public void AddMagazine(SelectEnterEventArgs args)
    {
        magazine = args.interactableObject.transform.GetComponent<Magazine>();
        source.PlayOneShot(reload);
        hasSlide = false;
    }
    public void RemoveMagazine(SelectExitEventArgs args)
    {
        Debug.Log("Magazine removed");
        magazine = null;
        source.PlayOneShot(reload);
    }
    public void Slide()
    {
        Debug.Log("Slider activated");
        hasSlide = true;
        source.PlayOneShot(reload);
    }
    private void FireRaycastIntoScene()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            if(hit.transform.GetComponent<ITargetInterface>() != null)
            {
                Debug.Log("Target Hit!");

                hit.transform.GetComponent<ITargetInterface>().TargetShot();
                if(!GameManager.Instance.ShouldCreateHitGraphic)
                {
                    return;
                }
                CreateHitGraphicOnTarget(hit.point);
            }
            else
            {
                Debug.Log("Not inheriting from interface");
            }
        }
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        magazine.numberOfBullets--;
        source.PlayOneShot(fireSound);
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
