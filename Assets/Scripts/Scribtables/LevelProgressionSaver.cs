using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Create Level Progression Asset",menuName ="ScriptableObjects/LevelProgressionSaver")]
public class LevelProgressionSaver : ScriptableObject
{
    [SerializeField]
    private int _currentLevel = 1;

    [SerializeField]
    private List<GameObject> _levels=new List<GameObject>();
    public int CurrentLevel { get { return _currentLevel; } }

    public int LevelAmmount { get { return _levels.Count; } }

    public void ResetToLastAvailableLevel() 
    {
        _currentLevel = LevelAmmount;
    }

    public void IncreaceCurrentLevel() 
    {
        _currentLevel += 1;
    }
    public GameObject GetCurrentLevelGeometry() 
    {
        return _levels[_currentLevel - 1];
    }

    public void ResetGame() 
    {
        _currentLevel = 1;
    }
}
