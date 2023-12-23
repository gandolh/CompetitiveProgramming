namespace AoC.Quest15
{
    public class BoxLens
    {
        public string Label { get; set; } = "";
        public int Value { get; set; }
        public BoxLens(string label, int value)
        {
            Label = label;
            Value = value;
        }
    }
}
