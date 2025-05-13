using System;
using Cysharp.Threading.Tasks;
using ilsFramework;
using UnityEngine.SceneManagement;

public class StartMenuProcedure : ProcedureNode
{
        AudioEmitter emitter;
        
        public override void OnInit()
        {
                base.OnInit();
        }

        public override void OnEnter()
        {
                if (SceneManager.GetActiveScene().buildIndex !=0)
                {
                        SceneManager.LoadScene(0);
                }
                GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderToGuidelinesScene, ListenerToOrderToGuidelinesScene);
                GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderStartGame,ListenerToStartGame);

                
                
                
                UIManager.Instance.GetUIPanel<MenuUI>().Open();
                UIManager.Instance.LoadUIPanel<UI_SystemFadeHandler>();
                
                var config = Config.GetConfig<GameControlConfig>();

                emitter = AudioManager.Instance.Play(AudioChannelName.BGM, config.StartMenuSound);
                
                ScreenManager.Instance.SetCurrentScreenSize(ScreenManager.Instance.CurrentScreenSize);
                
                base.OnEnter();
        }

        public override void OnUpdate()
        {
                base.OnUpdate();
        }

        public override void OnLateUpdate()
        {
                base.OnLateUpdate();
        }
        public override void OnFixedUpdate()
        {
                base.OnFixedUpdate();
        }

        public override void OnExit()
        {
        GlobalEventCenter.Instance?.RemoveListener(GlobalEventSets.OrderToGuidelinesScene, ListenerToOrderToGuidelinesScene);
        GlobalEventCenter.Instance?.RemoveListener(GlobalEventSets.OrderStartGame,ListenerToStartGame);
        emitter?.Stop();
                base.OnExit();
        }

        public override void OnDestroy()
        {

                base.OnDestroy();
        }

        public override void ChangeState<T>()
        {
                base.ChangeState<T>();
        }

        public override void ChangeStateByPopStack()
        {
                base.ChangeStateByPopStack();
        }


        private void ListenerToStartGame(EventArgs args)
        {
                if (!IsExecuting)
                        return;
                ChangeState<GamePlayProcedure>();
        }
    private void ListenerToOrderToGuidelinesScene(EventArgs args)
    {
        if (!IsExecuting)
            return;
        ChangeState<GamePlay_GuidelinesProcedure>();
    }
}