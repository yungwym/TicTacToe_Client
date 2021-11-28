using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
    private int tileSignifier;

    public Sprite xSprite;
    public Sprite oSprite;

    private Sprite gameSprite;


    public bool IsPlayersTurn = false;

    //Networked Client
    GameObject networkedClient;


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

    // Update is called once per frame
    void Update()
    {
        if (IsPlayersTurn)
        {
            CheckForInput();
        }
       
    }



    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Cicked");

            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TurnTaken + "");
        }

        IsPlayersTurn = false;
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
