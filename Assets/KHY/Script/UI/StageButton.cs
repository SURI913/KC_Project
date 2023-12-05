using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StageButton : MonoBehaviour
{
    public int stageIndex; // �� ��ư�� �ش��ϴ� �������� ��ȣ�� �ν����Ϳ��� ����
    public Monster monster; // M_D01 ��ũ��Ʈ�� �ν����Ϳ��� �Ҵ�

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
       
    }
  
    public void OnButtonClick()
    {
        //�������� �ε����z �����ؼ� ����
        if (monster != null)
        {
            int rowIndex = stageIndex;

            if (rowIndex >= 1 && rowIndex <= monster.monsterData.monsterdatas.Length)
            {
                monster.SetMonsterDataByIndex(rowIndex);
            }
            else
            {
                Debug.LogError("Invalid stage index: " + stageIndex);
            }
        }
        /*
                if (monster != null)
                {

                    int rowIndex = stageIndex;

                    if (rowIndex >= 1 && rowIndex <= monster.monsterData.monsterdatas.Length)
                    {
                        // �ش� ���������� ���� �����͸� �������� ó��
                        //MonsterD ���� ������ ����Ʈ�� ���� (�����Ͱ� ����Ǿ�����)
                        //���������Ʈ�� ���� 1���� ������ �׷��� rowindex -1
                         MonsterD stageData = monster.monsterData.monsterdatas[rowIndex - 1];
                       //  monster.SetMonsterData(stageData);

                        // �������� �����͸� ����Ͽ� ������ �Ӽ��� ����
                    *//*    monster.stageID = stageData.stageID;
                        monster.HP = stageData.hp;
                        monster.Attack = stageData.attack;
                        monster.AtkTime = stageData.atktime;*//*

                        //Ȯ���� ���� ��� 
                        Debug.Log("Clicked stage button with stageIndex: " + stageIndex);
                        Debug.Log("Stage " + stageIndex + " - ID: " + monster.stageID + "," +
                            " HP: " + monster.HP + ", Attack: " + monster.Attack + ", Attack Time: " + monster.AtkTime);

                    }
                    else
                    {
                        Debug.LogError("Invalid stage index: " + stageIndex);
                    }
                }*/
        //Ŭ���ϰ� �����ϱ� ��ư ������ �� �ѱ��
        // ENTER ��ũ��Ʈ �ϳ� ¥�� �׳� , �ε� ���� �ϱ�� 
    }
}
//����Ƽ
/*, ������ ������ StageButton ��ũ��Ʈ�� UI ��ư�� �߰��ϰ� �� ��ư�� �ش��ϴ� 
 * �������� ��ȣ�� �Ҵ��ϸ� �˴ϴ�. �� ��ũ��Ʈ�� �� �������� ��ư�� Ŭ���Ǹ� 
 * �ش� �������� �����͸� �������� ���Ϳ� �Ҵ��ϴ� ������ �մϴ�. ���� �������� ��ư�� 
 * ���� ��ũ��Ʈ ���� ��ȣ �ۿ��� ���� �ν����Ϳ��� �� ��ư�� ���� M_D01 ���� ��ũ��Ʈ�� �Ҵ��ؾ� �մϴ�.

��ư�� Ŭ���� �� �ش� ���������� �����͸� �������� ������ �Ӽ��� �����Ϸ���
������ ������ ��ũ��Ʈ�� ����Ͻø� �˴ϴ�. �� ��ũ��Ʈ�� ���ϴ� ������ ������ ���Դϴ�.
*/
