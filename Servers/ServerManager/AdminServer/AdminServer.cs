using System;
using System.Collections;
using System.Collections.Generic;
using CommonLibraries;
using CommonShuffleLibrary;
using NodeLibraries.Common.Logging;
using NodeLibraries.NodeJS;
using NodeLibraries.Redis;
namespace ServerManager.AdminServer
{
    public class AdminServer
    {
        private string __dirname;
        private List<ProcessInformation> chats;
        private bool debug;
        private List<ProcessInformation> debugs;
        private Func<string, Process> exec;
        private FS fs = Global.Require<FS>("fs");
        private List<ProcessInformation> games;
        private List<ProcessInformation> gateways;
        private ProcessInformation head;
        private int indexPageData = 0;
        private Process nodeInspector;
        private string[] nonDebuggable;
        private int numOfChatServers = 1;
        private int numOfGameServers = 1;
        private int numOfGateways = 1;
        private int numOfSiteServers = 1;
        private List<ProcessInformation> sites;
        private Util util;

        public AdminServer()
        {
            var fs = Global.Require<FS>("fs");
            Logger.Start("Admin");

            Logger.Log("Shuffly Admin V0.49",LogLevel.DebugInformation);
            Logger.Log("Shuffly Admin V0.49", LogLevel.Information);

            var redis = Global.Require<Redis>("redis");
            var client = redis.CreateClient(6379, IPs.RedisIP);
           /* client.On<string,object>("monitor",(time, args) => {
                                   Logger.Log("Monitor: "+time+" "+Json.Stringify(args),LogLevel.DebugInformation); 
                                });*/
             
            util = Global.Require<Util>("util");
            exec = Global.Require<ChildProcess>("child_process").Exec;
            __dirname = ExtensionMethods.HARDLOCATION;
            nonDebuggable = new[] {"node-inspector", "pkill"};

            Global.Require<Http>("http").CreateServer(handler).Listen(8090);

            debug = true;
            Global.SetInterval(() => { Console.Log("keep alive " + new DateTime().ToString().Substring(17, 24)); }, 10 * 1000);

            Global.Process.On("exit",
                              () => {
                                  Logger.Log("Exiting ", LogLevel.DebugInformation);
                                  onAsk("k");
                                  runProcess("pkill", new[] {"node"});
                              });

            if (debug)
                onAsk("d", true);
           onAsk("d", true);
            if (debug) {
                nodeInspector = runProcess("node-inspector", new string[0]);
                Logger.Log("node-inspector Started", LogLevel.DebugInformation);
            }

            onAsk("s");
        }


        private void handler(HttpRequest request, HttpResponse response)
        {
            fs.ReadFile(__dirname + "/blank.html",
                        "ascii",
                        (err, content) => {
                            var fieldSets = "";
                            fieldSets += string.Format("<span>Main Site: {0}</span>", "<a href='#" + (int.Parse((Math.Random() * 20000).ToString())) + "' onclick='goHere(\"http://198.211.107.101\",\"MainSite\");'>Launch</a>");

                            fieldSets += buildFieldset(sites, "Site Servers");
                            fieldSets += buildFieldset(gateways, "Gateway Servers");
                            fieldSets += buildFieldset(games, "Game Servers");
                            fieldSets += buildFieldset(debugs, "Debug Servers");
                            fieldSets += buildFieldset(chats, "Chat Servers");

                            var dict = new JsDictionary();
                            dict["Content-Type"] = "text/html";
                            response.WriteHead(200, dict);
                            response.End(content.Replace("{0}", fieldSets));
                        });
        }

        private string buildFieldset(List<ProcessInformation> items, string name)
        {
            string str = "<fieldset>";
            str += "<ul style='list-style-type:none;'>";

            str += string.Format("<li >{0}</li>", name);
            str += string.Format("<li ></li>");

            foreach (var process in items) {
                str += "<li>";
                str += string.Format("<span>{0} ({1}): {2}</span>",
                                     process.Name,
                                     process.Index + 1,
                                     debug
                                             ? string.Format(
                                                     "<a href='#" + (int.Parse((Math.Random() * 20000).ToString())) + "' onclick='goHere(\"http://198.211.107.101:8080/debug?port={0}\",\"" + name + "(" + (process.Index + 1) + ")" +
                                                     "\");'>Debug</a>",
                                                     process.DebugPort + "&foo=" + int.Parse(( Math.Random() * 5000000 ).ToString()))
                                             : "Debug");
                str += "</li>";

                //document.frames["test"].location.reload();
            }
            str += "</ul>";

            str += "</fieldset>";
            return str;
        }

        private void loop()
        {
            ask("?: ", "", a => onAsk(a));
        }

        private void onAsk(string data, bool ignore = false)
        {
            var rest = data.Substring(0, 2);
            switch (data.CharAt(0)) {
                case "d":

                    debug = !debug;
                    Logger.Log("Debug " + (debug ? "Enabled" : "Disabled"), LogLevel.DebugInformation);
                    break;
                case "s":

                    sites = new List<ProcessInformation>();
                    games = new List<ProcessInformation>();
                    chats = new List<ProcessInformation>();
                    debugs = new List<ProcessInformation>();
                    gateways = new List<ProcessInformation>();
                    head = new ProcessInformation(runProcess("node", new[] { __dirname + "ServerManager.js", "head" }, 4000), "Head Server", 0, 4000);
                    Logger.Log("Head Server Started", LogLevel.DebugInformation);
                    for (var j = 0; j < numOfSiteServers; j++) {
                        sites.Add(new ProcessInformation(runProcess("node", new string[] { __dirname + "ServerManager.js", "site" }, 4100 + j), "Site Server", j, 4100 + j));
                    }

                    Logger.Log(sites.Count + " Site Servers Started", LogLevel.DebugInformation);
                    for (var j = 0; j < numOfGateways; j++) {
                        gateways.Add(new ProcessInformation(runProcess("node", new[] { __dirname + "ServerManager.js", "gateway" }, 4400 + j), "Gateway Server", j, 4400 + j));
                    }
                    Logger.Log(gateways.Count + " Gateway Servers Started", LogLevel.DebugInformation);

                    for (var j = 0; j < numOfGameServers; j++) {
                        games.Add(new ProcessInformation(runProcess("node", new[] { __dirname + "ServerManager.js", "game" }, 4200 + j), "Game Server", j, 4200 + j));
                    }
                    Logger.Log(games.Count + " Game Servers Started", LogLevel.DebugInformation);

                    for (var j = 0; j < numOfChatServers; j++) {
                        chats.Add(new ProcessInformation(runProcess("node", new[] { __dirname + "ServerManager.js", "chat" }, 4500 + j), "Chat Server", j, 4500 + j));
                    }
                    Logger.Log(chats.Count + " Chat Servers Started", LogLevel.DebugInformation);

                    debugs.Add(new ProcessInformation(runProcess("node", new[] { __dirname + "ServerManager.js", "debug" }, 4300), "Debug Server", 0, 4300));
                    Logger.Log(debugs.Count + " Debug Servers Started", LogLevel.DebugInformation);

                    break;
                case "q":
                    Global.Process.Exit();
                    break;
            }

            if (!ignore)
                loop();
        }

        private void ask(string question, string format, Action<string> callback)
        {
            var stdin = Global.Process.STDIn;
            var stdout = Global.Process.STDOut;

            stdin.Resume();
            stdout.Write(question);

            stdin.Once("data",
                       (data) => {
                           data = data.ToString().Trim();
                           callback(data);
                       });
        }

        private Process runProcess(string process, string[] args, int debugPort = 0, string appArgs = null)
        {
            string[] al;
            var name = "";
            if (args.Length > 0)
                name = ( al = args[0].Split("/") )[al.Length - 1].Split(".")[0];
            if (nonDebuggable.IndexOf(process) == -1 && debug) {
                var jf = " --debug=";
                if (name.IndexOf("Gatewa-") > -1)
                    jf = " --debug-brk=";
                args[0] = jf + debugPort + " " + args[0];
            }
            var dummy = exec(process + " " + args.Join(" ") + " " + ( appArgs ?? "" ));
            if (nonDebuggable.IndexOf(process) == -1) {
                dummy.STDOut.On<string>("data",
                                        (data) => {
                                            if (data.IndexOf("debug: ") == -1) {
                                                util.Print(string.Format("--{0}: {1}   {2}   {3}", name, debugPort, new DateTime().ToString().Substring(17, 24), data));
                                                util.Print("?: ");
                                            }
                                        });
                dummy.STDError.On<string>("data",
                                          (data) => {
                                              util.Print(string.Format("--{0}: {1}   {2}   {3}", name, debugPort, new DateTime().ToString().Substring(17, 24), data));
                                              util.Print("?: ");
                                          });
            }
            return dummy;
        }
    }
}