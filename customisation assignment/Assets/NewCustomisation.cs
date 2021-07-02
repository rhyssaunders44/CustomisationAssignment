using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCustomisation : MonoBehaviour
{
    public Texture2D[] skins;
    public Texture2D[] eyes;
    public Texture2D[] hair;
    public Texture2D[] mouth;
    public Texture2D[] clothes;
    public Texture2D[] armour;

    Texture2D[][] textureOverlord = new Texture2D[7][];

    public Renderer characterRenderer;

    public static int[] pieceSelector = new int[] {0,0,0,0,0,0,0};
    public Material[] mat;
    public bool positive;
    public Text[] matNum;
    public Text loadedCharcter;

    public InputField nameEnter;
    public static string characterName;

    public void Start()
    {

        textureOverlord = new Texture2D[][] {null, skins, eyes, hair, mouth, clothes, armour };

        if (GameSceneManager.loadCharacter)
        {
            ReLoadTextures();
            InGameLoad();
        }
        else
        {
            Randomise();
        }
    }

    public void NextTexture(int arrayIndex)
    {
        LoadTextures(positive);

        if (positive)
            pieceSelector[arrayIndex]++;
        else
            pieceSelector[arrayIndex]--;


        if(pieceSelector[arrayIndex] > (textureOverlord[arrayIndex].Length -1 ))
            pieceSelector[arrayIndex] = 0;

        if(pieceSelector[arrayIndex] < 0)
            pieceSelector[arrayIndex] = textureOverlord[arrayIndex].Length - 1;

        mat = characterRenderer.materials;
        mat[arrayIndex].mainTexture = textureOverlord[arrayIndex][pieceSelector[arrayIndex]];
        characterRenderer.materials = mat;

        matNum[arrayIndex].text = pieceSelector[arrayIndex].ToString();
    }

    public void LoadTextures(bool pos)
    {
        positive = pos;
    }


    public void Randomise()
    {
        for (int i = 1; i < pieceSelector.Length; i++)
        {
            pieceSelector[i] = Random.Range(0, textureOverlord[i].Length);
            matNum[i].text = pieceSelector[i].ToString();
            NextTexture(i);
        }
    }

    public void NameInput(bool save)
    {
        if (save)
        {
            characterName = nameEnter.text;
            loadedCharcter.text = characterName + " has been created!";
        }
        else
        {
            characterName = DataMaster.characterName;
            loadedCharcter.text = characterName + " has been Loaded!";
        }
    }

    public void InGameLoad()
    {
        characterName = DataMaster.characterName;
        loadedCharcter.text = characterName;
    }

    public void ReLoadTextures()
    {
        mat = characterRenderer.materials;

        for (int i = 1; i < textureOverlord.Length; i++)
        {
            mat[i].mainTexture = textureOverlord[i][DataMaster.hairCut[i]];

            matNum[i].text = DataMaster.hairCut[i].ToString();
        }

        loadedCharcter.text = "Character Loaded";
    }
}