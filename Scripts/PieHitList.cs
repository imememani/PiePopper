using System;

namespace PiePopper.Scripts
{
    /// <summary>
    /// Information about a popper.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PieHitList : Attribute
    {
        public string authorName = ".MemeMan#4489";
        public string crackName = "TrialsoftheShinobi.dll";
        public string version = "1.0.0.0";
        public string dateCracked = "01/01/1997";

        public PieHitList(string authorName = ".MemeMan#4489", string crackName = "*.dll", string version = "1.0.0.0", string dateCracked = "01/01/1997")
        {
            this.authorName = authorName;
            this.crackName = crackName;
            this.version = version;
            this.dateCracked = dateCracked;
        }
    }
}