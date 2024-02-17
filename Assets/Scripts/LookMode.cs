using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LookMode : MonoBehaviour
{
    private PostProcessVolume vol;
    public PostProcessProfile standard; // normal görüş
    public PostProcessProfile nightVision; // gece görüşü
    public PostProcessProfile inventory; // envanter profili
    public GameObject nightVisionOverlay; // gece görüş UI
    public GameObject flashLightOverlay; // el feneri UI
    private Light flashLight; // el fenerini kapatıp açmak için kullanacağız
    private bool nightVisionOn = false;
    private bool flashLightOn = false;
    private bool inventoryOn = false;

    // Start is called before the first frame update
    void Start()
    {
        vol = GetComponent<PostProcessVolume>();
        flashLight = GameObject.Find("FlashLight").GetComponent<Light>(); // el fenerinin(spotlight) ışık bileşeni
        flashLight.enabled = false;
        nightVisionOverlay.SetActive(false);
        flashLightOverlay.SetActive(false);
        vol.profile = standard;
    }

    // Update is called once per frame
    void Update()
    {
        // eğer gece görüşü kapalıysa N'e basıldığında açılsın
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (nightVisionOn == false)
            {
                vol.profile = nightVision;
                nightVisionOverlay.SetActive(true);
                nightVisionOn = true;
                NightVisionOff(); // Pilimizde güç yoksa gece görüşünün hiç açılmasını istemeyiz.
            }
            else if (nightVisionOn == true)
            { // eğer gece görüşü açıksa N'e tıklandığında kapansın
                vol.profile = standard;
                nightVisionOverlay.SetActive(false);
                nightVisionOverlay.GetComponent<NightVisionScript>().StopDrain(); // gece görüşü pilin azalmasını engeller
                this.gameObject.GetComponent<Camera>().fieldOfView = 60; // gece görüşünde yapılan yakınlaştırma gece görüşü kapatıldığında sıfırlansın
                nightVisionOn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashLightOn == false)
            {
                flashLightOverlay.SetActive(true);
                flashLight.enabled = true; // el fenerinin ışığını aç
                flashLightOn = true;
                FlashLightSwitchOff();
            }
            else if (flashLightOn == true)
            {
                flashLightOverlay.SetActive(false);
                flashLight.enabled = false; // el fenerinin ışığını kapat
                flashLightOverlay.GetComponent<FlashLightScript>().StopDrain(); // el feneri pilin azalmasını engeller
                flashLightOn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) // envanter
        {
            if (inventoryOn == false)
            {
                vol.profile = inventory;
                inventoryOn = true;
                // envanter açıkken gece görüşü ve el feneri iptal edilmeli
            }
            else if (inventoryOn == true)
            {
                vol.profile = standard;
                inventoryOn = false;
            }
        }

        if (nightVisionOn == true)
        {
            NightVisionOff(); // Pilimizde güç yoksa gece görüşünün hiç açılmasını istemeyiz.
        }

        if (flashLightOn == true)
        {
            FlashLightSwitchOff();
        }
    }

    private void NightVisionOff()
    { // Pil gücü bittiğinde yani sıfıra ulaştığında gece görüş modu kapanıp standart mod tekrar açılacak.
        if (nightVisionOverlay.GetComponent<NightVisionScript>().batteryPower <= 0)
        {
            vol.profile = standard;
            nightVisionOverlay.SetActive(false);
            this.gameObject.GetComponent<Camera>().fieldOfView = 60; // gece görüşünde yapılan yakınlaştırma gece görüşü kapatıldığında sıfırlansın
            nightVisionOn = false;
        }
    }

    private void FlashLightSwitchOff()
    {
        if (flashLightOverlay.GetComponent<FlashLightScript>().batteryPower <= 0)
        {
            flashLightOverlay.SetActive(false);
            flashLight.enabled = false;
            flashLightOverlay.GetComponent<FlashLightScript>().StopDrain();
            flashLightOn = false;
        }
    }
}
