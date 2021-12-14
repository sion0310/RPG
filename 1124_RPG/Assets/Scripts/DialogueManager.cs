using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Delegate scripts")]
    public InteractionCtrl interCtrl = null;
    public QuestManager questManager = null;
    public DialogueUICtrl dialUI = null;
    public DialogueData dialData = null;

    string playerName = "�ÿ�";   //���߿� ��ǲ�ʵ�� �Է¹��� ���� �־��ش�

    DialogueData.Values values;

    int questIndex = 0;
    int dialIndex = 0;

    bool istalking = false;

    private void Start()
    {
        //�ݹ� �Լ���
        interCtrl.interact_pro = ShowDialogue;
        dialUI.exit_pro = ExitDial;
        dialUI.accept_pro = AcceptQuest;
        questManager.questMg = ShowQuest;

        //�����Ҷ����� ����Ʈ�� �����
        values = dialData.GetQuestValues(questIndex);
        questManager.GiveQuestState(values._giveNpcNum,values._doneNpcNum);
    }
    private void Update()
    {
        values = dialData.GetQuestValues(questIndex);
    }

    void ShowDialogue(GameObject hitobj)
    {
        //�޾ƿ� ������Ʈ�� npc�ѹ��� ������ npcNum�� �ִ´�
        int npcNum = hitobj.GetComponent<NpcInfor>().GetNum();
        //����Ʈ �Ŵ����� npc�� �������ִ� �Լ��� �־��ش�
        questManager.SetTalkNpc(npcNum);

        //���� ����: header�� ����Ʈ�� ���� �ٸ��� �����������Ѵ�
        string header = values._giveQ;

        string dialogue = GetDialogue(npcNum, header).Replace("����", playerName);
        //Ui�� ����ش�.
        dialUI.OpenDialBar(dialogue);
        
    }
    
    public string GetDialogue(int _npcNum,string _header)
    {
        //npc[npcNum]�� ���� �뺻�� dialogueNum��° ��縦 �����´�
        string _dialogue = dialData.GetDialogue(_npcNum, dialIndex, _header);
        if (_dialogue == "end")
        {
            _dialogue= dialData.GetDialogue(_npcNum, dialIndex-1, _header);
            dialUI.OpenQuestBar(values._questName, values._questExplan);
            return _dialogue;
        }
        dialIndex++;
        //������ ��� ��ȯ
        return _dialogue;
    }

    void ShowQuest()
    {
        dialUI.OpenQuestBar(values._questName, values._questExplan);
    }

    void ExitDial()
    {
        interCtrl.isTalking = false;
        dialIndex = 0;
    }

    void AcceptQuest()
    {
        interCtrl.isTalking = false;
        dialIndex = 0;
        questManager.AcceptQuest();
        
    }
}
