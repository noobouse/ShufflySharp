using System.Runtime.CompilerServices;

namespace global
{
    [Record]
    public sealed class GameCardGameOptions
    {
        [IntrinsicProperty]
        [ScriptName("numberOfCards")]
        public int NumberOfCards { get; set; }

        [IntrinsicProperty]
        [ScriptName("numberOfJokers")]
        public int NumberOfJokers { get; set; }
    }
}