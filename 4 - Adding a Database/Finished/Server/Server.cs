using CitizenFX.Core;
using Dapper;
using Server.Utils;
using System;

namespace Server
{
    public class Server : BaseScript
    {
        public Server()
        {
            EventHandlers["init_player"] += new Action<Player>(InitPlayer);

            ServerInit();
        }

        private async void ServerInit()
        {
            using (var connection = Database.GetConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(Queries.createPlayersTable);
                Debug.WriteLine($"Server Init rowsAffected: {rowsAffected}");
            }
        }

        private async void InitPlayer([FromSource] Player source)
        {
            Debug.WriteLine("Calling Init Player!");
            var parameters = new { Identifier = source.Identifiers["license"], Money = 5000, XP = 0 };
            using (var connection = Database.GetConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(Queries.initPlayer, parameters);
                Debug.WriteLine($"Player Init rowsAffected: {rowsAffected}");
            }
        }
    }
}
