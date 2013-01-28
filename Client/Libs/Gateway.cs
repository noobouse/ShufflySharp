﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Models;
using SocketIOWebLibrary;
namespace Client.Libs
{
    public delegate void GatewayMessage(object obj);
    public class Gateway
    {
        private Dictionary<string, GatewayMessage> channels;
        [IntrinsicProperty]
        protected SocketIOClient GatewaySocket { get; set; }

        public Gateway(string gatewayServer)
        {
            channels = new Dictionary<string, GatewayMessage>();
            GatewaySocket = SocketIOClient.Connect(gatewayServer);
            GatewaySocket.On<SocketClientMessageModel>("Client.Message", data => channels[data.Channel](data.Content));
        }

        [IgnoreGenericArguments]
        public void Emit(string channel, object content = null, string gameServer = null)
        {
            GatewaySocket.Emit("Gateway.Message", new GatewayMessageModel(channel, content, gameServer));
        }

        [IgnoreGenericArguments]
        public void On(string channel, GatewayMessage callback)
        {
            channels[channel] = callback;
        }

        //todo global login
        public void Login(string userName, string password)
        {
            GatewaySocket.Emit("Gateway.Login", new UserModel {UserName = userName, Password = password});
        }
    }
}