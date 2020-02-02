using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : Base_PickUp
{   
    // Virtual method of all pickups that is called when player enters trigger
    public override void OnCollection()
    {
        if (gameObject != null)
        {
            PickupSound.Play();
            print("SOUND PLAYED");
            base.OnCollection();
        }
        
    }

    // When player enters Pickup trigger
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
        PickupSound = GetComponent<AudioSource>();
        print(PickupSound.GetType().ToString());
        print(PickupSound.clip.name);
    }

    protected override void Update()
    {
        RotatePickup();
        YAxisHoverMotion();
        base.Update();
    }
}
