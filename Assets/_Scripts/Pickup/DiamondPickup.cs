using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : Base_PickUp
{
    public override void OnCollection()
    {
        if (gameObject != null)
        {
            pickupSound.Play();
            print("SOUND PLAYED");
        }
        base.OnCollection();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Player entered Diamond collider");
            OnCollection();
        }
    }

    private void Awake()
    {
        pickupSound = GetComponent<AudioSource>();
        print(pickupSound.GetType().ToString());
        print(pickupSound.clip.name);
    }

    private void Update()
    {
        RotatePickup();
        YAxisHoverMotion();
    }
}
