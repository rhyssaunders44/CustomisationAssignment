using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomisationGet : MonoBehaviour
{
    [Header("Character Name")]
    public string characterName;

    [Header("Character Class")]
    public CharacterClass characterClass =  CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedClassIndex = 0;
    public int[] baseStats;
    public int[] tempstats;

    [Header("DropDown Menu")]
    public bool showDropDownMenu;
    public Vector2 ScrollPos;
    public string ClassButton = "";
    public int statPoints = 10;

    [Header("Texture Lists")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();

    [Header("Index")]
    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex, armourIndex, clothesIndex;

    [Header("Rendered")]
    public Renderer characterRenderer;

    [Header("Max amount of Textures per Type")]
    public int skinMax;
    public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;

    [Header("Mat Name")]
    public string[] matName = new string[6];

    private void Start()
    {
        #region TextureGet
        matName = new string[] { "Skin", "Eyes", "Mouth", "Hair", "Clothes", "Armour" };
        selectedClass = new string[] { "Barbarian", "Bard", "Cleric","Druid","Fighter","Monk","Paladin","Ranger","Rouge","Sorcerer","Wizard"};

        for (int i = 0; i < skinMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Warrior/Tex/Skin_" + i) as Texture2D;
            skin.Add(tempTexture);
        }

        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Warrior/Tex/Eyes_" + i) as Texture2D;
            eyes.Add(tempTexture);
        }

        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Warrior/Tex/Mouth_" + i) as Texture2D;
            mouth.Add(tempTexture);
        }

        for (int i = 0; i < hairMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Warrior/Tex/Hair_" + i) as Texture2D;
            hair.Add(tempTexture);
        }

        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Warrior/Clothes_" + i) as Texture2D;
            clothes.Add(tempTexture);
        }

        for (int i = 0; i < armourMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Warrior/Tex/Armour_" + i) as Texture2D;
            armour.Add(tempTexture);
        }
        #endregion

    }

    void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];

        switch (type)
        {
            case "Skin":
                index = skinMax;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;

            case "Eyes":
                index = eyesMax;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 2;
                break;

            case "Mouth":
                index = mouthMax;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;

            case "Hair":
                index = hairMax;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 4;
                break;

            case "Clothes":
                index = clothesMax;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 5;
                break;

            case "Armour":
                index = armourMax;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 6;
                break;
        }

        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }

        Material[] mat = characterRenderer.materials;
       // Debug.Log(index + ", " + textures.Length);
        mat[matIndex].mainTexture = textures[index];
        characterRenderer.materials = mat;
    }

    private void OnGUI()
    {
        #region GUI set up
        Vector2 scr = new Vector2(Screen.width / 16, Screen.height / 9);

        float left = 0.25f * scr.x;
        float mid = 0.75f * scr.x;
        float right = 2.25f * scr.x;

        float x = 0.5f * scr.x;
        float y = 0.5f * scr.y;
        float label = 1.5f * scr.x;

        #endregion
        for (int i = 0; i < matName.Length; i++)
        {
            if (GUI.Button(new Rect(left, y + i * y, x, y), "<"))
            {
                SetTexture(matName[i], -1);
            }

            if (GUI.Button(new Rect(mid, y + i * y, x, y), matName[i]))
            {
                SetTexture(matName[i], 1);
            }

        }

    }

    void ChooseClass(int classIndex)
    {
        switch (classIndex)
        {
            case 0:
                break;

        }
    }
}

public enum CharacterClass
{
    Barbarian,
    Bard,
    Cleric,
    Druid,
    Fighter,
    Monk,
    Paladin,
    Ranger,
    Rouge,
    Sorcerer,
    Wizard
}