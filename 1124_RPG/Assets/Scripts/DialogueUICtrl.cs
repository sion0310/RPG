using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUICtrl : MonoBehaviour
{
    public  delegate void DialUI();
    public DialUI exit_pro = null;
    public DialUI accept_pro = null;

    [Header("Dialogue UI")]
    [SerializeField] GameObject dialogueUI = null;     //��ȭâUI
    [SerializeField] Text txt_Dialogue = null;     //��ȭâ �ؽ�Ʈ

    [Header("Quest UI")]
    [SerializeField] GameObject questUI;
    [SerializeField] Text txt_questName;
    [SerializeField] Text txt_questExplan;
    [SerializeField] GameObject questDone;
    [SerializeField] Text txt_questAsk;

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

    public void OpenQuestBar(string _questName, string _questExplan, bool _questDone)
    {
        getQuest = true;
        
        txt_questName.text = "";
        txt_questExplan.text = "";
        txt_questAsk.text = "����Ʈ�� �����Ͻðڽ��ϱ�?";

        txt_questName.text = _questName;
        txt_questExplan.text = _questExplan;
        if (_questDone)
        {
            questDone.SetActive(true);
            txt_questAsk.text = "������ �����ðڽ��ϱ�?";
        }
        else questDone.SetActive(false);
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
        exit_pro?.Invoke();
        
        //��ȭ���� �ƴ��� ǥ���ϰ�
        isDialogue = false;
        getQuest = false;
        
    }

    public void AcceptBtn()
    {
        accept_pro?.Invoke();
        getQuest = false;
        isDialogue = false;
    }
    
    
}
