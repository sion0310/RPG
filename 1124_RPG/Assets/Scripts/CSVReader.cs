using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        //data.text�� ���� �ٲ𶧸��� �߶� lines�� �ִ´�
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        //lines�� ���̰� 1�����̸� �׳� ����Ʈ��
        if (lines.Length <= 1) return list;

        //lines ù ���� ĭ�� �ٲ𶧸��� �߶� header�� �ִ´�
        var header = Regex.Split(lines[0], SPLIT_RE);

        //header�� ������ ������ ���� ����
        for (var i = 1; i < lines.Length; i++)
        {
            //lines 1����� ĭ�� �ٲ𶧸��� �߶� values�� �ִ´�
            var values = Regex.Split(lines[i], SPLIT_RE);

            //������ ���̰� 0�̰ų� ���� �������� �����ܰ��
            if (values.Length == 0 || values[0] == "") continue;
            
            var entry = new Dictionary<string, object>();

            //�� ������ �׸񺰷� �־��ִ°���
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                
                string value = values[j];
                //���� \" ���� \" ���� ����� �߶� �ִ´ٴ°� ������(replace�� ���ڸ� �ٲ��ִ°�)
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;//�� ������ �ڷ����� �°� �ٲ��ִ� ����
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }

            //�з��� �����ֵ� ����Ʈ�� �־��ִ°�
            list.Add(entry);
        }
        return list;
    }
}