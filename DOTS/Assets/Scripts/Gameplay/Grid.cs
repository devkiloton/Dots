using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public LevelManager LevelManagerScript;

    public Vector2 SetGridOrigin;
    public GameObject Dot;

    private float xDistanceBetweenDots;
    private float yDistanceBetweenDots;

    private int[,] givenDots = new int[10, 10];
    private List<string> listGivenDots = new List<string>();
    public HashSet<string> clickedDots = new HashSet<string>();
    private string[] clickedDotsArray;
    private string[] dotsArray;
    public HashSet<Vector2> positionsLines = new HashSet<Vector2>();
    private Vector2[] positionsLinesArray;

    public PathDrawner Path;

    private int level;
    private int matriceLine = 0;
    private int matriceColumn = 0;
    private int line = 4;
    private int column = 4;

    void Start()
    {
        xDistanceBetweenDots = (float)Screen.width / 10 / 2;
        yDistanceBetweenDots = (float)Screen.height * 0.8f / 10 / 2;
        SetGridOrigin = new Vector2(0, Screen.height * 0.1f);

        level = LevelManagerScript.Level;

        for (int i = 0; i < level; i++)
        {
            line = ListRandomizer();
            column = ListRandomizer();

            if (givenDots[line, column] != 1)
            {
                givenDots[line, column] = 1;
                listGivenDots.Add($"{line},{column}");
            }
            else
            {
                i--;
            }
        }

        dotsArray = new string[listGivenDots.Count];
        dotsArray = listGivenDots.ToArray();

        //for(int i = 0; i< clickedDots.Count; i++)
        //{
        //    clickedDotsArray[i] = "Empty";
        //}

        for (int i = 1; i < 20; i += 2)
        {
            for (int j = 1; j < 20; j += 2)
            {
                if (givenDots[matriceLine, matriceColumn] == 1)
                {
                    Debug.Log($"this givenDotsArray: i0: {matriceLine},{matriceColumn}, i1: {matriceLine}, {matriceColumn}");
                    var instantiatedObject = Instantiate(Dot, new Vector2(xDistanceBetweenDots * i - (float)Screen.width / 2, yDistanceBetweenDots * j - (float)Screen.height * 0.8f / 2), Quaternion.identity);
                    instantiatedObject.name = $"{matriceLine},{matriceColumn}";
                }
                matriceColumn++;
            }
            matriceLine++;
            matriceColumn = 0;
        }
    }
    void Update()
    {
        level = LevelManagerScript.Level;

        clickedDotsArray = new string[level];
        clickedDots.CopyTo(clickedDotsArray);

        for(int i=0; i<level; i++)
        {
            if(clickedDotsArray[i]!=null && clickedDotsArray[i]!=dotsArray[i])
            {
                Debug.Log("wrong way fella");
            }
            
            if(clickedDotsArray[i]!=null && clickedDotsArray[i]==dotsArray[i])
            {
                Vector2 vector = GameObject.Find(clickedDotsArray[i]).transform.position;
                positionsLines.Add(vector);
                positionsLinesArray = new Vector2[level];
                positionsLines.CopyTo(positionsLinesArray);
            }

            if(clickedDotsArray[i]!=null && clickedDotsArray[i]==dotsArray[i] && clickedDots.Count > 1 && i+1 < level)
            {

                Path.PathMaker(positionsLinesArray[i], positionsLinesArray[i+1]);
            }
        }
    }

    private int ListRandomizer()
    {
        List<int> address;
        var random = new System.Random();
        if(level >10)
        {
            address = new List<int>{ 0,1,2,3,4,5,6,7,8,9 };
        }
        else
        {
            address = new List<int>{ 2,3,4,5,6,7 };
        }
        int index = random.Next(address.Count);
        return address[index];
    }
}
