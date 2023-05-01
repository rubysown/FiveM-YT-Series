namespace Server.Utils
{
    class Queries
    {
        public static string createPlayersTable = @"
            CREATE TABLE IF NOT EXISTS `players` (
                `id` int NOT NULL AUTO_INCREMENT,
                `identifier` varchar(128) NOT NULL,
                `money` float NOT NULL,
                `xp` int NOT NULL,
                PRIMARY KEY (`id`),
                UNIQUE KEY `identifier` (`identifier`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;";

        public static string initPlayer = @"
            INSERT IGNORE INTO `players` (identifier, money, xp)
            VALUES (@Identifier, @Money, @XP)";
    }
}
