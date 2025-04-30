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
    private float player1HealthInt = 1;
    private float currentPlayer1HealthInt;
    private float player1EnemyInt = 1;
    private float currentPlayer1EnemyInt;
    private float player2HealthInt = 1;
    private float currentPlayer2HealthInt;
    private float player2EnemyInt = 1;
    private float currentPlayer2EnemyInt;
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
            go.transform.parent = Player1SkillTransform;
        }
        else
        {
            go.transform.parent = Player2SkillTransform;
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
        if (!shouldUpdata)
        {
            return;
        }
        currentPlayer1EnemyInt = playerEnergyContainer1.CurrentEnergy;
        player1EnemyInt = playerEnergyContainer1.MaxEnergy;
        player1HealthInt = playerHealth1.healthSources[EHealthSourceType.Life].BaseMaxHealth;
        currentPlayer1HealthInt = playerHealth1.healthSources[EHealthSourceType.Life].CurrentHealth;
        currentPlayer2EnemyInt = playerEnergyContainer2.CurrentEnergy;
        player2EnemyInt = playerEnergyContainer2.MaxEnergy;
        player2HealthInt = playerHealth2.healthSources[EHealthSourceType.Life].BaseMaxHealth;
        currentPlayer2HealthInt = playerHealth2.healthSources[EHealthSourceType.Life].CurrentHealth;
        //��ȡѪ�������������뵱ǰ����Ѫ�����ٸ�ֵ
        player1HealthText.text = currentPlayer1HealthInt + "/" + player1HealthInt;
        player1EnemyText.text = currentPlayer1EnemyInt + "/" + player1EnemyInt;
        player2HealthText.text = currentPlayer2HealthInt + "/" + player2HealthInt;
        player2EnemyText.text = currentPlayer2EnemyInt + "/" + player2EnemyInt;
        //Ѫ���������仯ʱ���º����Լ�4��current
        player1HealthImage.fillAmount = currentPlayer1HealthInt / player1HealthInt;
        player1EnergyImage.fillAmount = currentPlayer1EnemyInt / player1EnemyInt;
        player2HealthImage.fillAmount = currentPlayer2HealthInt / player2HealthInt;
        player2EnergyImage.fillAmount = currentPlayer2EnemyInt / player2EnemyInt;

    }
}
