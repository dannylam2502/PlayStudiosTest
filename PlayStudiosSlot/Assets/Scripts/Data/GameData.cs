using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameData : MonoBehaviour
{
    public TextAsset jsonFile;

    public ReelData ReelData { get; private set; }
    public void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        ReelData = JsonConvert.DeserializeObject<ReelData>(jsonFile.text);
    }

}
