using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [SerializeField] private Collider playerCollider; 
    [SerializeField] private GameObject dropSpawner;
    [SerializeField] private GameObject InvetoryPanel;
    [SerializeField] private GameObject StatPanel;
    [SerializeField] private GameObject Crosshair;
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private bool firstPerson;
    [SerializeField] private GameObject[] questOptions;
    [SerializeField] private Image[] approvalBars;
    [SerializeField] private GameObject[] highfive;
    [SerializeField] private GameObject rewardButton;
    [SerializeField] private GameObject rewardPanel;
    private bool rewardclaimed =false;

    public static bool[] activeQuests;
    public static bool[] completedquest;
    [SerializeField] private Text activeQuestText;
    [SerializeField] private Text completedQuestText;
    [SerializeField] private Text approvalText;
    [SerializeField] private bool[] availableQuest;
    [SerializeField] private GameObject[] acceptDecline;
    [SerializeField] private GameObject QuestPanel;
    public GameObject shootButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject seeQuestsButton;
    [SerializeField] private GameObject[] BreezeResponse;
    [SerializeField] private Text[] breezeResponseText;
    [SerializeField] public static bool questCheck;
    [SerializeField] public static ParticleSystem cheer;
    [SerializeField] public static bool celebrate;
    [SerializeField] public GameObject questCheckPopUp;
    public int highlighedQuest;
    public int questint;
    public static bool startTalk;
    int responseIndex;
    public static float approval;
    [SerializeField] private int dialogueLevel;
    float approvalLevel;
    [SerializeField] private string eggTalk;
    [SerializeField] private Text eggtalkText;
    int question1;

    #region an array of mistakes
    string[] questions0;
    string[] questions1;
    string[] questions2;
    string[] questions3;
    string[] questions4;
    string[] questions5;
    string[] questions6;


    string[] answers0;
    string[] answers1;
    string[] answers2;
    string[] answers3;
    string[] answers4;
    string[] answers5;
    string[][] answerArray0;
    string[][] answerArray1;
    string[][] answerArray2;
    string[][] answerArray3;
    string[][][] answerGod;
    string[][] questionArray0;
    string[][] questionArray1;
    string[][] questionArray2;
    string[][] questionArray3;
    string[][] questionArray4;
    string[][][] questionGod;
    int dialogueTier;
    int question;
    #endregion

    public static bool refreshItems;
    public static int refreshSlot;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject Map;
    [SerializeField] private Text helpText;
    [SerializeField] public bool helpOn;
    public static bool dead;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject AxeObject;
    [SerializeField] private GameObject HeadObject;
    public bool mapUp;
    public bool questUp;
    public static bool chatting;
    [SerializeField] private string[] questDescriptions;
    [SerializeField] private CanvasGroup pickup;

    [SerializeField] private bool inventoryUp;
    [SerializeField] private bool statsUp;
    [SerializeField] private Canvas canvas;
    [SerializeField] public static List<Items> inventoryItems = new List<Items>();
    [SerializeField] public List<Sprite> itemSprites = new List<Sprite>();

    [SerializeField] public int itemIndex;
    [SerializeField] public int inventoryLength;
    [SerializeField] public Items collidedItem;
    [SerializeField] public GameObject[] inventoryslots;
    [SerializeField] public Image[] itemPicture;
    [SerializeField] public static Items[] InGameItems;
    [SerializeField] public GameObject[] ItemPrefabs;
    [SerializeField] public Text[] slottedAmount;
    [SerializeField] public Text[] slottedItemWorth;
    [SerializeField] public Text[] slottedItemName;
    [SerializeField] public Text[] itemTypeText;
    [SerializeField] public Image[] EquippedItems;
    [SerializeField] public Text[] EquippedItemText;
    [SerializeField] private int[] equippedItemint;
    [SerializeField] private bool[] equippedItembool;
    [SerializeField] private bool type;
    [SerializeField] private Text sortText;
    [SerializeField] public static int currency;
    [SerializeField] private Text currencyText;
    [SerializeField] private GameObject neckLight;

    #region new items
    [SerializeField] public Items HealthPotion;
    [SerializeField] public Items Apple;
    [SerializeField] public Items Armour;
    [SerializeField] public Items Axe;
    [SerializeField] public Items Belt;
    [SerializeField] public Items Bow;
    [SerializeField] public Items Book;
    [SerializeField] public Items Boots;
    [SerializeField] public Items Bracers;
    [SerializeField] public Items Cloak;
    [SerializeField] public Items Gem;
    [SerializeField] public Items Gloves;
    [SerializeField] public Items Helmet;
    [SerializeField] public Items Ingot;
    [SerializeField] public Items Meat;
    [SerializeField] public Items ManaPotion;
    [SerializeField] public Items Necklace;
    [SerializeField] public Items Pants;
    [SerializeField] public Items Rings;
    [SerializeField] public Items Scroll;
    [SerializeField] public Items Shield;
    [SerializeField] public Items Shoulders;
    [SerializeField] public Items Sword;
    #endregion


    //list comparers 
    public class SortType : IComparer<Items>
    {
        int IComparer<Items>.Compare(Items a, Items b)
        {
            string com1 = a.itemType.ToString();
            string com2 = b.itemType.ToString();
            return com1.CompareTo(com2);
        }
    }

    public class SortName : IComparer<Items>
    {
        int IComparer<Items>.Compare(Items a, Items b)
        {
            string com1 = a.name;
            string com2 = b.name;
            return com1.CompareTo(com2);
        }
    }


    void Start()
    {
        equippedItembool = new bool[14];
        activeQuests = new bool[4];
        completedquest = new bool[] {false, false, false, false };
        rewardclaimed = false;
        //should read pain in the neck, semi-sure the load function is redundant. oh well
        // also load is not allowed outside of a method so its here instead of the ugly block above
        #region item initialisation
        HealthPotion = new Items("Health Potion", 10, ItemType.Consumable, Resources.Load("UI/Icons/hp") as Sprite, 0, 0, "heals 50 damage over 5 seconds");
        Apple = new Items("Apple", 1, ItemType.Consumable, Resources.Load("UI/Icons/apple") as Sprite, 1, 0, "Yummy!, gives you 5 hp");
        Armour = new Items("Armour", 60, ItemType.ChestArmour, Resources.Load("UI/Icons/armour") as Sprite, 2, 0, "Leather Armour: 5 defense");
        Axe = new Items("Axe", 20, ItemType.PrimaryWeapon, Resources.Load("UI/Icons/axe 1") as Sprite, 3, 0, "Axe: 6 Attack");
        Bow = new Items("Bow", 30, ItemType.RangedWeapon, Resources.Load("UI/Icons/b_t_01 1") as Sprite, 4, 0, "Bow: 3 Attack");
        Book = new Items("Book", 10, ItemType.Treasure, Resources.Load("UI/Icons/book 1") as Sprite, 5, 0, "A valuble book");
        Boots = new Items("Boots", 10, ItemType.FootArmour, Resources.Load("UI/Icons/boots 1") as Sprite, 6, 0, "Leather Boots: 2 defense");
        Bracers = new Items("Bracers", 12, ItemType.WristArmour, Resources.Load("UI/Icons/bracers") as Sprite, 7, 0, "Leather Bracers: 1 attack, 1 defense");
        Cloak = new Items("Cloak", 15, ItemType.Cloak, Resources.Load("UI/Icons/cloaks") as Sprite, 8, 0, "A warm cloak: 1 defense");
        Gem = new Items("Gem", 100, ItemType.Treasure, Resources.Load("UI/Icons/gem") as Sprite, 9, 0, "Ohh... Shiney");
        Gloves = new Items("Gloves", 15, ItemType.HandArmour, Resources.Load("UI/Icons/gloves") as Sprite, 10, 0, "Leather Gloves:  2 defense, 1 attack");
        Helmet = new Items("Helmet", 30, ItemType.HeadArmour, Resources.Load("UI/Icons/helmets") as Sprite, 11, 0, "A simple Helmet: 3 defense");
        Ingot = new Items("Ingot", 5, ItemType.Material, Resources.Load("UI/Icons/ingots") as Sprite, 12, 0, "a crafting material");
        Meat = new Items("Meat", 8, ItemType.Consumable, Resources.Load("UI/Icons/Meat") as Sprite, 13, 0, "A hearty meal! heals 10 hp");
        ManaPotion = new Items("ManaPotion", 10, ItemType.Consumable, Resources.Load("UI/Icons/mp 1") as Sprite, 14, 0,"Restores 20 mana");
        Necklace = new Items("Necklace", 80, ItemType.NeckSlot, Resources.Load("UI/Icons/necklace 1") as Sprite, 15, 0, "Necklace of charm: +1 charisma");
        Pants = new Items("Pants", 60, ItemType.LegArmour, Resources.Load("UI/Icons/pants 1") as Sprite, 16, 0, "Leather Pants: 5 defense");
        Rings = new Items("Rings", 90, ItemType.RingSlot, Resources.Load("UI/Icons/rings 1") as Sprite, 17, 0, "Ring of Strength: +1 strength");
        Scroll = new Items("Scroll", 110, ItemType.Treasure, Resources.Load("UI/Icons/scroll 1") as Sprite, 18, 0, "Secrets of the ages, on sale now...");
        Shield = new Items("Shield", 30, ItemType.Shield, Resources.Load("UI/Icons/shield") as Sprite, 19, 0, "Wooden Shield: 4 defense");
        Shoulders = new Items("Shoulders", 20, ItemType.Shoulder, Resources.Load("UI/Icons/shoulders") as Sprite, 20, 0,"Exessive Pouldrons of XenoSlaying: + 5 strength, +5 constitution, 7 Attack");
        Sword = new Items("Sword", 30, ItemType.PrimaryWeapon, Resources.Load("UI/Icons/sword") as Sprite, 21, 0, "Sword: 6 Attack");
        Belt = new Items("Belt", 30, ItemType.Belt, Resources.Load("UI/Icons/sword") as Sprite, 21, 0, "Leather Belt: 1 defense, +1 Strength ");

        #endregion
        inventoryItems.Capacity = 7;
        InGameItems = new Items[] { HealthPotion, Apple, Armour, Axe, Bow, Book, Boots, Bracers, Cloak, Gem, Gloves, Helmet, Ingot, Meat, ManaPotion, Necklace, Pants, Rings, Scroll, Shield, Shoulders, Sword, Belt };

        #region multichoice

        // this is just heresy
        answers0 = new string[] { "yeah man", "actually nah" , null};
        answers1 = new string[] { "not really..." , "dunno" , "yeah man!" };
        answers2 = new string[] { "sucks to suck my dude", "Im sorry bro", null };
        answers3 = new string[] { "yeah on second thought they're pretty cool", "I have no strong feelings about shoes.", "HOLY GOD DAMN LIGHTS?! IN A  S H O E ?!?" };
        answers4 = new string[] { "BroJob" , "Lorna Shore" , "i dont listen to good music", null };
        answers5 = new string[] { "what the hell are you talking about?", null, null };


        questions0 = new string[] { "oh you wanna talk bro?", null, null };
        questions1 = new string[] { "do you like my shoes bro?", null, null };
        questions2 = new string[] { "thats pretty honest dude. honest but harsh", "you may not have given my fresh babies a proper check out. they light up bro.", "I'm all about enthusiasm bro. who's your favourite musician bro?" };
        questions3 = new string[] { "brutal. savage. rekt, bro.", "no worries bro.", null ,null };
        questions4 = new string[] { "thanks bro.", "get on my fly level before i go pogchamp on you omega lul.", "THEY ARE SO CASH MONEY RIGHT?" };
        questions5 = new string[] { "never has the world seen a more brutal or slamming deathcore band", "a voice straight from the void. ride on.", "ew." };
        questions6 = new string[] { "ok boomer.", null, null };


        questionArray0 = new string[][] { questions0, null, null };
        questionArray1 = new string[][] { questions1, null, null };
        questionArray2 = new string[][] { questions2, questions3, questions4 };
        questionArray3 = new string[][] { null, null, questions5 };
        questionArray4 = new string[][] { null, null, questions6 };
        questionGod = new string[][][] { questionArray0, questionArray1, questionArray2, questionArray3, questionArray4 };

        answerArray0 = new string[][] { answers0 , null, null};
        answerArray1 = new string[][] { answers1, null, null};
        answerArray2 = new string[][] { answers2, answers3, answers4 };
        answerArray3 = new string[][] { null, null, answers5 };
        answerGod = new string[][][] { answerArray0, answerArray1, answerArray2, answerArray3 };
        #endregion

        questDescriptions = new string[] {"head to the blue particles" , "head to the red particles", "head to the yellow particles", "head to the white particles" };

        //this could be an array of UI bools, it isnt.
        inventoryUp = false;
        statsUp = false;
        firstPerson = false;
        helpOn = true;
        mapUp = false;
        for (int i = 0; i < availableQuest.Length; i++)
        {
            availableQuest[i] = true;
        }

        type = false;

        for (int i = 0; i < equippedItembool.Length; i++)
        {
            equippedItembool[i] = false;
        }
        dead = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CloseInventory();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CloseStats();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            FpActive();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MapUp();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuestUp();
        }


        if (refreshItems)
        {
            RefreshOnPickup();
            RefreshDisplay();
        }

        if (dead)
        {
            deathPanel.transform.position = canvas.pixelRect.center;
            mapUp = false;
            inventoryUp = false;
            statsUp = false;
        }
        else
        {
            deathPanel.transform.position  = new Vector3(0, canvas.pixelRect.yMin - canvas.pixelRect.yMax);
        }

        if (chatting)
        {
            DialoguePanel.SetActive(true);
        }
        else
        {
            DialoguePanel.SetActive(false);
            dialogueLevel = 0;
            StopCoroutine("TalkGen");
            for (int i = 0; i < BreezeResponse.Length; i++)
            {
                BreezeResponse[i].SetActive(false);
            }
        }

        if (startTalk)
        {
            OpenDialogue();
        }

        if(approval <= -10)
        {
            approvalText.text = "disliked";
        }
        else if(approval > -10 && approval < 10)
        {
            approvalText.text = "neutral";
        }
        else
        {
            approvalText.text = "Liked";
        }

        if (questCheck)
        {
            QuestUpdate();
        }

    }

    public static void StartChat()
    {
        startTalk = true;      
    }

    public void OpenDialogue()
    {
        for (int i = 0; i < highfive.Length; i++)
        {
            highfive[i].SetActive(true);
        }

        if(approval <= -20)
        {
            eggTalk = "I'm disappointed in you bro. have you come to say sorry bro?";
        }
        if(approval > -20 && approval <=-15 )
        {
            eggTalk = "why you gotta be mean bro?";
        }
        if(approval > -15 && approval <= -10)
        {
            eggTalk = "our friendship has fallen on hard times bro";
        }
        if(approval > -10 && approval <= -5)
        {
            eggTalk = "you haven't come to dog me again have you bro?";
        }
        if(approval > -5 && approval <= 0)
        {
            eggTalk = "suh bro";
        }
        if(approval > 0 && approval <= 5)
        {
            eggTalk = "hey bro";
        }
        if(approval > 5 && approval <= 10)
        {
            eggTalk = "Hey dawg! I missed my bro";
        }
        if(approval > 10 && approval <= 15)
        {
            eggTalk = "BRO! YOURE BACK!";
        }
        if(approval > 15 && approval <= 20)
        {
            eggTalk = "we're ride or die bro";
        }
        if(approval > 20)
        {
            eggTalk = "I care about the whole world... youre my whole world bro";
        }

        StartCoroutine("TalkGen");
        startTalk = false;
        if (approval > 0)
        {
            approvalLevel = -(approval / 20);
            approvalBars[0].fillAmount = approvalLevel;
        }
        else if (approval == 0)
        {
            for (int i = 0; i < approvalBars.Length; i++)
            {
                approvalBars[i].fillAmount = 0;
            }
        }
        else
        {
            approvalLevel = (approval / 20);
            approvalBars[1].fillAmount = approvalLevel;
        }

    }

    void FixedUpdate()
    {
        //the higher the magic number in the lerp, the faster it go
        if (inventoryUp)
        {
            InvetoryPanel.transform.position = new Vector3(Mathf.Lerp(InvetoryPanel.transform.position.x, canvas.pixelRect.center.x, 4 * Time.deltaTime), canvas.pixelRect.center.y, 0);
        }
        else
        {
            InvetoryPanel.transform.position = new Vector3(Mathf.Lerp(InvetoryPanel.transform.position.x, canvas.pixelRect.xMax + canvas.pixelRect.xMax / 2, 4 * Time.deltaTime), canvas.pixelRect.center.y, 0);
        }

        if (statsUp)
        {
            StatPanel.transform.position = new Vector3(Mathf.Lerp(StatPanel.transform.position.x, canvas.pixelRect.center.x, 4 * Time.deltaTime), canvas.pixelRect.center.y, 0);
        }
        else
        {
            StatPanel.transform.position = new Vector3(Mathf.Lerp(StatPanel.transform.position.x, canvas.pixelRect.xMin - canvas.pixelRect.xMax / 2, 4 * Time.deltaTime), canvas.pixelRect.center.y, 0);
        }

        if (helpOn)
        {
            helpPanel.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(helpPanel.transform.position.y, canvas.pixelRect.center.y, 4 * Time.deltaTime), 0);
        }
        else
        {
            helpPanel.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(helpPanel.transform.position.y, canvas.pixelRect.yMin - canvas.pixelRect.yMax /5, 4 * Time.deltaTime), 0);
        }

        if (mapUp)
        {
            Map.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(Map.transform.position.y, canvas.pixelRect.center.y, 4 * Time.deltaTime), 0);
        }
        else
        {
            Map.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(Map.transform.position.y, canvas.pixelRect.yMin - canvas.pixelRect.yMax / 2, 4 * Time.deltaTime), 0);
        }

        if (questUp)
        {
            QuestPanel.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(QuestPanel.transform.position.y, canvas.pixelRect.center.y, 4 * Time.deltaTime), 0);
        }
        else
        {
            QuestPanel.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(QuestPanel.transform.position.y, canvas.pixelRect.yMin - canvas.pixelRect.yMax / 2, 4 * Time.deltaTime), 0);
        }
        if (celebrate)
        {
            cheer.Play();
        }

    }

    public void HighFive(bool upTop)
    {
        StopCoroutine("TalkGen");
        if (upTop)
        {
            approval += 5;
            if (approvalLevel > 0)
            {
                eggTalk = "this better not be a trick bro";
            }
            if (approval < 10 && approval >= 0)
            {
                eggTalk = "hell yeah, stranger bro!";
            }
            if (approval >= 10 && approval < 20)
            {
                eggTalk = "My Man!";
            }
            if(approval >= 20)
            {
                eggTalk = "I can Always count on you, You're like my brother dawg";
            }

        }
        else
        {
            approval -= 5;
            if (approval > 0)
            {
                eggTalk = "this is why you don't have friends bro";
            }
            if (approval >= 0 && approval < 10)
            {
                eggTalk = "not cool bro";
            }
            if (approval >= 10 && approval < 20)
            {
                eggTalk = "You didn' have to do me like that dawg";
            }
            if (approval >= 20)
            {
                eggTalk = "We was brothers man!";
            }
        }

        if (approval < 0)
        {
            approvalLevel = -(approval / 20);
            approvalBars[0].fillAmount = approvalLevel;
        }
        else if(approval == 0)
        {
            for (int i = 0; i < approvalBars.Length; i++)
            {
                approvalBars[i].fillAmount = 0;
            }
        }
        else
        {
            approvalLevel = (approval / 20);
            approvalBars[1].fillAmount = approvalLevel;
        }

        shootButton.SetActive(true);
        StartCoroutine("TalkGen");
    }
  
    public void SeeQuests()
    {
        Next();
        backButton.SetActive(true);
        seeQuestsButton.SetActive(false);
        for (int i = 0; i < highfive.Length; i++)
        {
            highfive[i].SetActive(false);
        }

        StopCoroutine("TalkGen");
        if (approval <= -10)
        {
            eggTalk = "Nu-uh, nothing for you fake bro";
        }
        else if (approval > -10 && approval < 10)
        {
            eggTalk = "I am Entrusting you with these missions bro";
            if (availableQuest[0] && !completedquest[0] && !activeQuests[0])
                questOptions[0].SetActive(true);
            else
                questOptions[0].SetActive(false);

            if (availableQuest[1] && !completedquest[1] && !activeQuests[1])
                questOptions[1].SetActive(true);
            else
                questOptions[1].SetActive(false);

        }
        else
        {
            eggTalk = "Can you go get me some stuff bro?";
            approvalText.text = "Liked";
            for (int i = 0; i < questOptions.Length; i++)
            {
                if (availableQuest[i] && !completedquest[i] && !activeQuests[i])
                    questOptions[i].SetActive(true);
                else
                    questOptions[i].SetActive(false);
            }
        }
        StartCoroutine("TalkGen");
    }

    public void YourEternalReward()
    {
        StopCoroutine("TalkGen");
        AddItem(0);
        rewardButton.SetActive(false);
        eggTalk = "thanks for doing that useless stuff for me, heres a healthpotion, check your inventory!";
        rewardclaimed = true;
        StartCoroutine("TalkGen");
    }

    public void AcceptQuest(int questAccepted)
    {
        for (int i = 0; i < questOptions.Length; i++)
        {
            if (completedquest[i])
            {
                questOptions[i].SetActive(false);
            }
        }

        StopCoroutine("TalkGen");

        switch (questAccepted)
        {
            case 0:
                 eggTalk = "BRO I NEED YOU TO FETCH ME SOME OF THAT GLOWING STUFF FROM THE COOL BLUE PARTICLE EFFECTS, you can see its location with the blue diamond on the map";
                break;
            case 1:
                eggTalk = "BRO I NEED YOU TO FETCH ME SOME OF THAT GLOWING STUFF FROM THE COOL RED PARTICLE EFFECTS, you can see its location with the red diamond on the map";
                break;
            case 2:
                eggTalk = "BRO I NEED YOU TO FETCH ME SOME OF THAT GLOWING STUFF FROM THE COOL YELLOW PARTICLE EFFECTS, you can see its location with the yellow diamond on the map";
                break;
            case 3:
                eggTalk = "BRO I NEED YOU TO FETCH ME SOME OF THAT GLOWING STUFF FROM THE COOL WHITE PARTICLE EFFECTS, , you can see its location with the white diamond on the map";
                break;
        }
        questint = questAccepted;
        backButton.SetActive(false);
        acceptDecline[0].SetActive(true);
        acceptDecline[1].SetActive(true);
        StartCoroutine("TalkGen");
        AJustEnd();
    }

    public void QuestIsOn(bool accepted)
    {
        activeQuestText.text = "";
        StopCoroutine("TalkGen");
        if (accepted)
        {
            eggTalk = "you're one in a million bro";
            activeQuests[questint] = true;
        }
        else
        {
            eggTalk = "you cut me real deep bro";
            backButton.SetActive(true);
        }
        acceptDecline[0].SetActive(false);
        acceptDecline[1].SetActive(false);
        StartCoroutine("TalkGen");

        QuestUpdate();
    }

    public void QuestUpdate()
    {
        activeQuestText.text = "";
        questCheckPopUp.SetActive(true);

        for (int i = 0; i < questOptions.Length; i++)
        {
            if (completedquest[i])
            {
                questOptions[i].SetActive(false);
            }
        }

        if((!questOptions[0].activeSelf && !questOptions[1].activeSelf && !questOptions[2].activeSelf && !questOptions[3].activeSelf) && approval > 20 && !rewardclaimed)
        {
            rewardButton.SetActive(true);
        }

        for (int i = 0; i < activeQuests.Length; i++)
        {
            if (activeQuests[i])
            {
                activeQuestText.text += questDescriptions[i];
            }
        }
        questCheck = false;
    }

    //after trying and failing nested switches and a 3D array Im hardcoding it. sue me.
    public void ShootTheBreeze()
    {
        StopCoroutine("TalkGen");
        eggTalk = questionGod[0][0][0];
        BreezeResponse[0].SetActive(true);
        BreezeResponse[1].SetActive(true);
        seeQuestsButton.SetActive(false);
        shootButton.SetActive(false);
        breezeResponseText[0].text = answerGod[0][0][0];
        breezeResponseText[1].text = answerGod[0][0][1];
        StartCoroutine("TalkGen");
    }

    //turns put not using return was the problem. this was neater but im not going to unf#@ck it.
    //this is an unmitigated clusterf&%k of stupid proportions.
    #region here there be monsters
    public void QuestionTime(int response)
    {

        if (dialogueLevel == 0)
        {
            if (response == 1)
            {
                Back();
                return;
            }

            for (int i = 0; i < BreezeResponse.Length; i++)
            {
                BreezeResponse[i].SetActive(true);
            }

            StreamA();
            return;
        }

        if (dialogueLevel == 1)
        {
            question = response;
            if(response == 0)
            {
                StreamA();
            }
            if (response == 1)
            {
                StreamB();
            }
            if(response == 2)
            {
                StreamC();
            }
            for (int i = 0; i < BreezeResponse.Length; i++)
            {
                BreezeResponse[i].SetActive(true);
            }
            return;
        }

        if (dialogueLevel == 2)
        {
            question1 = response;
            if (response == 0)
            {
                StreamA();
            }
            if (response == 1)
            {
                if (question == 0)
                {
                    StreamA();
                }
                else
                {
                    StreamB();
                }

            }
            if (response == 2)
            {
                StreamC();
            }
            return;
        }

        if (dialogueLevel == 3)
        {
            if (response == 0)
            {
               StreamA();
            }
            if (response == 1)
            {
                StreamB();
            }
            if (response == 2)
            {
                StreamC();
            }
            return;
        }

        if (dialogueLevel == 4)
        {
            StreamA();
        }
    }

    public void StreamA()
    {
        Next();
        Debug.Log("tick");
        StopCoroutine("TalkGen");
        if (dialogueLevel == 1)
        {
            eggTalk = questionGod[dialogueLevel][0][0];
            for (int i = 0; i < breezeResponseText.Length; i++)
            {
                breezeResponseText[i].text = answerGod[dialogueLevel][0][i];
            }
        }
        if(dialogueLevel == 2)
        {
            if (question == 2)
            {
                approval -= 5;
                eggTalk = questionGod[3][2][0];
                AJustEnd();
            }
            else
            {
                eggTalk = questionGod[dialogueLevel][1][0];
                for (int i = 0; i < breezeResponseText.Length; i++)
                {
                    breezeResponseText[i].text = answerGod[dialogueLevel][0][i];
                }
            }
        }
        if (dialogueLevel == 3)
        {

            if (question == 1)
            {
                eggTalk = questionGod[2][2][0];
            }

            if(question == 0)
            {
                eggTalk = questionGod[2][1][1];
            }

            if(question == 2)
            {
                eggTalk = questionGod[3][2][0];
            }
            AJustEnd();
        }
        if(dialogueLevel == 4)
        {
            if (question == 1)
            {
                //lmao 420
                eggTalk = questionGod[4][2][0];
                AJustEnd();
            }
        }


        StartCoroutine("TalkGen");
    }

    public void StreamB()
    {
        Debug.Log("tac");
        Debug.Log(question);
        Next();
        StopCoroutine("TalkGen");
        switch (dialogueLevel)
        {
            case 2:

                eggTalk = questionGod[dialogueLevel][0][1];
                for (int i = 0; i < breezeResponseText.Length; i++)
                {
                    breezeResponseText[i].text = answerGod[dialogueLevel][1][i];
                }

                break;

            case 3:
                {

                    if (question == 2)
                    {
                        eggTalk = questionGod[3][2][1];
                        AJustEnd();
                    }
                    else
                    {
                        eggTalk = questionGod[2][2][1];
                        for (int i = 0; i < breezeResponseText.Length; i++)
                        {
                            breezeResponseText[i].text = answerGod[dialogueLevel][2][i];
                        }
                    }

                }
                break;

            case 4:
                approval -= 5;
                eggTalk = questionGod[4][2][0];
                AJustEnd();
                break;


        }


        StartCoroutine("TalkGen");
    }

    public void HideReward()
    {
        rewardPanel.SetActive(false);
    }

    public void StreamC()
    {
        Debug.Log("toe");
        Next();
        StopCoroutine("TalkGen");
        switch (dialogueLevel)
        {
            case 2:
                eggTalk = questionGod[dialogueLevel][0][2];
                for (int i = 0; i < breezeResponseText.Length; i++)
                {
                    breezeResponseText[i].text = answerGod[dialogueLevel][2][i];
                }
                break;

            case 3:
                {
                    if (question == 1)
                    {
                        eggTalk = questionGod[2][2][2];
                    }
                    if (question == 2)
                    {
                        eggTalk = questionGod[3][2][2];
                        approval -= 5;
                    }
                    AJustEnd();
                }
                break;
        }

    }

    public void Next()
    {
        dialogueLevel += 1;
        if (approval > 0)
        {
            approvalLevel = -(approval / 20);
            approvalBars[0].fillAmount = approvalLevel;
        }
        else if (approval == 0)
        {
            for (int i = 0; i < approvalBars.Length; i++)
            {
                approvalBars[i].fillAmount = 0;
            }
        }
        else
        {
            approvalLevel = (approval / 20);
            approvalBars[1].fillAmount = approvalLevel;
        }
    }

    public void Back()
    {
        StopCoroutine("TalkGen");
        dialogueLevel = 0;
        backButton.SetActive(false);
        shootButton.SetActive(true);

        for (int i = 0; i < BreezeResponse.Length; i++)
        {
            BreezeResponse[i].SetActive(false);
        }
        for (int i = 0; i < highfive.Length; i++)
        {
            highfive[i].SetActive(true);
        }
        for (int i = 0; i < questOptions.Length; i++)
        {
            questOptions[i].SetActive(false);
        }
        seeQuestsButton.SetActive(true);
        OpenDialogue();

    }

    public void AJustEnd()
    {
        dialogueLevel = 0;
        backButton.SetActive(false);
        shootButton.SetActive(true);

        for (int i = 0; i < BreezeResponse.Length; i++)
        {
            BreezeResponse[i].SetActive(false);
        }
        for (int i = 0; i < highfive.Length; i++)
        {
            highfive[i].SetActive(true);
        }
        for (int i = 0; i < questOptions.Length; i++)
        {
            questOptions[i].SetActive(false);
        }
        seeQuestsButton.SetActive(true);
    }

    #endregion

    public IEnumerator TalkGen()
    {
        eggtalkText.text = "";

        foreach(char c in eggTalk)
        {
            eggtalkText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    #region panelLogic


    //tells the inventory where to try and go, out or in.
    public void CloseInventory()
    {
        if (!inventoryUp)
            inventoryUp = true;
        else
            inventoryUp = false;
    }

    public void CloseStats()
    {
        if (!statsUp)
            statsUp = true;
        else
            statsUp = false;
    }
    public void MapUp()
    {
        if (mapUp)
        {
            mapUp = false;
        }
        else
        {
            mapUp = true;
        }
    }

    public void FpActive()
    {
        if (firstPerson)
        {
            firstPerson = false;
            Crosshair.SetActive(false);
        }
        else
        {
            Crosshair.SetActive(true);
            firstPerson = true;
        }
    }

    public void HelpPanel()
    {
        if (!helpOn)
        {
            helpOn = true;
            helpText.text = "X";
        }

        else
        {
            helpOn = false;
            helpText.text = "^";
        }

    }

    public void QuestUp()
    {
        if (questUp)
        {
            questUp = false;
            questCheckPopUp.SetActive(false);
        }
        else
        {
            questUp = true;
        }
    }

    #endregion

    #region ItemManagement
    //same as remove item but with an instatiate part, could probably be condensed but i lack motivation and time
    public void DropItem(int droppedItem)
    {
        Instantiate(ItemPrefabs[inventoryItems[droppedItem].itemNumber], dropSpawner.transform.position, Quaternion.identity);

        InGameItems[inventoryItems[droppedItem].itemNumber].itemCount--;
        RefreshDisplay();
    }

    //remove item
    public void DestroyItem(int itemNumber)
    {
        InGameItems[inventoryItems[itemNumber].itemNumber].itemCount--;
        RefreshDisplay();
    }

    //update the item list
    public void RefreshDisplay()
    {
        foreach (Items stowedItem in inventoryItems.ToArray())
        {
            if(stowedItem.itemCount <= 0)
            {
                inventoryItems.Remove(stowedItem);
                inventoryslots[inventoryItems.Count].SetActive(false);
            }
        }

        if (inventoryItems.Count == 0)
        {
            inventoryslots[0].SetActive(false);
        }

        //make sure the items display correctly
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryslots[i].SetActive(true);
            itemPicture[i].GetComponent<Image>().sprite = itemSprites[inventoryItems[i].itemNumber];
            itemTypeText[i].text = inventoryItems[i].itemType.ToString();
            slottedAmount[i].text = inventoryItems[i].itemCount.ToString();
            slottedItemWorth[i].text = inventoryItems[i].value.ToString() + " gold";
            slottedItemName[i].text = inventoryItems[i].name;
        }
        refreshItems = false;
        currencyText.text = currency.ToString();

        //turn on your cute necklace glow and visually equip items
        if(equippedItembool[10])
        {
            neckLight.SetActive(true);
        }
        else
        {
            neckLight.SetActive(false);
        }


        if (equippedItembool[0])
        {
            HeadObject.SetActive(true);
        }
        else
        {
            HeadObject.SetActive(false);
        }
        if (equippedItembool[7])
        {
            AxeObject.SetActive(true);
        }
        else
        {
            AxeObject.SetActive(false);
        }

    }

    public void UseItem(int usedItem)
    {
        //this part does not work for some reason and still allows you to use items
        if (inventoryItems[usedItem].itemType == ItemType.Treasure)
            return;

        if (inventoryItems[usedItem].itemType == ItemType.Consumable)
        {
            Function(inventoryItems[usedItem].itemNumber);
        }
        else if (inventoryItems[usedItem].itemType != ItemType.Treasure)
        {
            switch (inventoryItems[usedItem].itemType)
            {
                case ItemType.HeadArmour:
                    Equip(0, 11);
                    break;
                case ItemType.ChestArmour:
                    Equip(1, 2);
                    break;
                case ItemType.LegArmour:
                    Equip(2, 16);
                    break;
                case ItemType.Cloak:
                    Equip(3, 8);
                    break;
                case ItemType.WristArmour:
                    Equip(5, 7);
                    break;
                case ItemType.Shoulder:
                    Equip(4, 20);
                    break;
                case ItemType.PrimaryWeapon:
                    if(inventoryItems[usedItem].itemNumber == 3)
                        Equip(7, 3);
                    else
                        Equip(7, 21);
                    break;
                case ItemType.Shield:
                    Equip(8, 19);
                    break;
                case ItemType.RangedWeapon:
                    Equip(9, 4);
                    break;
                case ItemType.FootArmour:
                    Equip(13, 6);
                    break;
                case ItemType.HandArmour:
                    Equip(6, 10);
                    break;
                case ItemType.NeckSlot:
                    Equip(10, 15);
                    break;
                case ItemType.Belt:
                    Equip(11, 22);
                    break;
                case ItemType.RingSlot:
                    Equip(12, 17);
                    break;
            }

            InGameItems[inventoryItems[usedItem].itemNumber].itemCount--;
        }
    }

    public void Equip(int slotEquip, int descriptionInt)
    {
        //if there is an item in the slot it will be added to the inventory
        Dequip(slotEquip, descriptionInt);

        //activate item sprite
        Color slotColor = EquippedItems[slotEquip].GetComponent<Image>().color;
        slotColor = Color.white;
  
        EquippedItemText[slotEquip].text = InGameItems[descriptionInt].description;
        equippedItemint[slotEquip] = descriptionInt;
        EquippedItems[slotEquip].GetComponent<Image>().sprite = itemSprites[descriptionInt];
        equippedItembool[slotEquip] = true;
    }

    //a button friendly dequip method
    public void DequipButton(int slotDequip)
    {
        if (equippedItembool[slotDequip])
        {
            Dequip(slotDequip, equippedItemint[slotDequip]);
            equippedItembool[slotDequip] = false;
        }
    }
    
    public void Dequip(int slotDequip, int itemInt)
    {
        if (equippedItembool[slotDequip])
        {
            // add to inventory
            inventoryItems.Add(InGameItems[InGameItems[equippedItemint[slotDequip]].itemNumber]);
            inventoryslots[inventoryItems.Count-1].SetActive(true);

            InGameItems[InGameItems[equippedItemint[slotDequip]].itemNumber].itemCount = 1;

            //deactivate item sprite
            Color32 slotColor = EquippedItems[slotDequip].GetComponent<Image>().color;
            slotColor = new Color32(80, 80, 80, 255);
            EquippedItemText[slotDequip].text = "None";
            RefreshDisplay();
        }
    }

    public void Function(int itemNumber)
    {
        switch (itemNumber)
        {
            case 0:
                player.AssignableStatManager.regenerating = true;
                break;
            case 1:
                if(player.AssignableStatManager.regenStats[0][1] <= player.AssignableStatManager.regenStats[0][0] - 5)
                {
                    player.AssignableStatManager.regenStats[0][1] = player.AssignableStatManager.regenStats[0][1] + 5;
                }
                player.AssignableStatManager.healing = true;
                break;
            case 12:
                if (player.AssignableStatManager.regenStats[0][1] <= player.AssignableStatManager.regenStats[0][0] - 20)
                {
                    player.AssignableStatManager.regenStats[0][1] = player.AssignableStatManager.regenStats[0][1] + 20;
                }
                player.AssignableStatManager.healing = true;
                break;
            case 14:
                player.AssignableStatManager.regenStats[2][1] = player.AssignableStatManager.regenStats[2][1] + 20;
                if(player.AssignableStatManager.regenStats[2][1] > player.AssignableStatManager.regenStats[2][0])
                {
                    player.AssignableStatManager.regenStats[2][1] = player.AssignableStatManager.regenStats[2][0];
                }
                break;
            default:
                break;

        }
    }


    //sorts using the constructor.
    public void SortItems()
    {
        if (type && inventoryItems.Count > 0)
        {
            foreach (Items playerItems in inventoryItems.ToArray())
            {
                inventoryItems.Sort(new SortType());
            }

            sortText.text = "Sort:Type";

        }
        
        if(!type && inventoryItems.Count > 0)
        {
            foreach (Items playerItems in inventoryItems.ToArray())
            {
                inventoryItems.Sort(new SortName());
            }

            sortText.text = "Sort:Name";
        }

        if (type)
            type = false;
        else
            type = true;


        RefreshDisplay();
    }

    public void RefreshOnPickup()
    {
        Audio.boop = true;
        if(inventoryItems.Count < inventoryslots.Length)
        {
            if (!inventoryItems.Contains(InGameItems[refreshSlot]) || (inventoryItems.Contains(InGameItems[refreshSlot]) && InGameItems[refreshSlot].itemType != ItemType.Consumable))
            {
                inventoryItems.Add(InGameItems[refreshSlot]);
                InGameItems[refreshSlot].itemCount++;
            }
            else
            {
                InGameItems[refreshSlot].itemCount++;
            }

            StartCoroutine(PanelFlash(pickup));
        }
        else
        {
            Instantiate(ItemPrefabs[refreshSlot], dropSpawner.transform.position, Quaternion.identity);
        }

        RefreshDisplay();

    }

    public static void AddItem(int itemNumber)
    {
        refreshSlot = itemNumber;
        refreshItems = true;
    }

    public IEnumerator PanelFlash(CanvasGroup flashGroup)
    {
        flashGroup.alpha = 1;
        yield return new WaitForSeconds(0.06f);

        flashGroup.alpha = 0;
        yield return null;
    }

    #endregion

}

//allows some space saving higher up as i can just call it on one line.
public class Items
{
    public string name;
    public int value;
    public ItemType itemType;
    public Sprite itemImage;
    public int itemNumber;
    public int itemCount;
    public string description;

    public Items(string _name, int _value, ItemType _itemType, Sprite _itemImage, int _itemNumber, int _itemCount, string _description)
    {
        name = _name;
        value = _value;
        itemType = _itemType;
        itemImage = _itemImage;
        itemNumber = _itemNumber;
        itemCount = _itemCount;
        description = _description;
    }
}

public enum ItemType { Consumable, ChestArmour, FootArmour, HeadArmour, Shield, HandArmour , Shoulder, Cloak, WristArmour, LegArmour, RingSlot, NeckSlot, PrimaryWeapon, RangedWeapon, Material, Treasure, Belt };
