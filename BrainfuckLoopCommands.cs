using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
            var bracketPairs = GetBracketPairs(vm.Instructions);

            vm.RegisterCommand('[', b =>
            {                
                if (b.Memory[b.MemoryPointer] == 0)                                    
                    b.InstructionPointer = bracketPairs[b.InstructionPointer];                
            });

            vm.RegisterCommand(']', b =>
            {                
                if (b.Memory[b.MemoryPointer] != 0) 
                    b.InstructionPointer = bracketPairs[b.InstructionPointer];                
            });
        }

        public static Dictionary<int, int> GetBracketPairs(string instructions)
        {
            var dict = new Dictionary<int, int>();
            var stack = new Stack<int>();
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i] == '[')
                    stack.Push(i);
                else if (instructions[i] == ']')
                {
                    if (stack.Count == 0) throw new ArgumentException("Unbalanced brackets");
                    int start = stack.Pop();
                    dict[start] = i;
                    dict[i] = start;
                }
            }
            if (stack.Count > 0) throw new ArgumentException("Unbalanced brackets");
            return dict;
        }  
    }
}