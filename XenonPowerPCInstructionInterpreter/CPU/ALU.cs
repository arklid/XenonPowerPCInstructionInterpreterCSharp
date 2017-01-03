using System;

namespace XenonPowerPCInstructionInterpreter.CPU
{
    public static class ALU
    {
        /// <summary>
        /// addx		Add
        /// </summary>
        public static void addx(this XenonPowerPC cpu)
        {
            cpu.rD = cpu.rA + cpu.rB;

            if (cpu.updatecr)
            {
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void add(this XenonPowerPC cpu) { addx(cpu); }
        public static void add_(this XenonPowerPC cpu) { cpu.updatecr = true; addx(cpu); }

        /// <summary>
        /// addox		Add with Overflow
        /// </summary>
        public static void addox(this XenonPowerPC cpu)
        {
            cpu.rD = cpu.rA + cpu.rB;

            // Experimental update XER flags
            if (cpu.rD < cpu.rA)
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            if (cpu.updatecr)
            {
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void addo(this XenonPowerPC cpu) { addox(cpu); }
        public static void addo_(this XenonPowerPC cpu) { cpu.updatecr = true; addox(cpu); }

        /// <summary>
        /// addcx		Add Carrying
        /// </summary>
        /// <param name="cpu"></param>
        public static void addcx(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            cpu.rD = a + cpu.rB;

            // update xer
            if (cpu.rD < a)
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void addc(this XenonPowerPC cpu) { addcx(cpu); }
        public static void addc_(this XenonPowerPC cpu) { cpu.updatecr = true; addcx(cpu); }

        /// <summary>
        /// addcox		Add Carrying with Overflow
        /// </summary>
        public static void addcox(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            cpu.rD = a + cpu.rB;

            // update xer
            if (cpu.rD < a)
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("addcox unimplemented\n");
            }
        }
        public static void addco(this XenonPowerPC cpu) { addcox(cpu); }
        public static void addco_(this XenonPowerPC cpu) { cpu.updatecr = true; addcox(cpu); }

        /// <summary>
        /// addex		Add Extended
        /// </summary>
        public static void addex(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint b = cpu.rB;
            uint ca = (uint)(Convert.ToBoolean(cpu.xer & cpu.XER_CA) ? 1 : 0);
            cpu.rD = a + b + ca;

            // update xer
            if (cpu.ppc_carry_3(a, b, ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void adde(this XenonPowerPC cpu) { addex(cpu); }
        public static void adde_(this XenonPowerPC cpu) { cpu.updatecr = true; addex(cpu); }

        /// <summary>
        /// addeox		Add Extended with Overflow
        /// </summary>
        public static void addeox(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint b = cpu.rB;
            uint ca = (uint)(Convert.ToBoolean(cpu.xer & cpu.XER_CA) ? 1 : 0);
            cpu.rD = a + b + ca;

            // update xer
            if (cpu.ppc_carry_3(a, b, ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("addeox unimplemented\n");
            }
        }
        public static void addeo(this XenonPowerPC cpu) { addeox(cpu); }
        public static void addeo_(this XenonPowerPC cpu) { cpu.updatecr = true; addeox(cpu); }


        /// <summary>
        /// addi		Add Immediate
        /// </summary>
        public static void addi(this XenonPowerPC cpu)
        {
            cpu.rD = (Convert.ToBoolean(cpu.rA) ? cpu.rA : 0) + cpu.imm;
        }

        /// <summary>
        /// li		Load Immediate
        /// </summary>
        public static void li(this XenonPowerPC cpu)
        {
            // Pseudoinstruction which translates to addi
            // addi rD,0,value
            addi(cpu);
        }

        /// <summary>
        /// la		Load Address
        /// </summary>
        public static void la(this XenonPowerPC cpu)
        {
            // Pseudoinstruction which translates to addi
            // addi rD,rA,disp
            //addi(cpu);
            throw new NotImplementedException();
        }

        /// <summary>
        /// subi		Subtract Immediate
        /// </summary>
        public static void subi(this XenonPowerPC cpu)
        {
            // Pseudoinstruction which translates to addi
            // addi rD,rA,-value
            cpu.imm = (uint)-cpu.imm;
            addi(cpu);
        }


        /// <summary>
        /// addic		Add Immediate Carrying
        /// </summary>
        public static void addic(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            cpu.rD = a + cpu.imm;

            // update XER
            if (cpu.rD < a)
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
        }

        /// <summary>
        /// subic		Subtract Immediate Carrying
        /// </summary>
        public static void subic(this XenonPowerPC cpu)
        {
            // Pseudoinstruction which translates to addic
            // addi rD,rA,-value
            cpu.imm = (uint)-cpu.imm;
            addic(cpu);
        }

        /// <summary>
        /// addic.		Add Immediate Carrying and Record
        /// </summary>
        public static void addic_(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            cpu.rD = a + cpu.imm;

            // update XER
            if (cpu.rD < a)
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
            
            // update cr0 flags
            cpu.UpdateCR0(cpu.rD);
        }

        /// <summary>
        /// subic		Subtract Immediate Carrying and Record
        /// </summary>
        public static void subic_(this XenonPowerPC cpu)
        {
            // Pseudoinstruction which translates to addic_
            // addi rD,rA,-value
            cpu.imm = (uint)-cpu.imm;
            addic_(cpu);
        }

        /// <summary>
        /// addis		Add Immediate Shifted
        /// </summary>
        public static void addis(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rA;
            cpu.imm = cpu.imm << 16;

            cpu.rD = (Convert.ToBoolean(cpu.rA) ? cpu.rA : 0) + cpu.imm;
        }

        /// <summary>
        /// subis		Subtract Immediate Shifted 
        /// </summary>
        public static void subis(this XenonPowerPC cpu)
        {
            // Pseudoinstruction which translates to addis
            // addi rD,rA,-value
            cpu.imm = (uint)-cpu.imm;
            addis(cpu);
        }

        /// <summary>
        /// lis		Load Immediate Shifted 
        /// </summary>
        public static void lis(this XenonPowerPC cpu)
        {
            // Pseudoinstruction which translates to addis
            // addi rD,0,value
            cpu.rA = 0;
            addis(cpu);
        }

        /// <summary>
        /// addmex		Add to Minus One Extended
        /// </summary>
        public static void addmex(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint ca = (uint)(Convert.ToBoolean(cpu.xer & cpu.XER_CA) ? 1 : 0);
            cpu.rD = (a + ca + 0xffffffff);
            if (Convert.ToBoolean(a) || Convert.ToBoolean(ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void addme(this XenonPowerPC cpu) { addmex(cpu); }
        public static void addme_(this XenonPowerPC cpu) { cpu.updatecr = true; addmex(cpu); }


        /// <summary>
        /// addmeox		Add to Minus One Extended with Overflow
        /// </summary>
        public static void addmeox(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (a + ca + 0xffffffff);
            if (Convert.ToBoolean(a) || Convert.ToBoolean(ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
            
            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("addmeox unimplemented\n");
            }
        }
        public static void addmeo(this XenonPowerPC cpu) { addmeox(cpu); }
        public static void addmeo_(this XenonPowerPC cpu) { cpu.updatecr = true; addmeox(cpu); }


        /// <summary>
        /// addzex		Add to Zero Extended
        /// </summary>
        public static void addzex(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (a + ca);
            if ((a == 0xffffffff) && Convert.ToBoolean(ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
            // update xer
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void addze(this XenonPowerPC cpu) { addzex(cpu); }
        public static void addze_(this XenonPowerPC cpu) { cpu.updatecr = true; addzex(cpu); }


        /// <summary>
        /// addzeox		Add to Zero Extended with Overflow
        /// </summary>
        public static void addzeox(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (a + ca);
            if ((a == 0xffffffff) && Convert.ToBoolean(ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
            // update xer
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("addzeox unimplemented\n");
            }
        }
        public static void addzeo(this XenonPowerPC cpu) { addzeox(cpu); }
        public static void addzeo_(this XenonPowerPC cpu) { cpu.updatecr = true; addzeox(cpu); }


        /// <summary>
        /// andx		AND
        /// </summary>
        public static void andx(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS & cpu.rB;

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void and(this XenonPowerPC cpu) { andx(cpu); }
        public static void and_(this XenonPowerPC cpu) { cpu.updatecr = true; andx(cpu); }


        /// <summary>
        /// andcx		AND with Complement
        /// </summary>
        public static void andcx(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS & ~cpu.rB;

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void andc(this XenonPowerPC cpu) { andcx(cpu); }
        public static void andc_(this XenonPowerPC cpu) { cpu.updatecr = true; andcx(cpu); }


        /// <summary>
        /// andi.		AND Immediate
        /// </summary>
        public static void andi_(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS & cpu.imm;

            // update cr0 flags
            cpu.UpdateCR0(cpu.rA);
        }

        /// <summary>
        /// andis.		AND Immediate Shifted
        /// </summary>
        public static void andis_(this XenonPowerPC cpu)
        {
            cpu.imm = cpu.imm << 16;
            cpu.rA = cpu.rS & cpu.imm;
            // update cr0 flags
            cpu.UpdateCR0(cpu.rA);
        }

        static uint[] ppc_cmp_and_mask = new uint[8] {
            0xfffffff0,
            0xffffff0f,
            0xfffff0ff,
            0xffff0fff,
            0xfff0ffff,
            0xff0fffff,
            0xf0ffffff,
            0x0fffffff,
        };

        /// <summary>
        /// cmp		Compare
        /// </summary>
        public static void cmp(this XenonPowerPC cpu)
        {
            cpu.cr >>= 2;
            int a = (int)cpu.rA;
            int b = (int)cpu.rB;
            int c;
            if (a < b)
            {
                c = 8;
            }
            else if (a > b)
            {
                c = 4;
            }
            else
            {
                c = 2;
            }

            if (Convert.ToBoolean(cpu.xer & cpu.XER_SO))
            {
                c |= 1;
            }

            cpu.cr = 7 - cpu.cr;
            cpu.cr &= ppc_cmp_and_mask[cpu.cr];
            cpu.cr |= (uint)(c << (int)cpu.cr * 4);
        }

        /// <summary>
        /// cmpd		Compare
        /// </summary>
        public static void cmpd(this XenonPowerPC cpu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// cmpw		Compare
        /// </summary>
        public static void cmpw(this XenonPowerPC cpu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// cmpi		Compare Immediate
        /// </summary>
        public static void cmpi(this XenonPowerPC cpu)
        {
            cpu.cr >>= 2;
            int a = (int)cpu.rA;
            int b = (int)cpu.imm;
            uint c;

            if (a < b)
            {
                c = 8;
            }
            else if (a > b)
            {
                c = 4;
            }
            else
            {
                c = 2;
            }

            if (Convert.ToBoolean(cpu.xer & cpu.XER_SO))
            {
                c |= 1;
            }

            cpu.cr = 7 - cpu.cr;
            cpu.cr &= ppc_cmp_and_mask[cpu.cr];
            cpu.cr |= c << ((int)cpu.cr * 4);
        }

        /// <summary>
        /// cmpdi		Compare Immediate
        /// </summary>
        public static void cmpdi(this XenonPowerPC cpu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// cmpwi		Compare Immediate
        /// </summary>
        public static void cmpwi(this XenonPowerPC cpu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// cmpl		Compare Logical
        /// </summary>
        public static void cmpl(this XenonPowerPC cpu)
        {
            cpu.cr >>= 2;
            uint a = cpu.rA;
            uint b = cpu.rB;
            uint c;
            if (a < b)
            {
                c = 8;
            }
            else if (a > b)
            {
                c = 4;
            }
            else
            {
                c = 2;
            }

            if (Convert.ToBoolean(Convert.ToBoolean(cpu.xer & cpu.XER_SO)))
            {
                c |= 1;
            }

            cpu.cr = 7 - cpu.cr;
            cpu.cr &= ppc_cmp_and_mask[cpu.cr];
            cpu.cr |= c << ((int)cpu.cr * 4);
        }

        /// <summary>
        /// cmpld		Compare Logical
        /// </summary>
        public static void cmpld(this XenonPowerPC cpu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// cmplw		Compare Logical
        /// </summary>
        public static void cmplw(this XenonPowerPC cpu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// cmpli		Compare Logical Immediate
        /// </summary>
        public static void cmpli(this XenonPowerPC cpu)
        {
            cpu.cr >>= 2;
            uint a = (uint)cpu.rA;
            uint b = (uint)cpu.imm;
            uint c;
            if (a < b)
            {
                c = 8;
            }
            else if (a > b)
            {
                c = 4;
            }
            else
            {
                c = 2;
            }

            if (Convert.ToBoolean(cpu.xer & cpu.XER_SO))
            {
                c |= 1;
            }

            cpu.cr = 7 - cpu.cr;
            cpu.cr &= ppc_cmp_and_mask[cpu.cr];
            cpu.cr |= c << ((int)cpu.cr * 4);
        }

        /// <summary>
        /// cntlzwx		Count Leading Zeros Word
        /// </summary>
        public static void cntlzwx(this XenonPowerPC cpu)
        {
            uint n = 0;
            uint x = 0x80000000;
            uint v = cpu.rS;
            while (!Convert.ToBoolean(v & x))
            {
                n++;
                if (n == 32) break;
                x >>= 1;
            }
            cpu.rA = n;
            
            
            if(cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void cntlzw(this XenonPowerPC cpu) { cntlzwx(cpu); }
        public static void cntlzw_(this XenonPowerPC cpu) { cpu.updatecr = true; cntlzwx(cpu); }


        /// <summary>
        /// crand		Condition Register AND
        /// </summary>
        public static void crand(this XenonPowerPC cpu)
        {
            if (Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rA))) && Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rB))))
            {
                cpu.cr |= (uint)(1 << (31 - (int)cpu.rD));
            }
            else
            {
                cpu.cr &= (uint)~(1 << (31 - (int)cpu.rD));
            }
        }

        /// <summary>
        /// crandc		Condition Register AND with Complement
        /// </summary>
        public static void crandc(this XenonPowerPC cpu)
        {
            if (Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rA))) && !(Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rB)))))
            {
                cpu.cr |= (uint)(1 << (31 - (int)cpu.rD));
            }
            else
            {
                cpu.cr &= (uint)~(1 << (31 - (int)cpu.rD));
            }
        }

        /// <summary>
        /// creqv		Condition Register Equivalent
        /// </summary>
        public static void creqv(this XenonPowerPC cpu)
        {
            if (Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rA))) && (Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rB))))
              || (!(Convert.ToBoolean((cpu.cr & (1 << (31 - (int)cpu.rA)))) && !(Convert.ToBoolean((cpu.cr & (1 << (31 - (int)cpu.rB))))))))
            {
                cpu.cr |= (uint)(1 << (31 - (int)cpu.rD));
            }
            else
            {
                cpu.cr &= (uint)~(1 << (31 - (int)cpu.rD));
            }
        }

        /// <summary>
        /// crnand		Condition Register NAND
        /// </summary>
        public static void crnand(this XenonPowerPC cpu)
        {
            if (!(Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rA)))) && Convert.ToBoolean((cpu.cr & (1 << (31 - (int)cpu.rB)))))
            {
                cpu.cr |= (uint)(1 << (31 - (int)cpu.rD));
            }
            else
            {
                cpu.cr &= (uint)~(1 << (31 - (int)cpu.rD));
            }
        }

        /// <summary>
        /// crnor		Condition Register NOR
        /// </summary>
        public static void crnor(this XenonPowerPC cpu)
        {
            uint t = (uint)((1 << (31 - (int)cpu.rA)) | (1 << (31 - (int)cpu.rB)));
            if (!Convert.ToBoolean((cpu.cr & t)))
            {
                cpu.cr |= (uint)(1 << (31 - (int)cpu.rD));
            }
            else
            {
                cpu.cr &= (uint)~(1 << (31 - (int)cpu.rD));
            }
        }

        /// <summary>
        /// cror		Condition Register OR
        /// </summary>
        public static void cror(this XenonPowerPC cpu)
        {
            uint t = (uint)((1 << (31 - (int)cpu.rA)) | (1 << (31 - (int)cpu.rB)));
            if (Convert.ToBoolean(cpu.cr & t))
            {
                cpu.cr |= (uint)(1 << (31 - (int)cpu.rD));
            }
            else
            {
                cpu.cr &= (uint)~(1 << (31 - (int)cpu.rD));
            }
        }

        /// <summary>
        /// crorc		Condition Register OR with Complement
        /// </summary>
        public static void crorc(this XenonPowerPC cpu)
        {
            if (Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rA))) || !Convert.ToBoolean((cpu.cr & (1 << (31 - (int)cpu.rB)))))
            {
                cpu.cr |= (uint)(1 << (31 - (int)cpu.rD));
            }
            else
            {
                cpu.cr &= (uint)~(1 << (31 - (int)cpu.rD));
            }
        }

        /// <summary>
        /// crxor		Condition Register XOR
        /// </summary>
        public static void crxor(this XenonPowerPC cpu)
        {
            if ((!Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rA))) && Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rB))))
              || Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rA))) && !Convert.ToBoolean(cpu.cr & (1 << (31 - (int)cpu.rB))))
            {
                cpu.cr |= (uint)(1 << (31 - (int)cpu.rD));
            }
            else
            {
                cpu.cr &= (uint)~(1 << (31 - (int)cpu.rD));
            }
        }

        /// <summary>
        /// divwx		Divide Word
        /// </summary>
        public static void divwx(this XenonPowerPC cpu)
        {
            if (!Convert.ToBoolean(cpu.rB))
            {
                throw new NotImplementedException("division by zero\n");
            }

            int a = (int)cpu.rA;
            int b = (int)cpu.rB;
            cpu.rD = (uint)(a / b);

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void divw(this XenonPowerPC cpu) { divwx(cpu); }
        public static void divw_(this XenonPowerPC cpu) { cpu.updatecr = true; divwx(cpu); }


        /// <summary>
        /// divwox		Divide Word with Overflow
        /// </summary>
        public static void divwox(this XenonPowerPC cpu)
        {
            if (!Convert.ToBoolean(cpu.rB))
            {
                throw new NotImplementedException("division by zero\n");
            }

            int a = (int)cpu.rA;
            int b = (int)cpu.rB;
            cpu.rD = (uint)(a / b);

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("divwox unimplemented\n");
            }
        }
        public static void divwo(this XenonPowerPC cpu) { divwox(cpu); }
        public static void divwo_(this XenonPowerPC cpu) { cpu.updatecr = true; divwox(cpu); }


        /// <summary>
        /// divwux		Divide Word Unsigned
        /// </summary>
        public static void divwux(this XenonPowerPC cpu)
        {
            if (!Convert.ToBoolean(cpu.rB))
            {
                throw new NotImplementedException("division by zero\n");
            }
            cpu.rD = cpu.rA / cpu.rB;

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void divwu(this XenonPowerPC cpu) { divwux(cpu); }
        public static void divwu_(this XenonPowerPC cpu) { cpu.updatecr = true; divwux(cpu); }


        /// <summary>
        /// divwuox		Divide Word Unsigned with Overflow
        /// </summary>
        public static void divwuox(this XenonPowerPC cpu)
        {
            if (!Convert.ToBoolean(cpu.rB))
            {
                throw new NotImplementedException("division by zero\n");
            }

            cpu.rD = cpu.rA / cpu.rB;

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("divwuox unimplemented\n");
            }
        }
        public static void divwuo(this XenonPowerPC cpu) { divwuox(cpu); }
        public static void divwuo_(this XenonPowerPC cpu) { cpu.updatecr = true; divwuox(cpu); }


        /// <summary>
        /// eqvx		Equivalent
        /// </summary>
        public static void eqvx(this XenonPowerPC cpu)
        {
            cpu.rA = ~(cpu.rS ^ cpu.rB);
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void eqv(this XenonPowerPC cpu) { eqvx(cpu); }
        public static void eqv_(this XenonPowerPC cpu) { cpu.updatecr = true; eqvx(cpu); }


        /// <summary>
        /// extsbx		Extend Sign Byte
        /// </summary>
        public static void extsbx(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS;
            if (Convert.ToBoolean(cpu.rA & 0x80))
            {
                cpu.rA |= unchecked(0xffffff00);
            }
            else
            {
                cpu.rA &= ~0xffffff00;
            }

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void extsb(this XenonPowerPC cpu) { extsbx(cpu); }
        public static void extsb_(this XenonPowerPC cpu) { cpu.updatecr = true; extsbx(cpu); }


        /// <summary>
        /// extshx		Extend Sign Half Word
        /// </summary>
        public static void extshx(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS;
            if (Convert.ToBoolean(cpu.rA & 0x8000))
            {
                cpu.rA |= unchecked(0xffff0000);
            }
            else
            {
                cpu.rA &= ~0xffff0000;
            }

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void extsh(this XenonPowerPC cpu) { extshx(cpu); }
        public static void extsh_(this XenonPowerPC cpu) { cpu.updatecr = true; extshx(cpu); }


        /// <summary>
        /// mulhwx		Multiply High Word
        /// </summary>
        public static void mulhwx(this XenonPowerPC cpu)
        {
            long a = (int)cpu.rA;
            long b = (int)cpu.rB;
            long c = a * b;
            //cpu.rD = (uint)((ulong)c) >> 32;
            cpu.rD = (uint)(c >> 32);

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void mulhw(this XenonPowerPC cpu) { mulhwx(cpu); }
        public static void mulhw_(this XenonPowerPC cpu) { cpu.updatecr = true; mulhwx(cpu); }


        /// <summary>
        /// mulhwux		Multiply High Word Unsigned
        /// </summary>
        public static void mulhwux(this XenonPowerPC cpu)
        {
            ulong a = cpu.rA;
            ulong b = cpu.rB;
            ulong c = a * b;
            cpu.rD = (uint)(c >> 32);

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void mulhwu(this XenonPowerPC cpu) { mulhwux(cpu); }
        public static void mulhwu_(this XenonPowerPC cpu) { cpu.updatecr = true; mulhwux(cpu); }


        /// <summary>
        /// mulli		Multiply Low Immediate
        /// </summary>
        public static void mulli(this XenonPowerPC cpu)
        {
            cpu.rD = cpu.rA * cpu.imm;
        }

        /// <summary>
        /// mullwx		Multiply Low Word
        /// </summary>
        public static void mullwx(this XenonPowerPC cpu)
        {
            cpu.rD = cpu.rA * cpu.rB;

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("mullwx unimplemented\n");
            }
        }
        public static void mullw(this XenonPowerPC cpu) { mullwx(cpu); }
        public static void mullw_(this XenonPowerPC cpu) { cpu.updatecr = true; mullwx(cpu); }


        /// <summary>
        /// nandx		NAND
        /// </summary>
        public static void nandx(this XenonPowerPC cpu)
        {
            cpu.rA = ~(cpu.rS & cpu.rB);
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void nand(this XenonPowerPC cpu) { nandx(cpu); }
        public static void nand_(this XenonPowerPC cpu) { cpu.updatecr = true; nandx(cpu); }


        /// <summary>
        /// negx		Negate
        /// </summary>
        public static void negx(this XenonPowerPC cpu)
        {
            cpu.rD = (uint)-cpu.rA;
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void neg(this XenonPowerPC cpu) { negx(cpu); }
        public static void neg_(this XenonPowerPC cpu) { cpu.updatecr = true; negx(cpu); }


        /// <summary>
        /// negox		Negate with Overflow
        /// </summary>
        public static void negox(this XenonPowerPC cpu)
        {
            cpu.rD = (uint)-cpu.rA;
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("negox unimplemented\n");
            }
        }
        public static void nego(this XenonPowerPC cpu) { negox(cpu); }
        public static void nego_(this XenonPowerPC cpu) { cpu.updatecr = true; negox(cpu); }


        /// <summary>
        /// norx		NOR
        /// </summary>
        public static void norx(this XenonPowerPC cpu)
        {
            cpu.rA = ~(cpu.rS | cpu.rB);
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void nor(this XenonPowerPC cpu) { norx(cpu); }
        public static void nor_(this XenonPowerPC cpu) { cpu.updatecr = true; norx(cpu); }


        /// <summary>
        /// orx		OR
        /// </summary>
        public static void orx(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS | cpu.rB;
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void or(this XenonPowerPC cpu) { orx(cpu); }
        public static void or_(this XenonPowerPC cpu) { cpu.updatecr = true; orx(cpu); }


        /// <summary>
        /// orcx		OR with Complement
        /// </summary>
        public static void orcx(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS | ~cpu.rB;
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void orc(this XenonPowerPC cpu) { orcx(cpu); }
        public static void orc_(this XenonPowerPC cpu) { cpu.updatecr = true; orcx(cpu); }


        /// <summary>
        /// ori		OR Immediate
        /// </summary>
        public static void ori(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS | cpu.imm;
        }

        /// <summary>
        /// nop		No Operation
        /// </summary>
        public static void nop(this XenonPowerPC cpu)
        {
        }

        /// <summary>
        /// oris		OR Immediate Shifted
        /// </summary>
        public static void oris(this XenonPowerPC cpu)
        {
            cpu.imm = cpu.imm << 16;
            cpu.rA = cpu.rS | cpu.imm;
        }

        /// <summary>
        /// rlwimix		Rotate Left Word Immediate then Mask Insert
        /// </summary>
        public static void rlwimix(this XenonPowerPC cpu)
        {
            uint v = cpu.ppc_word_rotl(cpu.rS, (int)cpu.SH);
            uint mask = cpu.ppc_mask((int)cpu.MB, (int)cpu.ME);
            cpu.rA = (v & mask) | (cpu.rA & ~mask);

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void rlwimi(this XenonPowerPC cpu) { rlwimix(cpu); }
        public static void rlwimi_(this XenonPowerPC cpu) { cpu.updatecr = true; rlwimix(cpu); }


        /// <summary>
        /// rlwinmx		Rotate Left Word Immediate then AND with Mask
        /// </summary>
        public static void rlwinmx(this XenonPowerPC cpu)
        {
            uint v = cpu.ppc_word_rotl((uint)cpu.rS, (int)cpu.SH);
            uint mask = cpu.ppc_mask((int)cpu.MB, (int)cpu.ME);
            cpu.rA = (v & mask);
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void rlwinm(this XenonPowerPC cpu) { rlwinmx(cpu); }
        public static void rlwinm_(this XenonPowerPC cpu) { cpu.updatecr = true; rlwinmx(cpu); }


        /// <summary>
        /// rlwnmx		Rotate Left Word then AND with Mask
        /// </summary>
        public static void rlwnmx(this XenonPowerPC cpu)
        {
            uint v = cpu.ppc_word_rotl(cpu.rS, (int)cpu.rB);
            uint mask = cpu.ppc_mask((int)cpu.MB, (int)cpu.ME);
            cpu.rA = (v & mask);
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void rlwnm(this XenonPowerPC cpu) { rlwnmx(cpu); }
        public static void rlwnm_(this XenonPowerPC cpu) { cpu.updatecr = true; rlwnmx(cpu); }


        /// <summary>
        /// slwx		Shift Left Word
        /// </summary>
        public static void slwx(this XenonPowerPC cpu)
        {
            uint s = (uint)cpu.rB & 0x3f;
            if (s > 31)
            {
                cpu.rA = 0;
            }
            else
            {
                cpu.rA = (uint)(cpu.rS << (int)s);
            }
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void slw(this XenonPowerPC cpu) { slwx(cpu); }
        public static void slw_(this XenonPowerPC cpu) { cpu.updatecr = true; slwx(cpu); }


        /// <summary>
        /// srawx		Shift Right Algebraic Word
        /// </summary>
        public static void srawx(this XenonPowerPC cpu)
        {
            uint SH = (uint)(cpu.rB & 0x3f);
            cpu.rA = cpu.rS;
            cpu.xer &= ~cpu.XER_CA;
            if (Convert.ToBoolean(cpu.rA & 0x80000000))
            {
                uint ca = 0;
                for (uint i = 0; i < SH; i++)
                {
                    if (Convert.ToBoolean(cpu.rA & 1)) ca = 1;
                    cpu.rA >>= 1;
                    cpu.rA |= unchecked(0x80000000);
                }
                if (Convert.ToBoolean(ca)) cpu.xer |= cpu.XER_CA;
            }
            else
            {
                if (SH > 31)
                {
                    cpu.rA = 0;
                }
                else
                {
                    cpu.rA >>= (int)SH;
                }
            }

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void sraw(this XenonPowerPC cpu) { srawx(cpu); }
        public static void sraw_(this XenonPowerPC cpu) { cpu.updatecr = true; srawx(cpu); }


        /// <summary>
        /// srawix		Shift Right Algebraic Word Immediate
        /// </summary>
        public static void srawix(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS;
            cpu.xer &= ~cpu.XER_CA;
            if (Convert.ToBoolean(cpu.rA & 0x80000000))
            {
                uint ca = 0;
                for (uint i = 0; i < cpu.SH; i++)
                {
                    if (Convert.ToBoolean(cpu.rA & 1)) ca = 1;
                    cpu.rA >>= 1;
                    cpu.rA |= unchecked(0x80000000);
                }
                if (Convert.ToBoolean(ca)) cpu.xer |= cpu.XER_CA;
            }
            else
            {
                if (cpu.SH > 31)
                {
                    cpu.rA = 0;
                }
                else
                {
                    cpu.rA >>= (int)cpu.SH;
                }
            }

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void srawi(this XenonPowerPC cpu) { srawix(cpu); }
        public static void srawi_(this XenonPowerPC cpu) { cpu.updatecr = true; srawix(cpu); }


        /// <summary>
        /// srwx		Shift Right Word
        /// </summary>
        public static void srwx(this XenonPowerPC cpu)
        {
            uint v = (uint)(cpu.rB & 0x3f);
            if (v > 31)
            {
                cpu.rA = 0;
            }
            else
            {
                cpu.rA = (cpu.rS >> (int)v);
            }

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void srw(this XenonPowerPC cpu) { srwx(cpu); }
        public static void srw_(this XenonPowerPC cpu) { cpu.updatecr = true; srwx(cpu); }


        /// <summary>
        /// subfx		Subtract From
        /// </summary>
        public static void subfx(this XenonPowerPC cpu)
        {
            cpu.rD = ~cpu.rA + cpu.rB + 1;
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void subf(this XenonPowerPC cpu) { subfx(cpu); }
        public static void subf_(this XenonPowerPC cpu) { cpu.updatecr = true; subfx(cpu); }


        /// <summary>
        /// subfox		Subtract From with Overflow
        /// </summary>
        public static void subfox(this XenonPowerPC cpu)
        {
            cpu.rD = ~cpu.rA + cpu.rB + 1;
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("subfox unimplemented\n");
            }
        }
        public static void subfo(this XenonPowerPC cpu) { subfox(cpu); }
        public static void subfo_(this XenonPowerPC cpu) { cpu.updatecr = true; subfox(cpu); }


        /// <summary>
        /// subfcx		Subtract From Carrying
        /// </summary>
        public static void subfcx(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint b = cpu.rB;
            cpu.rD = (~a + b + 1);
            // update xer
            if (cpu.ppc_carry_3(~a, b, 1))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void subfc(this XenonPowerPC cpu) { subfcx(cpu); }
        public static void subfc_(this XenonPowerPC cpu) { cpu.updatecr = true; subfcx(cpu); }


        /// <summary>
        /// subfcox		Subtract From Carrying with Overflow
        /// </summary>
        public static void subfcox(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint b = cpu.rB;
            cpu.rD = (~a + b + 1);
            // update xer
            if (cpu.ppc_carry_3(~a, b, 1))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("subfcox unimplemented\n");
            }
        }
        public static void subfco(this XenonPowerPC cpu) { subfcox(cpu); }
        public static void subfco_(this XenonPowerPC cpu) { cpu.updatecr = true; subfcox(cpu); }


        /// <summary>
        /// subfex		Subtract From Extended
        /// </summary>
        public static void subfex(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint b = cpu.rB;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (~a + b + ca);
            // update xer
            if (cpu.ppc_carry_3(~a, b, ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
            
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void subfe(this XenonPowerPC cpu) { subfex(cpu); }
        public static void subfe_(this XenonPowerPC cpu) { cpu.updatecr = true; subfex(cpu); }


        /// <summary>
        /// subfeox		Subtract From Extended with Overflow
        /// </summary>
        public static void subfeox(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint b = cpu.rB;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (~a + b + ca);
            // update xer
            if (cpu.ppc_carry_3(~a, b, ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("subfeox unimplemented\n");
            }
        }
        public static void subfeo(this XenonPowerPC cpu) { subfeox(cpu); }
        public static void subfeo_(this XenonPowerPC cpu) { cpu.updatecr = true; subfeox(cpu); }


        /// <summary>
        /// subfic		Subtract From Immediate Carrying
        /// </summary>
        public static void subfic(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            cpu.rD = (~a + cpu.imm + 1);
            // update XER
            if (cpu.ppc_carry_3(~a, (uint)cpu.imm, 1))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
        }

        /// <summary>
        /// subfmex		Subtract From Minus One Extended
        /// </summary>
        public static void subfmex(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (~a + ca + 0xffffffff);
            // update XER
            if ((a != 0xffffffff) || Convert.ToBoolean(ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void subfme(this XenonPowerPC cpu) { subfmex(cpu); }
        public static void subfme_(this XenonPowerPC cpu) { cpu.updatecr = true; subfmex(cpu); }


        /// <summary>
        /// subfmeox	Subtract From Minus One Extended with Overflow
        /// </summary>
        public static void subfmeox(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (~a + ca + 0xffffffff);
            // update XER
            if ((a != 0xffffffff) || Convert.ToBoolean(ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("subfmeox unimplemented\n");
            }
        }
        public static void subfmeo(this XenonPowerPC cpu) { subfmeox(cpu); }
        public static void subfmeo_(this XenonPowerPC cpu) { cpu.updatecr = true; subfmeox(cpu); }


        /// <summary>
        /// subfzex		Subtract From Zero Extended
        /// </summary>
        public static void subfzex(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (~a + ca);
            if (!Convert.ToBoolean(a) && Convert.ToBoolean(ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }
        }
        public static void subfze(this XenonPowerPC cpu) { subfzex(cpu); }
        public static void subfze_(this XenonPowerPC cpu) { cpu.updatecr = true; subfzex(cpu); }


        /// <summary>
        /// subfzeox	Subtract From Zero Extended with Overflow
        /// </summary>
        public static void subfzeox(this XenonPowerPC cpu)
        {
            uint a = cpu.rA;
            uint ca = (uint)(Convert.ToBoolean((cpu.xer & cpu.XER_CA)) ? 1 : 0);
            cpu.rD = (~a + ca);
            if (!Convert.ToBoolean(a) && Convert.ToBoolean(ca))
            {
                cpu.xer |= cpu.XER_CA;
            }
            else
            {
                cpu.xer &= ~cpu.XER_CA;
            }

            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rD);
            }

            if (cpu.updateoe)
            {
                // update XER flags
                throw new NotImplementedException("subfzeox unimplemented\n");
            }
        }
        public static void subfzeo(this XenonPowerPC cpu) { subfzeox(cpu); }
        public static void subfzeo_(this XenonPowerPC cpu) { cpu.updatecr = true; subfzeox(cpu); }


        /// <summary>
        /// xorx		XOR
        /// </summary>
        public static void xorx(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS ^ cpu.rB;
            
            if (cpu.updatecr)
            {
                // update cr0 flags
                cpu.UpdateCR0(cpu.rA);
            }
        }
        public static void xor(this XenonPowerPC cpu) { xorx(cpu); }
        public static void xor_(this XenonPowerPC cpu) { cpu.updatecr = true; xorx(cpu); }


        /// <summary>
        /// xori		XOR Immediate
        /// </summary>
        public static void xori(this XenonPowerPC cpu)
        {
            cpu.rA = cpu.rS ^ cpu.imm;
        }

        /// <summary>
        /// xoris		XOR Immediate Shifted
        /// </summary>
        public static void xoris(this XenonPowerPC cpu)
        {
            cpu.imm = cpu.imm << 16;
            cpu.rA = cpu.rS ^ cpu.imm;
        }
    }
}