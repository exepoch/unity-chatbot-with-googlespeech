using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    public GameObject camParent;
    public static bool resetOri = false;
    public float maxDegre;
    public float minDegre;

    private void Start()
    {
        Input.gyro.enabled = true;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        print("Degre : " + camParent.transform.rotation.y);

        camParent.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
        transform.Rotate(-Input.gyro.rotationRateUnbiased.x, 0, 0);
        
        inDegree();
        print("Gyro is Active");

        if(resetOri && camParent.transform.rotation.y != 0)
        {
            camParent.transform.Rotate(0, -camParent.transform.rotation.y, 0);
            if (camParent.transform.rotation.y == 0) ResetOri(false);
        }
        

    }


    void inDegree()
    {
        float  x = camParent.transform.rotation.y;
        Quaternion rot = camParent.transform.rotation;

        if (x <= -0.3)
        {
            rot.y = -0.3f;
            camParent.transform.rotation = rot;
            camParent.transform.Rotate(new Vector3(0, 0.3f * Time.deltaTime, 0));
        }
        else if (x >= 0.3)
        {
            rot.y = 0.3f;
            camParent.transform.rotation = rot;
            camParent.transform.Rotate(new Vector3(0, -0.3f * Time.deltaTime, 0));
        }
    }

    public void ResetOri(bool set)
    {
        resetOri = set;
    }
}
