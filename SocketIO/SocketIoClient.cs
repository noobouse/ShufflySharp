using System.Runtime.CompilerServices;

namespace SocketIO
{
    public class SocketIoClient
    {
        private SocketNamespace sockets;

        [ScriptName("set")]
        public void Set(string option, int value)
        {
        }
        [ScriptName("sockets")]
        public SocketNamespace Sockets
        {
            get { return sockets; }
            set { sockets=value; }
        }
    }
}