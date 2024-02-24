using System.Collections.Generic;
using Core.SceneObjects;
using GameInput;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float sensetivity = 1.0f;
    [SerializeField] private LevelInfo level;
    [SerializeField] private float gravity;
    private CharacterController controller;
    [SerializeField] private GameObject trailPart;
    public List<Transform> Cubes;
    private Camera _camera;
    private Vector3 dir;
    
    public bool isMoving;
    //public float curLine;
    public float speed;

    private float camera_offset;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        camera_offset = transform.position.z - _camera.transform.position.z;
        controller = GetComponentInChildren<CharacterController>();
        SwipeController.SwipeEvent += MoveToLine;
    }

    void MoveToLine(Vector2 direction)
    {
        var difference = direction.x * sensetivity;
        var groundWidth = level.lineWidth * level.linesCount;
        var moveLength = (difference / Screen.width) * groundWidth ;
        var moveVector = new Vector3
        {
            x = moveLength
        };
        var maxMovePosition = level.lineWidth * (int)(level.linesCount / 2);
        if (controller.transform.position.x + moveLength < -maxMovePosition)
        {
             var distanceToMaxMovePosition =  -maxMovePosition  - controller.transform.position.x;
             moveVector.x = distanceToMaxMovePosition;
           //moveVector.x = -maxMovePosition;
        }
        else if (controller.transform.position.x + moveLength > maxMovePosition)
        {
            var distanceToMaxMovePosition =  maxMovePosition  - controller.transform.position.x;
            moveVector.x = distanceToMaxMovePosition;
            //moveVector.x = +maxMovePosition;
        }

        controller.Move(moveVector);
        // if (direction == Vector2.left)
        // {
        //     if (curLine > 0)
        //     {
        //         curLine--;
        //         controller.Move( new Vector3(line_width * -1, 0, 0));
        //     }
        // }
        // else if (direction == Vector2.right)
        // {
        //     if (curLine < 2)
        //     {
        //         curLine++;
        //         controller.Move(new Vector3(line_width, 0, 0));
        //     }
        // }
        // else if (direction == Vector2.up)
        // {
        //     Jump();
        // }
        //controller.Move(direction);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            var controllerPos = controller.transform.position;
            var cameraTransform = _camera.transform;
            dir.z = speed;
            dir.y += -gravity * Time.fixedDeltaTime;
            var newPos = cameraTransform.position;
            newPos.z = controllerPos.z - camera_offset;
            cameraTransform.position = newPos;
            controller.Move(dir * Time.fixedDeltaTime);
        }
    }

    public void AddCube(CubeMoney cubeMoney)
    {
        Cubes.Add(cubeMoney.transform);
        cubeMoney.transform.parent = controller.transform;
        cubeMoney.transform.SetAsFirstSibling();
        //Cubes[0].SetAsLastSibling();
    }

    public void Die()
    {
        Debug.Log("Died");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
        #else
                Application.Quit();
        #endif
    }

    public void RemoveCube(CubeMoney cubeMoney)
    {
        Cubes.Remove(cubeMoney.transform);
    }
}