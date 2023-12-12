using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerItem : MonoBehaviour
{
   
   //이거는 담기는 아이템에 넣을 데이터로 쳐야하나
    public string ID { get; set; }
    protected int Lv = 0;  //레벨
    protected int MaxLv = 100;  //레벨
    public double effect1 { get; set; }//착용효과
    public double effect2 { get; set; }//착용효과
    public double increase1  { get; set; }
    public double increase2  { get; set; }
    public double retention_effect1 { get; set; } //보유효과
    public double retention_effect2 { get; set; } //보유효과
    public double retention_increase1 { get; set; }
    public double retention_increase2 { get; set; }
    public int ItemCount { get; set; }
    public bool Ative { get; set; }
    public bool ChoiceItem { get; set; }

    TextMeshProUGUI LvText;
    //Text Count;
    //int Maxnum = 4;

    private Button MyButton;
    private Button[] InfoButton;
    protected void initData()
    {
        Lv = 1;
        // 아이디, 증가값, 효과는 인스펙터창에서
        //StartCoroutine()

        LvText = GetComponentInChildren<TextMeshProUGUI>();
        LvText.text = "Lv: " +  Lv.ToString();

        item_name = InfoPlane.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        item_lv = InfoPlane.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();

        retention_effect1_name = InfoPlane.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        retention_effect1_current_value = retention_effect1_name.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        retention_effect1_update_value = retention_effect1_name.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();

        retention_effect2_name = InfoPlane.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        retention_effect2_current_value = retention_effect2_name.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        retention_effect2_update_value = retention_effect2_name.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();

        effectX1_name = InfoPlane.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        effectX1_current_value = effectX1_name.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        effectX1_update_value = effectX1_name.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();

        effectX2_name = InfoPlane.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        effectX2_current_value = effectX2_name.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        effectX2_update_value = effectX2_name.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();

        tooltip = InfoPlane.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        tooltip.text = "레벨 100 달성 시 파츠의 전체 효과가 2배가 됩니다.";


        MyButton = this.GetComponent<Button>();
        if (Ative && Lv != 100) //info버튼 활성화기능
        {
            MyButton.interactable = true;
        }
        else
        {
            MyButton.interactable = false; //버튼 비활성화 상태
        }
        InfoPlane.SetActive(false);
        InfoButton = InfoPlane.transform.GetChild(1).GetComponentsInChildren<Button>();
        InfoButton[0].interactable = true; //레벨업
        InfoButton[1].interactable = true;  //착용하기

        currency_sub_amout = InfoButton[0].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //처음이라면
        initData();
        //아니라면 유저 데이터 불러오기
        //Print();
        //info버튼 연결해주기
        MyButton.onClick.AddListener(ClickInfoButton);
        InfoButton[0].onClick.AddListener(LevelUP); //레벨업 버튼 연결
        InfoButton[1].onClick.AddListener(SelectCannon); //레벨업 버튼 연결
    }

    //코드 수정 1순위
    public void LevelUP()
    {

        if(Lv < MaxLv-1)
        {
            Lv++; //변경된 값 이상함 체크 필요
            print(ID +" 레벌업 1회");
            effect1 += increase1;
            retention_effect1 += retention_increase1;
            effect2 += increase2;
            retention_effect2 += retention_increase2;

            LvText.text = Lv.ToString();

        }
        else if(Lv==MaxLv-1)
        {
            effect1 *= 2;
            retention_effect1 *= 2;
            effect2 *= 2;
            retention_effect2 *= 2;
            //모든 효과 두배로 만들고 현재 레벨업 버튼 비활성
            InfoButton[0].onClick.RemoveListener(LevelUP);
            //다음 레벨업 버튼 생성
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
    TextMeshProUGUI item_name;
    TextMeshProUGUI item_lv;
    TextMeshProUGUI retention_effect1_name;
    TextMeshProUGUI retention_effect1_current_value;
    TextMeshProUGUI retention_effect1_update_value;
    TextMeshProUGUI retention_effect2_name;
    TextMeshProUGUI retention_effect2_current_value;
    TextMeshProUGUI retention_effect2_update_value;
    TextMeshProUGUI effectX1_name;
    TextMeshProUGUI effectX1_current_value;
    TextMeshProUGUI effectX1_update_value;
    TextMeshProUGUI effectX2_name;
    TextMeshProUGUI effectX2_current_value;
    TextMeshProUGUI effectX2_update_value;
    TextMeshProUGUI tooltip;
    TextMeshProUGUI currency_sub_amout;
    Image curreny_img;  //타워 데이터에 이미지 추가
    //Test Effect2;
    public void ClickInfoButton()
    {
        InfoPlane.SetActive(true);
        SetInfo();
    }

    void SetInfo() 
    {
        item_name.text = ID; //이름 x  / 정해지고 나면 변경
        item_lv.text = Lv.ToString();
        //보유효과
        retention_effect1_current_value.text = retention_effect1.ToString();
        retention_effect1_update_value.text = retention_effect1+ retention_increase1.ToString();
        retention_effect2_current_value.text = retention_effect2.ToString();
        retention_effect2_update_value.text = retention_effect2 + retention_increase2.ToString();
        //착용효과
        effectX1_current_value.text = effect1.ToString();
        effectX2_current_value.text = effect2.ToString();
        effectX1_update_value.text = effect1+ increase1.ToString();
        effectX2_update_value.text = effect2+ increase2.ToString();
        //업그레이드 비용
        currency_sub_amout.text = 0.ToString(); //변경사항
        if (ID[0] == 'C')
        {
            //캐논
            retention_effect1_name.text = "공격력";
            retention_effect2_name.text = "방어력";
            effectX1_name.text = "공격력 배수";
            effectX2_name.text = "방어력력 배수";

        }
        else
        {
            //수리공
            retention_effect1_name.text = "체력";
            retention_effect2_name.text = "회복력";
            effectX1_name.text = "체력 배수";
            effectX2_name.text = "회복력 배수";
        }
    }

   private void SelectCannon()
    {
        ChoiceItem = true;
    }

    //일시적으로 제한
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
