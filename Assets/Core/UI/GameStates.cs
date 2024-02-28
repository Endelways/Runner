using System;
using Core.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private LevelGenerator level;
    [SerializeField] private GameObject startMenu, loseMenu;
    private void Start()
    {
        character.isMoving = false;
        startMenu.SetActive(true);
        loseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        CharacterEvents.PlayerDied += LoseGame;
    }

    private void OnDisable()
    {
        CharacterEvents.PlayerDied -= LoseGame;
    }

    public void StartGame()
    {
        character.isMoving = true;
        startMenu.SetActive(false);
    }

    public void LoseGame()
    {
        character.isMoving = false;
        loseMenu.SetActive(true);
    }

    public void ResetGame()
    {
        var curScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(curScene.name);
        //SceneManager.UnloadSceneAsync(curScene);

        // level.ResetLevel();
        // character.ResetCharacter();
        // loseMenu.SetActive(false);
    }
}
