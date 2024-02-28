using System;
using System.Collections.Generic;
using System.Linq;
using Core.Events;
using Core.Misc;
using Core.SceneObjects;
using GameInput;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float sensitivity = 1.0f;
    [SerializeField] private LevelGenerator generator;
    //[SerializeField] private float gravity;
    private CharacterController _controller;
    [SerializeField] private GameObject trailPart;
    private readonly List<Transform> _cubes = new List<Transform>();
    private Camera _camera;
    private Vector3 _dir;
    private Vector3 _characterStartPos, _cameraStartPos;
    public bool isMoving;
    //public float curLine;
    public float speed;

    private float _cameraOffset;
    // Start is called before the first frame update
    void Start() 
    {
       _camera = GetComponentInChildren<Camera>();
       _cameraOffset = transform.position.z - _camera.transform.position.z;
       _controller = GetComponentInChildren<CharacterController>();
       _characterStartPos = _controller.transform.localPosition.Copy();
       _cameraStartPos = _camera.transform.localPosition.Copy();
       _cubes.Add(_controller.GetComponentInChildren<CubeMoney>().transform);
    }

    private void OnEnable()
    {
        SwipeController.SwipeEvent += MoveToLine;
        InteractEvents.OnMoneyInteract += AddCube;
        InteractEvents.OnLetInteract += LetInteract;
    }

    private void OnDisable()
    {
        SwipeController.SwipeEvent -= MoveToLine;
        InteractEvents.OnMoneyInteract -= AddCube;
        InteractEvents.OnLetInteract -= LetInteract;
    }

    //ToDo FixedUpdate
    void Update()
    { 
        if (isMoving) 
        {
            var controllerPos = _controller.transform.position;
            var cameraTransform = _camera.transform;
            _dir.z = speed;
            //_dir.y += -gravity * Time.fixedDeltaTime;
            var newPos = cameraTransform.position;
            newPos.z = controllerPos.z - _cameraOffset;
            cameraTransform.position = newPos;
            _controller.Move(_dir * Time.fixedDeltaTime);
        } 
    }    


    void MoveToLine(Vector2 direction)
    {
        if (isMoving)
        {
            var difference = direction.x * sensitivity;
            var groundWidth = generator.Level.lineWidth * generator.Level.linesCount;
            var moveLength = (difference / Screen.width) * groundWidth ;
            var moveVector = new Vector3
            {
                x = moveLength
            };
            var maxMovePosition = generator.Level.lineWidth * (int)(generator.Level.linesCount / 2);
            if (_controller.transform.position.x + moveLength < -maxMovePosition)
            {
                 var distanceToMaxMovePosition =  -maxMovePosition  - _controller.transform.position.x;
                 moveVector.x = distanceToMaxMovePosition;
               //moveVector.x = -maxMovePosition;
            }
            else if (_controller.transform.position.x + moveLength > maxMovePosition)
            {
                var distanceToMaxMovePosition =  maxMovePosition  - _controller.transform.position.x;
                moveVector.x = distanceToMaxMovePosition;
                //moveVector.x = +maxMovePosition;
            }

            _controller.Move(moveVector);
        }
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


    private void AddCube(CubeMoney cubeMoney)
    {
        var transform1 = cubeMoney.transform;
        _cubes.Add(transform1);
        foreach (Transform child in _controller.transform)
        {
            var newPos = child.position;
            newPos.y += transform1.localScale.y;
            child.position = newPos;
        }
        transform1.parent = _controller.transform;
        transform1.localPosition = new Vector3(0, generator.Level.types[0].localScale.y / 2 + transform1.localScale.y / 2, 0);
        transform1.GetComponent<Rigidbody>().isKinematic = false;
        transform1.GetComponent<Collider>().isTrigger = false;
        // cubeMoney.transform.SetAsFirstSibling();
        cubeMoney.isCharacterCube = true;
        // cubeMoney.transform.AddComponent<LayoutElement3D>();

        //Cubes[0].SetAsLastSibling();
    }

    private void LetInteract(LetCube cube, CubeMoney cubeMoney)
    {
        if (_cubes.Count < 2)
        {
            Die();
        }
        else
        {
            //var lastPosition = cubeMoney.transform.position;
            _cubes.Remove(cubeMoney.transform);
            cubeMoney.isCharacterCube = false;
            cubeMoney.transform.parent = cube.Column.Let.Ground;
            //cubeMoney.transform.position = lastPosition;
        }
    }
    private void Die()
    {
        Debug.Log("Died");
        CharacterEvents.PlayerDied();
        // #if UNITY_EDITOR
        //         UnityEditor.EditorApplication.isPlaying = false;
        // #elif UNITY_WEBPLAYER
        //         Application.OpenURL(webplayerQuitURL);
        // #else
        //         Application.Quit();
        // #endif
    }

    public void ResetCharacter()
    {
        _controller.transform.localPosition = _characterStartPos;
        _camera.transform.localPosition = _cameraStartPos;
        isMoving = true;
    }
}