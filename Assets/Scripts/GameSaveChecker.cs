using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveChecker : MonoBehaviour
{
    public LevelProgressionSaver LevelSaver;
    // Start is called before the first frame update
    void Start()
    {
        if (LevelSaver.CurrentLevel == 1) 
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
