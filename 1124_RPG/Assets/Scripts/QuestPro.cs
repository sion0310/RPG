using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPro : MonoBehaviour
{
    public delegate void QuestAchieve(bool isAchieved);
    public QuestAchieve questAchieve = null;
    public bool isAchieved;

    private void Update()
    {
        if (isAchieved)
        {
            questAchieve?.Invoke(isAchieved);
        }
    }
    public void AchievedQuest()
    {
        isAchieved = true;
    }


}
