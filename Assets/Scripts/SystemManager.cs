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

    //End ConditionPanel
    GameObject winConditionPanel;
    GameObject loseConditionPanel;

    //Player Msg Panel and UI
    GameObject playerMsgPanel;
    GameObject prefixedMsg1;
    GameObject prefixedMsg2;
    GameObject prefixedMsg3;
    GameObject prefixedMsg4;

    GameObject customMsgInputField;
    GameObject customMsgSendButton;

    //Opponent Msg Panel and UI


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

            //End Condition Panels
            else if (go.name == "WinPanel")
                winConditionPanel = go;

            else if (go.name == "LosePanel")
                loseConditionPanel = go;

            //Msg Panel
            else if (go.name == "PlayerMsgPanel")
                playerMsgPanel = go;

            else if (go.name == "PrefixedMsg1")
                prefixedMsg1 = go;

            else if (go.name == "PrefixedMsg2")
                prefixedMsg2 = go;

            else if (go.name == "PrefixedMsg3")
                prefixedMsg3 = go;

            else if (go.name == "PrefixedMsg4")
                prefixedMsg4 = go;

            else if (go.name == "CustomMsgInputField")
                customMsgInputField = go;

            else if (go.name == "SendButton")
                customMsgSendButton = go;
        }

        prefixedMsg1.GetComponent<Button>().onClick.AddListener(SendPrefixed1);
        prefixedMsg2.GetComponent<Button>().onClick.AddListener(SendPrefixed2);
        prefixedMsg3.GetComponent<Button>().onClick.AddListener(SendPrefixed3);
        prefixedMsg4.GetComponent<Button>().onClick.AddListener(SendPrefixed4);

        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        joinGameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
      
        loginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginTogglePressed);
        createToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateTogglePressed);

        ChangeState(GameStates.LoginMenu);
    }


    public void LoginTogglePressed(bool newValue)
    {
        createToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void CreateTogglePressed(bool newValue)
    {
        loginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void SendPrefixed1()
    {
        string buttonTxt = prefixedMsg1.GetComponent<Text>().text;
        SendMessageToOpponent(buttonTxt);
    }

    public void SendPrefixed2()
    {
        string buttonTxt = prefixedMsg2.GetComponent<Text>().text;
        SendMessageToOpponent(buttonTxt);
    }

    public void SendPrefixed3()
    {
        string buttonTxt = prefixedMsg3.GetComponent<Text>().text;
        SendMessageToOpponent(buttonTxt);
    }

    public void SendPrefixed4()
    {
        string buttonTxt = prefixedMsg3.GetComponent<Text>().text;
        SendMessageToOpponent(buttonTxt);
    }

    public void SendMessageToOpponent(string msg)
    {
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerMessage + "," + msg);
        Debug.Log(msg);
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

    public void JoinGameRoomButtonPressed()
    {
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.JoinQueueForGameRoom + "");
        ChangeState(GameStates.WaitingInQueueForOtherPlayers);
    }

    
    public void ChangeState(int newState)
    {
        basePanel.SetActive(false);
        loginPanel.SetActive(false);
        joinGameRoomPanel.SetActive(false);
        waitingPanel.SetActive(false);
        gameboard.SetActive(false);
        winConditionPanel.SetActive(false);
        loseConditionPanel.SetActive(false);

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

        else if (newState == GameStates.GameWin)
        {
            winConditionPanel.SetActive(true);
        }

        else if (newState == GameStates.GameLose)
        {
            loseConditionPanel.SetActive(true);
        }
    }

}


public static class GameStates
{

    public const int LoginMenu = 1;

    public const int MainMenu = 2;

    public const int WaitingInQueueForOtherPlayers = 3;

    public const int Game = 4;

    public const int GameWin = 5;

    public const int GameLose = 6;

}
