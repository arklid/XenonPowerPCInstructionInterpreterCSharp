using XenonPowerPCInstructionInterpreter.CPU;

namespace XenonPowerPCInstructionInterpreter
{
    public class Xenon
    {
        public XenonPowerPC xenonCPU { get; set; }

        /// <summary>
        /// Xenon Power PC Instruction Interpreter written in CSharp to easily emulate the instruction sets available on the Microsoft XBox 360.
        /// </summary>
        public Xenon()
        {
            xenonCPU = new XenonPowerPC();
        }

        /// <summary>
        /// Gets the current CPU.
        /// </summary>
        /// <returns>XenonPowerPC</returns>
        public XenonPowerPC gCPU()
        {
            return xenonCPU;
        }
    }
}
