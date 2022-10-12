using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClearConditions
{
    None,
    ArrivalClearPoint = 1,
    PressAllButton,
}

[System.Serializable]
public class StageData
{
    public int ButtonCount;
    public Collider[] ClearPoints;
}

[CreateAssetMenu(fileName = "StageDataSO", menuName = "Scriptable Object/StageDataSO")]
public class StageDataSO : ScriptableObject
{
    public StageData[] stageDatas;
}
