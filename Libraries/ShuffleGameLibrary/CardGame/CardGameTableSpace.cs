using System.Runtime.CompilerServices;
namespace global
{
    [ScriptName("TableSpace")]
    public class CardGameTableSpace
    {
        [IntrinsicProperty]
        public bool Vertical { get; set; }
        [IntrinsicProperty]
        public double X { get; set; }
        [IntrinsicProperty]
        public double Y { get; set; }
        [IntrinsicProperty]
        public double Width { get; set; }
        [IntrinsicProperty]
        public double Height { get; set; }
        [IntrinsicProperty]
        public CardGamePile Pile { get; set; }
        /*      [IntrinsicProperty]        public double Rotate { get; set; }*/
        [IntrinsicProperty]
        public CardGameAppearance Appearance { get; set; }
        [IntrinsicProperty]
        public bool Visible { get; set; }
        [IntrinsicProperty]
        public bool StackCards { get; set; }
        [IntrinsicProperty]
        public bool DrawCardsBent { get; set; }
        [IntrinsicProperty]
        public string Name { get; set; }
        [IntrinsicProperty]
        public CardGameOrder SortOrder { get; set; }
        [IntrinsicProperty]
        public int NumerOfCardsHorizontal { get; set; }
        [IntrinsicProperty]
        public int NumerOfCardsVertical { get; set; }
        [IntrinsicProperty]
        public TableSpaceResizeType ResizeType { get; set; }

        public CardGameTableSpace(CardGameTableSpaceOptions options)
        {
            Vertical = !options.Vertical ? false : options.Vertical;
            X = options.X == 0 ? 0 : options.X;
            Y = options.Y == 0 ? 0 : options.Y;
            Width = options.Width == 0 ? 0 : options.Width;
            Height = options.Height == 0 ? 0 : options.Height;
            Pile = options.Pile;
            //Rotate = options.Rotate == 0 ? 0 : options.Rotate;
            Visible = !options.Visible ? true : options.Visible;
            StackCards = !options.StackCards ? false : options.StackCards;
            DrawCardsBent = !options.DrawCardsBent ? true : options.DrawCardsBent;
            Name = options.Name ?? "TableSpace";
            SortOrder = options.SortOrder;
            NumerOfCardsHorizontal = options.NumerOfCardsHorizontal == 0 ? 1 : options.NumerOfCardsHorizontal;
            NumerOfCardsVertical = options.NumerOfCardsVertical == 0 ? 1 : options.NumerOfCardsVertical;
            ResizeType = options.ResizeType;
            //Rotate = ExtensionMethods.eval("options.rotate? options.rotate : 0");
            Appearance = new CardGameAppearance();
        }
    }
    //[NamedValues]todo:::
}