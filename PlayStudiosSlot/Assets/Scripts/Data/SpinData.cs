using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpinData
{
    public List<SpinDetailData> Spins;
}

[Serializable]
public class SpinDetailData
{
    public List<int> ReelIndex;
    public int ActiveReelCount;
    public int WinAmount;
}
