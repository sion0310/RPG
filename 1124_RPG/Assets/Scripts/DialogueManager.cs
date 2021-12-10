using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //����־��� ���߿� ������!!!!!!!!
    public QuestManager questManager;


    [SerializeField] GameObject go_DialogueBar;     //��ȭâUI

    [SerializeField] Text txt_Dialogue;     //��ȭâ �ؽ�Ʈ

    public InteractionCtrl interCtrl = null; //Ŭ���� npc������ �޾ƿ������ؼ� ������
    
    bool isDialogue = false;    //��ȭâ�� ������ ������ ǥ��

    private void Start()
    {
        //��ȣ�ۿ� ��������Ʈ�� ����Ǹ� ShowDialogue�Լ� ȣ��
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue(int npcNum)
    {
        questManager.SetTalkNpc(npcNum);

        //���� ���� �Ʒ� ������� �̸��� ������ �κ��� �÷��̾ ������ �̸����� �ٲܼ� �ִ�.(��絵 ����)
        //dial["give1"] = dial["give1"].ToString().Replace("����", "�ÿ�");


        //��ȭ�� �������̸�
        if (!questManager.CheckTalkDone()) 
        {
            //��ȭ������ �ٲ�
            isDialogue = true;

            //Ȥ�� ���� ������ ���� �ʵ��� �ؽ�Ʈ ���빰 ����ֱ�
            txt_Dialogue.text = "";

            //������ ������ ����ش�
            txt_Dialogue.text = questManager.GetDialogue();
        }
        else //���� ��ȭ�� �����ٸ�
        {
            //����־��Ŷ� ���߿� �����ؾ���
            questManager.ChangeValue();
            questManager.AfterGiveTalk();

            //��ȭ���� �ƴ��� ǥ���ϰ�
            isDialogue = false;
        }

        //isDialogue(��ȭ������ �ƴ���)������ ��ȭâ�� �Ѱ� ����.
        SettingUI(isDialogue);
    }



    // ��ȭâ ����,�ݱ�
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
    }


    
}
