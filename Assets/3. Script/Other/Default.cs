using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    public static class Constants
    {
        //상수 관리 
        public static readonly int ACTIVECHARACTER_COUNT = 5;
    }

    public abstract class GenericDatabase<TKey, TValue> : ScriptableObject where TValue : IData<TKey>
    {
        [SerializeField] protected List<TValue> items = new List<TValue>();
        protected Dictionary<TKey, TValue> dict;

        //Awake() or 게임 초기화에서 사용
        public void Initialize()
        {
            dict = new Dictionary<TKey, TValue>();
            foreach(var itme in items)
            {
                var key = itme.GetKey(); //해당 데이터에 IData가 있어야할듯 skillDataBase라던가
                if(dict.ContainsKey(key)) Debug.LogError($"[GenericDatabase] 중복 키 발견: {key}");
                else dict.Add(key, itme);
            }
        }

        public TValue Get(TKey key)
        {
            if (dict == null) Initialize();
            dict.TryGetValue(key, out var value); //값을 참조로 전달
            return value;
        }

        public bool ContainsKey(TKey key)
        {
            if (dict == null) Initialize();
            return dict.ContainsKey(key);
        }

        public IReadOnlyList<TValue> GetAll() => items;
    }
}
