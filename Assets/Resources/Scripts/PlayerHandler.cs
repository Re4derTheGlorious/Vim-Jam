using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public float speedMod = 10;

    void Update()
    {
        Vector3 direction = Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up;
        transform.Translate(direction*Time.deltaTime*speedMod);
    }
}
