using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //대충넣어논거 나중에 정리해!!!!!!!!
    public QuestManager questManager;


    [SerializeField] GameObject go_DialogueBar;     //대화창UI

    [SerializeField] Text txt_Dialogue;     //대화창 텍스트

    public InteractionCtrl interCtrl = null; //클릭한 npc정보를 받아오기위해서 가져옴
    
    bool isDialogue = false;    //대화창이 열리고 닫힘을 표시

    private void Start()
    {
        //상호작용 델리게이트가 실행되면 ShowDialogue함수 호출
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue(int npcNum)
    {
        questManager.SetTalkNpc(npcNum);

        //차후 수정 아래 방식으로 이름이 나오는 부분을 플레이어가 지정한 이름으로 바꿀수 있다.(대사도 가능)
        //dial["give1"] = dial["give1"].ToString().Replace("ㅇㅇ", "시온");


        //대화가 진행중이면
        if (!questManager.CheckTalkDone()) 
        {
            //대화중으로 바꿈
            isDialogue = true;

            //혹시 뭔가 쓰여져 있지 않도록 텍스트 내용물 비워주기
            txt_Dialogue.text = "";

            //가져온 내용을 띄워준다
            txt_Dialogue.text = questManager.GetDialogue();
        }
        else //만약 대화가 끝났다면
        {
            //대충넣어논거라 나중에 수정해야함
            questManager.ChangeValue();
            questManager.AfterGiveTalk();

            //대화중이 아님을 표시하고
            isDialogue = false;
        }

        //isDialogue(대화중인지 아닌지)에따라 대화창을 켜고 끈다.
        SettingUI(isDialogue);
    }



    // 대화창 열기,닫기
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
    }


    
}
