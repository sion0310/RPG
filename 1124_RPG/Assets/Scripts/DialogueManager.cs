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

    public InteractionCtrl interCtrl = null;
    
    bool isDialogue = false;    //��ȭâ�� ������ ������ ǥ��

    private void Start()
    {
        //��ȣ�ۿ� ��������Ʈ�� ����Ǹ� ShowDialogue�Լ� ȣ��
        interCtrl.interact_pro = ShowDialogue;
    }

    public void ShowDialogue()
    {
        //Ȥ�� ���� ������ ���� �ʵ��� �ؽ�Ʈ ���빰 ����ֱ�
        txt_Dialogue.text = "";
        txt_Name.text = "";

        SettingUI(true);
    }

    // ��ȭâ ����,�ݱ�
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
    }
}
