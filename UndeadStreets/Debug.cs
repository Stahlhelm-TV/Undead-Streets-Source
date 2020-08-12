namespace UndeadStreets
{
    using System;
    using System.IO;

    public class Debug
    {
        public static void Log(string message)
        {
            object[] objArray1 = new object[] { DateTime.Now, " : ", message, Environment.NewLine };
            File.AppendAllText("./scripts/UndeadStreets/Logs/UndeadStreets.log", string.Concat(objArray1));
        }
    }
}

