using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GearManager : MonoBehaviour
{
    List<GearSlot> gears;
    TextMeshProUGUI UIBaloontext;

    // Start is called before the first frame update
    void Start()
    {
        gears = new List<GearSlot>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Upper Slot"))
        {
            gears.Add(obj.GetComponent<GearSlot>());
            obj.GetComponent<GearSlot>().gearSettedAddListener(CheckGears);

        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Lower Slot"))
        {
            gears.Add(obj.GetComponent<GearSlot>());
            obj.GetComponent<GearSlot>().gearSettedAddListener(CheckGears);

        }
        UIBaloontext = GameObject.FindGameObjectWithTag("UIBaloonText").GetComponent<TextMeshProUGUI>();
    }

    void CheckGears()
    {
        bool toRotate = true;

        foreach (GearSlot gear in gears)
        {
            if (!gear.isGearSetted())
            {
                toRotate = false;
                break;
            }
        }

        foreach (GearSlot gear in gears)
        {
            gear.setRotate(toRotate);
        }

        if (toRotate)
        {
            UIBaloontext.text = "YAY, PARABENS. TASK CONCLUIDA!";
        }
        else
        {
            UIBaloontext.text = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM!";
        }
    }

   
}
