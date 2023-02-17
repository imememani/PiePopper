using Mono.Cecil;
using Mono.Cecil.Cil;
using PiePopper.Scripts;
using System;

namespace PiePopper.Crackers
{
    /// <summary>
    /// Cracks PatreonCore in all his mods, this will also remove any traces of Application.Quit().
    /// </summary>
    [PieHitList(".MemeMan#4489", "*.dll", "1.0.0.0", "07/02/2023")]
    public class PatreonCore : Popper
    {
        public override bool Crack()
        {
            // Obtain OnLoadCoroutine.
            TypeDefinition config = GetTypeDef("Config");

            if (config == null)
            { return false; }

            MethodDefinition onLoadCoroutine = GetMethodDef(config, "OnLoadCoroutine");

            // Ensure the module is found.
            if (onLoadCoroutine == null)
            { return false; }

            Notify($"Located OnLoadCoroutine [{onLoadCoroutine}]!", ConsoleColor.Green);

            // Modify OnLoadCoroutine.
            Notify($"OnLoadCoroutine IL instructions removed!", ConsoleColor.DarkGray);
            config.Methods.Remove(onLoadCoroutine);

            // Loop through all modules in the target assembly.
            // This will scan each method for calls to Quit the application
            // and remove them, this prevents his mods crashing your game.
            for (int i = 0; i < module.Modules.Count; i++)
            {
                ModuleDefinition modules = module.Modules[i];

                for (int x = 0; x < modules.Types.Count; x++)
                {
                    TypeDefinition type = modules.Types[x];

                    for (int j = 0; j < type.Methods.Count; j++)
                    {
                        MethodDefinition method = type.Methods[j];

                        ILProcessor processor = method.Body.GetILProcessor();

                        for (int k = method.Body.Instructions.Count - 1; k >= 0; k--)
                        {
                            string operand = method.Body.Instructions[k].Operand?.ToString();
                            if (method.Body.Instructions[k].OpCode == OpCodes.Call && string.CompareOrdinal(operand, "System.Void UnityEngine.Application::Quit()") == 0)
                            {
                                processor.Remove(method.Body.Instructions[k]);
                                Console.WriteLine($"Removed Application.Quit() from {type.Name}->{method.Name}!");
                            }
                        }
                    }
                }
            }

            // Write to module.
            Eat();
            return true;
        }
    }
}