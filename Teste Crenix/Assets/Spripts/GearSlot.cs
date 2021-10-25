using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GearSlot : MonoBehaviour  
{

    GameObject gear;

    bool toRotate;

    UnityEvent gearSetted;

    private void Awake()
    {
        gearSetted = new UnityEvent();
    }

    private void Start()
    {
        toRotate = false;

    }

    public void setGear(GameObject gearInstance)
    {
        gear = gearInstance;
        gearSetted.Invoke();
    }

    private void Update()
    {
        if (toRotate)
        {
            if (tag == "Upper Slot")
            {
                gear.transform.Rotate(new Vector3(0, 0, -60) * Time.deltaTime);
            }
            else
            {
                gear.transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);
            }
        }
    }
    public void gearSettedAddListener(UnityAction action)
    {
        gearSetted.AddListener(action);
    }

    public void setRotate(bool rotate)
    {
        toRotate = rotate;
    }

    public bool isGearSetted()
    {
        return gear != null;
    }
}
