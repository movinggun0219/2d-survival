using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : Singleton<ResManager>
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class CharSprite
    {
        public Sprite[] deadSprite;
        public Sprite[] runSprite;
        public Sprite[] standSprite;
    }

    [System.Serializable]
    public class CharacterSprite
    {
        public string name;
        public CharSprite charSprite;
    }
    public CharacterSprite[] charSprite;
    

}
