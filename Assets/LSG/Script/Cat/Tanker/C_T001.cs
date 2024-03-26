using UnityEngine;

public class C_T001 : BaseTanker, MyHeroesImp
{
    //스폰 값 필요

    public GrowingData base_growing_data;
    public CatData base_cat_data;


    private void Awake()
    {
        player_rb = GetComponent<Rigidbody2D>();
        cat_data = base_cat_data.all_cat_data[1];
        growing_data = base_growing_data;
        cat_motion = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
    }

    public Cat GetMyData()
    {
        return this;
    }
}
