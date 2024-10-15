using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PuzzleComputer : MonoBehaviour
{
    [Header("���� ��ư")]
    public LineButton[] line_Button; // �̵� ��ư(���� ��)

    [Header("�� ���� ��")]
    public List<int> lineNum; // ( -1, 0, 1 ) �߽ɱ������� ����
    private bool allZero; // ��� ���� 0���� ����

    [Header("����")]
    public LEDNode computerLightLine; // ��ǻ�� ���⼱
    public LEDNode activateLightLine; // Ȱ��ȭ ���⼱
    public bool activate; // Ȱ��ȭ 

    [Header("���� ����")]
    public LightningRod lightningRod; // �������� ��ġ
    public bool on; // ��ǻ�� ���� ����
    public GameObject barrier; // ���ܸ�

    [Header("Ȱ��ȭ�� ������ ������Ʈ")]
    public RotateObject[] gears; // ȸ�� ���
    public GameObject jumpPad; // �����е�
    public RotateObject[] RotateFloors; // Ŭ���� ����

    void Start()
    {
        // ����Ʈ �ʱ�ȭ �� ����Ʈ �� �߰�
        lineNum = new List<int>(line_Button.Length);
        foreach (var button in line_Button)
        {
            lineNum.Add(button.curruntNum);
        }
        allZero = false; // ó���� false�� ����
    }

    private void Update()
    {
        // lightningRod.activate�� ���� �Ǹ� on�� true�� ����
        if (lightningRod.activate)
        {
            on = true;
        }

        // on�� true�� ��, �� ��ǻ�Ͱ� Ȱ��ȭ�� ��쿡�� ����
        if (on && !allZero)
        {
            barrier.SetActive(false); // ���ܸ� ��Ȱ��ȭ
            OnLight(); // ���⼱ �� ������Ʈ ���� ������Ʈ
        }
    }

    void OnLight()
    {
        // ��� ������Ʈ ���� Ȯ�� (���� 0����)
        allZero = lineNum.TrueForAll(num => num == 0);

        // ���⼱ ���� ������Ʈ
        computerLightLine.activate = allZero;
        activateLightLine.activate = allZero;

        // ���� ������Ʈ Ȱ��ȭ �� ���� ������Ʈ
        ActivateObjects(gears, allZero);
        ActivateObjects(RotateFloors, allZero);
        jumpPad.SetActive(allZero);
    }

    void ActivateObjects(RotateObject[] objects, bool activate) // ������Ʈ ���� Ȱ��ȭ
    {
        foreach (var obj in objects)
        {
            obj.activate = activate;
        }
    }

    public void UpdateBlockPositions(GameObject[] blocks) // ��� ��ġ ������Ʈ
    {
        if (blocks != null && blocks.Length >= 3)
        {
            Vector3[] positions = { new Vector3(0, 1.7f, 0), new Vector3(0, 1.7f, 0), new Vector3(0, -3.4f, 0) };
            for (int i = 0; i < blocks.Length && i < positions.Length; i++)
            {
                blocks[i].transform.position += positions[i];
            }
        }
    }
}