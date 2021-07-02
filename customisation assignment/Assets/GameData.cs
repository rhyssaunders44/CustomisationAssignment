using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int[][] statRetainer;
    public int remainingPoints;
    public int[] selectedCharacterLook;
    public string characterName;
    public int characterRace;

    public void SaveStats()
    {
        characterName = NewCustomisation.characterName;
        statRetainer = player.AssignableStatManager.stats;
        remainingPoints = player.AssignableStatManager.pointPool;
        selectedCharacterLook = NewCustomisation.pieceSelector;
        characterRace = player.AssignableStatManager.selectRace;
    }
}
