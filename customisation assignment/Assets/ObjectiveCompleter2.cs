using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCompleter2 : MonoBehaviour
{
    [SerializeField] private Text completedQuestText;
    [SerializeField] private GameObject RewardPanel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && UIManager.activeQuests[2])
        {
            RewardPanel.SetActive(true);
            UIManager.questCheck = true;
            UIManager.completedquest[2] = true;
            UIManager.activeQuests[2] = false;
            player.AssignableStatManager.completeQuest = true;
            UIManager.approval += 5;
            completedQuestText.text += "The Third quest has been completed! ";
        }
    }
}

