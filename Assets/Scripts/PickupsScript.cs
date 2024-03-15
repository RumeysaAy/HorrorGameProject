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
    public float pickupDisplayDistance = 8f;

    public Image mainImage; // ışının çarptığı silahın resminin koyulacağı yer
    public Sprite[] weaponIcons; // silahların resmi
    public Sprite[] itemIcons; // item'ların resmi
    public Text mainTitle; // ışının çarptığı silahın adının koyulacağı yer
    public string[] weaponTitles; // silahların adı
    public string[] itemTitles; //item'ların adı

    private int objID = 0; // hangi silah türüne vurduğumuza bağlı değişecek (WeaponType.cs)
    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        pickupPanel.SetActive(false);
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // oyuncunun bir şeye çarpıp çarpmadığını tespit etmek için sürekli olarak bu ışını dünyaya fırlatmasını istiyoruz
        // ışının sonunda, çizginin sonunda ve herhangi bir yerde bir küre çizer. o kürenin çarptığı şey daha sonra tespit edilir.
        // oyuncunun konumu, yarıçap, dünyada ileriye doğru hareket etsin, ne vuruldu?, max ne kadar ileri gitsin?, seçilen katman dışındakiler(~)
        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 30, ~excludeLayers)) // Pickups Layer'ındaki bir nesneye çarpıp çarpmadığını tespit edelim.
        {
            // oyuncunun konumu ile vurulan nesnenin konumu arasındaki mesafe 8'den küçükse ise panel açılsın
            if (Vector3.Distance(transform.position, hit.transform.position) < pickupDisplayDistance)
            {
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

                    // e'ye bastığımızda silahı alacağız ve SaveScript.cs dosyasına silaha sahip olduğumuzu kaydedeceğiz
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // hangi silah alınmışsa o silahın indeksindeki değer 1 olur
                        SaveScript.weaponAmts[objID]++;

                        audioPlayer.Play(); // silah alındığında ses oynatılacak

                        SaveScript.change = true; // silah toplandığı için

                        // silahı aldığımız için yok edeceğiz
                        Destroy(hit.transform.gameObject, 0.2f);
                    }
                }

                // yalnızca item etiketi olan nesneleri tespit ettiğimden emin olmak istiyorum.
                else if (hit.transform.gameObject.CompareTag("item")) // yalnızca bir item ise
                {
                    // ışının çarptığı nesne item ise panel açılsın
                    pickupPanel.SetActive(true);

                    // hangi item'ı işaret ettiğimizi tespit edebilmek için ItemsType.cs dosyasını kullanacağız.
                    objID = (int)hit.transform.gameObject.GetComponent<ItemsType>().chooseItem;
                    // ışının çarptığı item'a bağlı olarak başlığın ve görselin değişmesi için
                    mainImage.sprite = itemIcons[objID];
                    mainTitle.text = itemTitles[objID];


                    // e'ye bastığımızda item'ı alacağız ve SaveScript.cs dosyasına item'a sahip olduğumuzu kaydedeceğiz
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // hangi item alınmışsa o item'ın indeksindeki değer 1 olur
                        SaveScript.itemAmts[objID]++;

                        audioPlayer.Play(); // item alındığında ses oynatılacak

                        SaveScript.change = true; // item toplandığı için

                        // item'ı aldığımız için yok edeceğiz
                        Destroy(hit.transform.gameObject, 0.2f);
                    }


                }
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
