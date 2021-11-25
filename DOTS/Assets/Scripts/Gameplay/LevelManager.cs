using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public int Level;
    public GameObject GridObject;
    private int levelValidation;

    public Text LevelText;
    void Awake()
    {
        Level = 2;
        levelValidation = Level;
    }

    // Update is called once per frame
    void Update()
    {
        LevelText.text = $"Level {Level}";
        if(Level != levelValidation)
        {
            levelValidation = Level;
            var instantiatedObject = Instantiate(GridObject, new Vector2(1.3f,0), Quaternion.identity);
            instantiatedObject.name = $"Grid";
        }
    }
}
