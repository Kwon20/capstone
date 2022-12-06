using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    top = 0,
    left = 1,
    down = 2,
    right = 3
};
public class DungeonCrawlerController : MonoBehaviour
{

    public static List<Vector2Int> positionVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> directionMovemnetMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.top,Vector2Int.up },
        {Direction.left,Vector2Int.left },
        {Direction.down,Vector2Int.down },
        {Direction.right,Vector2Int.right }
    };
    public static List<Vector2Int> GenerateDungeon(DungeonGenarationData dungeonData)//방들의 좌표 생성 및 저장
    {
        positionVisited.Clear();
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();
        for(int i=0;i<dungeonData.numberOfCrawlers;i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }
        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);
        for(int i=0;i<iterations;)
        {
            foreach(DungeonCrawler dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovemnetMap);
                if (!positionVisited.Exists(x => x == newPos))
                {
                    if (newPos.x == 0 && newPos.y == 0)
                    {
                        continue;
                    }
                    positionVisited.Add(newPos);
                    RoomController.instance.roomList.Add(newPos);
                    i++;
                }
                //positionVisited.Add(newPos);
                //i++;
            }
        }
        
        return positionVisited;
    }
}
