using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
    private int tileSignifier;

    public Sprite xSprite;
    public Sprite oSprite;

    private Sprite gameSprite;


    // Start is called before the first frame update
    void Start()
    {
        DetermineTile();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }


}
