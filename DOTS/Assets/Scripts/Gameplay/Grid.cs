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
    private List<string> Dots = new List<string>();
    //You need to associate this array to the collisions

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
            //Debug.Log(line);
            //Debug.Log(column);
            //Debug.Log(givenDots[line, column]);

            if (givenDots[line, column] != 1)
            {
                givenDots[line, column] = 1;
                Dots.Add($"{line},{column}");
            }
            else
            {
                i--;
            }
        }

        string[] dotsArray = Dots.ToArray();
        for(int x=0;x<Dots.Count;x++)
        {
            Debug.Log(dotsArray[x]);
        }


        for (int i = 1; i < 20; i += 2)
        {
            //Debug.Log(matriceLine);
            for (int j = 1; j < 20; j += 2)
            {
                //Debug.Log($"instantiate loop: {matriceLine},{matriceColumn} = {givenDots}");
                //Debug.Log(givenDots[matriceLine, matriceColumn]);
                if (givenDots[matriceLine, matriceColumn] == 1)
                {
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

    }

    public int ListRandomizer()
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
