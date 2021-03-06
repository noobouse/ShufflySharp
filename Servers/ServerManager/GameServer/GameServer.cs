using System;
using CommonLibraries;
using NodeLibraries.Common.Logging;
using NodeLibraries.NodeJS;
using global;
namespace ServerManager.GameServer
{
    public class GameServer
    {
        private ChildProcess childProcess;
        private string gameServerIndex;

        public GameServer()
        {
            gameServerIndex = "GameServer" + Guid.NewGuid();
            Logger.Start(gameServerIndex);
            
            new ArrayUtils();
            childProcess = Global.Require<ChildProcess>("child_process");
            Global.Scope.Fiber= Global.Require<NodeModule>("fibers");
            Global.Process.On("exit", () => Logger.Log("exi", LogLevel.Information));

            GameManager gameManager = new GameManager(gameServerIndex);
        }

 
    }
}