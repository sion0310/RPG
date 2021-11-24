using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hitInfo;                 //���̿� ������ ������Ʈ
    public bool isContact = false;      //����Ʈ �ߺ������� �������� ���
    public bool isInterat = false;      //��ȣ�ۿ������� �ƴ���

    public delegate void InteractPro();     //��ȣ�ۿ� ��������Ʈ
    public InteractPro interact_pro = null;

    // Update is called once per frame
    void Update()
    {
        ClickLeftBtn();
    }

    private void FixedUpdate()
    {
        CheckObject();
    }

    //���̷� ������Ʈ üũ
    void CheckObject()
    {
        Vector3 t_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        // ���콺�� �ö������� �ƴҶ� ����Ʈ�� �Ѱ� �������� Contact�� NotContact�Լ��� ����
        if(Physics.Raycast(cam.ScreenPointToRay(t_mousePos),out hitInfo, 100))
        {
            Contact();
        }
        else
        {
            NotContact();   
        }
    }

    void Contact()
    {
        // �±װ� "Interaction"�� ������Ʈ�� ��쿡�� �Լ��� ����ǰ� ��
        if (hitInfo.transform.CompareTag("Interaction"))
        {
            if (!isContact)
            {
                isContact = true;
                //���⿡ ��ȣ�ۿ밡���� ������Ʈ�� ���콺�� �ö�����
                //�ְ���� ����Ʈ�������� �ִ´�
                //��~ ������ �ȸ���ž�~
            }
        }
        else
        {
            NotContact();
        }
    }
    
    void NotContact()
    {
        if (isContact)
        {
            isContact = false;
        }
    }

    void ClickLeftBtn()
    {
        if (!isInterat)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isContact)
                {
                    Interact();
                }
            }
        }
        
    }
    
    void Interact()
    {
        isInterat = true;
        
        //��ȣ�ۿ� ��������Ʈ ����
        interact_pro?.Invoke();
        Debug.Log(hitInfo.transform.name);
    }
    
}
