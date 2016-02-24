using UnityEngine;
using System.Collections;

public class inwall : ICanbeFuck
{
    public int Hp;
    private SpriteRenderer SpriteRenderer;
    public Sprite DmgSprite;

    public void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Befuck(MoveObject mo)
    {
        Hp--;
        SpriteRenderer.sprite = DmgSprite;
        if (Hp<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
