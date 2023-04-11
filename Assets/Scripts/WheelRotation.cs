using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 100f;


    void Update()
    {
        transform.Rotate(new Vector3(0f,0f,rotationSpeed * Time.deltaTime));
    }
}
