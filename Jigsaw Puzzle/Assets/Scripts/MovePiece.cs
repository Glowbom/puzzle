using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{

    public string pieceStatus = "idle";

    public Transform edgeParticles;

    public KeyCode placePiece;
    public KeyCode returnInv;

    public string checkPlacement;

    public float yDiff;
    public Vector2 invPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        invControl();

        if (pieceStatus == "pickedup")
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }

        if (Input.GetKeyDown(placePiece) && pieceStatus == "pickedup")
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
            GetComponent<Renderer>().sortingOrder = 0;
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
        GetComponent<Renderer>().sortingOrder = 10;
        invPos = transform.position;
    }

    void invControl()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && pieceStatus != "locked")
        {
            transform.position = new Vector2(transform.position.x - 0.4f, -3.7f);
            yDiff -= 0.4f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && pieceStatus != "locked")
        {
            transform.position = new Vector2(transform.position.x + 0.4f, -3.7f);
            yDiff += 0.4f;
        }

        if (Input.GetKeyDown(returnInv) && pieceStatus == "pickedup")
        {
            transform.position = new Vector2(invPos.x + yDiff, -3.7f);
            pieceStatus = ""; 
        }
    }

}
