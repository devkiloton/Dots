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

    public GameObject PathInstantiaterScript;

    public PathDrawner Path;

    public Text dotNumberText;
    private int dotNumber;
    public Canvas CanvasObject;

    private int level;
    private int matriceLine = 0;
    private int matriceColumn = 0;
    private int line = 4;
    private int column = 4;

    //tests
    public Text m_Text;

    void Start()
    {
        xDistanceBetweenDots = (float)Screen.width / 10 / 2;
        yDistanceBetweenDots = (float)Screen.height * 0.8f / 10 / 2;
        SetGridOrigin = new Vector2(0, Screen.height * 0.1f);

        level = LevelManagerScript.Level;

        dotsArray = new string[level];
        for (int i = 0; i < level; i++)
        {
            line = ListRandomizer();
            column = ListRandomizer();

            if (givenDots[line, column] != 1)
            {
                givenDots[line, column] = 1;
                dotsArray[i] = $"{line},{column}";
            }
            else
            {
                i--;
            }
        }

        //for()

        for (int i = 1; i < 20; i += 2)
        {
            for (int j = 1; j < 20; j += 2)
            {
                if (givenDots[matriceLine, matriceColumn] == 1)
                {
                    //Debug.Log($"this givenDotsArray: i0: {matriceLine},{matriceColumn}, i1: {matriceLine}, {matriceColumn}");
                    var instantiatedObject = Instantiate(Dot, new Vector2(xDistanceBetweenDots * i - (float)Screen.width / 2, yDistanceBetweenDots * j - (float)Screen.height * 0.8f / 2), Quaternion.identity);
                    instantiatedObject.name = $"{matriceLine},{matriceColumn}";
                }
                matriceColumn++;
            }
            matriceLine++;
            matriceColumn = 0;
        }
        

        for (int i = 0; i< level; i++)
        {
            var instantiatedDotText = Instantiate(dotNumberText, new Vector2(GameObject.Find($"{dotsArray[i]}").transform.position.x + Screen.width/2, GameObject.Find($"{dotsArray[i]}").transform.position.y + Screen.height * 0.8f /2 +Screen.height*0.1f), Quaternion.identity);
                    instantiatedDotText.name = $"{i+1}";
                    instantiatedDotText.text = $"{i+1}";
                    instantiatedDotText.transform.SetParent(CanvasObject.transform);
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
                //Debug.Log($"dotsArray: {dotsArray[0]}, {dotsArray[1]}, {dotsArray[2]}");
                //Debug.Log($"dotsArray: {dotsArray[0]}, {dotsArray[1]}, {dotsArray[2]}");
                Debug.Log("wrong way fella");
            }
            
            if(clickedDotsArray[i]!=null && clickedDotsArray[i]==dotsArray[i])
            {
                Vector2 vector = GameObject.Find(clickedDotsArray[i]).transform.position;
                positionsLines.Add(vector);
                positionsLinesArray = new Vector2[level];
                positionsLines.CopyTo(positionsLinesArray);

                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);

                    // Update the Text on the screen depending on current position of the touch each frame
                    m_Text.text = "Touch Position : " + touch.position;
                    Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
                    transform.position = position;
                    Path.PathMaker(positionsLinesArray[i], position);
                }
                else
                {
                    m_Text.text = "No touch contacts";
                    Path.PathMaker(positionsLinesArray[i], positionsLinesArray[i] );
                }

                /*Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = position;
                Path.PathMaker(positionsLinesArray[i], position);*/
            }

            
        }
    }

    void LateUpdate()
    {
        for(int i=0; i<level; i++)
        {
        if(clickedDotsArray[i]!=null && clickedDotsArray[i]==dotsArray[i] && clickedDots.Count > 1 && i+1 < level && clickedDotsArray[i+1]!=null)
            {
                //Debug.Log($"PositionLinesArray i{i}: {positionsLinesArray[i]}, i{i+1}: {positionsLinesArray[i+1]}");
                if (GameObject.Find($"Line{positionsLinesArray[i]} to {positionsLinesArray[i+1]}") == null)
                {
                    var pathInstantiaterInstance = Instantiate(PathInstantiaterScript, new Vector2(0,0), Quaternion.identity);
                    pathInstantiaterInstance.name = $"Line{positionsLinesArray[i]} to {positionsLinesArray[i+1]}";
                    var script = pathInstantiaterInstance.GetComponent<PathInstantiater>();//.PathInstantiaterFunction(positionsLinesArray[i], positionsLinesArray[i+1]);
                    script.Beginning = positionsLinesArray[i];
                    script.End = positionsLinesArray[i+1];
                }
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

        int[] randomInt = new int[level];
        randomInt = address.ToArray(); //.CopyTo(randomInt);

        int index = random.Next(address.Count);
        return randomInt[index];
    }
}
