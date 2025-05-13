using ilsFramework;
using UnityEngine;

public class ScreenManager : ManagerSingleton<ScreenManager>,IManager
{
    
    public Vector2Int CurrentScreenSize {get; private set;}
    
    public bool CurrentScreenIsFullScreen {get; private set;}
    
    private const string FULL_SCREEN_KEY = "FullScreenSize";
    private const string SCREEN_SIZEX_KEY = "SCREEN_SizeX_KEY";
    private const string SCREEN_SIZEY_KEY = "SCREEN_SizeY_KEY";
    public void Init()
    {
       var  x = PlayerPrefs.GetInt(SCREEN_SIZEX_KEY, 1920);
       var y = PlayerPrefs.GetInt(SCREEN_SIZEY_KEY, 1080);
       CurrentScreenSize = new Vector2Int(x, y);
       CurrentScreenIsFullScreen = PlayerPrefs.GetInt(FULL_SCREEN_KEY,1) == 1;
       
       CurrentScreenSize.LogSelf();
       
       SetIsFullScreen(CurrentScreenIsFullScreen);
       SetCurrentScreenSize(CurrentScreenSize);
    }

    public void Update()
    {
        
    }

    public void LateUpdate()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void OnDestroy()
    {
        PlayerPrefs.SetInt(SCREEN_SIZEX_KEY,CurrentScreenSize.x);
        PlayerPrefs.SetInt(SCREEN_SIZEY_KEY,CurrentScreenSize.y);
        PlayerPrefs.SetInt(FULL_SCREEN_KEY,CurrentScreenIsFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnDrawGizmos()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        
    }

    public void SetIsFullScreen(bool isFullScreen)
    {
        if (CurrentScreenIsFullScreen != isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }
        CurrentScreenIsFullScreen = isFullScreen;
    }

    public void SetCurrentScreenSize(Vector2Int size)
    {
        CurrentScreenSize =size;
        Screen.SetResolution(size.x, size.y, CurrentScreenIsFullScreen);
    }
}