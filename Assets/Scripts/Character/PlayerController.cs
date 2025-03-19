public class PlayerController : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerController;

        public int PlayerID { get;private set; }
        
        
        public void Initialize(int playerID)
        {
                PlayerID = playerID;
        }
}