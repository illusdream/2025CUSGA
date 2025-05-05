using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ilsFramework.GlobalEventSets;

public class InHouseUICanvas : MonoBehaviour
{
    private PlayerEnergyContainer playerEnergyContainer1;
    private PlayerEnergyContainer playerEnergyContainer2;
    private PlayerHealth playerHealth1;
    private PlayerHealth playerHealth2;
    public GameObject prefanSkill;
    public Text player1HealthText;
    public Text player1EnemyText;
    public Text player2HealthText;
    public Text player2EnemyText;
    public Image player1HealthImage;
    public Image player1EnergyImage;
    public Image player2HealthImage;
    public Image player2EnergyImage;
    //����
    private float player1Health = 1;
    private float currentPlayer1Health;
    private float player1Energy = 1;
    private float currentPlayer1Energy;
    private float player2Health = 1;
    private float currentPlayer2Health;
    private float player2Energy = 1;
    private float currentPlayer2Energy;
    [Header("���ܸ�����")]
    public RectTransform Player1SkillTransform;
    public RectTransform Player2SkillTransform;
    private bool shouldUpdata;

    private void OnEnable()
    {
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.PlayerGetNewProp, OnAddSkillUI);
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.PlayerComsumeProp, OnUseSkill);
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.PlayerSpawn, UpEnergyAndHealth);
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.GameOver, OnGameOver);
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.GameRestart,OnGameRestart);
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.PlayerCurrentUsePropChanged, OnRefreshPropUI);

    }
    private void OnDisable()
    {
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.PlayerGetNewProp, OnAddSkillUI);
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.PlayerComsumeProp, OnUseSkill);
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.PlayerSpawn, UpEnergyAndHealth);
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.GameOver, OnGameOver);
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.GameRestart,OnGameRestart);
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.PlayerCurrentUsePropChanged, OnRefreshPropUI);
    }
    private void OnGameOver(EventArgs e)
    {
        UIManager.Instance.GetUIPanel<GameOverUI>().Open();
        

    }
    private void OnGameRestart(EventArgs obj)
    {
        Transform transform;
        for(int i = 0;i < Player1SkillTransform.transform.childCount; i++)
        {
            transform = Player1SkillTransform.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }

        for(int i = 0;i < Player2SkillTransform.transform.childCount; i++)
        {
            transform = Player2SkillTransform.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }
    public void OnOpenSet()
    {
        UIManager.Instance.GetUIPanel<StopGameUI>().Open();
    }
    private void OnAddSkillUI(EventArgs e)
    {
        var p = e as PlayerEvent.PlayerGetNewPropEventArgs;
        GameObject go = Instantiate(prefanSkill);
        if (go.TryGetComponent<Image>(out var img) && PropManager.Instance.TryGetPropConfig<BasePropConfig>(p.PropType,out var propConfig))
        {
            img.sprite = propConfig.PropSprite;
        }
        if(p.PlayerID == 1)
        {
            go.transform.SetParent(Player1SkillTransform);
        }
        else
        {
            go.transform.SetParent(Player2SkillTransform);
        }
    }
    private void OnUseSkill(EventArgs e)
    {
        var p =e as PlayerEvent.PlayerUsingPropEventArgs;
        if(p.PlayerID == 1)
        {
           Destroy(Player1SkillTransform.GetChild(0).gameObject);
        }
        else
        {
            Destroy(Player2SkillTransform.GetChild(0).gameObject);
        }
    }

    private void OnRefreshPropUI(EventArgs e)
    {
        if (e is PlayerEvent.PlayerCurrentUsePropChangedEventArgs args)
        {
            Transform transform;
            switch (args.PlayerID)
            {
                case 1:
                    for(int i = 0;i < Player1SkillTransform.transform.childCount; i++)
                    {
                        transform = Player1SkillTransform.transform.GetChild(i);
                        GameObject.Destroy(transform.gameObject);
                    }

                    foreach (var type in args.NewPropTypes)
                    {
                        GameObject go = Instantiate(prefanSkill, Player1SkillTransform, true);
                        if (go.TryGetComponent<Image>(out var img) && PropManager.Instance.TryGetPropConfig<BasePropConfig>(type,out var propConfig))
                        {
                            img.sprite = propConfig.PropSprite;
                        }
                    }
                    break;
                case 2:
                    for(int i = 0;i < Player2SkillTransform.transform.childCount; i++)
                    {
                        transform = Player2SkillTransform.transform.GetChild(i);
                        GameObject.Destroy(transform.gameObject);
                    }
                    foreach (var type in args.NewPropTypes)
                    {
                        GameObject go = Instantiate(prefanSkill, Player2SkillTransform, true);
                        if (go.TryGetComponent<Image>(out var img) && PropManager.Instance.TryGetPropConfig<BasePropConfig>(type,out var propConfig))
                        {
                            img.sprite = propConfig.PropSprite;
                        }
                    }
                    break;
            }
        }
    }
    
    private void UpEnergyAndHealth(EventArgs e)
    {
        
        PlayerSpawnEventArgs playerSpawnEventArgs = e as PlayerSpawnEventArgs;
        var handler = playerSpawnEventArgs.Controller.handler;
        if (playerSpawnEventArgs.PlayerID == 1)
        {
            if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer, out playerEnergyContainer1))
            {
            }

            if(handler.TryGetComponet(EntityComponetUsage.Health,out playerHealth1))
            {
            }
        }
        if (playerSpawnEventArgs.PlayerID == 2)
        {
            if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer, out playerEnergyContainer2))
            {
            }
            if (handler.TryGetComponet(EntityComponetUsage.Health, out playerHealth2))
            {
            }
        }
        if(playerEnergyContainer1!=null&& playerEnergyContainer2 != null)
        {
            shouldUpdata = true;
        }
    }
    private void Update()
    {
        if (shouldUpdata)
        {
            currentPlayer1Energy = playerEnergyContainer1.CurrentEnergy;
            //player1EnemyInt = playerEnergyContainer1.MaxEnergy;
            player1Energy = 100;
            player1Health = playerHealth1.healthSources[EHealthSourceType.Life].BaseMaxHealth;
            currentPlayer1Health = playerHealth1.healthSources[EHealthSourceType.Life].CurrentHealth;
            currentPlayer2Energy = playerEnergyContainer2.CurrentEnergy;
            //player2EnemyInt = playerEnergyContainer2.MaxEnergy;
            player2Energy = 100;
            player2Health = playerHealth2.healthSources[EHealthSourceType.Life].BaseMaxHealth;
            currentPlayer2Health = playerHealth2.healthSources[EHealthSourceType.Life].CurrentHealth;
            //
            player1HealthImage.fillAmount = currentPlayer1Health / player1Health;
            player1EnergyImage.fillAmount = currentPlayer1Energy / player1Energy;
            player2HealthImage.fillAmount = currentPlayer2Health / player2Health;
            player2EnergyImage.fillAmount = currentPlayer2Energy / player2Energy;

            //
            player1HealthText.text = currentPlayer1Health.ToString("0.0") + "/" + player1Health;
            player1EnemyText.text = currentPlayer1Energy.ToString("0.0") + "/" + player1Energy;
            player2HealthText.text = currentPlayer2Health.ToString("0.0") + "/" + player2Health;
            player2EnemyText.text = currentPlayer2Energy.ToString("0.0") + "/" + player2Energy;
        }
        

    }
}
