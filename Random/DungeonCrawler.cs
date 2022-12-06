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
    public Vector2Int Move(Dictionary<Direction,Vector2Int> directrionMovementMap) //상하좌우로 한칸씩 이동후 현재 좌표 리턴
    {
        Direction toMove = (Direction)Random.Range(0, directrionMovementMap.Count);
        Position += directrionMovementMap[toMove];
        return Position;
    }
}
