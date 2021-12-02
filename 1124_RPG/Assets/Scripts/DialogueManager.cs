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

    public InteractionCtrl interCtrl = null; //클릭한 npc정보를 받아오기위해서 가져옴
    public CSV_ex csv_ex;                    //대화 스크립트   
    
    bool isDialogue = false;    //대화창이 열리고 닫힘을 표시
    [SerializeField] InteractionNpc[] npcs = null;     //npc목록을 가져온다

    private void Start()
    {
        //상호작용 델리게이트가 실행되면 ShowDialogue함수 호출
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue(int npcNum)
    {
        //빈 npc 변수 하나 만들어주고
        InteractionNpc npc = null;
        //가져온 npc배열 중에서 클릭한 npcNum을 가진 npc를 변수에 넣는다
        foreach(InteractionNpc it in npcs)
        {
            if (it.GetNum() == npcNum)
            {
                npc = it;
                break;
            }
        }

        //dial 한줄을 창에 띄울건데, 클릭한 npc에서 GetDialogue()를 해서 가져온다
        Dictionary<string, object> dial = npc.GetDialogue();


        //대화중이지 않을때 && 가져온 내용이 null이 아닐때 대화창을 열고 내용을 띄운다
        if (dial != null) 
        {
            //대화중으로 바꿈
            isDialogue = true;

            //혹시 뭔가 쓰여져 있지 않도록 텍스트 내용물 비워주기
            txt_Dialogue.text = "";
            txt_Name.text = "";

            //가져온 내용을 띄워준다
            txt_Dialogue.text = dial["Dialogue"].ToString();
            txt_Name.text = dial["Name"].ToString();
        }
        
        //예외 적인 상황이면 대화창을 닫는다.
        else
        {
            isDialogue = false;
        }

        SettingUI(isDialogue);
        
      



    }



    // 대화창 열기,닫기
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
    }
}
