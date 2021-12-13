using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public InteractionCtrl interCtrl = null;

    public QuestManager questManager = null;
    public DialogueUICtrl dialUI = null;

    public DialogueData dialData = null;

    string playerName = "�ÿ�";   //���߿� ��ǲ�ʵ�� �Է¹��� ���� �־��ش�


    private void Start()
    {
        //��ȣ�ۿ� ��������Ʈ�� ����Ǹ� ShowDialogue�Լ� ȣ��
        interCtrl.interact_pro = ShowDialogue;
        dialUI.dialUI_pro = ExitDial;
        questManager.questMg = ShowQuest;
    }

    void ShowDialogue(GameObject hitobj)
    {
        //�޾ƿ� ������Ʈ�� npc�ѹ��� ������ npcNum�� �ִ´�
        int npcNum = hitobj.GetComponent<InteractionNpc>().GetNum();
        //����Ʈ �Ŵ����� npc�� �������ִ� �Լ��� �־��ش�
        questManager.SetTalkNpc(npcNum);
        //str������ ��縦 �޾ƿ´�
        string dialogue = questManager.GetDialogue().Replace("����", playerName);
        //Ui�� ����ش�.
        dialUI.OpenDialBar(dialogue);
    }

    void ShowQuest()
    {
        int _questIndex = questManager.GetQuestIndex();
        string _questName = dialData.GetQuest(_questIndex, "questName");
        string _questExplan = dialData.GetQuest(_questIndex, "explan");
        dialUI.OpenQuestBar(_questName, _questExplan);
    }

    void ExitDial()
    {
        interCtrl.isInteract = false;
        questManager.talkdone = true;
        questManager.dialogueNum = 0;
    }
}
