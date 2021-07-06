using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCompleter3 : MonoBehaviour
{
    [SerializeField] private Text completedQuestText;
    [SerializeField] private GameObject RewardPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && UIManager.activeQuests[3])
        {
            RewardPanel.SetActive(true);
            UIManager.questCheck = true;
            UIManager.completedquest[3] = true;
            UIManager.activeQuests[3] = false;
            player.AssignableStatManager.completeQuest = true;
            UIManager.approval += 5;
            completedQuestText.text += "The Final quest has been completed! ";
        }


    }
}

