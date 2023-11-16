using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTargetReached : MonoBehaviour
{
    public float threshhold = 0.02f;
    public Transform target;
    public UnityEvent OnReached;
    private bool wasReached = false;
    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance < threshhold && !wasReached)
        {
            OnReached.Invoke();
            wasReached = true;
        }else if(distance >= threshhold )
        {
            wasReached = false; 
        }
    }
}
