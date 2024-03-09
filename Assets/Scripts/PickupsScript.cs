using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsScript : MonoBehaviour
{
    // vurulan her şeyi kaydetmemiz gerekiyor
    private RaycastHit hit; // vurulan herhangi bir nesneyi depolayacak
    public LayerMask excludeLayers; // seçilen Layer'lar görmezden gelinir. sadece pickups layer'ını seçmedim. seçilen katmanlardaki nesneler algılanmayacak

    private int objID = 0; // hangi silah türüne vurduğumuza bağlı değişecek (WeaponType.cs)

    // Start is called before the first frame update
    void Start()
    {

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
                // hangi silahı işaret ettiğimizi tespit edebilmek için WeaponType.cs dosyasını kullanacağız.
                objID = (int)hit.transform.gameObject.GetComponent<WeaponType>().chooseWeapon;
                Debug.Log("Hit weapon: " + objID);
            }
        }
    }
}
