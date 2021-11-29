using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameData : MonoBehaviour
{
    public TextAsset reelDataJsonFile;
    public TextAsset spinDataJsonFile;

    public ReelData ReelData { get; private set; }
    public SpinData SpinData { get; private set; }
    public void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        ReelData = JsonConvert.DeserializeObject<ReelData>(reelDataJsonFile.text);
        SpinData = JsonConvert.DeserializeObject<SpinData>(spinDataJsonFile.text);
    }


    public SpinDetailData GetRandomSpinDetailData()
    {
        var random = Random.Range(0, SpinData.Spins.Count);
        return SpinData.Spins[random];
    }
}
