using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDiamondPickup : Base_PickUp
{
    override public int GetValue()
    {
        return pickUpValue;
    }

    protected override void Update()
    {
        RotatePickup();
        YAxisHoverMotion();
        base.Update();
    }
}
