using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorType : MonoBehaviour
{
    public enum typeOfDoor
    {
        cabinet,
        house,
        cabin
    }

    public typeOfDoor chooseDoor;
    public bool opened = false; // kapı açık mı?
    public bool locked = false; // kapı kilitli mi?
}
