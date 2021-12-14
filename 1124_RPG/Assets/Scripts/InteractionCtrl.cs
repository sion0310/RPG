using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    public delegate void InteractPro(GameObject hitobj);   //��ȣ�ۿ� ��������Ʈ
    public InteractPro interact_pro = null;

    [Header("for raycast")]
    [SerializeField] Camera cam;        
    RaycastHit hit;                     

    [Header("check state")]
    public bool isContact = false;      //����Ʈ �ߺ������� �������� ���(���콺�� ��Ҵ��� üũ)
    public bool isTalking = false;      //��ȣ�ۿ������� �ƴ���

    GameObject contactEffect;       //���콺�� ������ ����Ʈ
    GameObject hitobj;              //���̿� ������ ������Ʈ�� ������� ����


    void Update()
    {
        CheckObject();      //���̷� ������Ʈüũ
        InteractProcess();  //��Ŭ�� �Լ�
    }
    
    //���̷� ������Ʈ�� üũ�ϴ� �Լ�
    void CheckObject()
    {
        Vector3 t_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        // ���콺�� ������ ����Ʈ�� �Ѱ� �������� Contact�� NotContact�Լ��� ����
        if(Physics.Raycast(cam.ScreenPointToRay(t_mousePos),out hit, 100))
        {
            //���콺�� ���� ������ ����� �Լ�
            Contact();
        }
        else
        {
            //���콺�� ���� ������ ����� �Լ�
            NotContact();   
        }
    }

    //���콺�� ������Ʈ�� ������ ����ȴ�.
    void Contact()
    {
        // �±װ� "Interaction"�� ������Ʈ�� ��쿡�� �Լ��� ����ǰ� ��(ex:npc)
        if (hit.transform.CompareTag("Interaction"))
        {
            //���콺�� ������� �ʰ�(����Ʈ�� �������� ������� �ʱ�����),
            //��ȣ�ۿ����� �ƴҶ�(��ȣ�ۿ����� npc�� ����Ʈ��� �ѳ���)
            if (!isContact && !isTalking)
            {
                //���콺�� ��Ҵ�
                isContact = true;
                //���� ��ü�� ����Ʈ ������Ʈ�� ������ �־��ش�
                contactEffect = hit.transform.Find("QuestZoneBlue").gameObject;
                //����Ʈ�� ���ش�
                contactEffect.SetActive(true);
            }
        }
    }

    //���콺�� ���� ������ ����Ǵ� �Լ�
    void NotContact()
    {
        //��ȣ�ۿ����� npc�� ����Ʈ�� ��� �����ְ� �ϱ� ���ؼ� ��ȣ�ۿ��� �ƴҶ��� ����
        if (isContact && !isTalking)
        {
            //���콺�� ���� �ʾҴ�
            isContact = false;
            //����Ʈ ����
            contactEffect.SetActive(false);
        }
    }

    //��ȣ�ۿ��� �Ҷ� �Լ�
    void InteractProcess()
    {
        //���콺�� ����npc�� ��ȭ ������ ������ ��ȭ ����
        if (isContact && !isTalking)
        {   
            //��ȣ�ۿ� �����Ҷ� ����� �Լ�
            if (Input.GetMouseButtonDown(0))
            {
                //��ȣ�ۿ����̴�
                isTalking = true;
                //���̿� ���� ������Ʈ�� ������ �־��ش�
                hitobj = hit.transform.gameObject;
                //��ȣ�ۿ� ���϶� ����Ǵ� �ݹ�(� ������Ʈ���� �Ѱ���)
                interact_pro?.Invoke(hitobj);
            }
        }
        //��ȣ�ۿ� ���϶� ����Ǵ� �Լ�
        if (isTalking)
        {
            //��ȭ�� ����Ǹ� GŰ�� ���� ��縦 �ѱ� �� �ִ�
            if (Input.GetKeyDown(KeyCode.G))
            {
                interact_pro?.Invoke(hitobj);
            }
        }
    }

}
