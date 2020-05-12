using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    public Dictionary<string, Sprite[]> sprites;

    public LongClick[] buttons;
    public LongClick[] mediumButtons;

    public GameObject easyGame;
    public GameObject mediumGame;

    public bool rotate = true;


    private int firstClickedIndex = -1;
    private int[] dataEasy;
    private int[] dataMedium;
    private int[] dataHard;

    private string keyMedium = "sanfranciso_medium";
    private string keyEasy = "sanfranciso";


    public void puzzleLongClicked(int index)
    {
        if (easyGame.activeInHierarchy)
        {
            buttons[index].gameObject.transform.eulerAngles = new Vector3(
            buttons[index].gameObject.transform.eulerAngles.x,
            buttons[index].gameObject.transform.eulerAngles.y,
            buttons[index].gameObject.transform.eulerAngles.z + 90
            );
        }

        if (mediumGame.activeInHierarchy)
        {
            mediumButtons[index].gameObject.transform.eulerAngles = new Vector3(
            mediumButtons[index].gameObject.transform.eulerAngles.x,
            mediumButtons[index].gameObject.transform.eulerAngles.y,
            mediumButtons[index].gameObject.transform.eulerAngles.z + 90
            );
        }

    }

    public void puzzleClicked(int index)
    {
        if (firstClickedIndex == -1)
        {
            firstClickedIndex = index;

            if (easyGame.activeInHierarchy)
            {
                var tempColor = buttons[firstClickedIndex].gameObject.GetComponent<Image>().color;
                tempColor.a = 0.5f;
                buttons[firstClickedIndex].gameObject.GetComponent<Image>().color = tempColor;
            }

            if (mediumGame.activeInHierarchy)
            {
                var tempColor = mediumButtons[firstClickedIndex].gameObject.GetComponent<Image>().color;
                tempColor.a = 0.5f;
                mediumButtons[firstClickedIndex].gameObject.GetComponent<Image>().color = tempColor;
            }
        }
        else
        {
            if (index == firstClickedIndex)
            {
                puzzleLongClicked(index);
                refreshUi();

                if (easyGame.activeInHierarchy)
                {
                    var tempColor = buttons[firstClickedIndex].gameObject.GetComponent<Image>().color;
                    tempColor.a = 1f;
                    buttons[firstClickedIndex].gameObject.GetComponent<Image>().color = tempColor;
                }

                if (mediumGame.activeInHierarchy)
                {
                    var tempColor = mediumButtons[firstClickedIndex].gameObject.GetComponent<Image>().color;
                    tempColor.a = 1f;
                    mediumButtons[firstClickedIndex].gameObject.GetComponent<Image>().color = tempColor;
                }

                firstClickedIndex = -1;
                return;
            }
            swap(index);
        }
    }

    public void swap(int index)
    {
        if (easyGame.activeInHierarchy)
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
        }

        if (mediumGame.activeInHierarchy)
        {
            var tempColor = mediumButtons[firstClickedIndex].gameObject.GetComponent<Image>().color;
            tempColor.a = 1f;
            mediumButtons[firstClickedIndex].gameObject.GetComponent<Image>().color = tempColor;


            float lastButtonRotation = mediumButtons[index].gameObject.transform.eulerAngles.z;
            float firstButtonRotation = mediumButtons[firstClickedIndex].gameObject.transform.eulerAngles.z;

            mediumButtons[firstClickedIndex].gameObject.transform.eulerAngles = new Vector3(
                mediumButtons[firstClickedIndex].gameObject.transform.eulerAngles.x,
                mediumButtons[firstClickedIndex].gameObject.transform.eulerAngles.y,
                lastButtonRotation
            );

            mediumButtons[index].gameObject.transform.eulerAngles = new Vector3(
                mediumButtons[index].gameObject.transform.eulerAngles.x,
                mediumButtons[index].gameObject.transform.eulerAngles.y,
                firstButtonRotation
            );
        }


            if (easyGame.activeInHierarchy)
        {
            int firstData = dataEasy[firstClickedIndex];
            int lastData = dataEasy[index];

            dataEasy[firstClickedIndex] = lastData;
            dataEasy[index] = firstData;
        }

        if (mediumGame.activeInHierarchy)
        {
            int firstData = dataMedium[firstClickedIndex];
            int lastData = dataMedium[index];

            dataMedium[firstClickedIndex] = lastData;
            dataMedium[index] = firstData;
        }


        refreshUi();

        firstClickedIndex = -1;
    }

    private void refreshUi()
    {
        if (easyGame.activeInHierarchy)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.GetComponent<Image>().sprite = sprites[keyEasy][dataEasy[i]];
            }
        }

        if (mediumGame.activeInHierarchy)
        {
            for (int i = 0; i < mediumButtons.Length; i++)
            {
                mediumButtons[i].gameObject.GetComponent<Image>().sprite = sprites[keyMedium][dataMedium[i]];
            }
        }

    }

    private void loadResources()
    {
        sprites = new Dictionary<string, Sprite[]>();

        string[] spriteImages =
        {
            keyEasy,
            keyMedium,
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

        easyGame.SetActive(true);
        mediumGame.SetActive(false);

        dataEasy = new int[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            dataEasy[i] = i;
        }

        dataMedium = new int[mediumButtons.Length];
        for (int i = 0; i < mediumButtons.Length; i++)
        {
            dataMedium[i] = i;
        }


        refreshUi();
    }

    public void mediumPressed()
    {
        easyGame.SetActive(false);
        mediumGame.SetActive(true);

        for (int i = 0; i < mediumButtons.Length; i++)
        {
            dataMedium[i] = i;
        }

        List<int> easyList = new List<int>(dataMedium);
        for (int i = 0; i < mediumButtons.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, easyList.Count);
            dataMedium[i] = easyList[randomIndex];
            easyList.RemoveAt(randomIndex);

            if (rotate)
            {
                int randomRotation = UnityEngine.Random.Range(0, 3);
                mediumButtons[i].gameObject.transform.eulerAngles = new Vector3(
                    mediumButtons[i].gameObject.transform.eulerAngles.x,
                    mediumButtons[i].gameObject.transform.eulerAngles.y,
                    randomRotation * 90f
                );
            }

        }


        refreshUi();
    }

    public void easyPressed()
    {
        easyGame.SetActive(true);
        mediumGame.SetActive(false);

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
