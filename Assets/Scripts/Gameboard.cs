using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mark
{
    NONE,
    X,
    O
}




public class Gameboard : MonoBehaviour
{
    public static Gameboard gameBoardInstance;


    private int tileSignifier;

    public Mark PlayerMark;

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

        PlayerMark = Mark.NONE;

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
        bool hasWon = CheckForWin();

        if (hasWon)
        {
            Debug.Log("Player Has Won");
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.PlayerWin + "");
        }
        else
        {
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TurnTaken + "," + nodeID);
        }
        IsPlayersTurn = false;
    }

    public void SetTile(int tileSign)
    {
        tileSignifier = tileSign;

        if (tileSignifier == 1)
        {
            PlayerMark = Mark.X;
            playerSprite = xSprite;
            opponentSprite = oSprite;
        }
        else
        {
            PlayerMark = Mark.O;
            playerSprite = oSprite;
            opponentSprite = xSprite;
        }
    }
    public void PlaceOpponentNode(int nodeIndex)
    {
        Debug.Log(nodeIndex);

        nodes[nodeIndex].PlaceOpponentSprite();
        nodes[nodeIndex].isFull = true;
    }

    public bool CheckForWin()
    {
        return
        AreNodesMatched(0, 1, 2) || AreNodesMatched(3, 4, 5) || AreNodesMatched(6, 7, 8) ||
        AreNodesMatched(0, 3, 6) || AreNodesMatched(1, 4, 7) || AreNodesMatched(2, 5, 8) ||
        AreNodesMatched(0, 4, 8) || AreNodesMatched(2, 4, 6); 

    }

    private bool AreNodesMatched(int i, int j, int k)
    {
        Mark m = PlayerMark;

        bool isMatched = (nodes[i].NodeMark == m && nodes[j].NodeMark == m && nodes[k].NodeMark == m);
        return isMatched;
    }

}
