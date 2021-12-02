using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;     //��ȭâUI
    [SerializeField] GameObject go_DialogueNameBar; //��ȭâ �̸�

    [SerializeField] Text txt_Dialogue;     //��ȭâ �ؽ�Ʈ
    [SerializeField] Text txt_Name;         //��ȭâ �̸� �ؽ�Ʈ

    public InteractionCtrl interCtrl = null; //Ŭ���� npc������ �޾ƿ������ؼ� ������
    public CSV_ex csv_ex;                    //��ȭ ��ũ��Ʈ   
    
    bool isDialogue = false;    //��ȭâ�� ������ ������ ǥ��
    [SerializeField] InteractionNpc[] npcs = null;     //npc����� �����´�

    private void Start()
    {
        //��ȣ�ۿ� ��������Ʈ�� ����Ǹ� ShowDialogue�Լ� ȣ��
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue(int npcNum)
    {
        //�� npc ���� �ϳ� ������ְ�
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

        //dial ������ â�� ���ǵ�, Ŭ���� npc���� GetDialogue()�� �ؼ� �����´�
        Dictionary<string, object> dial = npc.GetDialogue();


        //��ȭ������ ������ && ������ ������ null�� �ƴҶ� ��ȭâ�� ���� ������ ����
        if (dial != null) 
        {
            //��ȭ������ �ٲ�
            isDialogue = true;

            //Ȥ�� ���� ������ ���� �ʵ��� �ؽ�Ʈ ���빰 ����ֱ�
            txt_Dialogue.text = "";
            txt_Name.text = "";

            //������ ������ ����ش�
            txt_Dialogue.text = dial["Dialogue"].ToString();
            txt_Name.text = dial["Name"].ToString();
        }
        
        //���� ���� ��Ȳ�̸� ��ȭâ�� �ݴ´�.
        else
        {
            isDialogue = false;
        }

        SettingUI(isDialogue);
        
      



    }



    // ��ȭâ ����,�ݱ�
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
    }
}
