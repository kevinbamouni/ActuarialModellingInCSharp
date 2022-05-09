using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;
using System.Data;

namespace SimpleLife
{
    public class LifeTable
    {
        public DataTable MortalityTable { get; set; }
        public decimal TechnicalInterestRate { get; set; }

        public LifeTable(string path, string schema, decimal interestRate)
        {
            MortalityTable = DataFromCsv.ReadDataTableFromCsv(path, schema);
            TechnicalInterestRate = interestRate;
        }

        /// <summary>
        /// The present value of an annuity-due.
        /// </summary>
        /// <param name="x">(int) Age</param>
        /// <param name="n">(int) length of annuity payments in years</param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <param name="k">(int, optional, default=0) number of split payments in a year</param>
        /// <param name="f">(int, optional, default=0) waiting period in years</param>
        /// <returns></returns>
        public decimal AnnDuenx(int x, int n, string sex, int tableID, int k=1, int f = 0)
        {
            if (Dx(x, sex, tableID) == 0)
            {
                return 0;
            } 
            else
            {
                decimal result = (Nx(x + f, sex, tableID) - Nx(x + f + n, sex, tableID)) / Dx(x, sex, tableID);
                if (k > 1)
                {
                    return result - (k - 1) / (2 * k) * (1 - Dx(x + f + n, sex, tableID) / Dx(x, sex, tableID));
                }
                else
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// The present value of a lifetime annuity due
        /// </summary>
        /// <param name="x">(int) Age</param>
        /// <param name="k">(int, optional, default=0) number of split payments in a year</param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <param name="f">(int, optional, default=0) waiting period in years</param>
        /// <returns></returns>
        public decimal AnnDuex(int x, int k, string sex, int tableID, int f = 0)
        {
            if (Dx(x, sex, tableID) == 0)
            {
                return 0;
            }
            else
            {
                decimal result = (Nx(x + f, sex, tableID)) / Dx(x, sex, tableID);
                if (k > 1)
                {
                    return result - (k - 1) / (2 * k);
                }
                else
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// The present value of a lifetime assurance on a person at age ``x`` payable immediately upon death, optionally with an waiting period of ``f`` years.
        /// </summary>
        /// <param name="x">(int) Age</param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <param name="f">(int, optional, default=0) waiting period in years</param>
        /// <returns></returns>
        public decimal Ax(int x, string sex, int tableID, int f=0)
        {
            if (Dx(x, sex, tableID) == 0)
            {
                return 0;
            }
            else
            {
                return Mx(x + f, sex, tableID) / Dx(x, sex, tableID);
            }
        }

        /// <summary>
        /// The present value of an assurance on a person at age x payable immediately upon death, optionally with an waiting period of ``f`` years.
        /// </summary>
        /// <param name="x">(int) Age</param>
        /// <param name="n">(int) length of annuity payments in years</param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <param name="f">(int, optional, default=0) waiting period in years</param>
        /// <returns></returns>
        public decimal Axn(int x, int n,string sex, int tableID,int f = 0)
        {
            if (Dx(x, sex, tableID) == 0)
            {
                return 0;
            }
            else
            {
                return (Mx(x + f, sex, tableID) - Mx(x + f + n, sex, tableID)) / Dx(x, sex, tableID);
            }
        }

        /// <summary>
        /// The commutation column \\overline{C_x}
        /// </summary>
        /// <param name="x">(int) Age</param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public decimal Cx(int x, string sex, int tableID)
        {
            double discount = ((double)disc());
            return dx(x, sex, tableID) * (decimal)Math.Pow(discount, (x + 1 / 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public decimal Dx(int x, string sex, int tableID)
        {
            double discount = ((double)disc());
            return lx(x, sex, tableID) * (decimal)Math.Pow(discount, x);
        }

        /// <summary>
        /// The value of an endowment on a person at age ``x`` payable after n years
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public decimal Exn(int x, int n, string sex, int tableID)
        {
            if (Dx(x, sex, tableID) == 0)
            {
                return 0;
            }
            else
            {
                return Dx(x + n, sex, tableID) / Dx(x, sex, tableID);
            }
        }

        /// <summary>
        /// The commutation column M_x.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public decimal Mx(int x, string sex, int tableID)
        {
            if (x >= 110)
            {
                return Dx(x, sex, tableID);
            }
            else
            {
                return Mx(x + 1, sex, tableID) + Cx(x, sex, tableID);
            }
        }

        /// <summary>
        /// The commutation column N_x.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public decimal Nx(int x, string sex, int tableID)
        {
            if (x >= 110)
            {
                return Dx(x, sex, tableID);
            }
            else
            {
                return Nx(x + 1, sex, tableID) + Dx(x, sex, tableID);
            }
        }

        /// <summary>
        /// The discount factor v = 1/(1 + i) with i the technical interest rate
        /// </summary>
        /// <returns></returns>
        public decimal disc()
        {
            return 1 / (1 + TechnicalInterestRate);
        }

        /// <summary>
        /// The number of persons who die between ages x and x+1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public decimal dx(int x, string sex, int tableID)
        {
            return lx(x, sex, tableID) * qx(x, sex, tableID);
        }

        /// <summary>
        /// The number of persons remaining at age ``x``.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public decimal lx(int x, string sex, int tableID)
        {
            if (x == 0)
            {
                return 100000;
            }
            else
            {
                return lx(x - 1, sex, tableID) - dx(x - 1, sex, tableID);
            }
        }

        /// <summary>
        /// Probability that a person at age ``x`` will die in one year got from the mortality table data
        /// </summary>
        /// <param name="x">Age x</param>
        /// <param name="tableID">Mortality table ID</param>
        /// <param name="sex">(string) female = "F"  male = "M" </param>
        /// <returns>(decimal) Probability that a person at age ``x`` will die in one year</returns>
        public decimal qx(int x, string sex, int tableID)
        {
            //return return MortalityTable[tableID, Sex, x];
            //MorttableID;Age;Sex;Qx
            var r = from mt in MortalityTable.AsEnumerable()
                    where mt.Field<int>("MorttableID") == tableID && mt.Field<string>("Sex") == sex && mt.Field<int>("Age") == x
                    select mt.Field<decimal>("Qx");
            return (decimal)r.FirstOrDefault<decimal>();
        }
        /// <summary>
        /// Age at which mortality becomes 1
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public int LastAge(string sex, int tableID)
        {
            var la = from mt in MortalityTable.AsEnumerable()
                     where mt.Field<int>("MorttableID") == tableID && mt.Field<string>("Sex") == sex && mt.Field<int>("Qx") >=1
                     select mt.Field<int>("Age");
            return la.Min<int>();
        }
    }
}
