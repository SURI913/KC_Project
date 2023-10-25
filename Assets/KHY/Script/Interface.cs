using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//interface는 클래스다 interface를 여러개 상속받을수 있음
//원래는 다중 상속 불가 

public interface IAttackDamage
{
   public void think();
}
public interface IAttackDetails
{
   // float atkspeed { get; set; }

//    float atkTime { get; set; }

}

