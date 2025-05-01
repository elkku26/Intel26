using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.Runtime.InteropServices.ComTypes;
using static CPU.CPUHelper;
using static CPU.DebugHelper;

// ReSharper di1le ConvertIfStatementToConditionalTernaryExpression

namespace CPU
{
    /// <summary>
    ///     Holds all the possible instructions
    /// </summary>
    public static class Instructions
    {
        internal static void Nop()
        {
            DebugPrint("NOP");
            Die(Error.OutOfMemory);
        }

        internal static void Lxi(int registerPair)
        {
            var firstByte = Cpu.Current.Memory[Cpu.Current.Pc + 1];
            var secondByte = Cpu.Current.Memory[Cpu.Current.Pc + 2];

            switch (registerPair)
            {
                case RegisterPair.B:
                    Cpu.Current.Registers[Register.B] = secondByte;
                    Cpu.Current.Registers[Register.C] = firstByte;
                    break;

                case RegisterPair.D:
                    Cpu.Current.Registers[Register.D] = secondByte;
                    Cpu.Current.Registers[Register.E] = firstByte;
                    break;

                case RegisterPair.H:
                    Cpu.Current.Registers[Register.H] = secondByte;
                    Cpu.Current.Registers[Register.L] = firstByte;
                    break;

                case RegisterPair.SP:
                    Cpu.Current.PushStack(secondByte, firstByte);
                    break;
            }

            Cpu.Current.Pc += 2;
        }

        internal static void Stax(int registerPair)
        {
            DebugPrint("STAX");

            throw new NotImplementedException("Unimplemented STAX");
        }

        internal static void Inx(int registerPair)
        {
            DebugPrint("INX");

            throw new NotImplementedException("Unimplemented INX");
        }

        internal static void Inr(int register)
        {
            DebugPrint("INR");

            int oldValue;

            if (register != Register.MRef)
                //Set the working int as the register in question
                oldValue = Cpu.Current.Registers[register];
            else
                //Set the working int as the memory reference
                oldValue = Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]];

            var newValue = oldValue + 1;


            SetSign(newValue);

            SetZero(newValue);

            SetParity(newValue);

            SetAux(newValue, oldValue);


            if (register != Register.MRef)
                Cpu.Current.Registers[register] = (byte)(newValue & 0xFF);
            else
                Cpu.Current.Memory[Cpu.Current.Registers[register]] = (byte)(newValue & 0xFF);
        }

        internal static void Dcr(int register)
        {
            DebugPrint("DCR");

            int oldValue;

            if (register != Register.MRef)
                //Set the working int as the register in question
                oldValue = Cpu.Current.Registers[register];
            else
                //Set the working int as the memory reference
                oldValue = Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]];


            var newValue = oldValue - 1;


            SetSign(newValue);

            SetZero(newValue);

            SetParity(newValue);

            SetAux(newValue, oldValue);


            if (register != Register.MRef)
                Cpu.Current.Registers[register] = (byte)(newValue & 0xFF);
            else
                Cpu.Current.Memory[Cpu.Current.Registers[register]] = (byte)(newValue & 0xFF);
        }

        internal static void Mvi(int register)
        {
            if (register == Register.MRef) throw new NotImplementedException("mref is not yet properly implemented!!");

            DebugPrint("MVI");
            var immediate = Cpu.Current.Memory[Cpu.Current.Pc + 1];
            Cpu.Current.Registers[register] = immediate;
            Cpu.Current.Pc++;
        }

        internal static void Rlc()
        {
            DebugPrint("RLC");

            throw new NotImplementedException("Unimplemented RLC");
        }

        internal static void Dad(int registerPair)
        {
            DebugPrint("DAD");

            throw new NotImplementedException("Unimplemented DAD");
        }

        internal static void Ldax(int registers)
        {
            DebugPrint("LDAX");

            throw new NotImplementedException("Unimplemented LDAX");
        }


        internal static void Dcx(int registerPair)
        {
            DebugPrint("DCX");

            throw new NotImplementedException("Unimplemented DCX");
        }


        internal static void Rrc()
        {
            DebugPrint("RRC");

            throw new NotImplementedException("Unimplemented RRC");
        }

        internal static void Ral()
        {
            DebugPrint("RAL");

            throw new NotImplementedException("Unimplemented RAL");
        }

        internal static void Rar()
        {
            DebugPrint("RAR");

            throw new NotImplementedException("Unimplemented RAR");
        }


        internal static void Shld()
        {
            DebugPrint("SHLD");

            throw new NotImplementedException("Unimplemented SHLD");
        }


        internal static void Daa()
        {
            DebugPrint("DAA");

            throw new NotImplementedException("Unimplemented DAA");
        }


        internal static void Lhld()
        {
            DebugPrint("LHLD");

            throw new NotImplementedException("Unimplemented LHLD");
        }

        internal static void Cma()
        {
            DebugPrint("CMA");
            Cpu.Current.Registers[Register.A] = (byte)~Cpu.Current.Registers[Register.A];
        }

        internal static void Rnz()
        {
            DebugPrint("RNZ");

            throw new NotImplementedException("Unimplemented RNZ");
        }

        internal static void Rnc()
        {
            DebugPrint("RNC");

            throw new NotImplementedException("Unimplemented RNC");
        }

        internal static void Rpo()
        {
            DebugPrint("RPO");

            throw new NotImplementedException("Unimplemented RPO");
        }

        internal static void Rp()
        {
            DebugPrint("RP");

            throw new NotImplementedException("Unimplemented RP");
        }

        internal static void Hlt()
        {
            DebugPrint("HLT");

            throw new NotImplementedException("Unimplemented HLT");
        }

        internal static void Mov(int src, int dst)
        {
            DebugPrint("MOV");

            Cpu.Current.Registers[dst] = Cpu.Current.Registers[src];
        }

        internal static void Add(int register)
        {
            DebugPrint("ADD");

            int addToAccumulator;
            int oldAccumulator = Cpu.Current.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                addToAccumulator = Cpu.Current.Registers[register];
            else
                //Set the working int as the memory reference
                addToAccumulator = Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]];

            //Make sure the new accumulator total isn't any more than 255 (1 byte)
            var newAccumulator = addToAccumulator + oldAccumulator;


            SetCarry(newAccumulator);

            SetSign(newAccumulator);

            SetZero(newAccumulator);

            SetParity(newAccumulator);

            SetAux(oldAccumulator, addToAccumulator);


            //Set the accumulator to its new value
            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Adc(int register)
        {
            DebugPrint("ADC");


            int addToAccumulator;
            int oldAccumulator = Cpu.Current.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                addToAccumulator = Cpu.Current.Registers[register] + (Cpu.Current.Flags & FlagSelector.Carry);
            else
                //Set the working int as the memory reference
                addToAccumulator = Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]] +
                                   (Cpu.Current.Flags & FlagSelector.Carry);


            var newAccumulator = addToAccumulator + oldAccumulator;


            SetCarry(newAccumulator);

            SetSign(newAccumulator);

            SetZero(newAccumulator);

            SetParity(newAccumulator);


            SetAux(oldAccumulator, addToAccumulator);

            //Set the accumulator to its new value
            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Sub(int register)
        {
            DebugPrint("SUB");

            int subtrahend;
            int minuend = Cpu.Current.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                subtrahend = Cpu.Current.Registers[register];
            else
                //Set the working int as the memory reference
                subtrahend = Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]];

            int twosComplementSubtrahend = GetTwosComplement(subtrahend);

            var newAccumulator = minuend - subtrahend;

            SetBorrow(newAccumulator);
            SetParity(newAccumulator);
            SetZero(newAccumulator);

            //Set/unset the aux carry flag
            //TODO I'm note 100% sure my understanding of aux carry is right here
            SetAux(minuend, twosComplementSubtrahend);

            SetSign(newAccumulator);

            //Set the accumulator to its new value
            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Sbb(int register)
        {
            DebugPrint("SBB");

            int subtrahend;
            int minuend = Cpu.Current.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                subtrahend = Cpu.Current.Registers[register] + (Cpu.Current.Flags & FlagSelector.Carry);
            else
                //Set the working int as the memory reference
                subtrahend = Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]] +
                             (Cpu.Current.Flags & FlagSelector.Carry);

            int twosComplementSubtrahend = GetTwosComplement(subtrahend);

            var newAccumulator = minuend - subtrahend;

            SetCarry(newAccumulator);
            SetZero(newAccumulator);
            SetParity(newAccumulator);
            SetAux(minuend, twosComplementSubtrahend);
            SetSign(newAccumulator);


            //Set the accumulator to its new value
            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Ana(int register)
        {
            DebugPrint("ANA");

            var newAccumulator = Cpu.Current.Registers[Register.A] & Cpu.Current.Registers[register];

            SetCarry(newAccumulator);
            SetZero(newAccumulator);
            SetSign(newAccumulator);
            SetParity(newAccumulator);

            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Xra(int register)
        {
            DebugPrint("XRA");

            var newAccumulator = Cpu.Current.Registers[Register.A] ^ Cpu.Current.Registers[register];

            SetCarry(newAccumulator);
            SetZero(newAccumulator);
            SetSign(newAccumulator);
            SetParity(newAccumulator);
            SetAux(newAccumulator, Cpu.Current.Registers[register]);

            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Ora(int register)
        {
            DebugPrint("ORA");

            var newAccumulator = Cpu.Current.Registers[Register.A] | Cpu.Current.Registers[register];

            SetCarry(newAccumulator);
            SetZero(newAccumulator);
            SetSign(newAccumulator);
            SetParity(newAccumulator);

            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }


        internal static void Cmp(int register)
        {
            DebugPrint("CMP");

            int subtrahend;
            int minuend = Cpu.Current.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                subtrahend = Cpu.Current.Registers[register];
            else
                //Set the working int as the memory reference
                subtrahend = Cpu.Current.Memory[Cpu.Current.Registers[Register.MRef]];

            int twosComplementSubtrahend = GetTwosComplement(subtrahend);

            var newAccumulator = minuend - subtrahend;

            SetBorrow(newAccumulator);
            SetZero(newAccumulator);
            SetParity(newAccumulator);

            //Set/unset the aux carry flag
            SetAux(minuend, twosComplementSubtrahend);

            SetSign(newAccumulator);
        }


        internal static void Stc()
        {
            DebugPrint("STC");

            Cpu.Current.SetFlags(1, FlagSelector.Carry);
        }

        internal static void Sbi()
        {
            DebugPrint("SBI");


            throw new NotImplementedException("Unimplemented SBI");
        }

        internal static void Ani()
        {
            DebugPrint("ANI");

            throw new NotImplementedException("Unimplemented ANI");
        }

        internal static void Sui()
        {
            DebugPrint("SUI");

            int subtrahend = Cpu.Current.Memory[Cpu.Current.Pc + 1];
            int minuend = Cpu.Current.Registers[Register.A];


            int twosComplementSubtrahend = GetTwosComplement(subtrahend);

            var newAccumulator = minuend - subtrahend;

            SetBorrow(newAccumulator);
            SetZero(newAccumulator);
            SetParity(newAccumulator);

            //Set/unset the aux carry flag
            //TODO I'm note 100% sure my understanding of aux carry is right here
            SetAux(minuend, twosComplementSubtrahend);

            SetSign(newAccumulator);

            //Set the accumulator to its new value
            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);

            Cpu.Current.Pc++;
        }

        internal static void Aci()
        {
            DebugPrint("ACI");

            var addToAccumulator = Cpu.Current.Memory[Cpu.Current.Pc + 1] + (Cpu.Current.Flags & FlagSelector.Carry);
            int oldAccumulator = Cpu.Current.Registers[Register.A];

            var newAccumulator = addToAccumulator + oldAccumulator;

            SetCarry(newAccumulator);

            SetSign(newAccumulator);

            SetZero(newAccumulator);

            SetParity(newAccumulator);

            SetAux(oldAccumulator, addToAccumulator);

            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);

            //increment pc to not interpret immediate data as opcode
            Cpu.Current.Pc++;
        }

        internal static void Adi()
        {
            DebugPrint("ADI");

            int addToAccumulator = Cpu.Current.Memory[Cpu.Current.Pc + 1];
            int oldAccumulator = Cpu.Current.Registers[Register.A];

            var newAccumulator = addToAccumulator + oldAccumulator;

            SetCarry(newAccumulator);

            SetSign(newAccumulator);

            SetZero(newAccumulator);

            SetParity(newAccumulator);

            SetAux(oldAccumulator, addToAccumulator);

            Cpu.Current.Registers[Register.A] = (byte)(newAccumulator & 0xFF);

            //increment pc to not interpret immediate data as opcode
            Cpu.Current.Pc++;
        }

        internal static void Xri()
        {
            DebugPrint("XRI");

            throw new NotImplementedException("Unimplemented XRI");
        }

        internal static void Ori()
        {
            DebugPrint("ORI");

            throw new NotImplementedException("Unimplemented ORI");
        }

        internal static void Cpi()
        {
            DebugPrint("CPI");

            throw new NotImplementedException("Unimplemented CPI");
        }

        internal static void Jnc()
        {
            DebugPrint("JNC");

            if ((Cpu.Current.Flags & FlagSelector.Carry) == 0) Cpu.Current.Jump();
            //   var address = BitConverter.ToUInt16(cpu.Memory, (int)cpu.Pc + 1);
            // cpu.Pc = (uint)address - 1;
        }

        internal static void Jnz()
        {
            DebugPrint("JNZ");
            if ((Cpu.Current.Flags & FlagSelector.Zero) == 0) Cpu.Current.Jump();
        }


        internal static void Jmp()
        {
            DebugPrint("JMP");
            Cpu.Current.Jump();
        }

        internal static void Jz()
        {
            DebugPrint("JZ");
            if ((Cpu.Current.Flags & FlagSelector.Zero) == 1) Cpu.Current.Jump();
        }

        internal static void Jc()
        {
            DebugPrint("JC");

            if ((Cpu.Current.Flags & FlagSelector.Carry) == 1) Cpu.Current.Jump();
        }

        internal static void Jpo()
        {
            DebugPrint("JPO");
            if ((Cpu.Current.Flags & FlagSelector.Parity) == 0) Cpu.Current.Jump();
        }

        internal static void Jpe()
        {
            DebugPrint("JPE");

            if ((Cpu.Current.Flags & FlagSelector.Carry) == 1) Cpu.Current.Jump();
        }

        internal static void Jp()
        {
            DebugPrint("JP");

            if ((Cpu.Current.Flags & FlagSelector.Parity) == 1) Cpu.Current.Jump();
        }

        internal static void Jm()
        {
            DebugPrint("JM");

            if ((Cpu.Current.Flags & FlagSelector.Sign) == 1) Cpu.Current.Jump();
        }

        internal static void Cmc()
        {
            DebugPrint("CMC");

            Cpu.Current.SetFlags(~(Cpu.Current.Flags & FlagSelector.Carry), FlagSelector.Carry);
        }

        internal static void Pop(int registerPair)
        {
            DebugPrint("POP");

            throw new NotImplementedException("Unimplemented POP");
        }

        internal static void Xchg()
        {
            DebugPrint("XCHG");

            (Cpu.Current.Registers[Register.H], Cpu.Current.Registers[Register.D]) =
                (Cpu.Current.Registers[Register.D], Cpu.Current.Registers[Register.H]);

            (Cpu.Current.Registers[Register.L], Cpu.Current.Registers[Register.E]) =
                (Cpu.Current.Registers[Register.E], Cpu.Current.Registers[Register.L]);
        }

        internal static void Xthl()
        {
            DebugPrint("XTHL");

            var temp_L = Cpu.Current.Registers[Register.L];
            Cpu.Current.Registers[Register.L] = Cpu.Current.Memory[Cpu.Current.Sp];
            Cpu.Current.Memory[Cpu.Current.Sp] = temp_L;
        }

        internal static void Sphl()
        {
            DebugPrint("SPHL");

            throw new NotImplementedException("Unimplemented SPHL");
        }


        internal static void Sta()
        {
            DebugPrint("STA");

            throw new NotImplementedException("Unimplemented STA");
        }

        internal static void Lda()
        {
            DebugPrint("LDA");

            throw new NotImplementedException("Unimplemented LDA");
        }

        internal static void Call()
        {
            DebugPrint("CALL");

            Cpu.Current.Call();
        }

        internal static void Cnz()
        {
            DebugPrint("CNZ");

            if ((Cpu.Current.Flags & FlagSelector.Zero) == 1) Cpu.Current.Call();
        }

        internal static void Cz()
        {
            DebugPrint("CZ");
            if ((Cpu.Current.Flags & FlagSelector.Zero) == 0) Cpu.Current.Call();
        }

        internal static void Cnc()
        {
            DebugPrint("CNC");

            if ((Cpu.Current.Flags & FlagSelector.Carry) == 0) Cpu.Current.Call();
        }

        internal static void Cc()
        {
            if ((Cpu.Current.Flags & FlagSelector.Carry) == 1) Cpu.Current.Call();
        }

        internal static void Cpo()
        {
            DebugPrint("CPO");

            if ((Cpu.Current.Flags & FlagSelector.Parity) == 0) Cpu.Current.Call();
        }

        internal static void Cpe()
        {
            DebugPrint("CPE");

            if ((Cpu.Current.Flags & FlagSelector.Parity) == 1) Cpu.Current.Call();
        }

        internal static void Cp()
        {
            DebugPrint("CP");

            if ((Cpu.Current.Flags & FlagSelector.Sign) == 0) Cpu.Current.Call();
        }

        internal static void Cm()
        {
            DebugPrint("CM");

            if ((Cpu.Current.Flags & FlagSelector.Sign) == 1) Cpu.Current.Call();
        }

        internal static void Ret()
        {
            DebugPrint("RET");
            if (Cpu.Current.Registers[Register.C] == 9)
            {
                //get characters from address stored in DE until $ (0x24)
                var address = Cpu.Current.GetWord(RegisterPair.D);
                var c =  ' ';
                var msg = "";
                while (c != '$')
                {
                    c = (char) Cpu.Current.Memory[address];
                    msg += c;
                    address++;
                }
                Debug.WriteLine("MSG: "+msg);
            }
            else if (Cpu.Current.Registers[Register.C] == 2)
            {
                Debug.WriteLine("E");
            }
            //we compensate for the automatic PC increment here, NOT when we push the pointer to the stack
            Cpu.Current.Pc = Cpu.Current.Pop() - 1;
        }

        internal static void Rz()
        {
            DebugPrint("RZ");
            if ((Cpu.Current.Flags & FlagSelector.Zero) == 1) Cpu.Current.Pc = Cpu.Current.Pop() - 1;
        }

        internal static void Rc()
        {
            DebugPrint("RC");
            if ((Cpu.Current.Flags & FlagSelector.Carry) == 1) Cpu.Current.Pc = Cpu.Current.Pop() - 1;
        }

        internal static void Rpe()
        {
            DebugPrint("RPE");

            if ((Cpu.Current.Flags & FlagSelector.Parity) == 1) Cpu.Current.Pc = Cpu.Current.Pop() - 1;
        }

        internal static void Rm()
        {
            DebugPrint("RM");
            if ((Cpu.Current.Flags & FlagSelector.Sign) == 1) Cpu.Current.Pc = Cpu.Current.Pop() - 1;
        }

        internal static void Ei()
        {
            DebugPrint("EI");

            throw new NotImplementedException("Unimplemented EI");
        }

        internal static void Di()
        {
            DebugPrint("DI");

            throw new NotImplementedException("Unimplemented DI");
        }

        internal static void In()
        {
            DebugPrint("IN");

            throw new NotImplementedException("Unimplemented IN");
        }

        internal static void Out()
        {
            DebugPrint("OUT");

            throw new NotImplementedException("Unimplemented OUT");
        }


        internal static void Push(int registerPair)
        {
            DebugPrint("PUSH");

            throw new NotImplementedException("Unimplemented PUSH");
        }

        internal static void Rst(int exp)
        {
            DebugPrint("RST");

            throw new NotImplementedException("Unimplemented RST");
        }

        internal static void Pchl()
        {
            DebugPrint("PCHL");

            throw new NotImplementedException("Unimplemented PCHL");
        }
    }
}