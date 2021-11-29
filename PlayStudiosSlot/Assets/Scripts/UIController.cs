using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameController gameController;
    public Text txtTotalWin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController)
        {
            txtTotalWin.text = gameController.TotalScore.ToString("n0");
        }
    }

    public void OnClickBtnSpin()
    {
        if (gameController)
        {
            gameController.StartSpin();
        }
    }
}
