using AoC.Data;

namespace AoC.Quest13
{
    internal class CheckResult
    {
        public bool MismatchFound { get; set; } = false;
        public bool SimetryFound { get; set; } = false;
        public Vector2<int> MissMatchPos { get; set; } = new(0,0);



    }
}
