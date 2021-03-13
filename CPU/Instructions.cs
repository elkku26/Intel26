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

        internal static void Mov(Cpu cpu)
        {
            DebugPrint("MOV", cpu);

            throw new NotImplementedException("Unimplemented MOV");

        }

        internal static void Add(Cpu cpu)
        {
            DebugPrint("ADD", cpu);

            throw new NotImplementedException("Unimplemented ADD");

        }

        internal static void Adc(Cpu cpu)
        {
            DebugPrint("ADC", cpu);

            throw new NotImplementedException("Unimplemented ADC");

        }


        internal static void Sub(Cpu cpu)
        {
            DebugPrint("SUB", cpu);

            throw new NotImplementedException("Unimplemented SUB");
        }

        internal static void Sbb(Cpu cpu)
        {
            DebugPrint("SBB", cpu);

            throw new NotImplementedException("Unimplemented SBB");
        }

        internal static void Ana(Cpu cpu)
        {
            DebugPrint("ANA", cpu);

            throw new NotImplementedException("Unimplemented ANA");
        }

        internal static void Xra(Cpu cpu)
        {
            DebugPrint("XRA", cpu);

            throw new NotImplementedException("Unimplemented XRA");
        }

        internal static void Ora(Cpu cpu)
        {
            DebugPrint("ORA", cpu);

            throw new NotImplementedException("Unimplemented ORA");
        }


        internal static void Cmp(Cpu cpu)
        {
            DebugPrint("CMP", cpu);

            throw new NotImplementedException("Unimplemented CMP");
        }

        internal static void Lxi(Cpu cpu)
        {
            DebugPrint("LXI", cpu);

            throw new NotImplementedException("Unimplemented LXI");
        }

        internal static void Inx(Cpu cpu)
        {
            DebugPrint("INX", cpu);

            throw new NotImplementedException("Unimplemented INX");
        }

        internal static void Inr(Cpu cpu)
        {
            DebugPrint("INR", cpu);

            throw new NotImplementedException("Unimplemented INR");
        }

        internal static void Dcr(Cpu cpu)
        {
            DebugPrint("DCR", cpu);

            throw new NotImplementedException("Unimplemented DCR");
        }

        internal static void Mvi(Cpu cpu)
        {
            DebugPrint("MVI", cpu);

            throw new NotImplementedException("Unimplemented MVI");
        }

        internal static void Rlc(Cpu cpu)
        {
            DebugPrint("RLC", cpu);

            throw new NotImplementedException("Unimplemented RLC");
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

        internal static void Dad(Cpu cpu)
        {
            DebugPrint("DAD", cpu);

            throw new NotImplementedException("Unimplemented DAD");
        }

        internal static void Dcx(Cpu cpu)
        {
            DebugPrint("DCX", cpu);

            throw new NotImplementedException("Unimplemented DCX");
        }

        internal static void Daa(Cpu cpu)
        {
            DebugPrint("DAA", cpu);

            throw new NotImplementedException("Unimplemented DAA");
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
            DebugPrint("ACI", cpu);

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

        internal static void Pop(Cpu cpu)
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

        internal static void Shld(Cpu cpu)
        {
            DebugPrint("SHLD", cpu);

            throw new NotImplementedException("Unimplemented SHLD");
        }

        internal static void Lhld(Cpu cpu)
        {
            DebugPrint("LHLD", cpu);

            throw new NotImplementedException("Unimplemented LHLD");
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
            DebugPrint("LDA", cpu);

            throw new NotImplementedException("Unimplemented LDA");
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
            DebugPrint("CCC", cpu);

            throw new NotImplementedException("Unimplemented CCC");
        }

        internal static void Cpo(Cpu cpu)
        {
            DebugPrint("CCC", cpu);

            throw new NotImplementedException("Unimplemented CCC");
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
            DebugPrint("RZ", cpu);

            throw new NotImplementedException("Unimplemented RZ");
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

        internal static void Stax(Cpu cpu)
        {
            DebugPrint("STAX", cpu);

            throw new NotImplementedException("Unimplemented STAX");
        }

        internal static void Ldax(Cpu cpu)
        {
            DebugPrint("LDAX", cpu);

            throw new NotImplementedException("Unimplemented LDAX");
        }
    }
}