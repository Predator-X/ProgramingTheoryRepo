using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform camtarget;
    public float pLerp = .02f,
        rLerp = .01f;



    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camtarget.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, camtarget.rotation, rLerp);
    }
}
