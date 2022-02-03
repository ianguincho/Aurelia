using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyBoardConfig", menuName = "KeyBoardConfig")]
public class KeyBoardConfig : ScriptableObject
{
    public KeyCode jump, pause;

    public KeyCode checkKey(string key)
    {
        switch (key)
        {
            case "Jump":
                return jump;
            case "PauseMenu":
                return pause;
            default:
                return KeyCode.None;
        }
    }
}
