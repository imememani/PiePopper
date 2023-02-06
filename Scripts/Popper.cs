using Mono.Cecil;
using System;
using System.Linq;

namespace PiePopper.Scripts
{
    /// <summary>
    /// Base cracker module.
    /// </summary>
    public class Popper
    {
        // Cecil.
        protected AssemblyDefinition module;

        // Cache.
        protected string path;

        /// <summary>
        /// Bake references for cracking.
        /// </summary>
        public virtual void Bake(string path)
        {
            this.path = path;
            module = AssemblyDefinition.ReadAssembly(path, new ReaderParameters { ReadWrite = true, ReadingMode = ReadingMode.Immediate, InMemory = true });
        }

        /// <summary>
        /// Crack the target.
        /// </summary>
        public virtual bool Crack()
        {
            return true;
        }

        /// <summary>
        /// Finish the crack.
        /// </summary>
        protected void Eat() 
        {
            // Write the new changes.
            module.Write(path);
        }

        /// <summary>
        /// Notify the user of an event.
        /// </summary>
        protected void Notify(object message, ConsoleColor colour = ConsoleColor.White)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine($"[{GetType().Name}] {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Clear all instructions in the target type.
        /// </summary>
        protected bool ClearMethod(string type, string methodName)
        {
            TypeDefinition tDef = GetTypeDef(type);

            if (tDef == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {type} could not be found!");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }

            MethodDefinition mDef = GetMethodDef(tDef, methodName);

            if (mDef == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {mDef} in {type} could not be found!");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }

            GetMethodDef(GetTypeDef(type), methodName).Body.Instructions.Clear();
            return true;
        }

        /// <summary>
        /// Get a type definition.
        /// </summary>
        protected TypeDefinition GetTypeDef(string name)
        {
            foreach (var module in module.Modules)
            {
                foreach (var def in module.Types)
                {
                    if (def.Name == name)
                    { return def; }
                }
            }

            return null;
        }

        /// <summary>
        /// Get a method definition.
        /// </summary>
        protected MethodDefinition GetMethodDef(TypeDefinition type, string name)
        {
            foreach (var method in type.Methods)
            {
                if (method.Name == name)
                { return method; }
            }

            return null;
        }
    }
}