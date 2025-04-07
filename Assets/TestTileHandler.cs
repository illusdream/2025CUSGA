using System;
using System.Collections;
using System.Collections.Generic;
using ilsFramework;
using UnityEngine;

public class TestTileHandler : MonoBehaviour
{
    private static readonly int DestoryAnimSingle = Shader.PropertyToID("_destoryAnimSingle");
    private static readonly int TillingAndOffest = Shader.PropertyToID("_TillingAndOffest");
    public SpriteRenderer spriteRenderer;

    public Sprite BreakSprite;
    
    public MaterialPropertyBlock materialPropertyBlock;
    // Start is called before the first frame update
    void Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        var textsize = new Vector2(spriteRenderer.sprite.texture.width, spriteRenderer.sprite.texture.height);
        var size = spriteRenderer.sprite.rect.size / textsize;
        var offset =spriteRenderer.sprite.rect.position / textsize;
        materialPropertyBlock.SetTexture(DestoryAnimSingle,BreakSprite.texture);
        materialPropertyBlock.SetVector(TillingAndOffest,new Vector4(size.x,size.y,offset.x,offset.y));
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
