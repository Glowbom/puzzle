using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    public Dictionary<string, Sprite[]> sprites;

    public LongClick[] buttons;
    public bool rotate = true;


    private int firstClickedIndex = -1;
    private int[] dataEasy;
    private string key = "sanfranciso";
        
    public void puzzleLongClicked(int index)
    {
        buttons[index].gameObject.transform.eulerAngles = new Vector3(
            buttons[index].gameObject.transform.eulerAngles.x,
            buttons[index].gameObject.transform.eulerAngles.y,
            buttons[index].gameObject.transform.eulerAngles.z + 90
        );
    }

    public void puzzleClicked(int index)
    {
        if (firstClickedIndex == -1)
        {
            firstClickedIndex = index;

            var tempColor = buttons[firstClickedIndex].gameObject.GetComponent<Image>().color;
            tempColor.a = 0.5f;
            buttons[firstClickedIndex].gameObject.GetComponent<Image>().color = tempColor;
        }
        else
        {
            if (index == firstClickedIndex)
            {
                puzzleLongClicked(index);
                refreshUi();
                var tempColor = buttons[firstClickedIndex].gameObject.GetComponent<Image>().color;
                tempColor.a = 1f;
                buttons[firstClickedIndex].gameObject.GetComponent<Image>().color = tempColor;

                firstClickedIndex = -1;
                return;
            }
            swap(index);
        }
    }

    public void swap(int index)
    {
        var tempColor = buttons[firstClickedIndex].gameObject.GetComponent<Image>().color;
        tempColor.a = 1f;
        buttons[firstClickedIndex].gameObject.GetComponent<Image>().color = tempColor;


        float lastButtonRotation = buttons[index].gameObject.transform.eulerAngles.z;
        float firstButtonRotation = buttons[firstClickedIndex].gameObject.transform.eulerAngles.z;

        buttons[firstClickedIndex].gameObject.transform.eulerAngles = new Vector3(
            buttons[firstClickedIndex].gameObject.transform.eulerAngles.x,
            buttons[firstClickedIndex].gameObject.transform.eulerAngles.y,
            lastButtonRotation
        );

        buttons[index].gameObject.transform.eulerAngles = new Vector3(
            buttons[index].gameObject.transform.eulerAngles.x,
            buttons[index].gameObject.transform.eulerAngles.y,
            firstButtonRotation
        );

        int firstData = dataEasy[firstClickedIndex];
        int lastData = dataEasy[index];

        dataEasy[firstClickedIndex] = lastData;
        dataEasy[index] = firstData;

        refreshUi();

        firstClickedIndex = -1;
    }

    private void refreshUi()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.GetComponent<Image>().sprite = sprites[key][dataEasy[i]];
        }
    }

    private void loadResources()
    {
        sprites = new Dictionary<string, Sprite[]>();

        string[] spriteImages =
        {
            "sanfranciso",
        };

        foreach (string path in spriteImages)
        {
            if (!sprites.ContainsKey(path))
            {
                sprites.Add(path, Resources.LoadAll<Sprite>("Textures/" + path));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        loadResources();

        dataEasy = new int[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            dataEasy[i] = i;
        }

        refreshUi();
    }

    public void easyPressed()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            dataEasy[i] = i;
        }

        List<int> easyList = new List<int>(dataEasy);
        for (int i = 0; i < buttons.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, easyList.Count);
            dataEasy[i] = easyList[randomIndex];
            easyList.RemoveAt(randomIndex);

            if (rotate)
            {
                int randomRotation = UnityEngine.Random.Range(0, 3);
                buttons[i].gameObject.transform.eulerAngles = new Vector3(
                    buttons[i].gameObject.transform.eulerAngles.x,
                    buttons[i].gameObject.transform.eulerAngles.y,
                    randomRotation * 90f
                );
            }

        }


        refreshUi();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
