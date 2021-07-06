using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCompleter : MonoBehaviour
{
    [SerializeField] private Text completedQuestText;
    [SerializeField] private GameObject RewardPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && UIManager.activeQuests[0])
        {
            RewardPanel.SetActive(true);
            UIManager.questCheck = true;
            UIManager.completedquest[0] = true;
            UIManager.activeQuests[0] = false;
            player.AssignableStatManager.completeQuest = true;
            UIManager.approval += 5;
            completedQuestText.text += "The First quest has been completed! ";
        }
    }
}

