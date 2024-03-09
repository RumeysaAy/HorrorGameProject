using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupsScript : MonoBehaviour
{
    // vurulan her şeyi kaydetmemiz gerekiyor
    private RaycastHit hit; // vurulan herhangi bir nesneyi depolayacak
    public LayerMask excludeLayers; // seçilen Layer'lar görmezden gelinir. sadece pickups layer'ını seçmedim. seçilen katmanlardaki nesneler algılanmayacak
    public GameObject pickupPanel; // baktığımız silahı gösteren panel

    public Image mainImage; // ışının çarptığı silahın resminin koyulacağı yer
    public Sprite[] weaponIcons; // silahların resmi
    public Text mainTitle; // ışının çarptığı silahın adının koyulacağı yer
    public string[] weaponTitles; // silahların adı

    private int objID = 0; // hangi silah türüne vurduğumuza bağlı değişecek (WeaponType.cs)

    // Start is called before the first frame update
    void Start()
    {
        pickupPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // oyuncunun bir şeye çarpıp çarpmadığını tespit etmek için sürekli olarak bu ışını dünyaya fırlatmasını istiyoruz
        // ışının sonunda, çizginin sonunda ve herhangi bir yerde bir küre çizer. o kürenin çarptığı şey daha sonra tespit edilir.
        // oyuncunun konumu, yarıçap, dünyada ileriye doğru hareket etsin, ne vuruldu?, max ne kadar ileri gitsin?, seçilen katman dışındakiler(~)
        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 30, ~excludeLayers))
        {
            // Pickups Layer'ındaki bir nesneye çarpıp çarpmadığını tespit edelim.
            // yalnızca weapon etiketi olan nesneleri tespit ettiğimden emin olmak istiyorum.
            if (hit.transform.gameObject.CompareTag("weapon")) // yalnızca bir silahsa
            {
                // ışının çarptığı nesne silahsa panel açılsın
                pickupPanel.SetActive(true);

                // hangi silahı işaret ettiğimizi tespit edebilmek için WeaponType.cs dosyasını kullanacağız.
                objID = (int)hit.transform.gameObject.GetComponent<WeaponType>().chooseWeapon;
                // ışının çarptığı silaha bağlı olarak başlığın ve görselin değişmesi için
                mainImage.sprite = weaponIcons[objID];
                mainTitle.text = weaponTitles[objID];
            }
            else
            {
                // pickups katmanında fakat etiketi weapon olmayan nesne (item)
                pickupPanel.SetActive(false);
            }
        }
        else
        {
            // katmanı pickups değilse 
            pickupPanel.SetActive(false);
        }
    }
}

// bu dosya FirstPersonCharacter nesnesine bileşen olarak eklenmiştir.
