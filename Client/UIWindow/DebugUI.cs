using System;
using System.Runtime.CompilerServices;
using Models;
using Models.GameManagerModels;
using WebLibraries.Common.ShuffUI;
using jQueryApi;
namespace Client.UIWindow
{
    public class DebugUI
    {
        public string selectedGame = "Sevens";
        [IntrinsicProperty]
        public ShuffWindow UIWindow { get; set; }
        [IntrinsicProperty]
        public ShuffTextbox txtNumOfPlayers { get; set; } 
        [IntrinsicProperty]
        public ShuffTextbox varText { get; set; }
        [IntrinsicProperty]
        public ShuffLabel lblAnother { get; set; }
        [IntrinsicProperty]
        public ShuffLabel lblHowFast { get; set; }  
        [IntrinsicProperty]
        public int Joined { get; set; }
        [IntrinsicProperty]
        public bool Created { get; set; }

        public DebugUI(ShuffUIManager shuffUIManager, PageHandler pageHandler)
        {
            UIWindow = shuffUIManager.CreateWindow(new ShuffWindow() {
                                                                             Title = "Developer",
                                                                             X = 500, //jQuery.Select("body").GetInnerWidth() -
                                                                             Y = 100,
                                                                             Width = 420,
                                                                             Height = 450,
                                                                             AllowClose = true,
                                                                             AllowMinimize = true,
                                                                             Visible = false
                                                                     });
             
             

            ShuffButton but = null;
            UIWindow.AddElement(but = new ShuffButton(280,
                                                      84,
                                                      150,
                                                      25,
                                                      new Func<string>(() => "Game: " + selectedGame),
                                                      (e) => {
                                                          if (selectedGame == "Sevens") selectedGame = "BlackJack";
                                                          else selectedGame = "Sevens";

                                                          pageHandler.ClientDebugManager.RequestGameSource(new GameSourceRequestModel(selectedGame));

                                                          string m = but.Text;
                                                      }
                                              ));

            lblHowFast = UIWindow.AddElement(new ShuffLabel(280 - 200, 80, "Time Taken:"));
            lblAnother = UIWindow.AddElement(new ShuffLabel(280 - 200, 100, "Another: "));

            /* devArea.AddButton(new ShuffButton()
             {
                 X = 280,
                 Y = 94,
                 Width = "150",
                 Height = "25",
                 Text = "Continue",
                 Click = (evt) =>
                 {
                     pageHandler.gateway.Emit("Area.Debug.Continue", new { }, devArea.Data.gameServer); //NO EMIT"ING OUTSIDE OF PageHandler
                 }
             });*/
             

            varText = UIWindow.AddElement(new ShuffTextbox(150, 134, 100, 25, "Var Lookup"));
            /*  devArea.AddButton(new ShuffButton()
              {
                  X = 280,
                  Y = 134,
                  Width = "150",
                  Height = "25",
                  Text = "Lookup",
                  Click = (evt) =>
                  {
                      pageHandler.gateway.Emit("Area.Debug.VariableLookup.Request", new { variableName = devArea.Data.varText.GetValue() }, devArea.Data.gameServer); //NO EMIT"ING OUTSIDE OF PageHandler
                  }
              });*/

            /*   devArea.AddButton(new ShuffButton()
               {
                   X = 280,
                   Y = 164,
                   Width = "150",
                   Height = "25",
                   Text = "Push New Source",
                   Click = (evt) =>
                   {
                       pageHandler.gateway.Emit("Area.Debug.PushNewSource", new { source = codeArea.Data.codeEditor.editor.GetValue(), breakPoints = codeArea.Data.breakPoints },
                           devArea.Data.gameServer); //NO EMIT"ING OUTSIDE OF PageHandler
                   }
               });*/
             

            txtNumOfPlayers = UIWindow.AddElement(new ShuffTextbox(130, 43, 130, 20, "6", "Number of players=", "font-size:13px"));
        }
    }
}