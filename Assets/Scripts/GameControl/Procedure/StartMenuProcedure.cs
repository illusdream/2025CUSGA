using System;
using Cysharp.Threading.Tasks;
using ilsFramework;
using UnityEngine.SceneManagement;

public class StartMenuProcedure : ProcedureNode
{
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
                
                GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderStartGame,ListenerToStartGame);
                UIManager.Instance.GetUIPanel<MenuUI>().Open();
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
                GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.OrderStartGame,ListenerToStartGame);
                
                UIManager.Instance.GetUIPanel<MenuUI>().Close();
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


        private async void ListenerToStartGame(EventArgs args)
        {
                if (!IsExecuting)
                        return;
                await SceneManager.LoadSceneAsync("SampleScene");
                ChangeState<GamePlayProcedure>();
        }
}