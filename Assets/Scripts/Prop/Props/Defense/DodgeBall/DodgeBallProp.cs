using System;
using UnityEngine;

namespace Props
{
    public class DodgeBallProp : BaseProp,IPropApplyEffect
    {
        public int CanUseCount = 2;
        public override Type ConfigType => typeof(DodgeBallPropConfig);

        public override Type PropStateType => typeof(DodgeBallState);

        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void ApplyEffect(EntityHandler handler)
        {
            var mapsize = TileManager.Instance.GetTileMapSize();
            float offest = 0.5f;
            var halfWidth = mapsize.width / 2f -offest;
            var halfHeight = mapsize.height / 2f -offest;
            handler.transform.position = new Vector3((mapsize.center.x - halfWidth,mapsize.center.x + halfWidth).RandomRange(), (mapsize.center.y - halfHeight,mapsize.center.y + halfHeight).RandomRange());
        }
    }
}