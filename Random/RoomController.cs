using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}
public class RoomController : MonoBehaviour
{
    public enum RoomState
    {
        Basement1, Basement2, Basement3, Basement4, Basement5
    }
    public static RoomController instance;
    public RoomState roomState;
    const int numMap = 12;
    string currentWorldName = "Basement";
    RoomInfo currentLoadRoomData;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    public Room currentRoom;
    bool isLoadingRoom = false;
    bool isUpdate = false;
    public List<Vector2> roomList = new List<Vector2>();
    EventFromPlayerAndDungeonToMinimap eventMinimap;
    bool isMinimapCreated;
    bool isLoadEnd;
    int count;
    // Start is called before the first frame update
    public bool DoesRoomExist(int x, int y)//좌표에 이미 방이 존재하는 지 확인하는 
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
    private void Awake()
    {
        isLoadEnd = false;
        if (instance == null)
        { instance = this; }
        roomList.Add(new Vector2(0, 0));

    }
    private void Start()
    {
        switch (roomState)
        {
            case RoomState.Basement1:
                currentWorldName = "Basement";
                break;
            case RoomState.Basement2:
                currentWorldName = "Basement2";
                break;

            case RoomState.Basement3:
                currentWorldName = "Basement3";
                break;
        }
        eventMinimap = new EventFromPlayerAndDungeonToMinimap();
        UIController.GetInstance().FindUIGameObject("MinimapController").TryGetComponent<EventFromPlayerAndDungeonToMinimap>(out eventMinimap);
        isMinimapCreated = false;
    }
    private void Update()
    {
        UpdateRoomQueue();
        UpdateDoor();
        OffRoom();
        if (count >= numMap)
        {
            eventMinimap.InitRoomIcon();
            return;
        }
        count++;
    }
    void OffRoom()
    {
        if (isLoadingRoom)
            return;
        else
            RoomController.instance.OffOtherRoom();

    }
    void UpdateRoomQueue()//큐에 들어있는 방들을 모두 
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if (isLoadEnd)
            {
                isLoadEnd = false;
                GameManager.instance.StartLoading();
            }
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        if (currentLoadRoomData.name.Equals("Empty 1"))
        {
            isLoadEnd = true;
        }
    }
    public void LoadRoom(string name, int x, int y)//정보에 따라 큐에 방을 추가
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;
        loadRoomQueue.Enqueue(newRoomData);
    }
    IEnumerator LoadRoomRoutine(RoomInfo info)//방의 이름으로 씬불러오기
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
        
    }
    public void RegisterRoom(Room room)//방의 좌표에 따라 방을 이동
    {
        room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);

        room.X = currentLoadRoomData.X;
        room.Y = currentLoadRoomData.Y;
        room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + " " + room.Y;
        room.transform.parent = transform;

        isLoadingRoom = false;
        if (loadedRooms.Count == 0)
        {
            CameraController.instance.currRoom = room;
            RoomController.instance.currentRoom = room;
            RoomController.instance.OffOtherRoom();
            CameraController.instance.SetBound();
        }
        loadedRooms.Add(room);
    }

    public int GetNumMap()
    { return numMap; }

    public void UpdateDoor()//문 방향에 다른 방이 없다면 문을 파괴
    {
        if (isLoadingRoom)
        {
            return;
        }
        else if (!isUpdate)
        {
            foreach (Room item in loadedRooms)
            {
                int x, y;
                x = item.X;
                y = item.Y;
                if (!DoesRoomExist(x - 1, y))
                {
                    Destroy(item.transform.Find("LeftDoor").gameObject);
                }
                if (!DoesRoomExist(x + 1, y))
                {
                    Destroy(item.transform.Find("RightDoor").gameObject);
                }
                if (!DoesRoomExist(x, y + 1))//
                {
                    Destroy(item.transform.Find("TopDoor").gameObject);
                }
                if (!DoesRoomExist(x, y - 1))//
                {
                    Destroy(item.transform.Find("BottomDoor").gameObject);
                }
            }
            ////////////////
            {
                MakeDugeonMinimap();
            }
            ////////////////
            isUpdate = true;
        }
    }

    public void OffOtherRoom()//현재방을 제외한 나머지 방을 끄는 함수
    {

        foreach (Room item in loadedRooms)
        {
            if (item.GetComponent<Room>().X != currentRoom.X || item.GetComponent<Room>().Y != currentRoom.Y)
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                item.gameObject.SetActive(true);
            }
        }
        if(PlayerMovement.instance.playerBoard != null)
        PlayerMovement.instance.playerBoard.InitPlayerBoard();
    }

    private void MakeDugeonMinimap()
    {
        if (eventMinimap.GetIsMinimapCreated() == true)
            return;
        eventMinimap.InitalEventData(0, instance.loadedRooms);
        eventMinimap.SetIsMinimapCreated();
    }
}
