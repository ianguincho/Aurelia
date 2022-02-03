using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public KeyBoardConfig KeyBoardConfig;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
        DontDestroyOnLoad(this);
    }

    public bool keyDown(string key)
    {
        if (Input.GetKeyDown(KeyBoardConfig.checkKey(key))) return true;
        else return false;
    }
}
