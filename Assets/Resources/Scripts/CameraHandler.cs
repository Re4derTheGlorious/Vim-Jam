using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject light;
    public Vector3 cameraShift;
    public Vector3 cameraRotation;

    void Start()
    {
        transform.Rotate(cameraRotation);
    }

    void Update()
    {
        transform.position = player.transform.position + cameraShift;
        light.transform.position = player.transform.position+cameraShift;

    }
}
