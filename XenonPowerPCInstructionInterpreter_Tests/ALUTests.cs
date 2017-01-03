using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenonPowerPCInstructionInterpreter.CPU;

namespace XenonPowerPCInstructionInterpreterCSharp_Tests
{
    [TestClass]
    public class ALUTests
    {
        public XenonPowerPC xenonCPU = new XenonPowerPCInstructionInterpreter.Xenon().gCPU();

        [TestMethod]
        public void add()
        {
            // Assume GPR 6 contains 0x0004 0000.
            // Assume GPR 3 contains 0x0000 4000.
            // add 4,6,3
            // GPR 4 now contains 0x0004 4000.

            xenonCPU.rD = 0;
            xenonCPU.rA = 0x00040000;
            xenonCPU.rB = 0x00004000;
            uint expected = 0x00044000;

            xenonCPU.add();
            Assert.AreEqual(expected, xenonCPU.rD, "add does not add correctly.");
        }

        [TestMethod]
        public void add_()
        {
            // Assume GPR 6 contains 0x8000 7000.
            // Assume GPR 3 contains 0x7000 8000.
            // add. 4,6,3
            // GPR 4 now contains 0xF000 F000.

            xenonCPU.rD = 0;
            xenonCPU.rA = 0x80007000;
            xenonCPU.rB = 0x70008000;
            uint expected = 0xF000F000;

            xenonCPU.add_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addx()
        {
            // Assume GPR 6 contains 0x0004 0000.
            // Assume GPR 3 contains 0x0000 4000.
            // add 4,6,3
            // GPR 4 now contains 0x0004 4000.

            xenonCPU.rA = 0x00040000;
            xenonCPU.rB = 0x00004000;
            uint expected = 0x00044000;

            xenonCPU.addx();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addox()
        {
            // Assume GPR 6 contains 0xEFFF FFFF.
            // Assume GPR 3 contains 0x8000 0000.
            // addo 4,6,3
            // GPR 4 now contains 0x6FFF FFFF.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.rB = 0x80000000;
            uint expected = 0x6FFFFFFF;

            xenonCPU.addox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addo()
        {
            // Assume GPR 6 contains 0xEFFF FFFF.
            // Assume GPR 3 contains 0x8000 0000.
            // addo 4,6,3
            // GPR 4 now contains 0x6FFF FFFF.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.rB = 0x80000000;
            uint expected = 0x6FFFFFFF;

            xenonCPU.addo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addo_()
        {
            // Assume GPR 6 contains 0xEFFF FFFF.
            // Assume GPR 3 contains 0xEFFF FFFF.
            // addo. 4,6,3
            // GPR 4 now contains 0xDFFF FFFE.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.rB = 0xEFFFFFFF;
            uint expected = 0xDFFFFFFE;
            xenonCPU.addo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addcx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 10 contains 0x8000 7000.
            // addc 6,4,10
            // GPR 6 now contains 0x1000 A000.

            xenonCPU.rA = 0x90003000;
            xenonCPU.rB = 0x80007000;
            uint expected = 0x1000A000;

            xenonCPU.addcx();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addc()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 10 contains 0x8000 7000.
            // addc 6,4,10
            // GPR 6 now contains 0x1000 A000.

            xenonCPU.rA = 0x90003000;
            xenonCPU.rB = 0x80007000;
            uint expected = 0x1000A000;

            xenonCPU.addc();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addc_()
        {
            // Assume GPR 4 contains 0x7000 3000.
            // Assume GPR 10 contains 0xFFFF FFFF.
            // addc. 6,4,10
            // GPR 6 now contains 0x7000 2FFF.

            xenonCPU.rA = 0x70003000;
            xenonCPU.rB = 0xFFFFFFFF;
            uint expected = 0x70002FFF;

            xenonCPU.addc_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addcox()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 10 contains 0x7B41 92C0.
            // addco 6,4,10
            // GPR 6 now contains 0x0B41 C2C0.

            xenonCPU.rA = 0x90003000;
            xenonCPU.rB = 0x7B4192C0;
            uint expected = 0x0B41C2C0;

            xenonCPU.addcox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addco()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 10 contains 0x7B41 92C0.
            // addco 6,4,10
            // GPR 6 now contains 0x0B41 C2C0.

            xenonCPU.rA = 0x90003000;
            xenonCPU.rB = 0x7B4192C0;
            uint expected = 0x0B41C2C0;

            xenonCPU.addco();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addco_()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0x8000 7000.
            // addco. 6,4,10
            // GPR 6 now contains 0x0000 7000.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0x80007000;
            uint expected = 0x00007000;

            xenonCPU.addco_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addex()
        {
            // Assume GPR 4 contains 0x1000 0400.
            // Assume GPR 10 contains 0x1000 0400.
            // Assume the Carry bit is one.
            // adde 6,4,10
            // GPR 6 now contains 0x2000 0801. 

            xenonCPU.rA = 0x10000400;
            xenonCPU.rB = 0x10000400;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x20000801;

            xenonCPU.addex();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void adde()
        {
            // Assume GPR 4 contains 0x1000 0400.
            // Assume GPR 10 contains 0x1000 0400.
            // Assume the Carry bit is one.
            // adde 6,4,10
            // GPR 6 now contains 0x2000 0801. 

            xenonCPU.rA = 0x10000400;
            xenonCPU.rB = 0x10000400;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x20000801;

            xenonCPU.adde();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void adde_()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 10 contains 0x7B41 92C0.
            // Assume the Carry bit is zero.
            // adde. 6,4,10
            // GPR 6 now contains 0x0B41 C2C0.

            xenonCPU.rA = 0x90003000;
            xenonCPU.rB = 0x7B4192C0;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0x0B41C2C0;

            xenonCPU.adde_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addeox()
        {
            // Assume GPR 4 contains 0x1000 0400.
            // Assume GPR 10 contains 0xEFFF FFFF.
            // Assume the Carry bit is one.
            // addeo 6,4,10
            // GPR 6 now contains 0x0000 0400.

            xenonCPU.rA = 0x10000400;
            xenonCPU.rB = 0xEFFFFFFF;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x00000400;

            xenonCPU.addeox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addeo()
        {
            // Assume GPR 4 contains 0x1000 0400.
            // Assume GPR 10 contains 0xEFFF FFFF.
            // Assume the Carry bit is one.
            // addeo 6,4,10
            // GPR 6 now contains 0x0000 0400.

            xenonCPU.rA = 0x10000400;
            xenonCPU.rB = 0xEFFFFFFF;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x00000400;

            xenonCPU.addeo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addeo_()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 10 contains 0x8000 7000.
            // Assume the Carry bit is zero.
            // addeo. 6,4,10
            // GPR 6 now contains 0x1000 A000.

            xenonCPU.rA = 0x90003000;
            xenonCPU.rB = 0x80007000;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0x1000A000;

            xenonCPU.addeo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addi()
        {
            // Assume GPR 5 contains 0x0000 0900.
            // addi 4,0xFFFF8FF0(5)
            // GPR 4 now contains 0xFFFF 98F0.

            xenonCPU.rA = 0x00000900;
            uint expected = 0xFFFF98F0;
            xenonCPU.imm = 0xFFFF8FF0;

            xenonCPU.addi();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void li()
        {
            // Assume GPR 5 contains 0x0000 0000.
            // li 4,0xFFFF8FF0
            // GPR 4 now contains 0xFFFF 8FF0.

            uint expected = 0xFFFF8FF0;
            xenonCPU.rD = 0;
            xenonCPU.imm = 0xFFFF8FF0;

            xenonCPU.li();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void la()
        {
            xenonCPU.la();
            Assert.Fail();
        }

        [TestMethod]
        public void subi()
        {
            uint expected = 0xFFFF86F0;
            xenonCPU.rA = 0xFFFF8FF0;
            xenonCPU.imm = 0x900;

            xenonCPU.subi();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addic()
        {
            // Assume GPR 4 contains 0x0000 2346.
            // addic 6,4,0xFFFFFFFF
            // GPR 6 now contains 0x0000 2345.

            xenonCPU.rA = 0x00002346;
            xenonCPU.imm = 0xFFFFFFFF;
            //addic xenonCPU.rD,rA,0xFFFFFFFF
            uint expected = 0x00002345;

            xenonCPU.addic();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subic()
        {
            xenonCPU.rA = 0x00002346;
            xenonCPU.imm = 0xFFFFFFFF;
            uint expected = 0x00002347;

            xenonCPU.subic();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addic_()
        {
            // Assume GPR 4 contains 0xEFFF FFFF.
            // addic. 6,4,0x1000
            // GPR 6 now contains 0xF000 0FFF.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.imm = 0x1000;
            //addic. xenonCPU.rD,rA,0x1000
            uint expected = 0xF0000FFF;

            xenonCPU.addic_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subic_()
        {
            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.imm = 0x1000;
            uint expected = 0xEFFFEFFF;

            xenonCPU.subic_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addis()
        {
            // Assume GPR 6 contains 0x0000 4000.
            // addis 7,6,0x0011
            // GPR 7 now contains 0x0011 4000.

            xenonCPU.rA = 0x00004000;
            xenonCPU.imm = 0x0011;
            uint expected = 0x00114000;

            xenonCPU.addis();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subis()
        {
            xenonCPU.rA = 0x00114000;
            xenonCPU.imm = 0x0011;
            uint expected = 0x00004000;

            xenonCPU.subis();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void lis()
        {
            uint expected = 0x00110000;
            xenonCPU.imm = 0x0011;

            xenonCPU.lis();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addmex()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume the Carry bit is zero.
            // addme 6,4
            // GPR 6 now contains 0x9000 2FFF.

            xenonCPU.rA = 0x90003000;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0x90002FFF;

            xenonCPU.addmex();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addme()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume the Carry bit is zero.
            // addme 6,4
            // GPR 6 now contains 0x9000 2FFF.

            xenonCPU.rA = 0x90003000;
            uint expected = 0x90002FFF;
            xenonCPU.xer = 0;  // Assume the Carry bit is zero.

            xenonCPU.addme();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addme_()
        {
            // Assume GPR 4 contains 0xB000 42FF.
            // Assume the Carry bit is zero.
            // addme. 6,4
            // GPR 6 now contains 0xB000 42FE.

            xenonCPU.rA = 0xB00042FF;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0xB00042FE;

            xenonCPU.addme_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addmeox()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume the Carry bit is zero.
            // addmeo 6,4
            // GPR 6 now contains 0x7FFF FFFF.

            xenonCPU.rA = 0x80000000;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0x7FFFFFFF;
            
            xenonCPU.addmeox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addmeo()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume the Carry bit is zero.
            // addmeo 6,4
            // GPR 6 now contains 0x7FFF FFFF.

            xenonCPU.rA = 0x80000000;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0x7FFFFFFF;
            
            xenonCPU.addmeo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addmeo_()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume the Carry bit is one.
            // addmeo. 6,4
            // GPR 6 now contains 0x8000 000.

            xenonCPU.rA = 0x80000000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x80000000;
            
            xenonCPU.addmeo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addzex()
        {
            // Assume GPR 4 contains 0x7B41 92C0.
            // Assume the Carry bit is zero.
            // addze 6,4
            // GPR 6 now contains 0x7B41 92C0.

            xenonCPU.rA = 0x7B4192C0;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0x7B4192C0;

            xenonCPU.addzex();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addze()
        {
            // Assume GPR 4 contains 0x7B41 92C0.
            // Assume the Carry bit is zero.
            // addze 6,4
            // GPR 6 now contains 0x7B41 92C0.

            xenonCPU.rA = 0x7B4192C0;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0x7B4192C0;

            xenonCPU.addze();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addze_()
        {
            // Assume GPR 4 contains 0xEFFF FFFF.
            // Assume the Carry bit is one.
            // addze. 6,4
            // GPR 6 now contains 0xF000 0000.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0xF0000000;

            xenonCPU.addze_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addzeox()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume the Carry bit is one.
            // addzeo 6,4
            // GPR 6 now contains 0x9000 3001.

            xenonCPU.rA = 0x90003000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x90003001;

            xenonCPU.addzeox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addzeo()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume the Carry bit is one.
            // addzeo 6,4
            // GPR 6 now contains 0x9000 3001.

            xenonCPU.rA = 0x90003000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x90003001;

            xenonCPU.addzeo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void addzeo_()
        {
            // Assume GPR 4 contains 0xEFFF FFFF.
            // Assume the Carry bit is zero.
            // addzeo. 6,4
            // GPR 6 now contains 0xEFFF FFFF. 

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.xer = 0;   // Assume the Carry bit is zero.
            uint expected = 0xEFFFFFFF;

            xenonCPU.addzeo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void andx()
        {
            // Assume GPR 4 contains 0xFFF2 5730.
            // Assume GPR 7 contains 0x7B41 92C0.
            // and 6,4,7
            // GPR 6 now contains 0x7B40 1200.

            xenonCPU.rS = 0xFFF25730;
            xenonCPU.rB = 0x7B4192C0;
            //and 6,rS,rB
            uint expected = 0x7B401200;

            xenonCPU.andx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void and()
        {
            // Assume GPR 4 contains 0xFFF2 5730.
            // Assume GPR 7 contains 0x7B41 92C0.
            // and 6,4,7
            // GPR 6 now contains 0x7B40 1200.

            xenonCPU.rS = 0xFFF25730;
            xenonCPU.rB = 0x7B4192C0;
            //and 6,rS,rB
            uint expected = 0x7B401200;

            xenonCPU.and();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void and_()
        {
            // Assume GPR 4 contains 0xFFF2 5730.
            // Assume GPR 7 contains 0xFFFF EFFF.
            // and. 6,4,7
            // GPR 6 now contains 0xFFF2 4730.

            xenonCPU.rS = 0xFFF25730;
            xenonCPU.rB = 0xFFFFEFFF;
            uint expected = 0xFFF24730;

            xenonCPU.and_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void andcx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 5 contains 0xFFFF FFFF.
            // The complement of 0xFFFF FFFF becomes 0x0000 0000.
            // andc 6,4,5
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0xFFFFFFFF;
            uint expected = 0x00000000;

            xenonCPU.andcx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void andc()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 5 contains 0xFFFF FFFF.
            // The complement of 0xFFFF FFFF becomes 0x0000 0000.
            // andc 6,4,5
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0xFFFFFFFF;
            uint expected = 0x00000000;

            xenonCPU.andc();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void andc_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 5 contains 0x7676 7676.
            // The complement of 0x7676 7676 is 0x8989 8989.
            // andc. 6,4,5
            // GPR 6 now contains 0x8000 0000.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rB = 0x76767676;
            uint expected = 0x80000000;

            xenonCPU.andc_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void andi_()
        {
            // Assume GPR 4 contains 0x7B41 92C0.
            // andi. 6,4,0x5730
            // GPR 6 now contains 0x0000 1200.
            // CRF 0 now contains 0x4.

            xenonCPU.rS = 0x7B4192C0;
            xenonCPU.imm = 0x5730;
            uint expectedcr = 0x40000000;   // CR now contains 0x4.	// Remember the endian..
            uint expected = 0x00001200;

            xenonCPU.andi_();
            Assert.AreEqual(expected, xenonCPU.rA);
            Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void andis_()
        {
            // Assume GPR 4 contains 0x7B41 92C0.
            // andis. 6,4,0x5730
            // GPR 6 now contains 0x5300 0000.

            xenonCPU.rS = 0x7B4192C0;
            xenonCPU.imm = 0x5730;
            uint expected = 0x53000000;

            xenonCPU.andis_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void cmp()
        {
            // Assume GPR 4 contains 0xFFFF FFE7.
            // Assume GPR 5 contains 0x0000 0011.
            // Assume 0 is Condition Register Field 0.
            // cmp 0,4,6
            // The LT bit of Condition Register Field 0 is set. 

            xenonCPU.rA = 0xFFFFFFE7;
            xenonCPU.rB = 0x00000011;
            xenonCPU.cr = 0;   // Assume 0 is Condition Register Field 0.
            uint expected = 0x80000007; // The LT bit of Condition Register Field 0 is set. 

            xenonCPU.cmp();
            Assert.AreEqual(expected, xenonCPU.cr);
        }

        [TestMethod]
        public void cmpd()
        {
            xenonCPU.rA = 0xFFFFFFE7;
            xenonCPU.rB = 0x00000011;
            xenonCPU.cr = 0;   // Assume 0 is Condition Register Field 0.
            uint expected = 0x80000007; // The LT bit of Condition Register Field 0 is set. 

            xenonCPU.cmpd();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void cmpw()
        {
            xenonCPU.rA = 0xFFFFFFE7;
            xenonCPU.rB = 0x00000011;
            xenonCPU.cr = 0;   // Assume 0 is Condition Register Field 0.
            uint expected = 0x80000007; // The LT bit of Condition Register Field 0 is set. 

            xenonCPU.cmpw();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void cmpi()
        {
            // Assume GPR 4 contains 0xFFFF FFE7.
            // cmpi 0,4,0x11
            // The LT bit of Condition Register Field 0 is set. 
            
            xenonCPU.rA = 0xFFFFFFE7;
            xenonCPU.imm = 0x11;
            uint expected = 0x80000007; // The LT bit of Condition Register Field 0 is set. 

            xenonCPU.cmpi();
            Assert.AreEqual(expected, xenonCPU.cr);
        }

        [TestMethod]
        public void cmpdi()
        {
            xenonCPU.rA = 0xFFFFFFE7;
            xenonCPU.imm = 0x11;
            uint expected = 0x80000007; // The LT bit of Condition Register Field 0 is set. 

            xenonCPU.cmpdi();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void cmpwi()
        {
            xenonCPU.rA = 0xFFFFFFE7;
            xenonCPU.imm = 0x11;
            uint expected = 0x80000007; // The LT bit of Condition Register Field 0 is set. 

            xenonCPU.cmpwi();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void cmpl()
        {
            // Assume GPR 4 contains 0xFFFF 0000.
            // Assume GPR 5 contains 0x7FFF 0000.
            // Assume 0 is Condition Register Field 0.
            // cmpl 0,4,5
            // The GT bit of Condition Register Field 0 is set. 

            xenonCPU.rA = 0xFFFF0000;
            xenonCPU.rB = 0x7FFF0000;
            xenonCPU.cr = 0;   // Assume 0 is Condition Register Field 0.
            uint expected = 0x40000007; // The GT bit of Condition Register Field 0 is set.

            xenonCPU.cmpl();
            Assert.AreEqual(expected, xenonCPU.cr);
        }

        [TestMethod]
        public void cmpld()
        {
            xenonCPU.rA = 0xFFFF0000;
            xenonCPU.rB = 0x7FFF0000;
            xenonCPU.cr = 0;   // Assume 0 is Condition Register Field 0.
            uint expected = 0x80000007; // The GT bit of Condition Register Field 0 is set.

            xenonCPU.cmpld();
            Assert.AreEqual(expected, xenonCPU.cr);
        }

        [TestMethod]
        public void cmplw()
        {
            xenonCPU.rA = 0xFFFF0000;
            xenonCPU.rB = 0x7FFF0000;
            xenonCPU.cr = 0;   // Assume 0 is Condition Register Field 0.
            uint expected = 0x80000007; // The GT bit of Condition Register Field 0 is set.

            xenonCPU.cmplw();
            Assert.AreEqual(expected, xenonCPU.cr);
        }

        [TestMethod]
        public void cmpli()
        {
            // Assume GPR 4 contains 0x0000 00ff.
            // cmpli 0,4,0xff
            // The EQ bit of Condition Register Field 0 is set. 

            xenonCPU.rA = 0x000000FF;
            xenonCPU.imm = 0xFF;
            xenonCPU.cr = 0;   // Assume 0 is Condition Register Field 0.
            uint expected = 0x20000007; // The EQ bit of Condition Register Field 0 is set.

            xenonCPU.cmpli();
            Assert.AreEqual(expected, xenonCPU.cr);
        }

        [TestMethod]
        public void cmpldir()
        {
            //xenonCPU.cmpldir();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void cmplwi()
        {
            //xenonCPU.cmplwi();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void cntlzwx()
        {
            // Assume GPR 3 contains 0x0FFF FFFF 0061 9920.
            // cntlzw 3,3
            // GPR 3 now holds 0x0000 0000 0000 0009. Note that the high-order 32 bits
            // are ignored when computing the result.
            xenonCPU.rS = 0x00619920;
            uint expected = 00000009;

            xenonCPU.cntlzwx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void cntlzw()
        {
            // Assume GPR 3 contains 0x0FFF FFFF 0061 9920.
            // cntlzw 3,3
            // GPR 3 now holds 0x0000 0000 0000 0009. Note that the high-order 32 bits
            // are ignored when computing the result.
            xenonCPU.rS = 0x00619920;
            uint expected = 00000009;

            xenonCPU.cntlzw();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void cntlzw_()
        {
            // Assume GPR 3 contains 0x0FFF FFFF 0061 9920.
            // cntlzw 3,3
            // GPR 3 now holds 0x0000 0000 0000 0009. Note that the high-order 32 bits
            // are ignored when computing the result.
            xenonCPU.rS = 0x00619920;
            uint expected = 00000009;

            xenonCPU.cntlzw_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void crand()
        {
            // Assume Condition Register bit 0 is 1.
            // Assume Condition Register bit 5 is 0.
            // crand 31,0,5
            // Condition Register bit 31 is now 0.

            xenonCPU.crand();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void crandc()
        {
            // Assume Condition Register bit 0 is 1.
            // Assume Condition Register bit 5 is 0.
            // crandc 31,0,5
            // Condition Register bit 31 is now 1.

            xenonCPU.crandc();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void creqv()
        {
            // Assume Condition Register bit 8 is 1.
            // Assume Condition Register bit 4 is 0.
            // creqv 4,8,4
            // Condition Register bit 4 is now 0.

            xenonCPU.creqv();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void crset()
        {
            //xenonCPU.crset();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void crnand()
        {
            // Assume Condition Register bit 8 is 1.
            // Assume Condition Register bit 4 is 0.
            // crnand 4,8,4
            // Condition Register bit 4 is now 1.

            xenonCPU.crnand();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void crnor()
        {
            // Assume Condition Register bit 8 is 1.
            // Assume Condition Register bit 4 is 0.
            // crnor 4,8,4
            // Condition Register bit 4 is now 0.

            xenonCPU.crnor();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void crnot()
        {
            //xenonCPU.crnot();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void cror()
        {
            // Assume Condition Register bit 8 is 1.
            // Assume Condition Register bit 4 is 0.
            // cror 4,8,4
            // Condition Register bit 4 is now 1.

            xenonCPU.cror();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void crorc()
        {
            // Assume Condition Register bit 8 is 1.
            // Assume Condition Register bit 4 is 0.
            // crorc 4,8,4
            // Condition Register bit 4 is now 1.

            xenonCPU.crorc();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void crxor()
        {
            // Assume Condition Register bit 8 is 1.
            // Assume Condition Register bit 4 is 1.
            // crxor 4,8,4
            // Condition Register bit 4 is now 0.

            xenonCPU.crxor();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void crclr()
        {
            //xenonCPU.crclr();
            //Assert.AreEqual(expected, xenonCPU.rD);
            Assert.Fail();
        }

        [TestMethod]
        public void divwx()
        {
            // Assume GPR 4 contains 0x0000 0000.
            // Assume GPR 6 contains 0x0000 0002.
            // divw 4,4,6
            // GPR 4 now contains 0x0000 0000.

            xenonCPU.rA = 0x00000000;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000000;

            xenonCPU.divwx();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divw()
        {
            // Assume GPR 4 contains 0x0000 0000.
            // Assume GPR 6 contains 0x0000 0002.
            // divw 4,4,6
            // GPR 4 now contains 0x0000 0000.

            xenonCPU.rA = 0x00000000;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000000;

            xenonCPU.divw();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divw_()
        {
            // Assume GPR 4 contains 0x0000 0002.
            // Assume GPR 6 contains 0x0000 0002.
            // divw. 4,4,6
            // GPR 4 now contains 0x0000 0001.

            xenonCPU.rA = 0x00000002;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000001;

            // cr = 0
            xenonCPU.divw_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwox()
        {
            // Assume GPR 4 contains 0x0000 0001.
            // Assume GPR 6 contains 0x0000 0000.
            // divwo 4,4,6
            // GPR 4 now contains an undefined quantity.

            xenonCPU.rA = 0x00000002;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000001;

            xenonCPU.divwox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwo()
        {
            // Assume GPR 4 contains 0x0000 0001.
            // Assume GPR 6 contains 0x0000 0000.
            // divwo 4,4,6
            // GPR 4 now contains an undefined quantity.

            xenonCPU.rA = 0x00000002;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000001;

            xenonCPU.divwo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwo_()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 6 contains 0xFFFF FFFF.
            // divwo. 4,4,6
            // GPR 4 now contains undefined quantity.

            xenonCPU.rA = 0x00000002;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000001;

            xenonCPU.divwo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwux()
        {
            // Assume GPR 4 contains 0x0000 0000.
            // Assume GPR 6 contains 0x0000 0002.
            // divwu 4,4,6
            // GPR 4 now contains 0x0000 0000.

            xenonCPU.rA = 0x00000000;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000000;

            xenonCPU.divwux();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwu()
        {
            // Assume GPR 4 contains 0x0000 0000.
            // Assume GPR 6 contains 0x0000 0002.
            // divwu 4,4,6
            // GPR 4 now contains 0x0000 0000.

            xenonCPU.rA = 0x00000000;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000000;

            xenonCPU.divwu();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwu_()
        {
            // Assume GPR 4 contains 0x0000 0002.
            // Assume GPR 6 contains 0x0000 0002.
            // divwu. 4,4,6
            // GPR 4 now contains 0x0000 0001.

            xenonCPU.rA = 0x00000002;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000001;

            xenonCPU.divwu_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwuox()
        {
            // Assume GPR 4 contains 0x0000 0001.
            // Assume GPR 6 contains 0x0000 0000.
            // divwuo 4,4,6
            // GPR 4 now contains an undefined quantity.

            xenonCPU.rA = 0x00000002;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000001;

            xenonCPU.divwuox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwuo()
        {
            // Assume GPR 4 contains 0x0000 0001.
            // Assume GPR 6 contains 0x0000 0000.
            // divwuo 4,4,6
            // GPR 4 now contains an undefined quantity.

            xenonCPU.rA = 0x00000002;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000001;

            xenonCPU.divwuo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void divwuo_()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 6 contains 0x0000 0002.
            // divwuo. 4,4,6
            // GPR 4 now contains 0x4000 0000.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x40000000;

            xenonCPU.divwuo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void eqvx()
        {
            // Assume GPR 4 holds 0xFFF2 5730.
            // Assume GPR 6 holds 0x7B41 92C0.
            // eqv 4,4,6
            // GPR 4 now holds 0x7B4C 3A0F.

            xenonCPU.rS = 0xFFF25730;
            xenonCPU.rB = 0x7B4192C0;
            uint expected = 0x7B4C3A0F;

            xenonCPU.eqvx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void eqv()
        {
            // Assume GPR 4 holds 0xFFF2 5730.
            // Assume GPR 6 holds 0x7B41 92C0.
            // eqv 4,4,6
            // GPR 4 now holds 0x7B4C 3A0F.

            xenonCPU.rS = 0xFFF25730;
            xenonCPU.rB = 0x7B4192C0;
            uint expected = 0x7B4C3A0F;

            xenonCPU.eqv();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void eqv_()
        {
            // Assume GPR 4 holds 0x0000 00FD.
            // Assume GPR 6  holds 0x7B41 92C0.
            // eqv. 4,4,6
            // GPR 4 now holds 0x84BE 6DC2.

            xenonCPU.rS = 0x000000FD;
            xenonCPU.rB = 0x7B4192C0;
            uint expected = 0x84BE6DC2;

            xenonCPU.eqv_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void extsbx()
        {
            // Assume GPR 6 holds 0x5A5A 5A5A.
            // extsb 4,6
            // GPR 6 now holds 0x0000 005A.

            xenonCPU.rS = 0x5A5A5A5A;
            uint expected = 0x0000005A;

            xenonCPU.extsbx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void extsb()
        {
            // Assume GPR 6 holds 0x5A5A 5A5A.
            // extsb 4,6
            // GPR 6 now holds 0x0000 005A.

            xenonCPU.rS = 0x5A5A5A5A;
            uint expected = 0x0000005A;

            xenonCPU.extsb();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void extsb_()
        {
            // Assume GPR 4 holds 0xA5A5 A5A5.
            // extsb. 4,4
            // GPR 4 now holds 0xFFFF FFA5.

            xenonCPU.rS = 0xA5A5A5A5;
            uint expected = 0xFFFFFFA5;

            xenonCPU.extsb_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void extshx()
        {
            // Assume GPR 6 holds 0x0000 FFFF.
            // extsh 4,6
            // GPR 6 now holds 0xFFFF FFFF.

            xenonCPU.rS = 0x0000FFFF;
            uint expected = 0xFFFFFFFF;

            xenonCPU.extshx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void extsh()
        {
            // Assume GPR 6 holds 0x0000 FFFF.
            // extsh 4,6
            // GPR 6 now holds 0xFFFF FFFF.

            xenonCPU.rS = 0x0000FFFF;
            uint expected = 0xFFFFFFFF;

            xenonCPU.extsh();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void extsh_()
        {
            // Assume GPR 4 holds 0x0000 2FFF.
            // extsh. 6,4
            // GPR 6 now holds 0x0000 2FFF.

            xenonCPU.rS = 0x00002FFF;
            uint expected = 0x00002FFF;

            xenonCPU.extsh_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void mulhwx()
        {
            // Assume GPR 4 contains 0x0000 0003.
            // Assume GPR 10 contains 0x0000 0002.
            // mulhw 6,4,10
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rA = 0x00000003;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000000;

            xenonCPU.mulhwx();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void mulhw()
        {
            // Assume GPR 4 contains 0x0000 0003.
            // Assume GPR 10 contains 0x0000 0002.
            // mulhw 6,4,10
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rA = 0x00000003;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000000;

            xenonCPU.mulhw();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void mulhw_()
        {
            // Assume GPR 4 contains 0x0000 4500.
            // Assume GPR 10 contains 0x8000 7000.
            // Assume XER(SO) = 0.
            // mulhw. 6,4,10
            // GPR 6 now contains 0xFFFF DD80.
            // Condition Register Field 0 now contains 0x4.

            xenonCPU.rA = 0x00004500;
            xenonCPU.rB = 0x80007000;
            xenonCPU.xer = 0;   // Assume XER(SO) = 0.
            uint expected = 0xFFFFDD80;
            //uint expectedcr = 0x40000000;  // Condition Register Field 0 now contains 0x4.

            xenonCPU.mulhw_();
            Assert.AreEqual(expected, xenonCPU.rD);
            //Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void mulhwux()
        {
            // Assume GPR 4 contains 0x0000 0003.
            // Assume GPR 10 contains 0x0000 0002.
            // mulhwu 6,4,10
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rA = 0x00000003;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000000;

            xenonCPU.mulhwux();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void mulhwu()
        {
            // Assume GPR 4 contains 0x0000 0003.
            // Assume GPR 10 contains 0x0000 0002.
            // mulhwu 6,4,10
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rA = 0x00000003;
            xenonCPU.rB = 0x00000002;
            uint expected = 0x00000000;

            xenonCPU.mulhwu();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void mulhwu_()
        {
            // Assume GPR 4 contains 0x0000 4500.
            // Assume GPR 10 contains 0x8000 7000.
            // Assume XER(SO) = 0.
            // mulhwu. 6,4,10
            // GPR 6 now contains 0x0000 2280.
            // Condition Register Field 0 now contains 0x4.

            xenonCPU.rA = 0x00004500;
            xenonCPU.rB = 0x80007000;
            xenonCPU.xer = 0;   // Assume XER(SO) = 0.
            uint expected = 0x00002280;
            uint expectedcr = 0x40000000; // Condition Register Field 0 now contains 0x4.

            xenonCPU.mulhwu_();
            Assert.AreEqual(expected, xenonCPU.rD);
            Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void mulli()
        {
            // Assume GPR 4 holds 0x0000 3000.
            // mulli 6,4,10
            // GPR 6 now holds 0x0001 E000.

            xenonCPU.rA = 0x00003000;
            xenonCPU.imm = 10;
            uint expected = 0x0001E000;
            xenonCPU.mulli();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void mullwx()
        {
            // Assume GPR 4 holds 0x0000 3000.
            // Assume GPR 10 holds 0x0000 7000.
            // mullw 6,4,10
            // GPR 6 now holds 0x1500 0000.

            xenonCPU.rA = 0x00003000;
            xenonCPU.rB = 0x00007000;
            uint expected = 0x15000000;

            xenonCPU.mullwx();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void mullw()
        {
            // Assume GPR 4 holds 0x0000 3000.
            // Assume GPR 10 holds 0x0000 7000.
            // mullw 6,4,10
            // GPR 6 now holds 0x1500 0000.

            xenonCPU.rA = 0x00003000;
            xenonCPU.rB = 0x00007000;
            uint expected = 0x15000000;

            xenonCPU.mullw();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void mullw_()
        {
            // Assume GPR 4 holds 0x0000 4500.
            // Assume GPR 10 holds 0x0000 7000.
            // Assume XER(SO) = 0.
            // mullw. 6,4,10
            // GPR 6 now holds 0x1E30 0000.
            // Condition Register Field 0 now contains 0x4.

            xenonCPU.rA = 0x00004500;
            xenonCPU.rB = 0x00007000;
            xenonCPU.xer = 0;   // Assume XER(SO) = 0.
            uint expected = 0x1E300000;
            uint expectedcr = 0x40000000;      // Condition Register Field 0 now contains 0x4.

            xenonCPU.mullw_();
            Assert.AreEqual(expected, xenonCPU.rD);
            Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void mullwo()
        {
            // Assume GPR 4 holds 0x0000 4500.
            // Assume GPR 10 holds 0x0007 0000.
            // Assume XER = 0.
            // mullwo 6,4,10
            // GPR 6 now holds 0xE300 0000.
            // XER now contains 0xc000 0000

            xenonCPU.rA = 0x00004500;
            xenonCPU.rB = 0x00070000;
            xenonCPU.xer = 0;    // Assume XER = 0.
            uint expected = 0xE3000000;
            uint expectedxer = 0xc0000000;    // XER now contains 0xc0000000

            //xenonCPU.mullwo();
            //Assert.AreEqual(expected, xenonCPU.rD);
            //Assert.AreEqual(expectedxer, xenonCPU.xer);
            Assert.Fail();
        }

        [TestMethod]
        public void mullwo_()
        {
            // Assume GPR 4 holds 0x0000 4500.
            // Assume GPR 10 holds 0x7FFF FFFF.
            // Assume XER = 0.
            // mullwo. 6,4,10
            // GPR 6 now holds 0xFFFF BB00.
            // XER now contains 0xc000 0000
            // Condition Register Field 0 now contains 0x9.

            xenonCPU.rA = 0x00004500;
            xenonCPU.rB = 0x7FFFFFFF;
            
            xenonCPU.xer = 0;       // Assume XER = 0.
            uint expected = 0xFFFFBB00;

            uint expectedxer = 0xc0000000;       // XER now contains 0xc000 0000
            uint expectedcr = 0x9;               // Condition Register Field 0 now contains 0x9.

            //xenonCPU.mullwo_();
            //Assert.AreEqual(expected, xenonCPU.rD);
            //Assert.AreEqual(expectedxer, xenonCPU.xer);
            //Assert.AreEqual(expectedcr, xenonCPU.cr);
            Assert.Fail();
        }

        [TestMethod]
        public void nandx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // nand 6,4,7
            // GPR 6 now contains 0xEFFF CFFF.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xEFFFCFFF;

            xenonCPU.nandx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void nand()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // nand 6,4,7
            // GPR 6 now contains 0xEFFF CFFF.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xEFFFCFFF;

            xenonCPU.nand();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void nand_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // nand. 6,4,7
            // GPR 6 now contains 0xCFFF CFFF.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xCFFFCFFF;

            xenonCPU.nand_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void negx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // neg 6,4
            // GPR 6 now contains 0x6FFF D000.

            xenonCPU.rA = 0x90003000;
            uint expected = 0x6FFFD000;

            xenonCPU.negx();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void neg()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // neg 6,4
            // GPR 6 now contains 0x6FFF D000.

            xenonCPU.rA = 0x90003000;
            uint expected = 0x6FFFD000;

            xenonCPU.neg();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void neg_()
        {
            // Assume GPR 4 contains 0x789A 789B.
            // neg. 6,4
            // GPR 6 now contains 0x8765 8765.

            xenonCPU.rA = 0x789A789B;
            uint expected = 0x87658765;

            xenonCPU.neg_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void negox()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // nego 6,4
            // GPR 6 now contains 0x6FFF D000.

            xenonCPU.rA = 0x90003000;
            uint expected = 0x6FFFD000;

            xenonCPU.negox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void nego()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // nego 6,4
            // GPR 6 now contains 0x6FFF D000.

            xenonCPU.rA = 0x90003000;
            uint expected = 0x6FFFD000;

            xenonCPU.nego();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void nego_()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // nego. 6,4
            // GPR 6 now contains 0x8000 0000.

            xenonCPU.rA = 0x80000000;
            uint expected = 0x80000000;

            xenonCPU.nego_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void norx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 6 contains 0x789A 789B.
            // nor 6,4,7
            // GPR 7 now contains 0x0765 8764.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0x07658764;

            xenonCPU.norx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void nor()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 6 contains 0x789A 789B.
            // nor 6,4,7
            // GPR 7 now contains 0x0765 8764.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0x07658764;

            xenonCPU.nor();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void nor_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // nor. 6,4,7
            // GPR 6 now contains 0x0761 8764.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0x07618764;

            xenonCPU.nor_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void orx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // or 6,4,7
            // GPR 6 now contains 0xF89A 789B.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xF89A789B;

            xenonCPU.orx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void or()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // or 6,4,7
            // GPR 6 now contains 0xF89A 789B.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xF89A789B;

            xenonCPU.or();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void or_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // or. 6,4,7
            // GPR 6 now contains 0xF89E 789B.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xF89E789B;

            xenonCPU.or_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void orcx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 7 contains 0x789A 789B, whose
            // complement is 0x8765 8764.
            // orc 6,4,7
            // GPR 6 now contains 0x9765 B764.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B; // whose complement is 0x8765 8764.
            uint expected = 0x9765B764;

            xenonCPU.orcx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void orc()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 7 contains 0x789A 789B, whose
            // complement is 0x8765 8764.
            // orc 6,4,7
            // GPR 6 now contains 0x9765 B764.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B; // whose complement is 0x8765 8764.
            uint expected = 0x9765B764;

            xenonCPU.orc();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void orc_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 7 contains 0x789A 789B, whose
            // complement is 0x8765 8764.
            // orc. 6,4,7
            // GPR 6 now contains 0xB765 B764.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rB = 0x789A789B; // whose complement is 0x8765 8764.
            uint expected = 0xB765B764;

            xenonCPU.orc_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void ori()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // ori 6,4,0x0079
            // GPR 6 now contains 0x9000 3079.

            xenonCPU.rS = 0x90003000;
            xenonCPU.imm = 0x0079;
            uint expected = 0x90003079;

            xenonCPU.ori();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void nop()
        {
            // Will never fail

            xenonCPU.nop();
        }

        [TestMethod]
        public void oris()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // oris 6,4,0x0079
            // GPR 6 now contains 0x9079 3000.

            xenonCPU.rS = 0x90003000;
            xenonCPU.imm = 0x0079;
            uint expected = 0x90793000;

            xenonCPU.oris();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void rlwimix()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 6 contains 0x0000 0003.
            // rlwimi 6,4,2,0,0x1D
            // GPR 6 now contains 0x4000 C003.
            // Under the same conditions
            //  rlwimi 6,4,2,0xFFFFFFFC
            // will produce the same result.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rA = 0x00000003;
            xenonCPU.SH = 2;
            xenonCPU.MB = 0;
            xenonCPU.ME = 0x1D;
            uint expected = 0x4000C003;

            xenonCPU.rlwimix();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void rlwimi()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 6 contains 0x0000 0003.
            // rlwimi 6,4,2,0,0x1D
            // GPR 6 now contains 0x4000 C003.
            // Under the same conditions
            //  rlwimi 6,4,2,0xFFFFFFFC
            // will produce the same result.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rA = 0x00000003;
            xenonCPU.SH = 2;
            xenonCPU.MB = 0;
            xenonCPU.ME = 0x1D;
            uint expected = 0x4000C003;

            xenonCPU.rlwimi();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void rlwimi_()
        {
            // Assume GPR 4 contains 0x789A 789B.
            // Assume GPR 6 contains 0x3000 0003.
            // rlwimi. 6,4,2,0,0x1A
            // GPR 6 now contains 0xE269 E263.
            // CRF 0 now contains 0x8.
            // Under the same conditions
            //  rlwimi. 6,4,2,0xFFFFFFE0
            // will produce the same result.

            xenonCPU.rS = 0x789A789B;
            xenonCPU.rA = 0x30000003;
            xenonCPU.SH = 2;
            xenonCPU.MB = 0;
            xenonCPU.ME = 0x1A;
            uint expected = 0xE269E263;
            uint expectedcr = 0x80000000;

            xenonCPU.rlwimi_();
            Assert.AreEqual(expected, xenonCPU.rA);
            Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void rlwinmx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 6 contains 0xFFFF FFFF.
            // rlwinm 6,4,2,0,0x1D
            // GPR 6 now contains 0x4000 C000.
            // Under the same conditions
            // rlwinm 6,4,2,0xFFFFFFFC
            // will produce the same result.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rA = 0xFFFFFFFF;
            xenonCPU.SH = 2;
            xenonCPU.MB = 0;
            xenonCPU.ME = 0x1D;
            uint expected = 0x4000C000;

            xenonCPU.rlwinmx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void rlwinm()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 6 contains 0xFFFF FFFF.
            // rlwinm 6,4,2,0,0x1D
            // GPR 6 now contains 0x4000 C000.
            // Under the same conditions
            // rlwinm 6,4,2,0xFFFFFFFC
            // will produce the same result.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rA = 0xFFFFFFFF;
            xenonCPU.SH = 2;
            xenonCPU.MB = 0;
            xenonCPU.ME = 0x1D;
            uint expected = 0x4000C000;

            xenonCPU.rlwinm();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void rlwinm_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 6 contains 0xFFFF FFFF.
            // rlwinm. 6,4,2,0,0x1D
            // GPR 6 now contains 0xC010 C000.
            // CRF 0 now contains 0x8.
            // Under the same conditions
            // rlwinm. 6,4,2,0xFFFFFFFC
            // will produce the same result.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rA = 0xFFFFFFFF;
            xenonCPU.SH = 2;
            xenonCPU.MB = 0;
            xenonCPU.ME = 0x1D;
            uint expected = 0xC010C000;

            xenonCPU.rlwinm_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void rlwnmx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 5 contains 0x0000 0002.
            // Assume GPR 6 contains 0xFFFF FFFF.
            // rlwnm 6,4,5,0,0x1D
            // GPR 6 now contains 0x4000 C000.
            // Under the same conditions
            //  rlwnm 6,4,5,0xFFFFFFFC
            // will produce the same result.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x00000002;
            xenonCPU.rA = 0xFFFFFFFF;
            xenonCPU.MB = 0;
            xenonCPU.ME = 0x1D;
            uint expected = 0x4000C000;

            xenonCPU.rlwnmx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void extlwi()
        {
            //xenonCPU.extlwi();
            Assert.Fail();
        }

        [TestMethod]
        public void extrwi()
        {
            //xenonCPU.extrwi();
            Assert.Fail();
        }

        [TestMethod]
        public void inslwi()
        {
            //xenonCPU.inslwi();
            Assert.Fail();
        }

        [TestMethod]
        public void insrwi()
        {
            //xenonCPU.insrwi();
            Assert.Fail();
        }

        [TestMethod]
        public void rotlwi()
        {
            //xenonCPU.rotlwi();
            Assert.Fail();
        }

        [TestMethod]
        public void rotrwi()
        {
            //xenonCPU.rotrwi();
            Assert.Fail();
        }

        [TestMethod]
        public void rotlw()
        {
            //xenonCPU.rotlw();
            Assert.Fail();
        }

        [TestMethod]
        public void slwi()
        {
            //xenonCPU.slwi();
            Assert.Fail();
        }

        [TestMethod]
        public void srwi()
        {
            //xenonCPU.srwi();
            Assert.Fail();
        }

        [TestMethod]
        public void clrlwi()
        {
            //xenonCPU.clrlwi();
            Assert.Fail();
        }

        [TestMethod]
        public void clrrwi()
        {
            //xenonCPU.clrrwi();
            Assert.Fail();
        }

        [TestMethod]
        public void clrlslwi()
        {
            //xenonCPU.clrlslwi();
            Assert.Fail();
        }

        [TestMethod]
        public void slwx()
        {
            // Assume GPR 5 contains 0x0000 002F.
            // Assume GPR 4 contains 0xFFFF FFFF.
            // slw 6,4,5
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rS = 0x0000002F;
            xenonCPU.rB = 0xFFFFFFFF;
            uint expected = 0x00000000;

            xenonCPU.slwx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void slw()
        {
            // Assume GPR 5 contains 0x0000 002F.
            // Assume GPR 4 contains 0xFFFF FFFF.
            // slw 6,4,5
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rS = 0x0000002F;
            xenonCPU.rB = 0xFFFFFFFF;
            uint expected = 0x00000000;

            xenonCPU.slw();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void slw_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 5 contains 0x0000 0005.
            // slw. 6,4,5
            // GPR 6 now contains 0x0086 0000.
            // Condition Register Field 0 now contains 0x4.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rB = 0x00000005;
            uint expected = 0x00860000;
            uint expectedcr = 0x40000000;   // Condition Register Field 0 now contains 0x4.

            xenonCPU.slw_();
            Assert.AreEqual(expected, xenonCPU.rA);
            Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void srawx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 5 contains 0x0000 0024.
            // sraw 6,4,5
            // GPR 6 now contains 0xFFFF FFFF.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x00000024;
            uint expected = 0xFFFFFFFF;

            xenonCPU.srawx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void sraw()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 5 contains 0x0000 0024.
            // sraw 6,4,5
            // GPR 6 now contains 0xFFFF FFFF.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x00000024;
            uint expected = 0xFFFFFFFF;

            xenonCPU.sraw();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void sraw_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 5 contains 0x0000 0004.
            // sraw. 6,4,5
            // GPR 6 now contains 0xFB00 4300.
            // Condition Register Field 0 now contains 0x8.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rB = 0x00000004;
            uint expected = 0xFB004300;
            uint expectedcr = 0x80000000; // Condition Register Field 0 now contains 0x8.

            xenonCPU.sraw_();
            Assert.AreEqual(expected, xenonCPU.rA);
            Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void srawix()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // srawi 6,4,0x4
            // GPR 6 now contains 0xF900 0300.

            xenonCPU.rS = 0x90003000;
            xenonCPU.SH = 0x4;
            uint expected = 0xF9000300;

            xenonCPU.srawix();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void srawi()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // srawi 6,4,0x4
            // GPR 6 now contains 0xF900 0300.

            xenonCPU.rS = 0x90003000;
            xenonCPU.SH = 0x4;
            uint expected = 0xF9000300;

            xenonCPU.srawi();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void srawi_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // srawi. 6,4,0x4
            // GPR 6 now contains 0xFB00 4300.
            // Condition Register Field 0 now contains 0x8.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.SH = 0x4;
            uint expected = 0xFB004300;
            uint expectedcr = 0x80000000;  // Condition Register Field 0 now contains 0x8.

            xenonCPU.srawi_();
            Assert.AreEqual(expected, xenonCPU.rA);
            Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void srwx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 5 contains 0x0000 0024.
            // srw 6,4,5
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x00000024;
            uint expected = 0x00000000;

            xenonCPU.srwx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void srw()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 5 contains 0x0000 0024.
            // srw 6,4,5
            // GPR 6 now contains 0x0000 0000.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x00000024;
            uint expected = 0x00000000;

            xenonCPU.srw();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void srw_()
        {
            // Assume GPR 4 contains 0xB004 3001.
            // Assume GPR 5 contains 0x0000 0004.
            // srw. 6,4,5
            // GPR 6 now contains 0x0B00 4300.
            // Condition Register Field 0 now contains 0x4.

            xenonCPU.rS = 0xB0043001;
            xenonCPU.rB = 0x00000004;
            uint expected = 0x0B004300;
            uint expectedcr = 0x40000000; // Condition Register Field 0 now contains 0x4.

            xenonCPU.srw_();
            Assert.AreEqual(expected, xenonCPU.rA);
            Assert.AreEqual(expectedcr, xenonCPU.cr);
        }

        [TestMethod]
        public void subfx()
        {
            // Assume GPR 4 contains 0x8000 7000.
            // Assume GPR 10 contains 0x9000 3000.
            // subf 6,4,10
            // GPR 6 now contains 0x0FFF C000.

            xenonCPU.rA = 0x80007000;
            xenonCPU.rB = 0x90003000;
            uint expected = 0x0FFFC000;

            xenonCPU.subfx();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subf()
        {
            // Assume GPR 4 contains 0x8000 7000.
            // Assume GPR 10 contains 0x9000 3000.
            // subf 6,4,10
            // GPR 6 now contains 0x0FFF C000.

            xenonCPU.rA = 0x80007000;
            xenonCPU.rB = 0x90003000;
            uint expected = 0x0FFFC000;

            xenonCPU.subf();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subf_()
        {
            // Assume GPR 4 contains 0x0000 4500.
            // Assume GPR 10 contains 0x8000 7000.
            // subf. 6,4,10
            // GPR 6 now contains 0x8000 2B00.

            xenonCPU.rA = 0x00004500;
            xenonCPU.rB = 0x80007000;
            uint expected = 0x80002B00;

            xenonCPU.subf_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfox()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0x0000 4500.
            // subfo 6,4,10
            // GPR 6 now contains 0x8000 4500.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0x00004500;
            uint expected = 0x80004500;

            xenonCPU.subfox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfo()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0x0000 4500.
            // subfo 6,4,10
            // GPR 6 now contains 0x8000 4500.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0x00004500;
            uint expected = 0x80004500;

            xenonCPU.subfo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfo_()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0x0000 7000.
            // subfo. 6,4,10
            // GPR 6 now contains 0x8000 7000.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0x00007000;
            uint expected = 0x80007000;

            xenonCPU.subfo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfcx()
        {
            // Assume GPR 4 contains 0x8000 7000.
            // Assume GPR 10 contains 0x9000 3000.
            // subfc 6,4,10
            // GPR 6 now contains 0x0FFF C000.

            xenonCPU.rA = 0x80007000;
            xenonCPU.rB = 0x90003000;
            uint expected = 0x0FFFC000;

            xenonCPU.subfcx();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfc()
        {
            // Assume GPR 4 contains 0x8000 7000.
            // Assume GPR 10 contains 0x9000 3000.
            // subfc 6,4,10
            // GPR 6 now contains 0x0FFF C000.

            xenonCPU.rA = 0x80007000;
            xenonCPU.rB = 0x90003000;
            uint expected = 0x0FFFC000;

            xenonCPU.subfc();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfc_()
        {
            // Assume GPR 4 contains 0x0000 4500.
            // Assume GPR 10 contains 0x8000 7000.
            // subfc. 6,4,10
            // GPR 6 now contains 0x8000 2B00.

            xenonCPU.rA = 0x00004500;
            xenonCPU.rB = 0x80007000;
            uint expected = 0x80002B00;

            xenonCPU.subfc_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfcox()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0x0000 4500.
            // subfco 6,4,10
            // GPR 6 now contains 0x8000 4500.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0x00004500;
            uint expected = 0x80004500;

            xenonCPU.subfcox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfco()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0x0000 4500.
            // subfco 6,4,10
            // GPR 6 now contains 0x8000 4500.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0x00004500;
            uint expected = 0x80004500;

            xenonCPU.subfco();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfco_()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0x0000 7000.
            // subfco. 6,4,10
            // GPR 6 now contains 0x8000 7000.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0x00007000;
            uint expected = 0x80007000;

            xenonCPU.subfco_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfex()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 10 contains 0x8000 7000.
            // Assume the Carry bit is one.
            // subfe 6,4,10
            // GPR 6 now contains 0xF000 4000.

            xenonCPU.rA = 0x90003000;
            xenonCPU.rB = 0x80007000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0xF0004000;

            xenonCPU.subfex();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfe()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 10 contains 0x8000 7000.
            // Assume the Carry bit is one.
            // subfe 6,4,10
            // GPR 6 now contains 0xF000 4000.

            xenonCPU.rA = 0x90003000;
            xenonCPU.rB = 0x80007000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0xF0004000;

            xenonCPU.subfe();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfe_()
        {
            // Assume GPR 4 contains 0x0000 4500.
            // Assume GPR 10 contains 0x8000 7000.
            // Assume the Carry bit is zero.
            // subfe. 6,4,10
            // GPR 6 now contains 0x8000 2AFF.

            xenonCPU.rA = 0x00004500;
            xenonCPU.rB = 0x80007000;
            xenonCPU.xer = 0x0; // Assume the Carry bit is zero.
            uint expected = 0x80002AFF;

            xenonCPU.subfe_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfeox()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0xEFFF FFFF.
            // Assume the Carry bit is one.
            // subfeo 6,4,10
            // GPR 6 now contains 0x6FFF FFFF.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0xEFFFFFFF;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x6FFFFFFF;

            xenonCPU.subfeox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfeo()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0xEFFF FFFF.
            // Assume the Carry bit is one.
            // subfeo 6,4,10
            // GPR 6 now contains 0x6FFF FFFF.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0xEFFFFFFF;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is one.
            uint expected = 0x6FFFFFFF;

            xenonCPU.subfeo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfeo_()
        {
            // Assume GPR 4 contains 0x8000 0000.
            // Assume GPR 10 contains 0xEFFF FFFF.
            // Assume the Carry bit is zero.
            // subfeo. 6,4,10
            // GPR 6 now contains 0x6FFF FFFE.

            xenonCPU.rA = 0x80000000;
            xenonCPU.rB = 0xEFFFFFFF;
            xenonCPU.xer = 0x0;     // Assume the Carry bit is zero. 
            uint expected = 0x6FFFFFFE;

            xenonCPU.subfeo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfic()
        {
            // Assume GPR 4 holds 0x9000 3000.
            // subfic 6,4,0x00007000
            // GPR 6 now holds 0x7000 4000.

            xenonCPU.rA = 0x90003000;
            xenonCPU.imm = 0x00007000;
            uint expected = 0x70004000;

            xenonCPU.subfic();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfmex()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume the Carry bit is set to one.
            // subfme 6,4
            // GPR 6 now contains 0x6FFF CFFF.

            xenonCPU.rA = 0x90003000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is set to one.
            uint expected = 0x6FFFCFFF;

            xenonCPU.subfmex();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfme()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume the Carry bit is set to one.
            // subfme 6,4
            // GPR 6 now contains 0x6FFF CFFF.

            xenonCPU.rA = 0x90003000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is set to one.
            uint expected = 0x6FFFCFFF;

            xenonCPU.subfme();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfme_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume the Carry bit is set to zero.
            // subfme. 6,4
            // GPR 6 now contains 0x4FFB CFFE.

            xenonCPU.rA = 0xB0043000;
            xenonCPU.xer = 0x0; // Assume the Carry bit is set to zero.
            uint expected = 0x4FFBCFFE;

            xenonCPU.subfme_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfmeox()
        {
            // Assume GPR 4 contains 0xEFFF FFFF.
            // Assume the Carry bit is set to one.
            // subfmeo 6,4
            // GPR 6 now contains 0x1000 0000.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is set to one.
            uint expected = 0x10000000;

            xenonCPU.subfmeox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfmeo()
        {
            // Assume GPR 4 contains 0xEFFF FFFF.
            // Assume the Carry bit is set to one.
            // subfmeo 6,4
            // GPR 6 now contains 0x1000 0000.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is set to one.
            uint expected = 0x10000000;

            xenonCPU.subfmeo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfmeo_()
        {
            // Assume GPR 4 contains 0xEFFF FFFF.
            // Assume the Carry bit is set to zero.
            // subfmeo. 6,4
            // GPR 6 now contains 0x0FFF FFFF.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.xer = 0x0; // Assume the Carry bit is set to zero.
            uint expected = 0x0FFFFFFF;

            xenonCPU.subfmeo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfzex()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume the Carry bit is set to one.
            // subfze 6,4
            // GPR 6 now contains 0x6FFF D000.

            xenonCPU.rA = 0x90003000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is set to one.
            uint expected = 0x6FFFD000;

            xenonCPU.subfzex();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfze()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume the Carry bit is set to one.
            // subfze 6,4
            // GPR 6 now contains 0x6FFF D000.

            xenonCPU.rA = 0x90003000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is set to one.
            uint expected = 0x6FFFD000;

            xenonCPU.subfze();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfze_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume the Carry bit is set to one.
            // subfze. 6,4
            // GPR 6 now contains 0x4FFB D000.

            xenonCPU.rA = 0xB0043000;
            xenonCPU.xer = 0x20000000;  // Assume the Carry bit is set to one.
            uint expected = 0x4FFBD000;

            xenonCPU.subfze_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfzeox()
        {
            // Assume GPR 4 contains 0xEFFF FFFF.
            // Assume the Carry bit is set to zero.
            // subfzeo 6,4
            // GPR 6 now contains 0x1000 0000.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.xer = 0; // Assume the Carry bit is set to zero.
            uint expected = 0x10000000;

            xenonCPU.subfzeox();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfzeo()
        {
            // Assume GPR 4 contains 0xEFFF FFFF.
            // Assume the Carry bit is set to zero.
            // subfzeo 6,4
            // GPR 6 now contains 0x1000 0000.

            xenonCPU.rA = 0xEFFFFFFF;
            xenonCPU.xer = 0; // Assume the Carry bit is set to zero.
            uint expected = 0x10000000;

            xenonCPU.subfzeo();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void subfzeo_()
        {
            // Assume GPR 4 contains 0x70FB 6500.
            // Assume the Carry bit is set to zero.
            // subfzeo 6,4
            // GPR 6 now contains 0x8F04 9AFF.

            xenonCPU.rA = 0x70FB6500;
            xenonCPU.xer = 0x0; // Assume the Carry bit is set to zero.
            uint expected = 0x8F049AFF;

            xenonCPU.subfzeo_();
            Assert.AreEqual(expected, xenonCPU.rD);
        }

        [TestMethod]
        public void xorx()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // xor 6,4,7
            // GPR 6 now contains 0xE89A 489B.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xE89A489B;

            xenonCPU.xorx();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void xor()
        {
            // Assume GPR 4 contains 0x9000 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // xor 6,4,7
            // GPR 6 now contains 0xE89A 489B.

            xenonCPU.rS = 0x90003000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xE89A489B;

            xenonCPU.xor();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void xor_()
        {
            // Assume GPR 4 contains 0xB004 3000.
            // Assume GPR 7 contains 0x789A 789B.
            // xor. 6,4,7
            // GPR 6 now contains 0xC89E 489B.

            xenonCPU.rS = 0xB0043000;
            xenonCPU.rB = 0x789A789B;
            uint expected = 0xC89E489B;

            xenonCPU.xor_();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void xori()
        {
            // Assume GPR 4 contains 0x7B41 92C0.
            // xori 6,4,0x5730
            // GPR 6 now contains 0x7B41 C5F0.

            xenonCPU.rS = 0x7B4192C0;
            xenonCPU.imm = 0x5730;
            uint expected = 0x7B41C5F0;

            xenonCPU.xori();
            Assert.AreEqual(expected, xenonCPU.rA);
        }

        [TestMethod]
        public void xoris()
        {
            // Assume GPR 4 holds 0x9000 3000.
            // xoris 6,4,0x0079
            // GPR 6 now holds 0x9079 3000.

            xenonCPU.rS = 0x90003000;
            xenonCPU.imm = 0x0079;
            uint expected = 0x90793000;

            xenonCPU.xoris();
            Assert.AreEqual(expected, xenonCPU.rA);
        }
    }
}
