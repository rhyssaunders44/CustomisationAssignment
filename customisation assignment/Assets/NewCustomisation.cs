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
    public int[] pieceSelector = new int[] {0,0,0,0,0,0,0};
    int arrayIndex;
    public Material[] mat;

    public Text[] matNum;

    public void Start()
    {
        textureOverlord = new Texture2D[][] {null, skins, eyes, hair, mouth, clothes, armour };
        Randomise();
    }

    public void LoadTextures(int arrayIndexer)
    {
        arrayIndex = arrayIndexer;
        mat = characterRenderer.materials;
        mat[arrayIndex].mainTexture = textureOverlord[arrayIndex][pieceSelector[arrayIndex]];
        characterRenderer.materials = mat;
        //matNum[arrayIndex].text = pieceSelector[arrayIndex].ToString();
    }

    public void NextTexture(bool positive)
    { 
        if (positive == true)
            pieceSelector[arrayIndex]++;
        else
            pieceSelector[arrayIndex]--;

        if(pieceSelector[arrayIndex] > (textureOverlord[arrayIndex].Length -1 ))
            pieceSelector[arrayIndex] = 0;

        if(pieceSelector[arrayIndex] < 0)
            pieceSelector[arrayIndex] = textureOverlord[arrayIndex].Length - 1;
    }

    public void Randomise()
    {
        for (int i = 1; i < pieceSelector.Length; i++)
        {
            pieceSelector[i] = Random.Range(0, textureOverlord[i].Length);
            LoadTextures(i);
        }
    }
}