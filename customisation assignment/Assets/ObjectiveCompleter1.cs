using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCompleter1 : MonoBehaviour
{
    [SerializeField] private Text completedQuestText;
    [SerializeField] private GameObject RewardPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && UIManager.activeQuests[1])
        {
            RewardPanel.SetActive(true);
            UIManager.questCheck = true;
            UIManager.completedquest[1] = true;
            UIManager.activeQuests[1] = false;
            player.AssignableStatManager.completeQuest = true;
            UIManager.approval += 5;
            completedQuestText.text += "The second quest has been completed! ";
        }


    }
}

