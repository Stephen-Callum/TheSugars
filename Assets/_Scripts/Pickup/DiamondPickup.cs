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
            m_CollectedEvent.Invoke(this);
            base.OnCollection();
        }
    }
    override public int GetValue()
    {
        return pickUpValue;
    }

    // When player enters Pickup trigger (SHOULD PROBABLY BE IN BASE CLASS) (MAKE PUBLIC VIRTUAL)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Player entered Diamond collider");
            OnCollection();
        }
    }

    protected override void Awake()
    {
        PickupSound = GetComponent<AudioSource>();
        PickUpValue = 10090;
        base.Awake();
    }

    protected override void Update()
    {
        RotatePickup();
        YAxisHoverMotion();
        base.Update();
    }
}
