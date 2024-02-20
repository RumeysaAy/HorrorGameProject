using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SaveScript : MonoBehaviour
{
    public static bool inventoryOpen = false;
    public static int weaponID = 0; // WeaponInventory.cs dosyasının erişmesi gerekiyor. UI'den silahı değiştirmek için gerekli
    public static bool[] weaponsPickedUp = new bool[8]; // toplanan/sahip olunan silahlar (hepsi default false)
    public static int itemID = 0;
    public static bool[] itemsPickedUp = new bool[13];

    // Start is called before the first frame update
    void Start()
    {
        weaponsPickedUp[0] = true; // 1. silah true yani 1. silaha sahibim (knife)
        weaponsPickedUp[1] = true;

        itemsPickedUp[0] = true;
        itemsPickedUp[1] = true;
        itemsPickedUp[9] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstPersonController.inventorySwitchedOn == true)
        {
            inventoryOpen = true;

        }
        else if (FirstPersonController.inventorySwitchedOn == false)
        {
            inventoryOpen = false;
        }

    }
}
