using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    //Base UI
    GameObject basePanel;

    //Login Panel Variables 
    GameObject loginPanel;

    GameObject submitButton;
    GameObject usernameText;
    GameObject passwordText;
    GameObject usernameInput;
    GameObject passwordInput;
    GameObject loginToggle;
    GameObject createToggle;

   //Join Game Room Panel
    GameObject joinGameRoomPanel;
    GameObject currentUserNumber;
    GameObject joinGameRoomButton;

    //WaitingPanel
    GameObject waitingPanel;

    GameObject playGameButton;

    //Networked Client
    GameObject networkedClient;

    //Gameboard
    GameObject gameboard;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            //Get Base Ui Elements
            if (go.name == "BaseUIPanel")
                basePanel = go;

            //Get all LoginPanel Elements
            else if (go.name == "LoginPanel")
                loginPanel = go;
            else if (go.name == "UserField")
                usernameInput = go;
            else if (go.name == "PasswordField")
                passwordInput = go;
            else if (go.name == "UserText")
                usernameText = go;
            else if (go.name == "PassText")
                passwordText = go;
            else if (go.name == "LoginToggle")
                loginToggle = go;
            else if (go.name == "CreateToggle")
                createToggle = go;
            else if (go.name == "SubmitButton")
                submitButton = go;

            //Get all joinGameRoomPanel Elements
            else if (go.name == "JoinGameRoomPanel")
                joinGameRoomPanel = go;
            else if (go.name == "CurrentUserNumber")
                currentUserNumber = go;
            else if (go.name == "JoinGameRoomButton")
                joinGameRoomButton = go;

            //Get PlayGamePanel Elements
            else if (go.name == "PlayGameButton")
                playGameButton = go;

            //Get WaitingPanel Elements
            else if (go.name == "WaitingPanel")
                waitingPanel = go;

            //Get NetworkedClient 
            else if (go.name == "NetworkedClient")
                networkedClient = go;

            else if (go.name == "GameBoard")
                gameboard = go;

        }

        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        joinGameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
       // playGameButton.GetComponent<Button>().onClick.AddListener(PlayGameButtonPressed);

        loginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginTogglePressed);
        createToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateTogglePressed);

        ChangeState(GameStates.LoginMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubmitButtonPressed()
    {
        //Send Login Info to Server
        Debug.Log("Submit Clicked");

        string user = usernameInput.GetComponent<InputField>().text;
        string pass = passwordInput.GetComponent<InputField>().text;

        string msg;

        if (createToggle.GetComponent<Toggle>().isOn)

            msg = ClientToServerSignifiers.CreateAccount + "," + user + "," + pass;
        else
            msg = ClientToServerSignifiers.Login + "," + user + "," + pass;


        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);

        Debug.Log(msg);
    }



    public void PlayGameButtonPressed()
    {
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayGame + "");
        ChangeState(GameStates.Game);
    }

    public void JoinGameRoomButtonPressed()
    {
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.JoinQueueForGameRoom + "");
        ChangeState(GameStates.WaitingInQueueForOtherPlayers);
    }

    public void LoginTogglePressed(bool newValue)
    {
        createToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void CreateTogglePressed(bool newValue)
    {
        loginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void ChangeState(int newState)
    {
        basePanel.SetActive(false);
        loginPanel.SetActive(false);
        joinGameRoomPanel.SetActive(false);
        waitingPanel.SetActive(false);
        gameboard.SetActive(false);

        if (newState == GameStates.LoginMenu)
        {
            basePanel.SetActive(true);
            loginPanel.SetActive(true);
        }
        else if (newState == GameStates.MainMenu)
        {
            basePanel.SetActive(true);
            joinGameRoomPanel.SetActive(true);
        }
        else if (newState == GameStates.WaitingInQueueForOtherPlayers)
        {
            basePanel.SetActive(true);
            waitingPanel.SetActive(true);
        }

        else if (newState == GameStates.Game)
        {
            gameboard.SetActive(true);
        }
    }

}


public static class GameStates
{

    public const int LoginMenu = 1;

    public const int MainMenu = 2;

    public const int WaitingInQueueForOtherPlayers = 3;

    public const int Game = 4;

}
