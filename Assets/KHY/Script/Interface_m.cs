using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IDamage
{
    public string ID_m { get; set; } //���͸�
    protected float HP { get; set; } //ü��
    protected float maxHP { get; set; } //�ִ�ü��
    protected float Attack { get; set; } //���ݷ�
    protected int AtcTime { get; set; } //������Ÿ��

    protected float movespeed { get; set; }// �̵�
   // void Damage_m(float damage);
        
}

