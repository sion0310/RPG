using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ν�����â���� ��������
[System.Serializable]
public class Dialgoue
{

    [Tooltip("ĳ���� �̸�")]
    public string name;

    [Tooltip("��� ����")]
    public string[] contexts;

    [Tooltip("�̺�Ʈ ��ȣ")]
    public string[] number;

    [Tooltip("��ŵ����")]
    public string[] skipnum;
    //[Tooltip("�̺�Ʈ��ȣ")]
    //public string[] number;

}

[System.Serializable]
public class DialgoueEvent
{

    //�̺�Ʈ �̸�
    public string name;

    //public Vector2 line;
    public Dialgoue[] dialgoues;

}
