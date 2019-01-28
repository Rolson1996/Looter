using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    public static CollisionManager instance = null;

	// Use this for initialization
	void Awake ()
    {
		if (instance == null)
        {
            instance = this;
        }
	}

    public delegate void GuardCollideInteraction(GameObject sender, GuardCollideEventArgs args);

    public static event GuardCollideInteraction E_GuardCollides;

    public static void GuardCollides(GameObject sender, GuardCollideEventArgs args)
    {
        if(E_GuardCollides != null)
        {
            E_GuardCollides(sender, args);
        }
    }


    public delegate void LootCollideInteraction(GameObject sender, LootCollideEventArgs args);

    public static event LootCollideInteraction E_LootCollides;

    public static void LootCollides(GameObject sender, LootCollideEventArgs args)
    {
        if (E_LootCollides != null)
        {
            E_LootCollides(sender, args);
        }
    }

}

public class GuardCollideEventArgs
{
    public Guard guard;

    public GuardCollideEventArgs(Guard g)
    {
        guard = g;
    }
}

public class LootCollideEventArgs
{
    public PickUp pickUp;
    public LootType lootType;

    public LootCollideEventArgs(PickUp pUp, LootType lType)
    {
        pickUp = pUp;
        lootType = lType;
    }
}