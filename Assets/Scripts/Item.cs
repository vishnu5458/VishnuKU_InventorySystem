using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : BaseItem
{
    public void Picked()
    {
        Destroy(gameObject);
    }
}
