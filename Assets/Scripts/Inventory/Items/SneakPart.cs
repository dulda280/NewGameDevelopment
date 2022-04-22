using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Robotpart object", menuName = "Inventory/Items/SneakPart")]
public class SneakPart : ItemObject
{
    public float weight;
    public float noise;
    public float speedVal;
    public float sneakVal;
    public float disguiseVal;
    public float invenVal;
    public void Awake()
    {
        type = ItemType.wheelpart;
    }

    public override float getWeight()
    {
        return weight;
    }

    public override float getspeedVal()
    {
        return speedVal;
    }

    public override float getNoise()
    {
        return noise;
    }

    public override float getdisguiseVal()
    {
        return disguiseVal;
    }

    public override float getinvenVal()
    {
        return invenVal;
    }

    public override float getsneakVal()
    {
        return sneakVal;
    }
}
