using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;     //대화창UI
    [SerializeField] GameObject go_DialogueNameBar; //대화창 이름

    [SerializeField] Text txt_Dialogue;     //대화창 텍스트
    [SerializeField] Text txt_Name;         //대화창 이름 텍스트

    public InteractionCtrl interCtrl = null;
    public CSV_ex csv_ex;
    
    bool isDialogue = false;    //대화창이 열리고 닫힘을 표시

    private void Start()
    {
        //상호작용 델리게이트가 실행되면 ShowDialogue함수 호출
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue()
    {
        
        if (!isDialogue)
        {
            //혹시 뭔가 쓰여져 있지 않도록 텍스트 내용물 비워주기
            txt_Dialogue.text = "";
            txt_Name.text = "";
            isDialogue = true;
        }
        txt_Dialogue.text = csv_ex.GetData(0);
        SettingUI(isDialogue);
        
    }



    // 대화창 열기,닫기
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
    }
}
