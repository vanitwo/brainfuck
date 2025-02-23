using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		public Dictionary<char, Action<IVirtualMachine>> Commands = new();

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			Memory = new byte[memorySize];			
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute) 
			=> Commands[symbol] = execute;        

		public void Run()
		{			
			for (; InstructionPointer < Instructions.Length; InstructionPointer++)            
                if (Commands.TryGetValue(Instructions[InstructionPointer], out var action))
                    action(this);            
        }        
    }
}