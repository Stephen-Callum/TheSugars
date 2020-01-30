using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : Base_PickUp
{

    public override void OnCollection(Collider playerCollider)
    {
        if (gameObject != null)
        {
            pickupSound.Play();
            print("SOUND PLAYED");
        }
        
        base.OnCollection(playerCollider);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Player entered Diamond collider");
            OnCollection(other);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        RotatePickup();
        YAxisHoverMotion();
    }
}
