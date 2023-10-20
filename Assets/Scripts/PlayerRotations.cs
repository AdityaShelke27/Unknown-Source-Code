using System;
using UnityEditor;
using UnityEngine;

public class PlayerRotations : MonoBehaviour
{
    public float sensitivity = 300f;
    public float HorRot;
    public float HorizontalRot;
    public float VerRot;
    public float VerticalRot;
    public float MaxWallRunCameraTilt, WallRunCameraTilt;
    private PlayerMovement Movement;

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HorizontalRot = transform.rotation.y;
        VerticalRot = transform.GetChild(0).rotation.z;
    }

    void Update()
    {
        HorRot = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        VerRot = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;
        HorizontalRot += HorRot;
        VerticalRot -= VerRot;
        VerticalRot = Mathf.Clamp(VerticalRot, -90f, 90f);
        transform.eulerAngles = new Vector3(0,HorizontalRot,0);
        transform.GetChild(0).localEulerAngles = new Vector3(VerticalRot, 0, WallRunCameraTilt);
       
        

        if (Math.Abs(WallRunCameraTilt) < MaxWallRunCameraTilt && Movement.isWallRunning && Movement.isWallRight)
        {
            WallRunCameraTilt += Time.deltaTime * MaxWallRunCameraTilt * 2;
        }
            
        if (Math.Abs(WallRunCameraTilt) < MaxWallRunCameraTilt && Movement.isWallRunning && Movement.isWallLeft)
        {
            WallRunCameraTilt -= Time.deltaTime * MaxWallRunCameraTilt * 2;
        }
            
        //Tilts camera back again
        if (WallRunCameraTilt > 0 && !Movement.isWallRight && !Movement.isWallLeft)
        {
            WallRunCameraTilt -= Time.deltaTime * MaxWallRunCameraTilt * 2;
        }
            
        if (WallRunCameraTilt < 0 && !Movement.isWallRight && !Movement.isWallLeft)
        {
            WallRunCameraTilt += Time.deltaTime * MaxWallRunCameraTilt * 2;
        }
                       
    }
}
