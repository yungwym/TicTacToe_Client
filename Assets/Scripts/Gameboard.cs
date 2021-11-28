using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
    public static Gameboard gameBoardInstance;


    private int tileSignifier;

    public Sprite xSprite;
    public Sprite oSprite;

    public Sprite gameSprite;


    public bool IsPlayersTurn = false;

    //Networked Client
    GameObject networkedClient;


    private void Awake()
    {
        if (gameBoardInstance != null)
        {
            return;
        }
        gameBoardInstance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.name == "NetworkedClient")
                networkedClient = go;
        }
    }
  
    public void PlayerHasTakenTurn(int nodeID)
    {
        Debug.Log(nodeID);
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TurnTaken + "");
        IsPlayersTurn = false;
    }

    public void SetTile(int tileSign)
    {
        tileSignifier = tileSign;

        if (tileSignifier == 1)
        {
            gameSprite = oSprite;
            Debug.Log("X's");
        }
        else
        {
            gameSprite = oSprite;
            Debug.Log("O's");
        }
    }

}
