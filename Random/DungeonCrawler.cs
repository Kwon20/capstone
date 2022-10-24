using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler : MonoBehaviour
{
    public Vector2Int Position { get; set; }
   public DungeonCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }
    public Vector2Int Move(Dictionary<Direction,Vector2Int> directrionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directrionMovementMap.Count);
        Position += directrionMovementMap[toMove];
        return Position;
    }
}
