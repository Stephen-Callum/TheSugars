using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : Base_PickUp
{

    public new int PickUpValue { get => _diamondPickUpValue; set => _diamondPickUpValue = value; }
    
    [SerializeField]
    private int _diamondPickUpValue = 100;

    // Virtual method of all pickups that is called when player enters trigger
    public override void OnCollection()
    {
        if (gameObject != null)
        {
            PickupSound.Play();
            m_CollectedEvent.Invoke(this);
            print(m_CollectedEvent.GetPersistentEventCount());
            base.OnCollection();
        }

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
        print(PickupSound.GetType().ToString());
        print(PickupSound.clip.name);
        base.Awake();
    }

    protected override void Update()
    {
        RotatePickup();
        YAxisHoverMotion();
        base.Update();
    }
}
