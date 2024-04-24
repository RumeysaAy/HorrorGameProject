using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleThrow : MonoBehaviour
{
    public float rotationSpeed = 0.5f; // dönüş hızı
    public float throwPower = 40f; // fırlatma gücü (şişeyi fırlatmak için uygulanan güç)
    public GameObject bottleObj; // fırlatılacak şişe nesnesi
    public Transform throwPoint; // atış noktası (şişenin fırlatılacağı nokta)

    // Update is called once per frame
    void Update()
    {
        // fırlatılacak olan şişenin rotasyonunun hesaplanması

        float HorizontalRotation = Input.GetAxis("Mouse X") * 2;
        float VerticalRotation = Input.GetAxis("Mouse Y") * 2;
        // çizgiyi kamerayla aynı hizada tutacak. Bu yapılmazsa oluşturulan çizgi dünyada kalacak ve etrafta hareket etmeyecek.
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, HorizontalRotation * rotationSpeed, VerticalRotation * rotationSpeed));

        // Farenin yukarı veya aşağı hareket ettirilmesine bağlı olarak mesafenin ayarlanması
        // atış mesafesinin kontrol edilmesi

        // fareyi ileriye doğru hareket ettirdiğimde
        if (Input.GetAxis("Mouse Y") > 0)
        {
            if (throwPower < 70)
            {
                throwPower += 6 * Time.deltaTime;
            }
        }

        // fareyi geriye doğru hareket ettirdiğimde
        if (Input.GetAxis("Mouse Y") < 0)
        {
            if (throwPower > 20)
            {
                throwPower -= 12 * Time.deltaTime;
            }
        }
    }
}
