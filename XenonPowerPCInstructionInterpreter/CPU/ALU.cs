namespace XenonPowerPCInstructionInterpreter.CPU
{
    public static class ALU
    {
        /// <summary>
        /// addx		Add
        /// 
        /// Add (x’7C00 0214’)
        /// </summary>
        private static void Addx(this XenonPowerPC cpu)
        {
            cpu.rD = cpu.rA + cpu.rB;

            if (cpu.updatecr & cpu.PPC_OPC_Rc)
            {
                cpu.UpdateCR0(cpu.rD);
            }
        }

        /// <summary>
        /// add rD,rA,rB (OE = 0 Rc = 0)
        /// </summary>
        public static void Add(this XenonPowerPC gCPU)
        {
            Addx(gCPU);
        }

        /// <summary>
        /// add. rD,rA,rB (OE = 0 Rc = 1)
        /// </summary>
        public static void Add_(this XenonPowerPC gCPU)
        {
            gCPU.updatecr = true;
            Addx(gCPU);
        }
    }
}
