using System;
using static CPU.DebugPrinter;

namespace CPU
{


    /// <summary>
    ///     Holds all of the possible instructions
    /// </summary>
    public static class Instructions
    {

        internal static void Nop(Cpu cpu)
        {
            DebugPrint("NOP", cpu);
        }

        internal static void Lxi(Cpu cpu, int registerPair)
        {
            DebugPrint("LXI", cpu);

            throw new NotImplementedException("Unimplemented LXI");
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

            throw new NotImplementedException("Unimplemented INR");
        }

        internal static void Dcr(Cpu cpu, int register)
        {
            DebugPrint("DCR", cpu);

            throw new NotImplementedException("Unimplemented DCR");
        }

        internal static void Mvi(Cpu cpu, int register)
        {
            DebugPrint("MVI", cpu);

            throw new NotImplementedException("Unimplemented MVI");
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

            throw new NotImplementedException("Unimplemented CMA");
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

            throw new NotImplementedException("Unimplemented MOV");

        }

        internal static void Add(Cpu cpu, int register)
        {
            DebugPrint("ADD", cpu);

            int addToAccumulator;
            int oldAccumulatorTotal = cpu.Registers[Register.A];

            if (register != Register.MRef)
                //Set the working int as the register in question
                addToAccumulator = cpu.Registers[register];
            else
                //Set the working int as the memory reference
                addToAccumulator = cpu.Memory[cpu.Registers[Register.MRef]];

            var newAccumulatorTotal = addToAccumulator + oldAccumulatorTotal;


            //Check if carry bit should be set
            if (newAccumulatorTotal > 255)
                //Switch carry bit to 1
                cpu.Flags |= FlagSelector.Carry;
            else
                //Switch carry bit to 0
                cpu.Flags &= ~FlagSelector.Carry & 0xFF;


            //Check if sign bit should be set
            if ((newAccumulatorTotal & FlagSelector.Sign) == FlagSelector.Sign)
                //Set the sign bit to 1
                cpu.Flags |= FlagSelector.Sign;
            else
                //Set the sign bit to 0
                cpu.Flags &= ~FlagSelector.Sign & 0xFF;


            //Check if zero bit should be set
            if ((newAccumulatorTotal & 0xFF) == 0)
                //Set the zero bit to 1
                cpu.Flags |= FlagSelector.Zero;
            else
                //Set the zero bit to 0
                cpu.Flags &= ~FlagSelector.Zero & 0xFF;


            //Check if parity bit should be set
            if (BitHelper.ParityCounter(newAccumulatorTotal) == 1)
                //Set the parity bit to 1
                cpu.Flags |= FlagSelector.Parity;
            else
                //Set the parity bit to 0
                cpu.Flags &= ~FlagSelector.Parity & 0xFF;


            //Check if Aux Carry bit should be set
            if ((addToAccumulator & (0xF + addToAccumulator) & 0xF) > 15)
                //Set the aux carry flag to 1
                BitHelper.SetFlag(1, FlagSelector.AuxCarry, cpu);
            else
                //Set the aux carry flag to 0
                BitHelper.SetFlag(0, FlagSelector.AuxCarry, cpu);


            //Set the accumulator to its new value and cast it to one byte to 'force' an overflow
            cpu.Registers[Register.A] = (byte) (newAccumulatorTotal & 0xFF);


            //TODO: Implement rest of the flags and test

        }

        internal static void Adc(Cpu cpu, int register)
        {
            DebugPrint("ADC", cpu);

            throw new NotImplementedException("Unimplemented ADC");

        }


        internal static void Sub(Cpu cpu, int register)
        {
            DebugPrint("SUB", cpu);

            throw new NotImplementedException("Unimplemented SUB");
        }

        internal static void Sbb(Cpu cpu, int register)
        {
            DebugPrint("SBB", cpu);

            throw new NotImplementedException("Unimplemented SBB");
        }

        internal static void Ana(Cpu cpu, int register)
        {
            DebugPrint("ANA", cpu);

            throw new NotImplementedException("Unimplemented ANA");
        }

        internal static void Xra(Cpu cpu, int register)
        {
            DebugPrint("XRA", cpu);

            throw new NotImplementedException("Unimplemented XRA");
        }

        internal static void Ora(Cpu cpu, int register)
        {
            DebugPrint("ORA", cpu);

            throw new NotImplementedException("Unimplemented ORA");
        }


        internal static void Cmp(Cpu cpu, int register)
        {
            DebugPrint("CMP", cpu);

            throw new NotImplementedException("Unimplemented CMP");
        }


        internal static void Stc(Cpu cpu)
        {
            DebugPrint("STC", cpu);

            throw new NotImplementedException("Unimplemented STC");
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

            throw new NotImplementedException("Unimplemented ACI");
        }

        internal static void Adi(Cpu cpu)
        {
            DebugPrint("ADI", cpu);

            throw new NotImplementedException("Unimplemented ACI");
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

            throw new NotImplementedException("Unimplemented JMP");
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

            throw new NotImplementedException("Unimplemented CMC");
        }

        internal static void Pop(Cpu cpu, int registerPair)
        {
            DebugPrint("POP", cpu);

            throw new NotImplementedException("Unimplemented POP");
        }

        internal static void Xchg(Cpu cpu)
        {
            DebugPrint("XCHG", cpu);

            throw new NotImplementedException("Unimplemented XCHG");
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