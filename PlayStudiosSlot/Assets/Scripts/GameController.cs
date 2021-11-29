using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlotMachineController slotMachineController;

    public int TotalScore { get; set; }

    public delegate void OnSpinEnd(int amountWin);
    // Start is called before the first frame update
    void Start()
    {
        if (slotMachineController)
        {
            slotMachineController.SetOnSpinEndCallback(OnSpinEndCallback);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Time.timeScale += 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Time.timeScale -= 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            TotalScore = 0;
        }
#endif
    }

    public void StartSpin()
    {
        if (slotMachineController)
        {
            slotMachineController.Spin();
        }
    }

    public void OnSpinEndCallback(int winAmount)
    {
        TotalScore += winAmount;
    }
}
