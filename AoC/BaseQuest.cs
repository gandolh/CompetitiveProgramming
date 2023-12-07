using System.Text.RegularExpressions;

namespace AoC
{
    internal abstract class BaseQuest
    {
        protected static string GetPathTo(string quizFile)
        {
            string inOutFolder = "in_out";
            string startupPath = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
            return Path.Join(startupPath, inOutFolder, quizFile);
        }

        public abstract Task Solve();
    }

}
