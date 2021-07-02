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
    [SerializeField] public static int race;
    [SerializeField] public static List<string> saveList = new List<string>();

    public void Start()
    {

        if(saveList.Count == 0)
        {
            saveDropDown.captionText.text = "No Characters!";
            saveDropDown.interactable = false;
        }
        else
        {
            saveDropDown.AddOptions(saveList);
        }
    }


    public void SaveThings()
    {
        if (saveList.Contains(saveData.characterName))
        {
            //this character already Exists!
            // if you say yes you will overrwite it!
        }
        else
        {
            saveDropDown.ClearOptions();
            saveData.SaveStats();

            LoadSaveCharacter.instance.SaveGame(saveData, saveData.characterName);
            Debug.Log(saveData.characterName);
            saveList.Add(saveData.characterName);
            saveDropDown.AddOptions(saveList);
        }
    }

    public void LoadThings(string loadString)
    {
        if (!GameSceneManager.loadCharacter)
        {
            loadString = saveDropDown.captionText.text.ToString();
        }

        saveData = LoadSaveCharacter.instance.LoadGame(loadString);

        race = saveData.characterRace;
        saveName = saveData.characterName;
        hairCut = saveData.selectedCharacterLook;
        characterStats = saveData.statRetainer;
        characterPointPool = saveData.remainingPoints;
    }
}
