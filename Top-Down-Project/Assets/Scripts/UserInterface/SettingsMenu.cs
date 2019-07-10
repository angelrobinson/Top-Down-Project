using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    //volume UI components
    [Header("SFX")]
    [SerializeField] Slider masterVol;
    [SerializeField] Slider soundVol;
    [SerializeField] Slider musicVol;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AnimationCurve volToDecibel = default;

    //graphics UI Components
    [Header("VFX")]
    [SerializeField] TMP_Dropdown resolution;
    [SerializeField] TMP_Dropdown quality;
    [SerializeField] Toggle fullScreen;

    //Buttons
    [Header("General")]
    [SerializeField] Button acceptButton;
    [SerializeField] Button back;

    //other variables
    List<Resolution> resolutions;

    private void Awake()
    {
        //get components if not assigned in inspector
        if (mixer == null)
        {
            mixer = Resources.Load("Audio/AudioMixer") as AudioMixer;
        }

        if (masterVol == null)
        {
            masterVol = transform.Find("MasterVol").GetComponent<Slider>();           
        }

        if (soundVol == null)
        {
            soundVol = transform.Find("SoundVol").GetComponent<Slider>();
        }

        if (musicVol == null)
        {
            musicVol = transform.Find("MusicVol").GetComponent<Slider>();
        }

        if (resolution == null)
        {
            resolution = transform.Find("Resolution").GetComponent<TMP_Dropdown>();
        }

        if (quality == null)
        {
            quality = transform.Find("Quality").GetComponent<TMP_Dropdown>();
        }

        if (fullScreen == null)
        {
            fullScreen = transform.Find("FullScreen").GetComponent<Toggle>();
        }

        if (acceptButton == null)
        {
            acceptButton = transform.Find("AcceptButton").GetComponent<Button>();
        }

        if (back == null)
        {
            back = transform.Find("BackButton").GetComponent<Button>();
        }

        //defaults
        resolutions = Screen.resolutions.ToList();

        /*Get Dropdown Info*/

        #region Resolution
        //clear current/default options
        resolution.ClearOptions();

        //create empty list for option data        
        List<string> resOptions = new List<string>();

        //format the option data text and add to list
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resOptions.Add(string.Format("{0} x {1}", resolutions[i].width, resolutions[i].height));
        }

        //add list of option data to the dropdown
        resolution.AddOptions(resOptions);
        #endregion

        #region Quality
        //clear current/default options
        quality.ClearOptions();

        //add quality options
        quality.AddOptions(QualitySettings.names.ToList());
        #endregion

        


    }

    private void OnEnable()
    {
        //find current resolution
        TMP_Dropdown.OptionData current = new TMP_Dropdown.OptionData
        {
            text = string.Format("{0} x {1}", Screen.currentResolution.width, Screen.currentResolution.height)
        };
        
        //compare current to resolution drop down options and pick correct one
        resolution.value = resolution.options.FindIndex((i) => { return i.text.Equals(current.text); });
        
        //look for value of "Master Volume", "Sound Volume", "Music Volume" in player prefs, if not there set default to Max
        masterVol.value = PlayerPrefs.GetFloat("Master Volume", masterVol.maxValue);
        mixer.SetFloat("MasterVol", volToDecibel.Evaluate(masterVol.value));
        soundVol.value = PlayerPrefs.GetFloat("Sound Volume", soundVol.maxValue);
        mixer.SetFloat("SoundVol", volToDecibel.Evaluate(soundVol.value));
        musicVol.value = PlayerPrefs.GetFloat("Music Volume", musicVol.maxValue);
        mixer.SetFloat("MusicVol", volToDecibel.Evaluate(musicVol.value));
        

        fullScreen.isOn = Screen.fullScreen;

#if UNITY_EDITOR
        EditorWindow window = UnityEditor.EditorWindow.focusedWindow;
        window.maximized = Screen.fullScreen;
#endif


        quality.value = resolution.options.FindIndex((i) => { return i.text.Equals(QualitySettings.GetQualityLevel()); });
        acceptButton.interactable = false;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    #region Helper Methods

    public void OnAccept()
    {
        //update full screen
        Screen.fullScreen = fullScreen.isOn;

        //set resolultion
        Screen.SetResolution(resolutions[resolution.value].width,resolutions[resolution.value].height, fullScreen.isOn);

        //set quality
        //TODO: not saving correctly in build
        QualitySettings.SetQualityLevel(quality.value);


        //set Master Volume
        PlayerPrefs.SetFloat("Master Volume", masterVol.value);
        mixer.SetFloat("MasterVol", volToDecibel.Evaluate(masterVol.value));
        //set Sound volume
        PlayerPrefs.SetFloat("Sound Volume", soundVol.value);
        mixer.SetFloat("SoundVol", volToDecibel.Evaluate(soundVol.value));
        //set musti volume
        PlayerPrefs.SetFloat("Music Volume", musicVol.value);
        mixer.SetFloat("MusicVol", volToDecibel.Evaluate(musicVol.value + 1));
    }
    public void OnFullScreenChange()
    {
#if UNITY_EDITOR
        EditorWindow window = UnityEditor.EditorWindow.focusedWindow;
        window.maximized = !window.maximized;
#endif
    }
#endregion
}
