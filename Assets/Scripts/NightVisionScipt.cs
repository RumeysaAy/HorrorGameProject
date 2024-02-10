using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightVisionScipt : MonoBehaviour
{
    private Image zoomBar;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        zoomBar = GameObject.Find("ZoomBar").GetComponent<Image>();
        cam = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // yukarı
        {
            if (cam.fieldOfView > 10)
            {
                cam.fieldOfView -= 5;
                zoomBar.fillAmount = cam.fieldOfView / 100; // UI
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // aşağı
        {
            if (cam.fieldOfView < 60)
            {
                cam.fieldOfView += 5;
                zoomBar.fillAmount = cam.fieldOfView / 100; // UI
            }

            // Uzaklaştıkça, kameranın görüş alanını 60'a ulaşıncaya kadar arttırıyoruz.
        }

    }
}
