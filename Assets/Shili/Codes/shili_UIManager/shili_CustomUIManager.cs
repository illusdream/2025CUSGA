using ilsFramework;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class CustomPlayer
{
    public int id;
    public int health;
    public int energy;
    public int cude;
    public List<EPropType> propChoiceButtonSet;
    public CustomPlayer(int id,int health,int energy,int cude, List<EPropType> propChoiceButtonSet)
    {
        this.id = id;
        this.health = health;
        this.energy = energy; 
        this.cude = cude;
        this.propChoiceButtonSet = propChoiceButtonSet;
    }
}
[System.Serializable]
public class MapSet
{
    public int playerCubeHealth;
    public int neutralCubeHealth;
    public int cubeTime;
    public MapSet(int playerCubeHealth,int neutralCubeHealth,int cubeTime)
    {
        this.playerCubeHealth = playerCubeHealth;
        this.neutralCubeHealth = neutralCubeHealth;
        this.cubeTime = cubeTime;
    }
}

public class shili_CustomUIManager : MonoBehaviour
{
    public bool isCustom;
    private List<EPropType> propChoiceButtonSet1;//玩家1能随机到的道具列表
    private List<EPropType> propChoiceButtonSet2;//玩家2能随机到的道具列表
    private static shili_CustomUIManager instance;
    [ShowInInspector] private List<CustomPlayer> CustomPlayerlist;//玩家血量，能量阈值，初始方块数量，玩家能随机到的道具列表
    [ShowInInspector]private MapSet mapSet;//地图设置
    [ShowInInspector] private List<ERandomEventType> randomEventButtonSets;//随机事件列表
    public static shili_CustomUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("shili_CustomUIManager").AddComponent<shili_CustomUIManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    private void Awake()
    {
        propChoiceButtonSet1 = new List<EPropType>(1) { PropManager.Instance.GetAllEPropTypes()[0] };
        propChoiceButtonSet2 = new List<EPropType>(1) { PropManager.Instance.GetAllEPropTypes()[0] };
        CustomPlayerlist = new List<CustomPlayer>();
        mapSet = new MapSet(10,5,5);
        randomEventButtonSets = new List<ERandomEventType>(1) { 0 };//记得给予一个默认的事件
        CustomPlayerlist.Add(new CustomPlayer(1,100,100,0, propChoiceButtonSet1));
        CustomPlayerlist.Add(new CustomPlayer(2, 100, 100, 0, propChoiceButtonSet2));
    }
    /// <summary>
    /// 保存时添加
    /// </summary>
    /// <param name="customPlayer"></param>
    public void AddCustomPlayer(int id,int hp,int e,int cs)
    {
        if(id == 1)
        {
            CustomPlayerlist[0].health = hp;
            CustomPlayerlist[0].energy = e;
            CustomPlayerlist[0].cude = cs;
        }
        else
        {
            CustomPlayerlist[1].health = hp;
            CustomPlayerlist[1].energy = e;
            CustomPlayerlist[1].cude = cs;
        }
    }
    /// <summary>
    /// 获取列表，用于比较来判断是否保存
    /// </summary>
    /// <returns></returns>
    public List<CustomPlayer> GetCustomPlayerlist()
    {
        return CustomPlayerlist;
    }
    public bool isSame1(List<PropChoiceButtonSet> customPlayers)
    {
        if(customPlayers.Count!= CustomPlayerlist[0].propChoiceButtonSet.Count) return false;
        for(int i = 0; i < customPlayers.Count; i++)
        {
            if(CustomPlayerlist[0].propChoiceButtonSet[i]!= customPlayers[i].id)
            {
                return false;
            }
        }
        return true;
    }
    public bool isSame2(List<PropChoiceButtonSet> customPlayers)
    {
        if (customPlayers.Count != CustomPlayerlist[1].propChoiceButtonSet.Count) return false;
        for (int i = 0; i < customPlayers.Count; i++)
        {
            if (CustomPlayerlist[1].propChoiceButtonSet[i] != customPlayers[i].id)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 分别填入：玩家方块血量，中立方块血量，方块刷新间隔
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    public void SetMapSet(int a,int b,int c)
    {
        mapSet.playerCubeHealth = a;
        mapSet.neutralCubeHealth = b;
        mapSet.cubeTime = c;
    }
    public MapSet GetMapSet()
    {
        return mapSet;
    }
    public void SetRandomEventButtonSets(List<RandomEventButtonSet> randomEventButtonSet)
    {
        List<ERandomEventType> p = new List<ERandomEventType>();
        for(int i  = 0;i < randomEventButtonSet.Count; i++)
        {
            p.Add(randomEventButtonSet[i].id);
        }
        randomEventButtonSets = p;
        for (int i = 0;i < randomEventButtonSets.Count; i++)
        {
            Debug.Log(randomEventButtonSets[i]);
        }
    }
    public List<ERandomEventType> GetRandomEventButtonSet()
    {
        return randomEventButtonSets;
    }
    public void OnStartGame()
    {
        GameManager.Instance.Player1_MaxHealth = CustomPlayerlist[0].health;
        GameManager.Instance.Player1_StartedHealth = CustomPlayerlist[0].health;
        GameManager.Instance.Player2_MaxHealth = CustomPlayerlist[1].health;
        GameManager.Instance.Player2_StartedHealth = CustomPlayerlist[1].health;
        GameManager.Instance.Player1_EnergyCanBeComeProp = CustomPlayerlist[0].energy;
        GameManager.Instance.Player2_EnergyCanBeComeProp = CustomPlayerlist[1].energy;
        GameManager.Instance.Player1StartHasBlockCount = CustomPlayerlist[0].cude;
        GameManager.Instance.Player2StartHasBlockCount = CustomPlayerlist[1].cude;
        GameManager.Instance.SetPlayer1_RandomSelectedProps(CustomPlayerlist[0].propChoiceButtonSet);
        GameManager.Instance.SetPlayer2_RandomSelectedProps(CustomPlayerlist[1].propChoiceButtonSet);
        GameManager.Instance.CommonTileHealth = mapSet.neutralCubeHealth;
        GameManager.Instance.PlayerTileHealth = mapSet.playerCubeHealth;
        GameManager.Instance.RefreshTileEmptyInterval = mapSet.cubeTime;
        GameManager.Instance.SetLevelRandomSelectedEvents(randomEventButtonSets);
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderStartGame, EventArgs.Empty);
        Debug.Log("随机事件");
        for (int i = 0; i < randomEventButtonSets.Count; i++)
        {
            Debug.Log(randomEventButtonSets[i]);
        }
        Debug.Log("玩家1道具");
        for (int i = 0; i < CustomPlayerlist[0].propChoiceButtonSet.Count; i++)
        {
            Debug.Log(CustomPlayerlist[0].propChoiceButtonSet[i]);
        }
        Debug.Log("玩家2道具");
        for (int i = 0; i < CustomPlayerlist[1].propChoiceButtonSet.Count; i++)
        {
            Debug.Log(CustomPlayerlist[1].propChoiceButtonSet[i]);
        }
    }
}