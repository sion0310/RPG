using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //����־��� ���߿� ������!!!!!!!!
    public GameObject questManager;


    [SerializeField] GameObject go_DialogueBar;     //��ȭâUI

    [SerializeField] Text txt_Dialogue;     //��ȭâ �ؽ�Ʈ

    public InteractionCtrl interCtrl = null; //Ŭ���� npc������ �޾ƿ������ؼ� ������
    
    bool isDialogue = false;    //��ȭâ�� ������ ������ ǥ��
    [SerializeField] InteractionNpc[] npcs = null;     //npc����� �����´�
    public InteractionNpc talkingNpc = null;        //���� ��ȭ���� npc�� �ֱ� ���� ����

    private void Start()
    {
        //��ȣ�ۿ� ��������Ʈ�� ����Ǹ� ShowDialogue�Լ� ȣ��
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue(int npcNum)
    {
        // npc ���� �ϳ� ������ְ�
        InteractionNpc npc = null;
        //������ npc�迭 �߿��� Ŭ���� npcNum�� ���� npc�� ������ �ִ´�
        foreach(InteractionNpc it in npcs)
        {
            if (it.GetNum() == npcNum)
            {
                npc = it;
                break;
            }
        }
        
        // talkingNpc�� ������� npc�� �־��ش�.
        if (talkingNpc == null) talkingNpc = npc;
        // �� ���������, ���� ��ȭ�� ���������� npc�� �־��ش�.
        if (talkingNpc != null && talkingNpc.TalkDone()) talkingNpc = npc;

        //dial ������ â�� ���ǵ�, Ŭ���� npc���� GetDialogue()�� �ؼ� �����´�
        Dictionary<string, object> dial = talkingNpc.GetDialogue();

        //���� ���� �Ʒ� ������� �̸��� ������ �κ��� �÷��̾ ������ �̸����� �ٲܼ� �ִ�.(��絵 ����)
        //dial["give1"] = dial["give1"].ToString().Replace("����", "�ÿ�");


        //��ȭ�� �������̸�
        if (!talkingNpc.TalkDone()) 
        {
            //��ȭ������ �ٲ�
            isDialogue = true;

            //Ȥ�� ���� ������ ���� �ʵ��� �ؽ�Ʈ ���빰 ����ֱ�
            txt_Dialogue.text = "";

            //������ ������ ����ش�
            txt_Dialogue.text = dial["give1"].ToString();
        }

        //���� ��ȭ�� �����ٸ�
        if (talkingNpc.TalkDone())
        {
            //����Ʈ�� �ִ� ��簡 �����ٸ�, npc���¸� doing(2)���� �ٲ��ش�
            if(npc.npcState==InteractionNpc.NpcState.haveQuest)
                npc.NpcStateSetUp(2);
            //����Ʈ ������ �����߰� ��ȭ�� �����ٸ�, npc���¸� nomal(0)�� �ٲ��ش�
            if (npc.npcState == InteractionNpc.NpcState.doneQuest)
            {
                //����־��Ŷ� ���߿� �����ؾ���
                questManager.GetComponent<NpcManager>().ChangeValue();
            }
            //��ȭ���� �ƴ��� ǥ���ϰ�
            isDialogue = false;
            //��ȭ���� npc�� null�� �ٲ��ش�
            talkingNpc = null;
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
