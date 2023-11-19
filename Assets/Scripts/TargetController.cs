using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour, ITargetInterface
{
    [SerializeField]
    public float scoreValue;
    public void PlayAnimation()
    {
        throw new System.NotImplementedException();
    }

    public void PlayAudio()
    {
        throw new System.NotImplementedException();
    }

    public void TargetShot()
    {
        //Destroy(gameObject);
        GameManager.Instance.PlayerScored(scoreValue);
    }
}
