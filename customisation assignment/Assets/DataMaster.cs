using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataMaster : MonoBehaviour
{
    GameData saveData = new GameData();

    [SerializeField] private string saveName;
    [SerializeField] private Dropdown saveDropDown;
    [SerializeField] public static string characterName;
    [SerializeField] public static int[] hairCut;
    [SerializeField] public static int[][] characterStats;
    [SerializeField] public static int characterPointPool;
    [SerializeField] public static int characterClass;
    [SerializeField] public static int race;
    [SerializeField] private bool runload;
    [SerializeField] public static List<string> saveList = new List<string>();

    public void Start()
    {
        runload = false;

        if (saveList.Count == 0)
        {
            saveDropDown.captionText.text = "No Characters!";
            saveDropDown.interactable = false;
        }
        else
        {
            saveDropDown.ClearOptions();
            saveDropDown.AddOptions(saveList);
        }

        if (GameSceneManager.loadCharacter)
        {
            LoadThings(PlayerPrefs.GetString("passedLoad"));
        }
    }


    public void SaveThings()
    {
        if (saveList.Contains(saveData.characterName))
        {
            //there should be a pop up telling you to name it something else
            return;
        }
        else
        {
            runload = true;
            saveDropDown.ClearOptions();
            saveData.SaveStats();

            PlayerPrefs.SetString("passedLoad", saveData.characterName);
            LoadSaveCharacter.instance.SaveGame(saveData, saveData.characterName);
            saveList.Add(saveData.characterName);
            saveDropDown.AddOptions(saveList);

        }
    }


    //if you run a new game, save the character playerpref as the name, and dont reset the playerpref.
    public void LoadThings(string loadString)
    {
        if (!GameSceneManager.loadCharacter && runload)
        {
            loadString = saveDropDown.captionText.text.ToString();
            PlayerPrefs.SetString("passedLoad", loadString);
        }
        else
        {
            loadString = PlayerPrefs.GetString("passedLoad");
        }

        saveData = LoadSaveCharacter.instance.LoadGame(loadString);


        // the accessible data of the save file 
        race = saveData.characterRace;
        saveName = saveData.characterName;
        hairCut = saveData.selectedCharacterLook;
        characterStats = saveData.statRetainer;
        characterPointPool = saveData.remainingPoints;
        characterClass = saveData.characterClass;
    }

    public void NewCharacter(bool yes)
    {
        if (yes)
            runload = false;
        else
            runload = true;
    }
}
