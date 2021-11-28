using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
    public static Gameboard gameBoardInstance;


    private int tileSignifier;

    public Sprite xSprite;
    public Sprite oSprite;

    public Sprite playerSprite;
    public Sprite opponentSprite;

    public bool IsPlayersTurn = false;

    [SerializeField] private Node[] nodes;

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


        //nodes = FindObjectsOfType<Node>();
    }
  
    public void PlayerHasTakenTurn(int nodeID)
    {
        Debug.Log(nodeID);
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TurnTaken + "," + nodeID);
        IsPlayersTurn = false;
    }

    public void SetTile(int tileSign)
    {
        tileSignifier = tileSign;

        if (tileSignifier == 1)
        {
            playerSprite = xSprite;
            opponentSprite = oSprite;
            Debug.Log("X's");
        }
        else
        {
            playerSprite = oSprite;
            opponentSprite = xSprite;
            Debug.Log("O's");
        }
    }
    public void PlaceOpponentNode(int nodeIndex)
    {
        Debug.Log(nodeIndex);

        nodes[nodeIndex].PlaceOpponentSprite();
        nodes[nodeIndex].isFull = true;
    }

}
