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
    public List<int> propChoiceButtonSet;
    public CustomPlayer(int id,int health,int energy,int cude, List<int> propChoiceButtonSet)
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
    private List<int> propChoiceButtonSet1;//玩家1能随机到的道具列表
    private List<int> propChoiceButtonSet2;//玩家2能随机到的道具列表
    private static shili_CustomUIManager instance;
    [ShowInInspector] private List<CustomPlayer> CustomPlayerlist;//玩家血量，能量阈值，初始方块数量，玩家能随机到的道具列表
    [ShowInInspector]private MapSet mapSet;//地图设置
    [ShowInInspector] private List<int> randomEventButtonSets;//随机事件列表
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
        propChoiceButtonSet1 = new List<int>(1) { 0 };
        propChoiceButtonSet2 = new List<int>(1) { 0};
        CustomPlayerlist = new List<CustomPlayer>();
        mapSet = new MapSet(10,5,5);
        randomEventButtonSets = new List<int>(1) { 0 };//记得给予一个默认的事件
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
        List<int> p = new List<int>();
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
    public List<int> GetRandomEventButtonSet()
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
        GameManager.Instance.SetPlayer1_RandomSelectedProps(CustomPlayerlist[0].propChoiceButtonSet.Select(i => (EPropType)(i)).ToList());
        Debug.Log(CustomPlayerlist[0].propChoiceButtonSet.Select(i => (EPropType)(i)).ToList()[0]);
        GameManager.Instance.SetPlayer2_RandomSelectedProps(CustomPlayerlist[1].propChoiceButtonSet.Select(i => (EPropType)(i)).ToList());
        Debug.Log(CustomPlayerlist[1].propChoiceButtonSet.Select(i => (EPropType)(i)).ToList()[0]);
        GameManager.Instance.CommonTileHealth = mapSet.neutralCubeHealth;
        GameManager.Instance.PlayerTileHealth = mapSet.playerCubeHealth;
        GameManager.Instance.RefreshTileEmptyInterval = mapSet.cubeTime;
        GameManager.Instance.SetLevelRandomSelectedEvents(randomEventButtonSets.Select(i=>(ERandomEventType)(i)).ToList());
        Debug.Log(randomEventButtonSets.Select(i => (ERandomEventType)(i)).ToList()[0]);
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderStartGame, EventArgs.Empty);
    }
}