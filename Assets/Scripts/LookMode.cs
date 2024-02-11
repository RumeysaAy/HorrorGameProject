using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LookMode : MonoBehaviour
{
    private PostProcessVolume vol;
    public PostProcessProfile standard; // normal görüş
    public PostProcessProfile nightVision; // gece görüşü
    public GameObject nightVisionOverlay; // gece görüş UI
    private bool nightVisionOn = false;

    // Start is called before the first frame update
    void Start()
    {
        vol = GetComponent<PostProcessVolume>();
        nightVisionOverlay.SetActive(false);
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
            }
            else if (nightVisionOn == true)
            { // eğer gece görüşü açıksa N'e tıklandığında kapansın
                vol.profile = standard;
                nightVisionOverlay.SetActive(false);
                this.gameObject.GetComponent<Camera>().fieldOfView = 60; // gece görüşünde yapılan yakınlaştırma gece görüşü kapatıldığında sıfırlansın
                nightVisionOn = false;
            }
        }
    }
}
