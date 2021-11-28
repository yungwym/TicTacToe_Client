using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

   
    [SerializeField] private int nodeID;

    public bool isFull = false;

    private Collider2D collider;
    private SpriteRenderer spriteRenderer;


    private Gameboard gameboard;

    // Start is called before the first frame update
    void Start()
    {
        gameboard = Gameboard.gameBoardInstance;


        collider = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameboard.IsPlayersTurn)
        {
            CheckForInput();
        }
    }


    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;

            if (collider.bounds.Contains(mousePos) && isFull == false)
            {
                PlaceSprite();

                gameboard.PlayerHasTakenTurn(nodeID);
            }
        }
    }


    private void PlaceSprite()
    {
        gameboard.PlaceSprite(transform, transform.rotation);
    }
}
