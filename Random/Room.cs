using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType roomType = RoomType.NORMAL;
    public bool isCreated;
    public bool isVisited;
    public int Width;
    public int Height;
    public int X;
    public int Y;
    // Start is called before the first frame update
    public bool isDoorOpen = true;

    
    void Start()
    {
        isVisited = false;
        if (RoomController.instance==null)
        {
            Debug.Log("You pressed play in the wrong Scene!");
            return;
        }
        RoomController.instance.RegisterRoom(this);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }
   
    public Room GetLeft()
    {
        return RoomController.instance.loadedRooms.Find(item=>item.X==X - 1&&item.Y== Y);
    }
    public Room GetRight()
    {
        return RoomController.instance.loadedRooms.Find(item => item.X == X + 1 && item.Y == Y);
    }
    public Room GetTop()
    {
        return RoomController.instance.loadedRooms.Find(item => item.X == X && item.Y == Y+1);
    }
    public Room GetBottom()
    {
        return RoomController.instance.loadedRooms.Find(item => item.X == X  && item.Y == Y-1);
    }
    public void OpenDoor()
	{
       if(this.transform.Find("LeftDoor")!=null)
		{
            //this.transform.Find("LeftDoor").gameObject.SetActive(true);
            this.transform.Find("LeftDoor").GetComponentInChildren<Door>().Play();
        }
        if (this.transform.Find("RightDoor") != null)
        {
            //this.transform.Find("RightDoor").gameObject.SetActive(true);
            this.transform.Find("RightDoor").GetComponentInChildren<Door>().Play();
        }
        if (this.transform.Find("TopDoor") != null)
        {
            //this.transform.Find("TopDoor").gameObject.SetActive(true);
            this.transform.Find("TopDoor").GetComponentInChildren<Door>().Play();
        }
        if (this.transform.Find("BottomDoor") != null)
        {
            //this.transform.Find("BottomDoor").gameObject.SetActive(true);
            this.transform.Find("BottomDoor").GetComponentInChildren<Door>().Play();
        }
    }
    public void CloseDoor()
	{
        if (this.transform.Find("MonsterList") == null)
            return;
        if (this.transform.Find("LeftDoor") != null)
        {
            this.transform.Find("LeftDoor").gameObject.SetActive(true);
        }
        if (this.transform.Find("RightDoor") != null)
        {
            this.transform.Find("RightDoor").gameObject.SetActive(true);
        }
        if (this.transform.Find("TopDoor") != null)
        {
            this.transform.Find("TopDoor").gameObject.SetActive(true);
        }
        if (this.transform.Find("BottomDoor") != null)
        {
            this.transform.Find("BottomDoor").gameObject.SetActive(true);
        }
    }
}
