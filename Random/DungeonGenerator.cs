using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    
    public DungeonGenarationData dungeonGenarationData;
    private List<Vector2Int> dungeonRooms;
    private void Awake()
    {
      
    }
    private void Start()
    {
        //·Îµù ½ÃÀÛ½ÃÁ¡
        
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenarationData); //방의 좌표들을 저장
        
        SpawnRooms(dungeonRooms,dungeonRooms.Count);
    }
    private void SpawnRooms(IEnumerable<Vector2Int> rooms,int count) //방 생성
    {

        RoomController.instance.LoadRoom("Start", 0, 0);
        List<int> roomList = new List<int>();
        for(int i=2;i<=RoomController.instance.GetNumMap();)//2~전체 방번호 까지 추가후 셔플
        {
            roomList.Add(i++);
        }
        ShuffleList(roomList);

        roomList.Add(1);            //보스방으로 가는 텔레포트방 번호가 1번
        foreach (int item in roomList)
        {
            Debug.Log(item.ToString());
        }
        int index = 0;
        foreach (Vector2Int roomLocation in rooms) //큐에 방이름/방좌표를 저장
        {
            if (index+1 > roomList.Count)
                break;
            RoomController.instance.LoadRoom("Empty "+roomList[index++].ToString(), roomLocation.x, roomLocation.y);
          
        }
        
    }
    public void ShuffleList(List<int> list)
    {
        int random1;
        int random2;

        int tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }

}
