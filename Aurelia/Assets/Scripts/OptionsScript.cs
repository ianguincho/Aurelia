using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsScript : MonoBehaviour
{
    public Toggle fullscreenToggle, vsyncToggle;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        }
        else
            vsyncToggle.isOn = true;

        bool foundNativeRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundNativeRes = true;
                selectedResolution = i;
                updateResLabel();
            }
        }

        if(!foundNativeRes)
        {
            ResItem weirdRes = new ResItem();
            weirdRes.horizontal = Screen.width;
            weirdRes.vertical = Screen.height;

            resolutions.Add(weirdRes);
            selectedResolution = resolutions.Count - 1;
            updateResLabel();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resLeft()
    {
        if (selectedResolution <= 0)
        {
            selectedResolution = resolutions.Count - 1;
        }
        else
            selectedResolution--;

        updateResLabel();
    }

    public void resRight()
    {
        if (selectedResolution >= resolutions.Count - 1)
        {
            selectedResolution = 0;
        }
        else
            selectedResolution++;

        updateResLabel();
    }

    public void updateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }

    public void applyGraphics()
    {

        if (vsyncToggle.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenToggle.isOn);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}