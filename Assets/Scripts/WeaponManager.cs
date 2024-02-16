using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum weaponSelect
    {
        knife, // 0
        cleaver, // 1
        bat, // 2
        pistol, // 3
        shotgun // 4
    }


    public weaponSelect chosenWeapon;
    public GameObject[] weapons; // weapon prefab
    private int weaponID = 0;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        weaponID = (int)chosenWeapon;
        anim = GetComponent<Animator>();
        ChangeWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // x'e tıklayarak silahları değiştirebiliriz
        {
            if (weaponID < weapons.Length - 1)
            {
                weaponID++;
                ChangeWeapons();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) // z'ye tıklayarak silahları değiştirebiliriz
        {
            if (weaponID > 0)
            {
                weaponID--;
                ChangeWeapons();
            }
        }
    }

    private void ChangeWeapons()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[weaponID].SetActive(true); // o anda seçili olan silahı açmak istiyoruz.
        chosenWeapon = (weaponSelect)weaponID;
        anim.SetInteger("WeaponID", weaponID);
        anim.SetBool("weaponChanged", true); // sürekli olarak çağrılmasını engellemek ve animasyonu bitirmesini sağlamak için
        StartCoroutine(WeaponReset());
    }

    IEnumerator WeaponReset()
    {
        yield return new WaitForSeconds(0.5f); // animasyonu bitirmesi için gereken zaman
        anim.SetBool("weaponChanged", false);
    }
}

/*
Knife 0
Cleaver 1
BaseballBat 2
Pistol 3
Shotgun 4

WeaponID üçten az olduğu sürece, yani sıfır bir veya iki olduğu sürece knife idle animasyon oynatılacak

Weapon ID'si üçe ulaştığında pistol idle oynamasını istiyoruz

dört olursa shotgun idle oynamasını istiyoruz.
*/
