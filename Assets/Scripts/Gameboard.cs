using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
    public static Gameboard gameBoardInstance;


    private int tileSignifier;

    public Sprite xSprite;
    public Sprite oSprite;

    private Sprite gameSprite;


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

    public void PlaceSprite(Transform spriteTransform, Quaternion spriteQuaternion)
    {
        Instantiate(gameSprite, spriteTransform.position, spriteQuaternion);
    }


    private void DetermineTile()
    {
        if (tileSignifier == 1)
        {
            gameSprite = xSprite;
        }
        else
        {
            gameSprite = oSprite;
        }
    }


    public void SetTile(int tileSign)
    {
        tileSignifier = tileSign;

        if (tileSignifier == 1)
        {
            Debug.Log("X's");
        }
        else
        {
            Debug.Log("O's");
        }
    }

}
