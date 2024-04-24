using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSmash : MonoBehaviour
{
    private AudioSource audioPlayer;
    private Rigidbody rb;
    private bool playSound = false;
    public GameObject bottleParent;

    // Start is called before the first frame update
    void Start()
    {
        // bu bileşenler bu dosyayla aynı nesnede bulunur
        audioPlayer = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // eğer fırlatılan şişe herhangi bir nesne ile çarpışırsa
    private void OnCollisionEnter(Collision other)
    {
        // bir kez kırılma sesi oynatılacak
        if (playSound == false)
        {
            playSound = true;
            audioPlayer.Play();
            rb.isKinematic = true; // şişe çarptığı yerde kalacak
            // bu dosyanın bulunduğu nesnenin parent nesnesini yok edeceğim
            Destroy(bottleParent, 3); // 3 saniye sonra
        }
    }
}
