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
    
    bool isDialogue = false;    //대화창이 열리고 닫힘을 표시
    [SerializeField] InteractionNpc[] npcs = null;     //npc목록을 가져온다
    public InteractionNpc talkingNpc = null;        //현재 대화중인 npc를 넣기 위한 변수

    private void Start()
    {
        //상호작용 델리게이트가 실행되면 ShowDialogue함수 호출
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue(int npcNum)
    {
        // npc 변수 하나 만들어주고
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
        
        // talkingNpc가 비었으면 npc를 넣어준다.
        if (talkingNpc == null) talkingNpc = npc;
        // 안 비었을때엔, 이전 대화가 끝났을때만 npc를 넣어준다.
        if (talkingNpc != null && talkingNpc.TalkDone()) talkingNpc = npc;

        //dial 한줄을 창에 띄울건데, 클릭한 npc에서 GetDialogue()를 해서 가져온다
        Dictionary<string, object> dial = talkingNpc.GetDialogue();

        //차후 수정 아래 방식으로 이름이 나오는 부분을 플레이어가 지정한 이름으로 바꿀수 있다.(대사도 가능)
        dial["Name"] = dial["Name"].ToString().Replace("ㅇㅇ", "시온");


        //대화가 진행중이면
        if (!talkingNpc.TalkDone()) 
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

        //만약 대화가 끝났다면
        if (talkingNpc.TalkDone())
        {
            //대화중이 아님을 표시하고
            isDialogue = false;
            //대화중인 npc를 null로 바꿔준다
            talkingNpc = null;
        }

        //isDialogue(대화중인지 아닌지)에따라 대화창을 켜고 끈다.
        SettingUI(isDialogue);
    }



    // 대화창 열기,닫기
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
    }
}
