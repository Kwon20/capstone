using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="DungeonGenerationData.asset",menuName ="DungeonGenertationData/Dungeon Data")]
public class DungeonGenarationData : ScriptableObject
{
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
