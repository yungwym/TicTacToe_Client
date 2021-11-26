using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    // UI Variables 
    GameObject submitButton;
    GameObject usernameText;
    GameObject passwordText;
    GameObject usernameInput;
    GameObject passwordInput;
    GameObject loginToggle;
    GameObject createToggle;

    GameObject gameRoomButton;
    GameObject playGameButton;

    //Member Variables 
    GameObject networkedClient;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.name == "UserField")
                usernameInput = go;
            else if (go.name == "PasswordField")
                passwordInput = go;
            else if (go.name == "UserText")
                usernameText = go;
            else if (go.name == "PassText")
                passwordText = go;
            else if (go.name == "SubmitButton")
                submitButton = go;
            else if (go.name == "LoginToggle")
                loginToggle = go;
            else if (go.name == "CreateToggle")
                createToggle = go;
            else if (go.name == "NetworkedClient")
                networkedClient = go;
            else if (go.name == "GameRoomButton")
                gameRoomButton = go;
            else if (go.name == "PlayGameButton")
                playGameButton = go;
        }

        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
       // gameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
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
        submitButton.SetActive(false);
        usernameInput.SetActive(false);
       
        usernameText.SetActive(false);
        passwordText.SetActive(false);
        loginToggle.SetActive(false);
        createToggle.SetActive(false);
        passwordInput.SetActive(false);
        //  gameRoomButton.SetActive(false);

        //playGameButton.SetActive(false);


        if (newState == GameStates.LoginMenu)
        {
            submitButton.SetActive(true);
            usernameInput.SetActive(true);
            passwordInput.SetActive(true);
            usernameText.SetActive(true);
            passwordText.SetActive(true);
            loginToggle.SetActive(true);
            createToggle.SetActive(true);
        }
        else if (newState == GameStates.MainMenu)
        {
           // gameRoomButton.SetActive(true);
        }
        else if (newState == GameStates.Game)
        {
            //playGameButton.SetActive(true);
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
