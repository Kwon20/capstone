using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="DungeonGenerationData.asset",menuName ="DungeonGenertationData/Dungeon Data")]
public class DungeonGenarationData : ScriptableObject
{
    public int numberOfCrawlers;    //크롤러의 갯수
    public int iterationMin;        //방의 최소갯수
    public int iterationMax;        //방의 최대갯수
}
