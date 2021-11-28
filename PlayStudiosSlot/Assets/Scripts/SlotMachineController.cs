using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    public GameData gameDataRef;
    public List<Reel> reels;
    // Start is called before the first frame update
    void Start()
    {
        var reelData = gameDataRef.ReelData;
        for (int i = 0; i < reelData.ReelStrips.Count && i < reels.Count; i++)
        {
            reels[i].LoadReelStrip(reelData.ReelStrips[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
