using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScale : MonoBehaviour
{
    private float scaleValue = 1f;

    // Start is called before the first frame update
    void Start()
    { // 4K UHD yaptığımızda UI elemanlarının boyutunu iki katına çıkaralım.
        if (Screen.width > 1920)
        {
            scaleValue = 2f; // boyut 2 katına çıkarıldı
        }

        this.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
