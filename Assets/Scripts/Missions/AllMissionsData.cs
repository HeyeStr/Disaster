using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Missions Content", menuName = "Missions/Allmissions")]

public class AllMissionsData : ScriptableObject
{
    [Header("博客基本信息")]
    public List<MissionInformation> AllMisions;
    public int missionindex;

}


