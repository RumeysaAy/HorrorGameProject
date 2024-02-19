using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    public Sprite[] bigIcons;
    public Image bigIcon;
    public string[] titles;
    public Text title;
    public string[] descriptions;
    public Text description;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChooseWeapon(int weaponNumber)
    {
        bigIcon.sprite = bigIcons[weaponNumber];
        title.text = titles[weaponNumber];
        description.text = descriptions[weaponNumber];
    }
}

/*
knife, // 0
cleaver, // 1
bat, // 2
axe, // 3
pistol, // 4
shotgun, // 5
sprayCan, // 6
bottle // 7
*/