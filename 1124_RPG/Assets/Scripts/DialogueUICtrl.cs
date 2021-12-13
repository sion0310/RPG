using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUICtrl : MonoBehaviour
{
    public  delegate void DialUI();
    public DialUI dialUI_pro = null;


    [SerializeField] GameObject dialogueUI = null;     //��ȭâUI
    [SerializeField] Text txt_Dialogue = null;     //��ȭâ �ؽ�Ʈ

    public GameObject questUI;
    public Text txt_questName;
    public Text txt_questExplan;
    
    bool isDialogue = false;    //��ȭâ�� ������ ������ ǥ��
    bool getQuest = false;

    public void OpenDialBar(string dial)
    {
        //��ȭ������ �ٲ�
        isDialogue = true;

        //Ȥ�� ���� ������ ���� �ʵ��� �ؽ�Ʈ ���빰 ����ֱ�
        txt_Dialogue.text = "";

        //������ ������ ����ش�
        txt_Dialogue.text = dial;
    }

    public void OpenQuestBar(string questName,string questExplan)
    {
        getQuest = true;
        
        txt_questName.text = "";
        txt_questExplan.text = "";

        txt_questName.text = questName;
        txt_questExplan.text = questExplan;
    }
    
    private void Update()
    {
        //isDialogue(��ȭ������ �ƴ���)������ ��ȭâ�� �Ѱ� ����.
        SetDialUI(isDialogue);
        SetQuestUI(getQuest);
    }

    // ��ȭâ ����,�ݱ�
    void SetDialUI(bool flag)
    {
        dialogueUI.SetActive(flag);
    }

    void SetQuestUI(bool flag)
    {
        questUI.SetActive(flag);
    }

    public void ExitBtn()
    {
        dialUI_pro?.Invoke();
        
        //��ȭ���� �ƴ��� ǥ���ϰ�
        isDialogue = false;
        getQuest = false;
        
    }

    
}
