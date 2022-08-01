using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeadLook : MonoBehaviour
{


    public Animator anim;


    public float lookIKweight;
    public float bodyWeight;
    public float headweight;
    public float eyesWeight;
    public float clampWeight;

    public Transform lookPos;
    
    private void OnEnable() => StartCoroutine(StartTransitionIK());

    public void ChangeLookIK(Transform newTarget) //Signal fonksiyonları icin event cagırıcı
    {
        print("ChangeLookIK");
        StartCoroutine(ResetIKWeight(newTarget));
    }
    
    IEnumerator StartTransitionIK() //LookWeighti arttıarak hedefe yonelme
    {
        lookIKweight = 0;
        while (lookIKweight <= 0.4f)
        {
            lookIKweight = lookIKweight + 0.02f;
            yield return new WaitForSeconds(.01f);
        }
    }
    
    IEnumerator ResetIKWeight(Transform target) //LookWeighti default animIK'ya cekme
    {
        print("ResetIKW_Entrance");
        while (lookIKweight > 0)
        {
            lookIKweight = lookIKweight - 0.02f;
            yield return new WaitForSeconds(.01f);
        }

        if (lookIKweight < 0) lookIKweight = 0;

        lookPos = target;
        print("Target musthave changed");
        StartCoroutine(StartTransitionIK());
        yield return null;
    }

    

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(lookIKweight, bodyWeight, headweight, eyesWeight, clampWeight);
        anim.SetLookAtPosition(lookPos.position);
        //Debug.Log("it is working");
    }

}
