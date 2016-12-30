using System;

namespace XenonPowerPCInstructionInterpreter.CPU
{
    public class XenonPowerPC
    {
        /// <summary>
        /// Condition Register (CR) - Conditions of integer arithmetic operations
        /// </summary>
        public int cr { get; set; }

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
        /// Registers for now, might want to make a better way of handling registers in the future but this will do for now
        /// </summary>
        public uint rA { get; set; }
        public uint rB { get; set; }
        public uint rD { get; set; }

        /// <summary>
        /// xer bits:
        /// 0 so
        /// 1 ov
        /// 2 carry
        /// 3-24 res
        /// 25-31 number of bytes for lswx/stswx
        /// </summary>
        public const int XER_SO = (1 << 31);
        public const int XER_OV = (1 << 30);
        public const int XER_CA = (1 << 29);

        /// <summary>
        /// cr0 bits: .68
        /// lt
        /// gt
        /// eq
        /// so
        /// </summary>
        public const int CR_CR0_LT = (1 << 31);
        public const int CR_CR0_GT = (1 << 30);
        public const int CR_CR0_EQ = (1 << 29);
        public const int CR_CR0_SO = (1 << 28);

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
                cr |= CR_CR0_LT;
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
        /// 
        /// </summary>
        public bool PPC_OPC_Rc
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Mask
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="me"></param>
        /// <returns></returns>
        public uint Mask(int mb, int me)
        {
            uint mask;
            if (mb <= me)
            {
                if (me - mb == 31)
                {
                    mask = 0xffffffff;
                }
                else
                {
                    mask = (uint)((1 << (me - mb + 1)) - 1) << (31 - me);
                }
            }
            else
            {
                mask = WordRotl((uint)(1 << (32 - mb + me + 1)) - 1, 31 - me);
            }

            return mask;
        }

        /// <summary>
        /// Rotate Word Left
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="n">Shift</param>
        /// <returns>Rotated value</returns>
        private uint WordRotl(uint data, int n)
        {
            n &= 0x1f;
            return (data << n) | (data >> (32 - n));
        }
    }
}
