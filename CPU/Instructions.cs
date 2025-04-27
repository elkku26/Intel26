using System;
using System.Diagnostics;
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
        internal static void Nop(Cpu cpu)
        {
            DebugPrint("NOP", cpu);
        }

        internal static void Lxi(Cpu cpu, int registerPair)
        {

            ushort immediate = BitConverter.ToUInt16(cpu.Memory, (int) cpu.Pc+1);

            var mostSignificant = (byte)(immediate & 0x00FF);
            var leastSignificant = (byte)((immediate & 0xFF00) >> 8);

            switch (registerPair)
            {
                case RegisterPair.B:
                    cpu.Registers[Register.B] = mostSignificant;
                    cpu.Registers[Register.C] = leastSignificant;
                    break;

                case RegisterPair.D:
                    cpu.Registers[Register.D] = mostSignificant;
                    cpu.Registers[Register.E] = leastSignificant;
                    break;

                case RegisterPair.H:
                    cpu.Registers[Register.H] = mostSignificant;
                    cpu.Registers[Register.L] = leastSignificant;
                    break;

                case RegisterPair.SP:
                    //Swap the endianness of the two bytes
                    var immediateArray = BitConverter.GetBytes(immediate);
                    Array.Reverse(immediateArray);

                    cpu.Sp = BitConverter.ToUInt16(immediateArray, 0);
                    break;
            }

            //increment pc by two because the next two bytes are immediate data for this instruction
            cpu.Pc += 2;
        }

        internal static void Stax(Cpu cpu, int registerPair)
        {
            DebugPrint("STAX", cpu);

            throw new NotImplementedException("Unimplemented STAX");
        }

        internal static void Inx(Cpu cpu, int registerPair)
        {
            DebugPrint("INX", cpu);

            throw new NotImplementedException("Unimplemented INX");
        }

        internal static void Inr(Cpu cpu, int register)
        {
            DebugPrint("INR", cpu);

            int oldValue;

            if (register != Register.MRef)
                //Set the working int as the register in question
                oldValue = cpu.Registers[register];
            else
                //Set the working int as the memory reference
                oldValue = cpu.Memory[cpu.Registers[Register.MRef]];

            var newValue = oldValue + 1;


            SetSign(cpu, newValue);

            SetZero(cpu, newValue);

            SetParity(cpu, newValue);

            SetAux(cpu, newValue, oldValue);


            if (register != Register.MRef)
                cpu.Registers[register] = (byte)(newValue & 0xFF);
            else
                cpu.Memory[cpu.Registers[register]] = (byte)(newValue & 0xFF);
        }

        internal static void Dcr(Cpu cpu, int register)
        {
            DebugPrint("DCR", cpu);

            int oldValue;

            if (register != Register.MRef)
                //Set the working int as the register in question
                oldValue = cpu.Registers[register];
            else
                //Set the working int as the memory reference
                oldValue = cpu.Memory[cpu.Registers[Register.MRef]];


            var newValue = oldValue - 1;


            SetSign(cpu, newValue);

            SetZero(cpu, newValue);

            SetParity(cpu, newValue);

            SetAux(cpu, newValue, oldValue);


            if (register != Register.MRef)
                cpu.Registers[register] = (byte)(newValue & 0xFF);
            else
                cpu.Memory[cpu.Registers[register]] = (byte)(newValue & 0xFF);
        }

        internal static void Mvi(Cpu cpu, int register)
        {

            if (register == Register.MRef)
            {
                throw new NotImplementedException("mref is not yet properly implemented!!");
            }
            
            DebugPrint("MVI", cpu);
            byte immediate = cpu.Memory[cpu.Pc +1];
            cpu.Registers[register] = immediate;
            cpu.Pc++;
        }

        internal static void Rlc(Cpu cpu)
        {
            DebugPrint("RLC", cpu);

            throw new NotImplementedException("Unimplemented RLC");
        }

        internal static void Dad(Cpu cpu, int registerPair)
        {
            DebugPrint("DAD", cpu);

            throw new NotImplementedException("Unimplemented DAD");
        }

        internal static void Ldax(Cpu cpu, int registers)
        {
            DebugPrint("LDAX", cpu);

            throw new NotImplementedException("Unimplemented LDAX");
        }


        internal static void Dcx(Cpu cpu, int registerPair)
        {
            DebugPrint("DCX", cpu);

            throw new NotImplementedException("Unimplemented DCX");
        }


        internal static void Rrc(Cpu cpu)
        {
            DebugPrint("RRC", cpu);

            throw new NotImplementedException("Unimplemented RRC");
        }

        internal static void Ral(Cpu cpu)
        {
            DebugPrint("RAL", cpu);

            throw new NotImplementedException("Unimplemented RAL");
        }

        internal static void Rar(Cpu cpu)
        {
            DebugPrint("RAR", cpu);

            throw new NotImplementedException("Unimplemented RAR");
        }


        internal static void Shld(Cpu cpu)
        {
            DebugPrint("SHLD", cpu);

            throw new NotImplementedException("Unimplemented SHLD");
        }


        internal static void Daa(Cpu cpu)
        {
            DebugPrint("DAA", cpu);

            throw new NotImplementedException("Unimplemented DAA");
        }


        internal static void Lhld(Cpu cpu)
        {
            DebugPrint("LHLD", cpu);

            throw new NotImplementedException("Unimplemented LHLD");
        }

        internal static void Cma(Cpu cpu)
        {
            DebugPrint("CMA", cpu);
            cpu.Registers[Register.A] = (byte)~cpu.Registers[Register.A];
        }

        internal static void Rnz(Cpu cpu)
        {
            DebugPrint("RNZ", cpu);

            throw new NotImplementedException("Unimplemented RNZ");
        }

        internal static void Rnc(Cpu cpu)
        {
            DebugPrint("RNC", cpu);

            throw new NotImplementedException("Unimplemented RNC");
        }

        internal static void Rpo(Cpu cpu)
        {
            DebugPrint("RPO", cpu);

            throw new NotImplementedException("Unimplemented RPO");
        }

        internal static void Rp(Cpu cpu)
        {
            DebugPrint("RP", cpu);

            throw new NotImplementedException("Unimplemented RP");
        }

        internal static void Hlt(Cpu cpu)
        {
            DebugPrint("HLT", cpu);

            throw new NotImplementedException("Unimplemented HLT");
        }

        internal static void Mov(Cpu cpu, int src, int dst)
        {
            DebugPrint("MOV", cpu);

            cpu.Registers[dst] = cpu.Registers[src];
        }

        internal static void Add(Cpu cpu, int register)
        {
            DebugPrint("ADD", cpu);

            int addToAccumulator;
            int oldAccumulator = cpu.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                addToAccumulator = cpu.Registers[register];
            else
                //Set the working int as the memory reference
                addToAccumulator = cpu.Memory[cpu.Registers[Register.MRef]];

            //Make sure the new accumulator total isn't any more than 255 (1 byte)
            var newAccumulator = addToAccumulator + oldAccumulator;


            SetCarry(cpu, newAccumulator);

            SetSign(cpu, newAccumulator);

            SetZero(cpu, newAccumulator);

            SetParity(cpu, newAccumulator);

            SetAux(cpu, oldAccumulator, addToAccumulator);


            //Set the accumulator to its new value
            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Adc(Cpu cpu, int register)
        {
            DebugPrint("ADC", cpu);


            int addToAccumulator;
            int oldAccumulator = cpu.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                addToAccumulator = cpu.Registers[register]+ (cpu.Flags & FlagSelector.Carry);
            else
                //Set the working int as the memory reference
                addToAccumulator = cpu.Memory[cpu.Registers[Register.MRef]]+ (cpu.Flags & FlagSelector.Carry);


            var newAccumulator = addToAccumulator + oldAccumulator ;


            SetCarry(cpu, newAccumulator);

            SetSign(cpu, newAccumulator);

            SetZero(cpu, newAccumulator);

            SetParity(cpu, newAccumulator);

            
            SetAux(cpu, oldAccumulator, addToAccumulator);

            //Set the accumulator to its new value
            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Sub(Cpu cpu, int register)
        {
            DebugPrint("SUB", cpu);

            int subtrahend;
            int minuend = cpu.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                subtrahend = cpu.Registers[register];
            else
                //Set the working int as the memory reference
                subtrahend = cpu.Memory[cpu.Registers[Register.MRef]];

            int twosComplementSubtrahend = GetTwosComplement(subtrahend);

            var newAccumulator = minuend + twosComplementSubtrahend;

            SetBorrow(cpu, newAccumulator);
            SetZero(cpu, newAccumulator);
            SetParity(cpu, newAccumulator);

            //Set/unset the aux carry flag
            //TODO I'm note 100% sure my understanding of aux carry is right here
            SetAux(cpu, minuend, twosComplementSubtrahend);

            SetSign(cpu, newAccumulator);

            //Set the accumulator to its new value
            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Sbb(Cpu cpu, int register)
        {
            DebugPrint("SBB", cpu);

            int subtrahend;
            int minuend = cpu.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                subtrahend = cpu.Registers[register] + (cpu.Flags & FlagSelector.Carry);
            else
                //Set the working int as the memory reference
                subtrahend = cpu.Memory[cpu.Registers[Register.MRef]] + (cpu.Flags & FlagSelector.Carry);

            int twosComplementSubtrahend = GetTwosComplement(subtrahend);

            var newAccumulator = minuend + twosComplementSubtrahend;

            SetCarry(cpu, newAccumulator);
            SetZero(cpu, newAccumulator);
            SetParity(cpu, newAccumulator);
            SetAux(cpu, minuend, subtrahend);
            SetSign(cpu, newAccumulator);


            //Set the accumulator to its new value
            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Ana(Cpu cpu, int register)
        {
            DebugPrint("ANA", cpu);

            var newAccumulator = cpu.Registers[Register.A] & cpu.Registers[register];

            SetCarry(cpu, newAccumulator);
            SetZero(cpu, newAccumulator);
            SetSign(cpu, newAccumulator);
            SetParity(cpu, newAccumulator);

            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Xra(Cpu cpu, int register)
        {
            DebugPrint("XRA", cpu);

            var newAccumulator = cpu.Registers[Register.A] ^ cpu.Registers[register];

            SetCarry(cpu, newAccumulator);
            SetZero(cpu, newAccumulator);
            SetSign(cpu, newAccumulator);
            SetParity(cpu, newAccumulator);
            SetAux(cpu, newAccumulator, cpu.Registers[register]);

            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }

        internal static void Ora(Cpu cpu, int register)
        {
            DebugPrint("ORA", cpu);

            var newAccumulator = cpu.Registers[Register.A] | cpu.Registers[register];

            SetCarry(cpu, newAccumulator);
            SetZero(cpu, newAccumulator);
            SetSign(cpu, newAccumulator);
            SetParity(cpu, newAccumulator);

            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);
        }


        internal static void Cmp(Cpu cpu, int register)
        {
            DebugPrint("CMP", cpu);

            int subtrahend;
            int minuend = cpu.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                subtrahend = cpu.Registers[register];
            else
                //Set the working int as the memory reference
                subtrahend = cpu.Memory[cpu.Registers[Register.MRef]];

            int twosComplementSubtrahend = GetTwosComplement(subtrahend);

            var newAccumulator = minuend + twosComplementSubtrahend;

            SetBorrow(cpu, newAccumulator);
            SetZero(cpu, newAccumulator);
            SetParity(cpu, newAccumulator);

            //Set/unset the aux carry flag
            SetAux(cpu, minuend, twosComplementSubtrahend);

            SetSign(cpu, newAccumulator);
        }


        internal static void Stc(Cpu cpu)
        {
            DebugPrint("STC", cpu);

            cpu.SetFlags(1, FlagSelector.Carry, cpu);
        }

        internal static void Sbi(Cpu cpu)
        {
            DebugPrint("SBI", cpu);


            throw new NotImplementedException("Unimplemented SBI");
        }

        internal static void Ani(Cpu cpu)
        {
            DebugPrint("ANI", cpu);

            throw new NotImplementedException("Unimplemented ANI");
        }

        internal static void Sui(Cpu cpu)
        {
            DebugPrint("SUI", cpu);

            throw new NotImplementedException("Unimplemented SUI");
        }

        internal static void Aci(Cpu cpu)
        {
            DebugPrint("ACI", cpu);

            int addToAccumulator = cpu.Memory[cpu.Pc+1] + (cpu.Flags & FlagSelector.Carry);
            int oldAccumulator = cpu.Registers[Register.A];

            var newAccumulator = addToAccumulator + oldAccumulator;

            SetCarry(cpu, newAccumulator);

            SetSign(cpu, newAccumulator);

            SetZero(cpu, newAccumulator);

            SetParity(cpu, newAccumulator);

            SetAux(cpu, oldAccumulator, addToAccumulator);

            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);

            //increment pc to not interpret immediate data as opcode
            cpu.Pc++;
        }

        internal static void Adi(Cpu cpu)
        {
            DebugPrint("ADI", cpu);
            
            int addToAccumulator = cpu.Memory[cpu.Pc+1];
            int oldAccumulator = cpu.Registers[Register.A];

            var newAccumulator = addToAccumulator + oldAccumulator;

            SetCarry(cpu, newAccumulator);

            SetSign(cpu, newAccumulator);

            SetZero(cpu, newAccumulator);

            SetParity(cpu, newAccumulator);

            SetAux(cpu, oldAccumulator, addToAccumulator);

            cpu.Registers[Register.A] = (byte)(newAccumulator & 0xFF);

            //increment pc to not interpret immediate data as opcode
            cpu.Pc++;
        }

        internal static void Xri(Cpu cpu)
        {
            DebugPrint("XRI", cpu);

            throw new NotImplementedException("Unimplemented XRI");
        }

        internal static void Ori(Cpu cpu)
        {
            DebugPrint("ORI", cpu);

            throw new NotImplementedException("Unimplemented ORI");
        }

        internal static void Cpi(Cpu cpu)
        {
            DebugPrint("CPI", cpu);

            throw new NotImplementedException("Unimplemented CPI");
        }

        internal static void Jnc(Cpu cpu)
        {
            DebugPrint("JNC", cpu);

            throw new NotImplementedException("Unimplemented JNC");
        }

        internal static void Jnz(Cpu cpu)
        {
            DebugPrint("JNZ", cpu);

            throw new NotImplementedException("Unimplemented JNZ");
        }


        internal static void Jmp(Cpu cpu)
        {
            DebugPrint("JMP", cpu);


            //throw new NotImplementedException("Unimplemented JMP");
        }

        internal static void Jz(Cpu cpu)
        {
            DebugPrint("JZ", cpu);

            throw new NotImplementedException("Unimplemented JZ");
        }

        internal static void Jc(Cpu cpu)
        {
            DebugPrint("JC", cpu);

            throw new NotImplementedException("Unimplemented JC");
        }

        internal static void Jpo(Cpu cpu)
        {
            DebugPrint("JPO", cpu);

            throw new NotImplementedException("Unimplemented JPO");
        }

        internal static void Jpe(Cpu cpu)
        {
            DebugPrint("JPE", cpu);

            throw new NotImplementedException("Unimplemented JPE");
        }

        internal static void Jp(Cpu cpu)
        {
            DebugPrint("JP", cpu);

            throw new NotImplementedException("Unimplemented JP");
        }

        internal static void Jm(Cpu cpu)
        {
            DebugPrint("JM", cpu);

            throw new NotImplementedException("Unimplemented JM");
        }

        internal static void Cmc(Cpu cpu)
        {
            DebugPrint("CMC", cpu);

            cpu.SetFlags(~(cpu.Flags & FlagSelector.Carry), FlagSelector.Carry, cpu);
        }

        internal static void Pop(Cpu cpu, int registerPair)
        {
            DebugPrint("POP", cpu);

            throw new NotImplementedException("Unimplemented POP");
        }

        internal static void Xchg(Cpu cpu)
        {
            DebugPrint("XCHG", cpu);

            (cpu.Registers[Register.H], cpu.Registers[Register.D]) =
                (cpu.Registers[Register.D], cpu.Registers[Register.H]);

            (cpu.Registers[Register.L], cpu.Registers[Register.E]) =
                (cpu.Registers[Register.E], cpu.Registers[Register.L]);
        }

        internal static void Xthl(Cpu cpu)
        {
            DebugPrint("XTHL", cpu);

            throw new NotImplementedException("Unimplemented XTHL");
        }

        internal static void Sphl(Cpu cpu)
        {
            DebugPrint("SPHL", cpu);

            throw new NotImplementedException("Unimplemented SPHL");
        }


        internal static void Sta(Cpu cpu)
        {
            DebugPrint("STA", cpu);

            throw new NotImplementedException("Unimplemented STA");
        }

        internal static void Lda(Cpu cpu)
        {
            DebugPrint("LDA", cpu);

            throw new NotImplementedException("Unimplemented LDA");
        }

        internal static void Call(Cpu cpu)
        {
            DebugPrint("CALL", cpu);

            throw new NotImplementedException("Unimplemented CALL");
        }

        internal static void Cnz(Cpu cpu)
        {
            DebugPrint("CNZ", cpu);

            throw new NotImplementedException("Unimplemented CNZ");
        }

        internal static void Cz(Cpu cpu)
        {
            DebugPrint("CZ", cpu);

            throw new NotImplementedException("Unimplemented CZ");
        }

        internal static void Cnc(Cpu cpu)
        {
            DebugPrint("CNC", cpu);

            throw new NotImplementedException("Unimplemented CNC");
        }

        internal static void Cc(Cpu cpu)
        {
            DebugPrint("CC", cpu);

            throw new NotImplementedException("Unimplemented CC");
        }

        internal static void Cpo(Cpu cpu)
        {
            DebugPrint("CPO", cpu);

            throw new NotImplementedException("Unimplemented CPO");
        }

        internal static void Cpe(Cpu cpu)
        {
            DebugPrint("CPE", cpu);

            throw new NotImplementedException("Unimplemented CPE");
        }

        internal static void Cp(Cpu cpu)
        {
            DebugPrint("CP", cpu);

            throw new NotImplementedException("Unimplemented CP");
        }

        internal static void Cm(Cpu cpu)
        {
            DebugPrint("CM", cpu);

            throw new NotImplementedException("Unimplemented CM");
        }

        internal static void Ret(Cpu cpu)
        {
            DebugPrint("RET", cpu);

            throw new NotImplementedException("Unimplemented RET");
        }

        internal static void Rz(Cpu cpu)
        {
            DebugPrint("RZ", cpu);

            throw new NotImplementedException("Unimplemented RZ");
        }

        internal static void Rc(Cpu cpu)
        {
            DebugPrint("RC", cpu);

            throw new NotImplementedException("Unimplemented RC");
        }

        internal static void Rpe(Cpu cpu)
        {
            DebugPrint("RPE", cpu);

            throw new NotImplementedException("Unimplemented RPE");
        }

        internal static void Rm(Cpu cpu)
        {
            DebugPrint("RM", cpu);

            throw new NotImplementedException("Unimplemented RM");
        }

        internal static void Ei(Cpu cpu)
        {
            DebugPrint("EI", cpu);

            throw new NotImplementedException("Unimplemented EI");
        }

        internal static void Di(Cpu cpu)
        {
            DebugPrint("DI", cpu);

            throw new NotImplementedException("Unimplemented DI");
        }

        internal static void In(Cpu cpu)
        {
            DebugPrint("IN", cpu);

            throw new NotImplementedException("Unimplemented IN");
        }

        internal static void Out(Cpu cpu)
        {
            DebugPrint("OUT", cpu);

            throw new NotImplementedException("Unimplemented OUT");
        }


        internal static void Push(Cpu cpu, int registerPair)
        {
            DebugPrint("PUSH", cpu);

            throw new NotImplementedException("Unimplemented PUSH");
        }

        internal static void Rst(Cpu cpu, int exp)
        {
            DebugPrint("RST", cpu);

            throw new NotImplementedException("Unimplemented RST");
        }

        internal static void Pchl(Cpu cpu)
        {
            DebugPrint("PCHL", cpu);

            throw new NotImplementedException("Unimplemented PCHL");
        }
    }
}