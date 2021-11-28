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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);

            if (collider.bounds.Contains(mousePos) && isFull == false)
            {
                Debug.Log("Place Sprite");

                PlaceSprite();

                gameboard.PlayerHasTakenTurn(nodeID);
            }
        }
    }


    private void PlaceSprite()
    {
        spriteRenderer.sprite = gameboard.gameSprite;
    }
}
