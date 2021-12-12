using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hitInfo;                 //���̿� ������ ������Ʈ
    public bool isContact = false;      //����Ʈ �ߺ������� �������� ���
    public bool isInteract = false;      //��ȣ�ۿ������� �ƴ���

    public delegate void InteractPro(int npcNum);     //��ȣ�ۿ� ��������Ʈ
    public InteractPro interact_pro = null;

    GameObject contactEffect;

    // Update is called once per frame
    void Update()
    {
        //��Ŭ�� �Լ�
        ClickLeftBtn();
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
            if (!isContact && !isInteract)
            {
                isContact = true;
                //���⿡ ��ȣ�ۿ밡���� ������Ʈ�� ���콺�� �ö�����
                //�ְ���� ����Ʈ�������� �ִ´�
                //��~ ������ �ȸ���ž�~
                contactEffect = hitInfo.transform.Find("QuestZoneBlue").gameObject;
                contactEffect.SetActive(true);
            }
        }
    }
    void NotContact()
    {
        if (isContact && !isInteract)
        {
            isContact = false;
            //���콺�� �������� ����� �ϴ� �͵� �ֱ�
            contactEffect.SetActive(false);
        }
    }


    void ClickLeftBtn()
    {
        if (isContact && !isInteract)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //��ȣ�ۿ��Ҷ� ����� �Լ�
                Interact();

            }
        }
       
    }
    
    void Interact()
    {
        //��ȣ�ۿ� ������ ǥ��
        isInteract = true;
        int npcNnm = hitInfo.transform.GetComponent<InteractionNpc>().GetNum();
        //��ȣ�ۿ� ��������Ʈ ����
        interact_pro?.Invoke(npcNnm);
        
    }
    public void NotInteract()
    {
        isInteract = false;
    }
    
}
