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
    public Button[] weaponButtons;

    private AudioSource audioPlayer;
    public AudioClip click, select;
    private int chosenWeaponNumber;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        // ilk olarak menüde knife yani bıçak görüntülensin
        bigIcon.sprite = bigIcons[0];
        title.text = titles[0];
        description.text = descriptions[0];
    }

    private void OnEnable() // envanter menüsü her açıldığında
    {
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if (SaveScript.weaponsPickedUp[i] == false)
            {
                // silah toplanmamışsa buton kullanılamaz
                weaponButtons[i].image.color = new Color(1, 1, 1, 0.06f); // alfa azaltılır, görünürlük azalır
                weaponButtons[i].image.raycastTarget = false; // butona tıklanamaz
            }

            if (SaveScript.weaponsPickedUp[i] == true)
            {
                // silah toplanmışsa buton kullanılır hale getirilir
                weaponButtons[i].image.color = new Color(1, 1, 1, 1f);
                weaponButtons[i].image.raycastTarget = true; // butona tıklanabilir
            }
        }
    }

    public void ChooseWeapon(int weaponNumber)
    {
        bigIcon.sprite = bigIcons[weaponNumber];
        title.text = titles[weaponNumber];
        description.text = descriptions[weaponNumber];
        audioPlayer.clip = click;
        audioPlayer.Play();
        chosenWeaponNumber = weaponNumber;
    }

    public void AssignWeapon()
    {
        SaveScript.weaponID = chosenWeaponNumber;
        audioPlayer.clip = select;
        audioPlayer.Play();
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