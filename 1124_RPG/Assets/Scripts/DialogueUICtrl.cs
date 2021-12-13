using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUICtrl : MonoBehaviour
{
    public  delegate void DialUI();
    public DialUI dialUI_pro = null;


    [SerializeField] GameObject dialogueUI = null;     //대화창UI
    [SerializeField] Text txt_Dialogue = null;     //대화창 텍스트

    public GameObject questUI;
    public Text txt_questName;
    public Text txt_questExplan;
    
    bool isDialogue = false;    //대화창이 열리고 닫힘을 표시
    bool getQuest = false;

    public void OpenDialBar(string dial)
    {
        //대화중으로 바꿈
        isDialogue = true;

        //혹시 뭔가 쓰여져 있지 않도록 텍스트 내용물 비워주기
        txt_Dialogue.text = "";

        //가져온 내용을 띄워준다
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
        //isDialogue(대화중인지 아닌지)에따라 대화창을 켜고 끈다.
        SetDialUI(isDialogue);
        SetQuestUI(getQuest);
    }

    // 대화창 열기,닫기
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
        
        //대화중이 아님을 표시하고
        isDialogue = false;
        getQuest = false;
        
    }

    
}
