using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    robotpart,
    wheelpart,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;

    public abstract float getWeight();
    public abstract float getNoise();
    public abstract float getspeedVal();
    public abstract float getsneakVal();
    public abstract float getdisguiseVal();
    public abstract float getinvenVal();


}
