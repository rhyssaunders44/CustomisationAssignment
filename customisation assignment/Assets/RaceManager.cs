using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace player
{
    public class RaceManager : MonoBehaviour
    {
        public int[] baseStats = { 20, 20, 20, 20, 20, 20 };
        public int[] racialStats = { 0, 0, 0, 0, 0, 0 };
        public int[] mutibleStats = { 5, 5, 5, 5, 5, 5 };
        public int selectedRace;
        public int[] basicStats;

        public Text[] baseStatText;
        public Text[] racialStatText;
        public Text[] basicStatText;
        public Text[] mutibleStatsText;

        public void Start()
        {
            CalculateStats();
            DisplayStats();
        }

        public virtual void CalculateStats()
        {
            for (int i = 0; i < basicStats.Length; i++)
            {
                basicStats[i] = baseStats[i] + mutibleStats[i] + racialStats[i];
            }
        }

        public void DisplayStats()
        {
            for (int i = 0; i < basicStats.Length; i++)
            {
                basicStatText[i].text = basicStats[i].ToString();
                mutibleStatsText[i].text = mutibleStats[i].ToString();
                racialStatText[i].text = racialStats[i].ToString();
            }
        }

    }

    public class Human : RaceManager
    {
        public override void CalculateStats()
        {
            base.CalculateStats();

        }
    }

    public class Elf : RaceManager
    {

    }

    public class Dwarf : RaceManager
    {

    }
}

