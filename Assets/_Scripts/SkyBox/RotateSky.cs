using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1.2f;

    public float RotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", RotateSpeed * Time.time);
    }
}
