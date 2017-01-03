using System;

namespace XenonPowerPCInstructionInterpreter.CPU
{
    public class XenonPowerPC
    {
        /// <summary>
        /// Condition Register (CR) - Conditions of integer arithmetic operations
        /// </summary>
        public uint cr { get; set; }

        /// <summary>
        /// Integer Exception Register (XER): - Overflow and carry bits
        /// </summary>
        public int xer { get; set; }
        public int xer_ca { get; set; }

        /// <summary>
        /// Update Condition Register flag
        /// </summary>
        public bool updatecr { get; set; }

        /// <summary>
        /// Update XER flag
        /// </summary>
        public bool updateoe { get; set; }

        /// <summary>
        /// Registers for now, might want to make a better way of handling registers in the future but this will do for now
        /// </summary>
        public uint rA { get; set; }
        public uint rB { get; set; }
        public uint rD { get; set; }
        public uint rS { get; set; }
        public uint imm { get; set; }
        public uint SH { get; set; }
        public uint MB { get; set; }
        public uint ME { get; set; }

        /// <summary>
        /// xer bits:
        /// 0 so
        /// 1 ov
        /// 2 carry
        /// 3-24 res
        /// 25-31 number of bytes for lswx/stswx
        /// </summary>
        public int XER_SO = (1 << 31);
        public int XER_OV = (1 << 30);
        public int XER_CA = (1 << 29);
        
        /// <summary>
        /// cr0 bits: .68
        /// lt
        /// gt
        /// eq
        /// so
        /// </summary>
        public int CR_CR0_LT = (1 << 31);
        public uint CR_CR0_GT = (1 << 30);
        public uint CR_CR0_EQ = (1 << 29);
        public uint CR_CR0_SO = (1 << 28);


        public int PPC_OPC_OE = (1 << 10);
        public int PPC_OPC_Rc = 1;

        /// <summary>
        /// Initializes the Xenon Power PC CPU
        /// </summary>
        public XenonPowerPC()
        {
        }


        /// <summary>
        /// cr: .67
        /// 0- 3 cr0
        /// 4- 7 cr1
        /// 8-11 cr2
        /// 12-15 cr3
        /// 16-19 cr4
        /// 20-23 cr5
        /// 24-27 cr6
        /// 28-31 cr7
        /// </summary>
        public int CR_CR0(int v)
        {
            return ((v) >> 28);
        }

        public int CR_CR1(int v)
        {
            return (((v) >> 24) & 0xf);
        }

        public int CR_CRx(int v, int x)
        {
            return (((v) >> (4 * (7 - (x)))) & 0xf);
        }

        /// <summary>
        /// Update cr0
        /// </summary>
        /// <param name="r"></param>
        public void UpdateCR0(uint r)
        {
            cr &= 0x0fffffff;

            if (!Convert.ToBoolean(r))
            {
                cr |= CR_CR0_EQ;
            }
            else if (Convert.ToBoolean(r & 0x80000000))
            {
                cr |= (uint)CR_CR0_LT;
            }
            else
            {
                cr |= CR_CR0_GT;
            }

            if (Convert.ToBoolean(xer & XER_SO))
            {
                cr |= CR_CR0_SO;
            }

            // Reset UpdateCr.
            updatecr = false;
        }

        public int XER_n(int v)
        {
            return (v) & 0x7f;
        }

        /// <summary>
        /// Rotate Word Left
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="n">Shift</param>
        /// <returns>Rotated value</returns>
        public uint ppc_word_rotl(uint data, int n)
        {
            n &= 0x1f;
            return (data << n) | (data >> (32 - n));
        }

        /// <summary>
        /// Power PC Mask
        /// </summary>
        /// <param name="MB"></param>
        /// <param name="ME"></param>
        /// <returns></returns>
        public uint ppc_mask(int MB, int ME)
        {
            uint mask;
            if (MB <= ME)
            {
                if (ME - MB == 31)
                {
                    mask = 0xffffffff;
                }
                else
                {
                    mask = (uint)((1 << (ME - MB + 1)) - 1) << (31 - ME);
                }
            }
            else
            {
                mask = ppc_word_rotl((uint)(1 << (32 - MB + ME + 1)) - 1, 31 - ME);
            }
            return mask;
        }

        public bool ppc_carry_3(uint a, uint b, uint c)
        {
            if ((a + b) < a)
            {
                return true;
            }
            if ((a + b + c) < c)
            {
                return true;
            }
            return false;
        }
    }
}
