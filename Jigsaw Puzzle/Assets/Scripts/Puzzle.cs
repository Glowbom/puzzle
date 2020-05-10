using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    public Button[] buttons;

    public void puzzleClicked(int index)
    {
        buttons[index].gameObject.transform.eulerAngles = new Vector3(
            buttons[index].gameObject.transform.eulerAngles.x,
            buttons[index].gameObject.transform.eulerAngles.y,
            buttons[index].gameObject.transform.eulerAngles.z + 90
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
