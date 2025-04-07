using System;
using ilsFramework;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

/// <summary>
/// 用于处理Tile绘制与物理碰撞
/// </summary>
public class TileHandler : MonoBehaviour
{
    public SpriteRenderer tileSpriteRender;
    
    private static readonly int DestoryAnimSingle = Shader.PropertyToID("_destoryAnimSingle");
    private static readonly int TillingAndOffest = Shader.PropertyToID("_TillingAndOffest");
    private static readonly int DestoryTillingAndOffest = Shader.PropertyToID("_destoryTillingAndOffest");

    public Sprite BreakSprite;
    
    private MaterialPropertyBlock materialPropertyBlock;
    
    
    private PlayableGraph playableGraph;
    private AnimationPlayableOutput output;

    public Animator animator;
    
    public AnimationClip animationClip;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        playableGraph = PlayableGraph.Create();
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        
        output = AnimationPlayableOutput.Create(playableGraph, "TileAnimationHandler", animator);
    }

    void Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        tileSpriteRender.GetPropertyBlock(materialPropertyBlock);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        tileSpriteRender.GetPropertyBlock(materialPropertyBlock);
        if (tileSpriteRender?.sprite)
        {
            var textsize = new Vector2(tileSpriteRender.sprite.texture.width, tileSpriteRender.sprite.texture.height);
            var mainTexSize = tileSpriteRender.sprite.rect.size / textsize;
            var mainTexOffset =tileSpriteRender.sprite.rect.position / textsize;
            materialPropertyBlock.SetVector(TillingAndOffest,new Vector4(mainTexSize.x,mainTexSize.y,mainTexOffset.x,mainTexOffset.y));
        }
        materialPropertyBlock.SetTexture(DestoryAnimSingle,BreakSprite.texture);
        if (BreakSprite)
        {
            var destroyTexSize = new Vector2(BreakSprite.texture.width, BreakSprite.texture.height);
            var destroySpriteSize = BreakSprite.rect.size / destroyTexSize;
            var destroySpriteOffset =BreakSprite.rect.position / destroyTexSize;
            materialPropertyBlock.SetVector(DestoryTillingAndOffest,new Vector4(destroySpriteSize.x,destroySpriteSize.y,destroySpriteOffset.x,destroySpriteOffset.y));
        }
        tileSpriteRender.SetPropertyBlock(materialPropertyBlock);
    }

    public void SetDestroySprite(Sprite sprite)
    {
        BreakSprite = sprite;
    }
    public void PlayTileAnimation(AnimationClip animationClip)
    {
        AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(playableGraph,animationClip);
        output.SetSourcePlayable(clipPlayable);
        
        playableGraph.Play();
    }

    private void OnDestroy()
    {
        playableGraph.Destroy();;
    }
}