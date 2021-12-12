using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUICtrl : MonoBehaviour
{
    public QuestManager questManager = null;


    [SerializeField] GameObject go_DialogueBar = null;     //��ȭâUI

    [SerializeField] Text txt_Dialogue = null;     //��ȭâ �ؽ�Ʈ

    public InteractionCtrl interCtrl = null; //Ŭ���� npc������ �޾ƿ������ؼ� ������
    
    bool isDialogue = false;    //��ȭâ�� ������ ������ ǥ��
    
    string playerName = "�ÿ�";   //���߿� ��ǲ�ʵ�� �Է¹��� ���� �־��ش�

    int _npcNum;

    private void Start()
    {
        //��ȣ�ۿ� ��������Ʈ�� ����Ǹ� ShowDialogue�Լ� ȣ��
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue(int npcNum)
    {
        _npcNum = npcNum;
        questManager.SetTalkNpc(_npcNum);

        //���� ���� �Ʒ� ������� �̸��� ������ �κ��� �÷��̾ ������ �̸����� �ٲܼ� �ִ�.(��絵 ����)
        //dial["give1"] = dial["give1"].ToString().Replace("����", "�ÿ�");
        string dialogue = questManager.GetDialogue().Replace("����", playerName);

        //��ȭ�� �������̸�
        if (!questManager.CheckTalkDone()) 
        {
            //��ȭ������ �ٲ�
            isDialogue = true;

            //Ȥ�� ���� ������ ���� �ʵ��� �ؽ�Ʈ ���빰 ����ֱ�
            txt_Dialogue.text = "";

            //������ ������ ����ش�
            txt_Dialogue.text = dialogue;
            
        }

        //if(����Ʈ�� �ִ� ��ȭ�� �������̸�)
        //{
        //    ��ȭ����.���������� ��� ����
        //    ���� ������ �ٽ� ��ȭ ����
                
        //        isDialogue = false;
        //}

        else //���� ��ȭ�� �����ٸ�
        {
            //��ȭ���� �ƴ��� ǥ���ϰ�
            isDialogue = false;
            interCtrl.isInteract = false;
        }

        //isDialogue(��ȭ������ �ƴ���)������ ��ȭâ�� �Ѱ� ����.
        SettingUI(isDialogue);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShowDialogue(_npcNum);
        }
    }


    // ��ȭâ ����,�ݱ�
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
    }


    
}
