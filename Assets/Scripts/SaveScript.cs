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
    public static int[] weaponAmts = new int[8]; // her silahtan kaç tane var?
    public static int[] itemAmts = new int[13]; // her item'dan kaç tane var?
    public static int[] ammoAmts = new int[2]; // her mermiden kaç tane var?
    public static bool change = false; // eğer silah toplandıysa true olur
    public static int[] currentAmmo = new int[9]; // O andaki tabancanın ve tüfeğin içerisindeki mermi sayısı
    // Toplamda 9 silah var, bunlar arasında şişe ve kumaş da bulunuyor
    // 4'te tabanca ve 5'te tüfek bulunuyor

    // Start is called before the first frame update
    void Start()
    {
        // bıçak her zaman olacak
        weaponsPickedUp[0] = true; // 1. silah true yani 1. silaha sahibim (knife)

        itemsPickedUp[0] = true; // el feneri toplandı mı?
        itemsPickedUp[1] = true; // gece görüşü toplandı mı?

        itemAmts[0] = 1; // el fenerinden 1 tane var
        itemAmts[1] = 1; // gece görüş gözlüğünden 1 tane var
        weaponAmts[0] = 1; // bıçaktan 1 tane var
        ammoAmts[0] = 12; // pistolAmmo'dan 12 tane var
        ammoAmts[1] = 2; // shotgunAmmo'dan 2 tane var

        // ilk başladığımda mermim olmadığından olayı sıfıra eşitledim
        for (int i = 0; i < currentAmmo.Length; i++)
        {
            // yakın dövüş silahlarının mermisi olmaz
            currentAmmo[i] = 2;
        }

        currentAmmo[4] = 12; // tabancanın içerisindeki mermi sayısı

        // sadece sol tuşa basılı tuttuğumda çalışması için spreyin mermi sayısını sıfırladım
        currentAmmo[6] = 0;
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

        if (change == true) // silah veya item toplandıysa
        {
            change = false;

            for (int i = 1; i < weaponAmts.Length; i++)
            {
                if (weaponAmts[i] > 0)
                {
                    // i. indeksteki silahın i. indeksteki değeri eğer 0'dan büyükse toplanmıştır.
                    weaponsPickedUp[i] = true; // i. indeksteki silah
                }
                else if (weaponAmts[i] == 0)
                {
                    // i. indeksteki silah hiç toplanmamışsa
                    weaponsPickedUp[i] = false;
                }
            }

            for (int i = 2; i < itemAmts.Length; i++)
            {
                if (itemAmts[i] > 0)
                {
                    // i. indeksteki item'ın i. indeksteki değeri eğer 0'dan büyükse toplanmıştır.
                    itemsPickedUp[i] = true; // i. indeksteki item
                }
                else if (itemAmts[i] == 0)
                {
                    // i. indeksteki item hiç toplanmamışsa
                    itemsPickedUp[i] = false;
                }
            }
        }
    }
}

/*
WeaponManager.cs

    // arms@knife > Weapons
    public enum weaponSelect
    {
        knife, // 0
        cleaver, // 1
        bat, // 2
        axe, // 3
        pistol, // 4
        shotgun, // 5
        sprayCan, // 6
        bottle, // 7
        bottleWithCloth // 8
    }
*/
