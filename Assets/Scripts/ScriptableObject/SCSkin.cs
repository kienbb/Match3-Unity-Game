using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Tools/Scriptable Object/Skin")]
public class SCSkin : ScriptableObject
{
    private static SCSkin instance;
    public static SCSkin Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = Resources.Load<SCSkin>(Constants.SKIN);
            }
            return instance;
        }
    }

    [Header("Just pick skin index before playing the game")]
    public int SelectedSkinIndex;
    public List<Skin> Skins = new List<Skin>();

    public Skin GetCurrentSkin()
    {        
        return Skins[SelectedSkinIndex];
    }

    [System.Serializable]
    public struct Skin
    {
        public string Name;
        public List<ElementDefine> Elements;
        public Sprite GetSprite(string name)
        {
            foreach (var element in Elements)
            {
                if (element.Name == name)
                {
                    return element.Sprite;
                }
            }
            return null;
        }
    }

    [System.Serializable]
    public struct ElementDefine
    {
        /// <summary>
        /// <see cref="Constants"/> class"/>
        /// </summary>
        public string Name;
        public Sprite Sprite;
    }
}
