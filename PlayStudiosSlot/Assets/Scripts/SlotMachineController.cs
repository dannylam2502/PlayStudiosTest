using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    public GameData gameDataRef;
    public List<Reel> reels;

    public List<float> reelStopTimes;
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

    public void Spin()
    {
        foreach (var reel in reels)
        {
            reel.SetState(ReelState.kSpinning);
        }
        StopAllCoroutines();
        var randomSpinResult = gameDataRef.GetRandomSpinDetailData();
        StartCoroutine(RoutineStopReelAndShowResult(randomSpinResult));
    }

    public IEnumerator RoutineStopReelAndShowResult(SpinDetailData spinResult)
    {
        for (int i = 0; i < reels.Count && i < reelStopTimes.Count; i++)
        {
            yield return new WaitForSeconds(reelStopTimes[i]);
            reels[i].SetStopIndex(spinResult.ReelIndex[i]);
            reels[i].SetState(ReelState.kSpinToResult);
            yield return StartCoroutine(RoutineWaitForTheReelToStop(i));
        }
        // Show Result
        if (spinResult.WinAmount > 0)
        {
            for (int i = 0; i < spinResult.ActiveReelCount; i++)
            {
                reels[i].AnimateWinSymbol();
            }
        }
        yield break;
    }

    public IEnumerator RoutineWaitForTheReelToStop(int currentReelIndex)
    {
        while (reels[currentReelIndex].GetReelState() != ReelState.kStopped)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
