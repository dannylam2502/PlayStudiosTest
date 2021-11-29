using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public SlotMachineController slotMachineController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBtnSpin()
    {
        if (slotMachineController)
        {
            slotMachineController.Spin();
        }
    }
}
