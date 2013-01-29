using System;
using System.Runtime.CompilerServices;
namespace Models
{
    [Serializable]
    public class UserLoginResponse
    {
        public bool Successful { get; set; }
        public UserModel User { get; set; }
        [ObjectLiteral]
        public UserLoginResponse(bool successful, UserModel user)
        {
            Successful = successful;
            User = user;
        }
    }
    [Serializable]
    public class UserDisconnectModel
    {
        public UserModel User { get; set; }
        [ObjectLiteral]
        public UserDisconnectModel( UserModel user)
        {
            User = user;
        }
    }
    
}