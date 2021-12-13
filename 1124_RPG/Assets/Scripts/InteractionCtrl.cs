using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hitInfo;                 //���̿� ������ ������Ʈ
    public bool isContact = false;      //����Ʈ �ߺ������� �������� ���
    public bool isInteract = false;      //��ȣ�ۿ������� �ƴ���

    public delegate void InteractPro(GameObject hitobj);   //��ȣ�ۿ� ��������Ʈ
    public InteractPro interact_pro = null;

    GameObject contactEffect;
    GameObject hitobj;
    int npcNum;

    // Update is called once per frame
    void Update()
    {
        //��Ŭ�� �Լ�
        InteractProcess();
    }

    private void FixedUpdate()
    {
        //���̷� ������Ʈüũ
        CheckObject();
    }

    void CheckObject()
    {
        Vector3 t_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        // ���콺�� �ö������� �ƴҶ� ����Ʈ�� �Ѱ� �������� Contact�� NotContact�Լ��� ����
        if(Physics.Raycast(cam.ScreenPointToRay(t_mousePos),out hitInfo, 100))
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

    void Contact()
    {
        // �±װ� "Interaction"�� ������Ʈ�� ��쿡�� �Լ��� ����ǰ� ��
        if (hitInfo.transform.CompareTag("Interaction"))
        {
            //���콺�� ������� �ʰ�, ��ȣ�ۿ����� �ƴҶ�(��ȭ�߿� Ŭ���� npc���� ����Ʈ �ȶ�)
            if (!isContact && !isInteract)
            {
                //��ȣ�ۿ������� �ٲ��ְ�
                isContact = true;
                //������� ����Ʈ ���ֱ�
                contactEffect = hitInfo.transform.Find("QuestZoneBlue").gameObject;
                contactEffect.SetActive(true);
            }
        }
    }
    void NotContact()
    {
        //��ȣ�ۿ����� npc�� ����Ʈ�� ��� �����ְ� �ϱ� ���ؼ� ��ȣ�ۿ��� �ƴҶ��� ����
        if (isContact && !isInteract)
        {
            isContact = false;
            contactEffect.SetActive(false);
        }
    }


    void InteractProcess()
    {
        //���콺�� ����npc�� ��ȣ�ۿ� ������ ������ ��ȣ�ۿ� ����
        if (isContact && !isInteract)
        {   
            //��ȣ�ۿ� �����Ҷ� ����� �Լ�
            if (Input.GetMouseButtonDown(0))
            {
                isInteract = true;
                hitobj = hitInfo.transform.gameObject;
                interact_pro?.Invoke(hitobj);

            }
        }
        //��ȣ�ۿ� ���϶� ����Ǵ� �Լ�
        if (isInteract)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                interact_pro?.Invoke(hitobj);
            }
        }
       
    }
    

    //��ȣ�ۿ������� �������� ��ȭâ�� ������ �����ų� ����Ʈ�� ����������.
    public void NotInteract()
    {
        isInteract = false;
    }
    
}
