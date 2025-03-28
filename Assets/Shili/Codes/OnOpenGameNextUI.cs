using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[UIPanelSetting(EUILayer.Lower, 0, true, EAssetLoadMode.Resources, "Prefab/Shili/OnOpenGameNext")]
public class OnOpenGameNextUI : UIPanel
{
    //应该要写一些数字更新啥的，以及把数字放到玩家头上
    List<EntityID> entityIDs;
    [AutoUIElement("Player1")]
    private GameObject player1;
    [AutoUIElement("Player2")]
    private GameObject player2;
    public override void Open()
    {
        base.Open();
        UIPanelObject.SetActive(true);
        entityIDs = CharacterManager.Instance.GetAllPlayerID();
        for(int i = 0; i < entityIDs.Count; i++)
        {
            Debug.Log(entityIDs[i].ID);
        }
    }
    public override void Close()
    {
        base.Close();
        UIPanelObject.SetActive(false);
    }
}
