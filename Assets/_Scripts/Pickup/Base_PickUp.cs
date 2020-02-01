using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_PickUp : MonoBehaviour
{
    protected AudioSource pickupSound;
    
    // Reference to ScoreText UI element
    private GameObject ScoreText;
    private float _amplitude = .1f;
    private float _frequency = 1f;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    // Rotation speed access method
    public float PickupRotationSpeed { get; } = 1.0f;

    // Pickup rotation motion around Y axis
    public virtual void RotatePickup() => transform.Rotate(0, PickupRotationSpeed, 0, Space.World);

    // Pickup hover motion on Y Axis
    public virtual void YAxisHoverMotion() 
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _amplitude;
        transform.position = tempPos;
    }

    // On collection of Pickup
    public virtual void OnCollection() => Destroy(gameObject);

    private void Start()
    {
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText");
        posOffset = transform.position;
        pickupSound = GetComponent<AudioSource>();
    }
}
