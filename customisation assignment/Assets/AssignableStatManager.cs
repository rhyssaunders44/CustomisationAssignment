using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace player
{
    public class AssignableStatManager : MonoBehaviour
    {
        #region stat Setup
        public static int[][] stats;
        public int[] statTotal;
        public int[] mutibleStats;
        public int[][] raceStats;
        public int[][] classStats;
        public static int selectRace;
        public static int selectClass;
        public int statIndex;

        public Text[][] statDisplay;
        public Text[] statTotalText;
        public Text[] mutibleStatsText;
        public Text[] raceStatsText;
        public Text[] classStatsText;

        public Text pointPoolText;
        public Text raceName;
        public Text raceAbilityText;
        public Text className;
        public Text className1;
        public Text classAbilityText;

        public CanvasGroup deathScreen;
        public static bool dead;
        public CanvasGroup helpCanvas;
        [SerializeField] CanvasGroup pain;
        [SerializeField] private CharacterController controller;

        public static int pointPool = 10;
        bool positive;

        public string[] raceNames;
        public string[] raceAbilityString;
        public string[] hudNames;

        public string[] classNames;
        public string[] classAbilityString;

        public static bool completeQuest;
        public class Race
        {
            public string name;
            public int[] raceStats;
            public string racialAbility;
        }

        public class CharacterClass
        {
            public string classType;
            public int[] classBonuses;
            public string classAbility;
        }
        #endregion

        #region HUD Stats
        public static int[][] regenStats;
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
        public static int xpCurrent;
        public Text xpTracker;
        public Text levelCounter;
        int currentLevel;
        int levelMax;
        private float dieTime;
        #endregion

        public bool running;
        int spellCost = 30;
        public static bool regenerating;
        public static bool healing;
        private int regenTick = 0;
        [SerializeField] private GameObject Warrior;
        [SerializeField] private GameObject Spawn;
        bool isFull;

        public void Start()
        {
            completeQuest = new bool();
            completeQuest = false;
            // defines all of the aspects of each race
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

            selectRace = 0;


            CharacterClass Fighter = new CharacterClass();
            Fighter.classType = "Fighter";
            Fighter.classBonuses = new int[] { 1, 0, 1, 0, 0, 0 };
            Fighter.classAbility = "All damage Dealt is increased by 1";

            CharacterClass Mage = new CharacterClass();
            Mage.classType = "Mage";
            Mage.classBonuses = new int[] { 0, 0, 0, 1, 1, 0 };
            Mage.classAbility = "Spells you cast cost 5% less";

            CharacterClass Rogue = new CharacterClass();
            Rogue.classType = "Rogue";
            Rogue.classBonuses = new int[] { 0, 1, 0, 0, 0, 1 };
            Rogue.classAbility = "You deal 2 bonus damage with bows."; 

            classNames = new string[] { Fighter.classType, Mage.classType, Rogue.classType };
            classStats = new int[][] { Fighter.classBonuses, Mage.classBonuses, Rogue.classBonuses };
            classAbilityString = new string[] {Fighter.classAbility, Mage.classAbility, Rogue.classAbility };


            statDisplay = new Text[][] { mutibleStatsText, statTotalText };
            stats = new int[][] { mutibleStats, statTotal };

            selectClass = 0;
            #endregion

            // initialisation of the in-game stats
            // this could be a class to simplify it
            // it is not.
            hp = new int[] { 0, 0, 0 };
            stamina = new int[] { 0, 0, 0 };
            mana = new int[] { 0, 0, 0 };
            regenStats = new int[][] { hp, stamina, mana };
            hudNames = new string[] {"HitPoints: ", "Stamina: ", "Mana: " };

            //initialises the text of the hud
            playerResourcesDisplay = new Text[][] {hpDisplay, staminaDisplay, manaDisplay };

            //the initial xp stats
            xpCurrent = 0;
            xpMax = 60;
            levelMax = 20;

            if (!GameSceneManager.loadCharacter)
            {
                // all of the hud objects are off
                for (int i = 0; i < hudObject.Length; i++)
                {
                    hudObject[i].SetActive(false);
                }
            }
            else
            {
                onCharacterLoad();
                FinishCharacter();
            }


            dead = false;

            //update the in-game stats when the game loads
            StatUpdate();
        }

        public void Update()
        {

            if (completeQuest)
            {
                xpCurrent = xpCurrent + 200;
                LevelUp();
                completeQuest = false;
            }

            if(xpCurrent > xpMax)
            {
                LevelUp();
            }
            //deal damage to the player 
            if (Input.GetKeyDown(KeyCode.P))
            {
                TakeDamage();
            }

            if (regenStats[0][1] <= 0)
            {
                UIManager.dead = true;
                Audio.dead = true;
                dead = true;
                helpCanvas.alpha = 0;
                controller.enabled = false;
                StopCoroutine("RegenStats");
            }
            else
            {
                Audio.dead = false;
            }

            //cast spell input
            if (Input.GetKeyDown(KeyCode.O))
            {
                //cast a spell if you have enough mana, same concept with the regen
                if (regenStats[2][1] > spellCost)
                {
                    CastSpell();
                }
            }

            //run input
            if (Input.GetKeyDown(KeyCode.LeftShift) && regenStats[1][1] > 5)
            {
                //stop regenning 
                StopCoroutine("StatRegen");
                //start running
                running = true;
                //start reducing stamina
                StartCoroutine("Run");
            }

            //when you stop running stop the coroutine 
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isFull = false;
                running = false;
                StartCoroutine("StatRegen");
            }

            // add xp to level up
            if (Input.GetKeyDown(KeyCode.L))
            {
                //add a random amount of xp and check if the player is able to level up
                xpCurrent = xpCurrent + Random.Range(5, 40);
                LevelUp();
            }

            if (regenerating)
            {
                StartCoroutine("healthPotionRegen");
                regenerating = false;
            }

            if (healing)
            {
                StatUpdate();
                healing = false;
            }


        }

        //scufffed method of implementing a death screen
        private void FixedUpdate()
        {
            if(deathScreen.alpha < 1 && dead)
            {
                deathScreen.alpha = deathScreen.alpha + 0.01f;
            }

            if (!dead)
            {
                deathScreen.alpha = 0;
            }

            if(pain.alpha > 0)
            {
                pain.alpha = pain.alpha - 0.5f;
            }
        }

        //a mess of gross conditions to fix up the spawn conditions
        public void Respawn()
        {
            regenStats[0][1] = regenStats[0][0] / 8;
            dead = false;
            controller.enabled = true;
            UIManager.dead = false;
            Warrior.transform.TransformVector(Spawn.transform.position);
        }

        /// <summary>
        /// adds a stat from the slected stat
        /// </summary>
        /// <param name="index"></param>
        public void AddStat(int index)
        {
            //add a point to the correct stat
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
            //remove a point from the correct stat
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
                //go to the next race in the array
                selectRace++;

                //loop the races
                if (selectRace > raceNames.Length - 1)
                    selectRace = 0;
            }
            else
            {
                // go to the prevoius array in the list
                selectRace--;

                //loop the races
                if (selectRace < 0)
                {
                    selectRace = raceNames.Length - 1;
                }

            }

            //update the UI
            StatUpdate();
        }

        public void NextClass(bool positive)
        {
            if (positive)
            {
                //go to the next class in the array
                selectClass++;

                //loop the races
                if (selectClass > classNames.Length - 1)
                    selectClass = 0;
            }
            else
            {
                // go to the prevoius array in the list
                selectRace--;

                //loop the classes
                if (selectClass < 0)
                {
                    selectClass = classNames.Length - 1;
                }

            }

            //update the UI
            StatUpdate();
        }
    
        public void Increasing(bool pos)
        {
            // needed to add a stat for the add and remove stat since you cant have 2 
            // arguments in a unity button
            positive = pos;
        }

        public void FinishCharacter()
        {
            //needed to level up
            xpCurrent = xpMax;

            // all stats are full and all UI elements are corrected
            LevelUp();
        }

        public void onCharacterLoad()
        {

            stats = DataMaster.characterStats;
            pointPool = DataMaster.characterPointPool;
            selectRace = DataMaster.race;
            selectClass = DataMaster.characterClass;

            StatUpdate();
        }

        public void LevelUp()
        {
            float xpbarfill;

            //if you surpass the max xp for that level
            if (xpCurrent >= xpMax)
            {
                //gain stats if you are not starting a new game
                if (currentLevel > 0)
                {
                    //pick a random stat and add 1 to it
                    stats[0][Random.Range(0, stats[0].Length)]++;
                    //update the stats
                    StatUpdate();
                    Audio.levelup = true;
                }

                // remove the max hp from the current xp to get your new xp amount out of the new total
                xpCurrent = xpCurrent - xpMax;
                xpMax = xpMax * 5 / 4;

                //you are now a higher level
                currentLevel++;

                // all of your stats go to max
                for (int i = 0; i < regenStats.Length; i++)
                {
                    regenStats[i][1] = regenStats[i][0];
                }

                //you stop regening stats. probably not necessary here since it works in the coroutine
                isFull = true;

            }

            xpbarfill = (float)xpCurrent / xpMax;
            statBars[3].fillAmount = xpbarfill;

            xpTracker.text = xpCurrent.ToString() + " / " + xpMax.ToString();
            StatUpdate();

            if(currentLevel < levelMax)
            {
                levelCounter.text = "Level: " + currentLevel.ToString();
            }
            else
            {
                levelCounter.text = "MAX: ";
            }

        }


        /// <summary>
        /// removes mana from the  player pool
        /// </summary>
        public void CastSpell()
        {
            //you are not at full mana
            isFull = false;
            StopCoroutine("StatRegen");
            //remove mana equal to the spell cost
            if(selectClass == 1)
            {
                regenStats[2][1] = regenStats[2][1] - (int)(spellCost * 0.95f);
            }
            else
            {
                regenStats[2][1] = regenStats[2][1] - spellCost;
            }

            // start regening stats
            StartCoroutine("StatRegen");
        }

        /// <summary>
        /// inflicts damage to the player
        /// </summary>
        public void TakeDamage()
        {
            pain.alpha = 1;
            Audio.pain = true;
            // you are not at full hp anymore
            isFull = false;
            StopCoroutine("StatRegen");
            //deal a random amount of damage
            regenStats[0][1] = regenStats[0][1] - Random.Range(5, 40);
            //start regening stats
            StartCoroutine("StatRegen");
        }

        public void DeathTime()
        {
            dieTime = Time.time + 0.1f;
        }

        /// <summary>
        /// regenerates the players stats when they arent full over time
        /// </summary>
        public IEnumerator StatRegen()
        {
            //if you are not at full for every stat
            while (!isFull)
            {
                //for every stat
                for (int x = 0; x < regenStats.Length; x++)
                {
                    int[] regen = regenStats[x];

                    //if the current amount is less than the max
                    if (regen[1] < regen[0])
                    {
                        //add the regen amount for that stat
                        regen[1] = regen[1] + regen[2];

                        //if a stat is greater than the max, it equals the max
                        if (regen[1] >= regen[0])
                        {
                            regen[1] = regen[0];
                        }

                        //if every stat is full stop the coroutine
                        if (regenStats[0][1] >= regenStats[0][0] && regenStats[1][1] >= regenStats[1][0] && regenStats[2][1] >= regenStats[2][0])
                        {
                            isFull = true;
                        }
                    }
                    // make sure the stats are up to date
                    StatUpdate();
                }
                // wait for a second then run the coroutine again
                yield return new WaitForSeconds(1);
            }
        }

        /// <summary>
        /// runs while youre running
        /// </summary>
        public IEnumerator Run()
        {
            // while youre pressing shift, remove a point of stamina every 0.1 seconds and update the stats
            // as a note moving is not a part of this project and the added condition of velocity > 0
            while(running && regenStats[1][1] > 0)
            {
                regenStats[1][1]--;
                StatUpdate();
                yield return new WaitForSeconds(0.1f);
            }
            yield return 0;
        }

        public IEnumerator healthPotionRegen()
        {
            int maxRegen = 50;

            if (regenTick < maxRegen)
            {
                regenTick++;
                regenStats[0][1]++;
                if(regenStats[0][1] > regenStats[0][0])
                {
                    regenStats[0][1] = regenStats[0][0];
                }
                StatUpdate();
                yield return new WaitForSeconds(0.1f);
                StartCoroutine("healthPotionRegen");
            }
            else
            {
                //regenTick = 0;
                //StopCoroutine("healthPotionRegen");
                yield return null;
            }
        }

        /// <summary>
        /// This function is called to update the in-game hud and character creation stats
        /// </summary>
        public void StatUpdate()
        {
            //for every stat, for every object in that stat:
            for (int x = 0; x < stats.Length; x++)
            {
                for (int i = 0; i < stats[1].Length; i++)
                {
                    //eg as above, put it to string
                    statDisplay[x][i].text = stats[x][i].ToString();
                    //put every race stat to text of the selected race
                    raceStatsText[i].text = raceStats[selectRace][i].ToString();

                    classStatsText[i].text = classStats[selectClass][i].ToString();

                    //display the mutible stat of every stat and put to string
                    hudStats[i].text = stats[1][i].ToString();
                    stats[1][i] = raceStats[selectRace][i] + classStats[selectClass][i] + stats[0][i];
                }
            }

            //shows how many points left in the point pool, the race name, and race ability
            #region UI text updating
            pointPoolText.text = pointPool.ToString();
            raceName.text = "Race: " + raceNames[selectRace];
            raceAbilityText.text = "Race Ability: " + raceAbilityString[selectRace];

            className.text = "Class: " + classNames[selectClass];
            className1.text = "Class: " + classNames[selectClass];
            classAbilityText.text = "Class Ability: " + classAbilityString[selectClass];
            #endregion

            //defines maxhp, max stamina, and max mana in that order
            #region ing game stat definition
            regenStats[0][0] = (stats[1][0] * 2) + (stats[1][2] * 5);
            regenStats[1][0] = stats[1][2] * 3;
            regenStats[2][0] = (stats[1][3] * 2) + (stats[1][4] * 4);

            #endregion

            for (int i = 0; i < regenStats.Length; i++)
            {
                // the image fill amount requires a float and int / int returns an int so a float is required
                float[] barCalc = new float[3];

                //the regen of each stat is 1/20th of the stat max
                regenStats[i][2] = regenStats[i][0] / 20;

                //aforementioned float for fill amount
                barCalc[i] = (float)regenStats[i][1] / regenStats[i][0];
                statBars[i].fillAmount = barCalc[i];

                // displays the current out of max for each stat and regen.
                playerResourcesDisplay[i][0].text = hudNames[i] + regenStats[i][1].ToString() + " / " + regenStats[i][0].ToString();
                playerResourcesDisplay[i][1].text = "+" + regenStats[i][2].ToString() + "/s";
            }
        }

        /// <summary>
        /// Quits the game
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}