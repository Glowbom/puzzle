using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{

    public string pieceStatus = "idle";

    public Transform edgeParticles;

    public KeyCode placePiece;

    public string checkPlacement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pieceStatus == "pickedup")
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }

        if (Input.GetKeyDown(placePiece))
        {
            checkPlacement = "y";
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == gameObject.name && checkPlacement == "y")
        {
            other.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            transform.position = other.gameObject.transform.position;
            pieceStatus = "locked";
            Instantiate(edgeParticles, other.gameObject.transform.position, edgeParticles.rotation);
            checkPlacement = "n";

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        if (other.gameObject.name != gameObject.name && checkPlacement == "y")
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            checkPlacement = "n";
        }
    }

    private void OnMouseDown()
    {
        pieceStatus = "pickedup";
        checkPlacement = "n";
    }
}
