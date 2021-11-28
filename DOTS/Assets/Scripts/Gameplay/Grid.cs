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
    public GameObject CanvasObject;

    private bool lastLine = false;

    private int lastLineIndex;

    private int level;
    private int matriceLine = 0;
    private int matriceColumn = 0;
    private int line = 4;
    private int column = 4;

    private float time = 3;
    private float timeSinceBeginning;

    void Start()
    {
        var EmptyObj = new GameObject();
        var instantiatedDotEmpty = Instantiate(EmptyObj, new Vector2(0,0), Quaternion.identity);
        Destroy(EmptyObj);
        CanvasObject = GameObject.Find("Canvas");
        instantiatedDotEmpty.transform.SetParent(CanvasObject.transform);
        instantiatedDotEmpty.name = "DotNumbers";
        //CanvasObject = GameObject.Find("Canvas").transform.GetChild(1).gameObject;

        Path = GameObject.Find("PathDrawner").GetComponent<PathDrawner>();
        xDistanceBetweenDots = (float)Screen.width / 10 / 2;
        yDistanceBetweenDots = (float)Screen.height * 0.8f / 10 / 2;
        SetGridOrigin = new Vector2(0, Screen.height * 0.1f);

        LevelManagerScript = GameObject.Find("LevelManager").GetComponent<LevelManager>();

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

        var EmptyObj2 = new GameObject();
        var instantiatedDotEmpty2 = Instantiate(EmptyObj2, new Vector2(0,0), Quaternion.identity);
        instantiatedDotEmpty2.name = "Dots";
        Destroy(EmptyObj2);
        for (int i = 1; i < 20; i += 2)
        {
            for (int j = 1; j < 20; j += 2)
            {
                if (givenDots[matriceLine, matriceColumn] == 1)
                {
                    var instantiatedObject = Instantiate(Dot, new Vector2(xDistanceBetweenDots * i - (float)Screen.width / 2, yDistanceBetweenDots * j - (float)Screen.height * 0.8f / 2), Quaternion.identity);
                    instantiatedObject.name = $"{matriceLine},{matriceColumn}";
                    instantiatedObject.transform.SetParent(GameObject.Find("Dots").transform);
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
                    instantiatedDotText.transform.SetParent(GameObject.Find("Canvas").transform.GetChild(4).gameObject.transform);
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
                lastLine = true;
                lastLineIndex = i;
                GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);

                if(LevelManagerScript.ContinueVariable)
                {
                    ContinueGame(lastLineIndex);
                }
            }
            
            if(clickedDotsArray[i]!=null && clickedDotsArray[i]==dotsArray[i])
            {
                Vector2 vector = GameObject.Find(clickedDotsArray[i]).transform.position;
                positionsLines.Add(vector);
                positionsLinesArray = new Vector2[level];
                positionsLines.CopyTo(positionsLinesArray);

                /*if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
                    transform.position = position;
                    Path.PathMaker(positionsLinesArray[i], position);
                }
                else
                {
                    Path.PathMaker(positionsLinesArray[i], positionsLinesArray[i] );
                }*/

                Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = position;
                Path.PathMaker(positionsLinesArray[i], position);
            }

            
        }
    }

    void LateUpdate()
    {
        for(int i=0; i<level; i++)
        {
        if(clickedDotsArray[i]!=null && clickedDotsArray[i]==dotsArray[i] && clickedDots.Count > 1 && i+1 < level && clickedDotsArray[i+1]!=null)
            {
                if (GameObject.Find($"Line{i}") == null)
                {
                    var pathInstantiaterInstance = Instantiate(PathInstantiaterScript, new Vector2(0,0), Quaternion.identity);
                    pathInstantiaterInstance.name = $"Line{i}";
                    var script = pathInstantiaterInstance.GetComponent<PathInstantiater>(); //.PathInstantiaterFunction(positionsLinesArray[i], positionsLinesArray[i+1]);
                    script.Beginning = positionsLinesArray[i];
                    script.End = positionsLinesArray[i+1];
                    if(lastLine)
                    {
                        Destroy(pathInstantiaterInstance);
                    }
                }
            }
        }
        
        if(clickedDotsArray[level-1] == dotsArray[level-1])
        {
            StartCoroutine(Fade());
        }

        if(LevelManagerScript.RestartVariable)
        {
            LevelManagerScript.RestartVariable = false;
            level=1;
            GridReloader(level);
        }
    }

    private int ListRandomizer()
    {
        int address;
        if(level >10)
        {
            address = UnityEngine.Random.Range( 0,10 );
        }
        else
        {
            address = UnityEngine.Random.Range( 2,8 );
        }
        return address;
    }

    public void GridReloader(int level)
    {
        LevelManagerScript.Level= level;
        this.gameObject.name = "PastGrid";
        Destroy(GameObject.Find("Dots"));
        Destroy(GameObject.Find("Canvas").transform.GetChild(4).gameObject);
        for(int i = 0; i<level; i++)
        {
            Destroy(GameObject.Find($"Line{i}"));
        }
        Path.PathMaker(new Vector2(0,0), new Vector2(0,0));
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void ContinueGame(int indexArray)
    {
        LevelManagerScript.ContinueVariable = false;
        lastLine = false;
        if(clickedDots.Contains(clickedDotsArray[indexArray]))
        {
            clickedDots.Remove(clickedDotsArray[indexArray]);
        }
        clickedDotsArray[indexArray] = null;
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator Fade() 
    {

        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        
        timeSinceBeginning += Time.deltaTime;
        GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Text>().text = $"{(int)(time - timeSinceBeginning + 1)}";
        if(LevelManagerScript.NextLevelVariable)
        {
            LevelManagerScript.NextLevelVariable = false;
            yield return new WaitForSeconds(0.1f);
            StartNextLevel();
        }
        else
        {
            yield return new WaitForSeconds(3);
            StartNextLevel();
        }
        
    }

    public void StartNextLevel()
    {
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        level++;
        GridReloader(level);
    }
}
