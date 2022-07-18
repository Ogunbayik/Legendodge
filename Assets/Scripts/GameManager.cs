using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerController player;
    private void Awake()
    {
        Instance = this;
        player = GetComponent<PlayerController>();
    }

    public enum GameStates
    {
        Start,
        InGame,
        GameOver,
        NextLevel
    }

    public GameStates currentState;

    public enum GamePanel
    {
        StartPanel,
        InGamePanel,
        GameOverPanel,
        NextLevelPanel
    }

  
    void Start()
    {
        currentState = GameStates.Start;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case GameStates.Start: GameStart();
                break;
            case GameStates.InGame: GameInGame();
                break;
            case GameStates.GameOver: GameOver();
                break;
            case GameStates.NextLevel: GamePassLevel();
                break;
        }

        Debug.Log(currentState);
    }

    private void GameStart()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentState = GameStates.InGame;
        }
    }

    private void GameInGame()
    {

    }

    private void GameOver()
    {

    }

    private void GamePassLevel()
    {

    }

}
