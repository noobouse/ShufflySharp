using System.Runtime.CompilerServices;

namespace global
{
    [ScriptName("Effect$Rotate")]
    public class CardGameAppearanceEffectRotate : CardGameAppearanceEffect
    {
        public CardGameAppearanceEffectRotate(CardGameEffectRotateOptions options) : base(EffectType.Rotate)
        {
            Degrees = options.Degrees == 0 ? 0 : options.Degrees;
            DrawTime = CardGameAppearanceEffectDrawTime.During;
        }



        [ScriptName("degrees")]
        [IntrinsicProperty]
        public double Degrees { get; set; }

    }
    [Record]
    public sealed class CardGameEffectRotateOptions
    {
        [ScriptName("degrees")]
        [IntrinsicProperty]
        public double Degrees { get; set; }
    }

}