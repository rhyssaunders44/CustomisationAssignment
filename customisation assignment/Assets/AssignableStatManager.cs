using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace player
{
    public class AssignableStatManager : MonoBehaviour
    {
        #region stat Setup
        public int[][] stats;
        public int[] statTotal;
        public int[] mutibleStats;
        public int[][] raceStats;
        public int selectRace;
        public int statIndex;

        public Text[][] statDisplay;
        public Text[] statTotalText;
        public Text[] mutibleStatsText;
        public Text[] raceStatsText;

        public Text pointPoolText;
        public Text raceName;
        public Text raceAbilityText;

        public int pointPool = 10;
        bool positive;

        public string[] raceNames;
        public string[] raceAbilityString;
        public string[] hudNames;

        public class Race
        {
            public string name;
            public int[] raceStats;
            public string racialAbility;
        }
        #endregion

        int[][] regenStats;
        int[] hp;
        int[] stamina;
        int[] mana;

        public Text[][] playerResourcesDisplay;
        public Text[] hpDisplay;
        public Text[] staminaDisplay;
        public Text[] manaDisplay;
        public Text[] hudStats;

        public Image[] statBars;
        public GameObject[] characterCreationObject;
        public GameObject[] hudObject;

        int xpMax;
        int xpCurrent;
        public Text xpTracker;
        public Text levelCounter;
        int currentLevel;
        int levelMax;

        public bool running;
        int spellCost = 30;

        public void Start()
        {
            #region character management
            Race human = new Race();
            human.name = "Human";
            human.raceStats = new int[] { 1, 1, 1, 1, 1, 1 };
            human.racialAbility = "Choose to learn one language of your choice, and proficiency in one weapon of your choice.";

            Race elf = new Race();
            elf.name = "Elf";
            elf.raceStats = new int[] { 0, 2, 0, 1, 1, 1 };
            elf.racialAbility = "You deal 1 extra damage with ranged weapons, you are proficient with bows, and you know the language elvish. ";

            Race dorf = new Race();
            dorf.name = "Dwarf";
            dorf.raceStats = new int[] { 2, 0, 2, 0, 1, 0 };
            dorf.racialAbility = "you deal 1 extra damage with axes, you are proficient with axes, and you know the language dwarvish.";

            Race stupid = new Race();
            stupid.name = "Half-ling";
            stupid.raceStats = new int[] { -2, -2, -2, -2, -2, -2 };
            stupid.racialAbility = "when you take an action roll a d20, on a 20 you are not killed instantly. Also everyone will attack you on sight.";
            raceAbilityString = new string[] { human.racialAbility, elf.racialAbility, dorf.racialAbility, stupid.racialAbility };

            raceNames = new string[] { human.name, elf.name, dorf.name, stupid.name };
            raceStats = new int[][] { human.raceStats, elf.raceStats, dorf.raceStats, stupid.raceStats };
            mutibleStats = new int[] { 10, 10, 10, 10, 10, 10 };

            statDisplay = new Text[][] { mutibleStatsText, statTotalText };
            stats = new int[][] { mutibleStats, statTotal };

            selectRace = 0;

            #endregion

            hp = new int[] { 0, 0, 0 };
            stamina = new int[] { 0, 0, 0 };
            mana = new int[] { 0, 0, 0 };
            regenStats = new int[][] { hp, stamina, mana };
            hudNames = new string[] {"HitPoints: ", "Stamina: ", "Mana: " };

            playerResourcesDisplay = new Text[][] {hpDisplay, staminaDisplay, manaDisplay };


            xpCurrent = 0;
            xpMax = 60;
            levelMax = 20;


            for (int i = 0; i < hudObject.Length; i++)
            {
                hudObject[i].SetActive(false);
            }
            LevelUp();
            StatUpdate();
        }

        public void AddStat(int index)
        {
            statIndex = index;

            if (pointPool > 0 && stats[0][index] < 20)
            {
                stats[0][index]++;
                pointPool--;
            }
            StatUpdate();
        }

        public void RemoveStat(int index)
        {
            statIndex = index;

            if (mutibleStats[index] > 8)
            {
                mutibleStats[index]--;
                pointPool++;
            }
            StatUpdate();
        }

        public void nextRace(bool positive)
        {
            if (positive)
            {
                selectRace++;

                if (selectRace > raceNames.Length - 1)
                    selectRace = 0;
            }
            else
            {
                selectRace--;

                if (selectRace < 0)
                {
                    selectRace = raceNames.Length - 1;
                }

            }
            StatUpdate();
        }

        public void Increasing(bool pos)
        {
            positive = pos;
        }

        public void StatUpdate()
        {
            for (int x = 0; x < stats.Length; x++)
            {
                for (int i = 0; i < stats[1].Length; i++)
                {
                    stats[1][i] = raceStats[selectRace][i] + stats[0][i];
                    statDisplay[x][i].text = stats[x][i].ToString();
                    raceStatsText[i].text = raceStats[selectRace][i].ToString();
                    hudStats[i].text = stats[1][i].ToString();
                }
            }

            pointPoolText.text = pointPool.ToString();
            raceName.text = "Race: " + raceNames[selectRace];
            raceAbilityText.text = "Race Ability: " + raceAbilityString[selectRace];

            regenStats[0][0] = (stats[1][0] * 2) + (stats[1][2] * 5);
            regenStats[1][0] = stats[1][2] * 3;
            regenStats[2][0] = (stats[1][3] * 2) + (stats[1][4] * 4);

            for (int i = 0; i < regenStats.Length; i++)
            {
                float[] barCalc = new float[3];
                regenStats[i][1] = regenStats[i][0];
                regenStats[i][2] = regenStats[i][0] / 20;
                playerResourcesDisplay[i][0].text = hudNames[i] + regenStats[i][1].ToString() + " / " + regenStats[i][0].ToString();
                barCalc[i] = (float)regenStats[i][1] / regenStats[i][0];
                statBars[i].fillAmount = barCalc[i];
            }

            for (int x = 0; x < regenStats.Length; x++)
            {
                playerResourcesDisplay[x][1].text = "+" + regenStats[x][2].ToString() + "/s";
            }
        }

        public void FinishCharacter()
        {
            for (int i = 0; i < characterCreationObject.Length; i++)
            {
                characterCreationObject[i].SetActive(false);
                hudObject[i].SetActive(true);
            }

            LevelUp();
            StatUpdate();
        }

        public void LevelUp()
        {
            if(xpCurrent > xpMax)
            {
                xpCurrent = xpCurrent - xpMax;
                xpMax = xpMax * 5 / 4;
                currentLevel++;
                stats[0][Random.Range(0, stats[0].Length)]++;
                StatUpdate();
            }

            float xpbarfill;
            xpbarfill = (float)xpCurrent / xpMax;
            statBars[3].fillAmount = xpbarfill;

            xpTracker.text = xpCurrent.ToString() + " / " + xpMax.ToString();

            if(currentLevel < levelMax)
            {
                levelCounter.text = "Level: " + currentLevel.ToString();
            }
            else
            {
                levelCounter.text = "MAX: ";
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                StopCoroutine("StatRegen");
                TakeDamage();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                if (regenStats[2][1] > spellCost)
                {
                    StopCoroutine("StatRegen");
                    CastSpell();
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && regenStats[1][1] > 5)
            {
                StopCoroutine("StatRegen");
                running = true;
                StartCoroutine("Run");
                float statbarFill = (float)regenStats[1][1] / regenStats[1][0];
                statBars[1].fillAmount = statbarFill;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                running = false;
                StartCoroutine("StatRegen");
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                xpCurrent = xpCurrent + Random.Range(5, 40);
                LevelUp();
            }
        }

        public void CastSpell()
        {
            regenStats[2][1] = regenStats[2][1] - spellCost;
            StartCoroutine("StatRegen");
        }

        public void TakeDamage()
        {
            regenStats[0][1] = regenStats[0][1] - Random.Range(5, 40);
            StartCoroutine("StatRegen");
        }

        public IEnumerator StatRegen()
        {
            foreach (int[] regen in regenStats)
            {
                while (regen[1] < regen[0])
                {
                    regen[1] = regen[1] + regen[2];

                    if (regen[1] > regen[0])
                    {
                        regen[1] = regen[0];
                    }

                    for (int i = 0; i < playerResourcesDisplay.Length; i++)
                    {
                        float[] barCalc = new float[3];
                        playerResourcesDisplay[i][0].text = hudNames[i] + regenStats[i][1].ToString() + " / " + regenStats[i][0].ToString();
                        barCalc[i] = (float)regenStats[i][1] / regenStats[i][0];
                        statBars[i].fillAmount = barCalc[i];
                    }
                    yield return new WaitForSeconds(1);
                }
            }
            StopCoroutine("StatRegen");
            yield return new WaitForSeconds(1);
        }

        public IEnumerator Run()
        {
            while(running == true && regenStats[1][1] > 0)
            {
                regenStats[1][1]--;

                float statbarFill = (float)regenStats[1][1] / regenStats[1][0];
                statBars[1].fillAmount = statbarFill;
                yield return new WaitForSeconds(0.1f);
            }
            yield return 0;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}