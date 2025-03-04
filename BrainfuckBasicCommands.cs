using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{            
            vm.RegisterCommand('+', b => { b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] + 1) % 256); });
            
            vm.RegisterCommand('-', b => { b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] - 1 + 256) % 256); });
            
            vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
            
            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)read(); });
            
            vm.RegisterCommand('>', b => { b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length; });
            
            vm.RegisterCommand('<', b => { b.MemoryPointer = (b.MemoryPointer - 1 + b.Memory.Length) % b.Memory.Length; });

            var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            foreach (var c in alphabet)
            {
                vm.RegisterCommand(c, b => { b.Memory[b.MemoryPointer] = (byte)c; });
            }
        }
	}
}