using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using PiePopper.Scripts;

namespace PiePopper.Crackers
{
    /*
     * ====================================
     * | OBSOLETE SINCE PatreonCracker.cs |
     * | PatreonCracker now cracks this   |
     * | and various other mods of his.   |
     * ====================================
     * *
     *  *
     *   *
    /// <summary>
    /// Cracks TrialsoftheShinobi.dll
    /// </summary>
    [PieHitList(".MemeMan#4489", "TrialsoftheShinobi.dll", "1.0.0.0", "06/02/2023", true)]
    public class TrialsoftheShinobi : Popper
    {
        /// <inheritdoc/>
        public override bool Crack()
        {
            // Clear all quit calls.
            if (!Clear_smethod_0("Amaterasu",
                                 "AtomicDismantling",
                                 "Kamui",
                                 "ExpansionJutsu",
                                 "FireballJutsu",
                                 "Rasenshuriken",
                                 "RequirementItem",
                                 "RequirementLevel",
                                 "Rinnegan",
                                 "ShadowClone",
                                 "ShurikenSpell",
                                 "Chidori"
                                )
                ||
                !ClearMethod("SameKillCount", "smethod_2") ||
                !ClearMethod("Rasengan", "smethod_11")
               )
            {
                Notify($"Something went wrong clearing the Application.Quit() calls!", System.ConsoleColor.Red);
                return false;
            }

            // Write to the file.
            Eat();

            Notify($"Mod has been cracked.", System.ConsoleColor.Green);
            return true;
        }

        private bool Clear_smethod_0(params string[] types)
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (!ClearMethod(types[i], "smethod_0"))
                { return false; }
            }

            return true;
        }
    }
    */
}