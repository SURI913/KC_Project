using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerItem : MonoBehaviour
{
   
   //�̰Ŵ� ���� �����ۿ� ���� �����ͷ� �ľ��ϳ�
    public string ID { get; set; }
    protected int Lv = 0;  //����
    protected int MaxLv = 100;  //����
    public double effect { get; set; }//����ȿ��
    public double increase  { get; set; }
    public double RetentionEffect { get; set; } //����ȿ��
    public double RetentionIncrease { get; set; }
    public int ItemCount { get; set; }
    public bool Ative { get; set; }
    public bool ChoiceItem { get; set; }

    Text LvText;
    //Text Count;
    //int Maxnum = 4;

    private Button MyButton;
    private Button[] InfoButton;
    protected void initData()
    {
        Lv = 1;
        // ���̵�, ������, ȿ���� �ν�����â����
        //StartCoroutine()

        LvText = GetComponentInChildren<Text>();
        LvText.text = "ID : " + ID;
        //this.transform.SetAsLastSibling(); //�̹��� ����
        Info = InfoPlane.transform.GetChild(1).GetComponent<Text>(); //2��° �ڽ�

        MyButton = this.GetComponent<Button>();
        if (Ative && Lv != 100) //info��ư Ȱ��ȭ���
        {
            MyButton.interactable = true;
        }
        else
        {
            MyButton.interactable = false; //��ư ��Ȱ��ȭ ����
        }
        InfoPlane.SetActive(false);
        InfoButton = InfoPlane.transform.GetChild(2).GetComponentsInChildren<Button>();
        InfoButton[0].interactable = true;

    }

    public void Print()
    {
        print(ID+" "+ Lv+" "+ effect+" "+ increase+" "+ RetentionEffect+" "+ RetentionIncrease);
    }

    private void Start()
    {
        //ó���̶��
        initData();
        //�ƴ϶�� ���� ������ �ҷ�����
        //Print();
        //info��ư �������ֱ�
        MyButton.onClick.AddListener(ClickInfoButton);
    }

    //�ڵ� ���� 1����
    public void LevelUP()
    {

        if(Lv < MaxLv-1)
        {
            Lv++; //����� �� �̻��� üũ �ʿ�
            print(ID +" ������ 1ȸ");
            effect += increase;
            RetentionEffect += RetentionIncrease;
            
            LvText.text = Lv.ToString();
            InfoButton[0].onClick.RemoveListener(LevelUP);

        }
        else if(Lv==MaxLv-1)
        {
            effect *= 2;
            RetentionEffect *= 2;
            //��� ȿ�� �ι�� ����� ���� ������ ��ư ��Ȱ��
            InfoButton[0].onClick.RemoveListener(LevelUP);
            //���� ������ ��ư ����
        }
        else
        {
            InfoButton[0].onClick.RemoveListener(LevelUP);
            InfoButton[0].interactable = false;
            return;
        }
        InfoButton[0].interactable = false;

        SetInfo();
    }

    [SerializeField] GameObject InfoPlane;
    Text Info;

    //Test Effect2;
    public void ClickInfoButton()
    {
        InfoPlane.SetActive(true);
        SetInfo();
        InfoButton[0].onClick.AddListener(LevelUP); //������ ��ư ����
        //���뵵 �۾��ؾ���
    }

    void SetInfo()
    {
        if (ID[0] == 'C')
        {
            //ĳ��
            Info.text = "Lv : " + Lv + '\n' + " ���� ���ݷ� : " + effect + '\n' + " ���� ���ݷ� : " + RetentionEffect;
        }
        else
        {
            //������
            Info.text = "Lv : " + Lv + '\n' + "ü�� : " + effect + '\n' + " ���� ü�� : " + RetentionEffect;
        }
    }

    //�Ͻ������� ����
    private float cooltime = 10f;
    private void Update()
    {
        if(cooltime >= 0 && !InfoButton[0].interactable)
        {
            cooltime -= Time.deltaTime;

        }
        else
        {
            cooltime = 10f;

            InfoButton[0].interactable = true;
        }

    }
}
