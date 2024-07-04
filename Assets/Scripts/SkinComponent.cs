using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SkinComponent : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private SpriteRenderer spriteRenderer;
    private void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        SCSkin.Skin skin = SCSkin.Instance.GetCurrentSkin();
        Sprite sprite = skin.GetSprite(this.name);
        spriteRenderer.sprite = sprite;
    }
}
