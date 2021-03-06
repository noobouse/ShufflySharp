using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NodeLibraries.MongoDB;
namespace Models.SiteManagerModels
{
    [Serializable]
    public class RoomData : MongoDocument
    {
        public string GameType { get; set; }
        public string RoomName { get; set; }
        public string ChatChannel { get; set; }
        public string GameChannel { get; set; }
        public string ChatServer { get; set; }
        public string GameServer { get; set; }
        public List<UserLogicModel> Players { get; set; }

        [ObjectLiteral]
        public RoomData(string gameType, string roomName, string chatChannel, string gameChannel, List<UserLogicModel> players, string chatServer = null, string gameServer = null)
        {
            GameType = gameType;
            RoomName = roomName;
            ChatChannel = chatChannel;
            GameChannel = gameChannel;
            ChatServer = chatServer;
            GameServer = gameServer;
            Players = players;
        }
    }
}