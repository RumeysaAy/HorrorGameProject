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
        axe, // 3
        pistol, // 4
        shotgun // 5
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
        Move();
        StartCoroutine(WeaponReset());
    }

    private void Move()
    {
        // shotgun seçildiğinde arms@knife Z konumu 0.46 olmalı.
        // Default olarak 0.66 olmalı.
        switch (chosenWeapon)
        {
            case weaponSelect.knife:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.cleaver:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.bat:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.axe:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.pistol:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.shotgun:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.46f);
                break;
        }
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
HRR_Axe_01(balta) 3
Pistol 4
Shotgun 5

WeaponID ikiden az olduğu sürece, yani sıfır veya bir olduğu sürece knife idle animasyon oynatılacak

WeaponID > 1 ise bat idle (2 veya 3)

Weapon ID'si 4'e ulaştığında pistol idle oynamasını istiyoruz

5 olursa shotgun idle oynamasını istiyoruz.
*/
