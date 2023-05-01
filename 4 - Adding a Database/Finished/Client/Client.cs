using CitizenFX.Core;

namespace Client
{
    public class Client : BaseScript
    {
        public Client()
        {
            TriggerServerEvent("init_player");
        }
    }
}
