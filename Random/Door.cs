using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    }
    public DoorType doorType;
    private GameObject parents;
    public bool IsOpen = false;
    public Transform animator;
    private Animator doorAnimator;
    private void Start()
    {
        animator.gameObject.SetActive(true);
        IsOpen = false;
        doorAnimator = animator.GetComponent<Animator>();
        if(doorAnimator.enabled == true)
        {
            Debug.Log(gameObject.name + "Enabled Door Open");
            animator.GetComponent<Animator>().SetBool("IsPlay", true);
            IsOpen = true;
        }
    }
    void Update()
    {
        if(IsOpen == false)
            return;
        if(doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("EndAni"))
        {
            animator.gameObject.SetActive(false);
        }
    }
    public void Play()
    {
        Debug.Log(gameObject.name + "Door Open");
        animator.gameObject.SetActive(true);
        doorAnimator.enabled = true;
        IsOpen = true;
        doorAnimator.Play("Door");
        animator.GetComponent<Animator>().SetBool("IsPlay", true);
    }
    [System.Obsolete]
    public void GoToNextRoom()
    {

        parents = this.transform.parent.gameObject;

        switch (this.doorType)
        {
            case Door.DoorType.left:
                CameraController.instance.currRoom = parents.GetComponent<Room>().GetLeft();
                SettingDoor("RightDoor", DoorType.left);
                break;
            case Door.DoorType.right:
                CameraController.instance.currRoom = parents.GetComponent<Room>().GetRight();
                SettingDoor("LeftDoor", DoorType.right);
                break;
            case Door.DoorType.top:
                CameraController.instance.currRoom = parents.GetComponent<Room>().GetTop();
                SettingDoor("BottomDoor", DoorType.top);
                break;
            case Door.DoorType.bottom:
                CameraController.instance.currRoom = parents.GetComponent<Room>().GetBottom();
                SettingDoor("TopDoor", DoorType.bottom);
                break;
        }
        GameManager.instance.MoveRoom();
        SceneCtrl.instance.FindMonsterList();
    }
    void SettingDoor(string doorName, DoorType type)
    {
        
        RoomController.instance.currentRoom = CameraController.instance.currRoom;
        PlayerMovement.instance.MoveNextRoom(CameraController.instance.currRoom.transform.Find(doorName).gameObject, type);
        RoomController.instance.OffOtherRoom();
        CameraController.instance.SetBound();
        if(!(CameraController.instance.currRoom.X == 0 && CameraController.instance.currRoom.Y == 0))
            CameraController.instance.currRoom.GetComponent<Room>().CloseDoor();
        
    }
}
