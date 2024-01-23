using UnityEngine;

public class C_T001 : MonoBehaviour, MyHeroesImp
{
    float skillEft = 5.0f;


    //스폰 값 필요

    public GrowingData base_growing_data;
    public GameObject damage_prefab;
    public BaseCatData base_cat_data;

    BaseTanker c_t001;


    private void Awake()
    {
        c_t001 = new BaseTanker(base_cat_data, base_growing_data, damage_prefab);

    }

    private void FixedUpdate()
    {
        c_t001.Move();
    }

    public Cat GetTargetCat()
    {
        return c_t001 as Cat;
    }
}
