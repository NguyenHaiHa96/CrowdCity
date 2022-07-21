using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float mouseSenitivity;
    [SerializeField] Transform target;

    private void Start()
    {
        
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenitivity * Time.deltaTime;
        target.Rotate(Vector3.up * mouseX);
    }
}
