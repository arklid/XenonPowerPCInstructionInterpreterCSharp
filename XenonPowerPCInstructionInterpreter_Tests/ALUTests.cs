using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenonPowerPCInstructionInterpreter.CPU;

namespace XenonPowerPCInstructionInterpreterCSharp_Tests
{
    [TestClass]
    public class ALUTests
    {
        public XenonPowerPC xenonCPU = new XenonPowerPCInstructionInterpreter.Xenon().gCPU();

        [TestMethod]
        public void TestAdd()
        {
            // arrange
            xenonCPU.rD = 0;
            xenonCPU.rA = 0x00040000;
            xenonCPU.rB = 0x00004000;
            uint expected = 0x00044000;
            
            // act
            xenonCPU.Add();

            // assert
            uint actual = xenonCPU.rD;
            Assert.AreEqual(expected, actual, "add does not add correctly.");
        }

        [TestMethod]
        public void TestAdd_()
        {
            // arrange
            xenonCPU.rD = 0;
            xenonCPU.rA = 0x80007000;
            xenonCPU.rB = 0x70008000;
            uint expected = 0xF000F000;

            // act
            xenonCPU.Add_();

            // assert
            uint actual = xenonCPU.rD;
            Assert.AreEqual(expected, actual, "add_ does not add correctly.");
        }
    }
}
