using Mono.Cecil;
using Mono.Cecil.Cil;
using PiePopper.Scripts;

namespace PiePopper.Crackers
{
    /// <summary>
    /// Cracks TrialsoftheShinobi.dll
    /// </summary>
    [PieHitList(".MemeMan#4489", "TrialsoftheShinobi.dll", "1.0.0.0", "06/02/2023")]
    public class TrialsoftheShinobi : Popper
    {
        /// <inheritdoc/>
        public override bool Crack()
        {
            // Obtain OnLoadCoroutine.
            MethodDefinition onLoadCoroutine = GetMethodDef(GetTypeDef("Config"), "OnLoadCoroutine");
            
            // Ensure the module is found.
            if (onLoadCoroutine == null)
            { return false; }

            Notify($"Located OnLoadCoroutine [{onLoadCoroutine}]!", System.ConsoleColor.Green);

            // Build cache.
            ILProcessor processor = onLoadCoroutine.Body.GetILProcessor();
            Instruction nop = processor.Create(OpCodes.Nop);

            // Modify OnLoadCoroutine.
            onLoadCoroutine.Body.Instructions.Clear();
            processor.Append(nop);
            Notify($"OnLoadCoroutine IL instructions removed!", System.ConsoleColor.DarkGray);
            // Break out the method.
            processor.Append(processor.Create(OpCodes.Ldarg_0));
            processor.Append(processor.Create(OpCodes.Call, onLoadCoroutine));
            processor.Append(processor.Create(OpCodes.Ret));
            Notify($"OnLoadCoroutine IL instructions fully replaced!", System.ConsoleColor.Green);

            // Clear all quit calls.
            if (!Clear_smethod_0("Amaterasu",
                                 "AtomicDismantling",
                                 "Kamui",
                                 "ExpansionJutsu",
                                 "FireballJutsu",
                                 "Rasengan",
                                 "Rasenshuriken",
                                 "RequirementItem",
                                 "RequirementLevel",
                                 "Rinnegan",
                                 "ShadowClone",
                                 "ShurikenSpell",
                                 "Chidori"
                                )
                ||
                !ClearMethod("SameKillCount", "smethod_2")
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
}