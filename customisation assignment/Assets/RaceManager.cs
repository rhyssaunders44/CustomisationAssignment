using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace player
{
    public class RaceManager : MonoBehaviour
    {
        public int[][] stats;
        public int[] statTotal;
        public int[] raceBonus;
        public int[] mutibleStats;

        public Text[][] statDisplay;
        public Text[] statTotalText;
        public Text[] raceBonusText;
        public Text[] mutibleStatsText;

        public int pointPool;
        public bool positive;


        public void Start()
        {
            statDisplay = new Text[][] { statTotalText, raceBonusText, mutibleStatsText };
            stats = new int[][] { statTotal, raceBonus, mutibleStats };

            raceBonus = new int[] { 0, 0, 0, 0, 0, 0 };
            mutibleStats = new int[] { 10, 10, 10, 10, 10, 10 };
            pointPool = 10;

            StatUpdate();
        }

        public void AddStat(int index)
        {
            AddStat(index, 1);
        }
        public void RemoveStat(int index)
        {
            AddStat(index, -1);
        }

        public void AddStat(int index, int ammount)
        {
            if (pointPool > 0 || mutibleStats[index] > 8)
            {
                pointPool -= ammount;
                mutibleStats[index] += ammount;
            }
        }


        public void Increasing(bool pos)
        {
            positive = pos;
        }


        public void StatUpdate()
        {
            for (int i = 0; i < statTotal.Length; i++)
            {
                statTotal[i] = raceBonus[i] + mutibleStats[i];

                for (int x = 0; x < statDisplay[i].Length; x++)
                {
                    statDisplay[i][x].text = stats[i][x].ToString();
                }
            }
        }
    }
}

