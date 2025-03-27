using ilsFramework;
using Sirenix.OdinInspector;

[AutoBuildOrLoadConfig("Game/GameControl")]
public class GameControlConfig : ConfigScriptObject
{
    public override string ConfigName => "GameControl";

    [LabelText("是否启用正常的游戏流程")]
    [ToggleLeft]
    public bool EnableCommenProcedure;
}