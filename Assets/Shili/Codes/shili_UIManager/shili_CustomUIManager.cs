using System.Collections.Generic;
using UnityEngine;
public class CustomPlayer
{
    public int id;
    public int health;
    public int energy;
    public int cude;
    public List<PropChoiceButtonSet> propChoiceButtonSet;
    public CustomPlayer(int id,int health,int energy,int cude, List<PropChoiceButtonSet> propChoiceButtonSet)
    {
        this.id = id;
        this.health = health;
        this.energy = energy;
        this.cude = cude;
        this.propChoiceButtonSet = propChoiceButtonSet;
    }
}
public class shili_CustomUIManager : MonoBehaviour
{
    public List<PropChoiceButtonSet> propChoiceButtonSet1;
    public List<PropChoiceButtonSet> propChoiceButtonSet2;
    private static shili_CustomUIManager instance;
    private List<CustomPlayer> CustomPlayerlist;
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
        propChoiceButtonSet1 = new List<PropChoiceButtonSet>();
        propChoiceButtonSet2 = new List<PropChoiceButtonSet>();
        CustomPlayerlist = new List<CustomPlayer>();
        CustomPlayerlist.Add(new CustomPlayer(1,100,100,0, propChoiceButtonSet1));
        CustomPlayerlist.Add(new CustomPlayer(2, 100, 100, 0, propChoiceButtonSet2));
    }
    /// <summary>
    /// 保存时添加
    /// </summary>
    /// <param name="customPlayer"></param>
    public void AddCustomPlayer(CustomPlayer customPlayer)
    {
        int index = CustomPlayerlist.FindIndex(t => t.id == customPlayer.id);
        if (index != -1)
        {
            // 存在则替换
            CustomPlayerlist[index] = customPlayer;
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
        if(customPlayers.Count!= propChoiceButtonSet1.Count) return false;
        for(int i = 0; i < customPlayers.Count; i++)
        {
            if(propChoiceButtonSet1[i]!= customPlayers[i])
            {
                return false;
            }
        }
        return true;
    }
    public bool isSame2(List<PropChoiceButtonSet> customPlayers)
    {
        if (customPlayers.Count != propChoiceButtonSet2.Count) return false;
        for (int i = 0; i < customPlayers.Count; i++)
        {
            if (propChoiceButtonSet2[i] != customPlayers[i])
            {
                return false;
            }
        }
        return true;
    }
}