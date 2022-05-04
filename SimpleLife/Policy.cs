using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SimpleLife
{
    internal class Policy
    {
        public DataRow PolicyRow { get; set; }
        public static LifeTable mortalityTable = new LifeTable(Input.PathMortRate, Input.SchemasMortRate, 0.0m);
        public DataTable ProductSpecTable { get; set; }

        /// <summary>
        /// Permet de lancer une requete sur la table Assumption
        /// </summary>
        /// <param name="assumption"></param>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public dynamic AssumtionParameterTableQuery(string specification, string product, int policyType, int generation)
        {
            var r = from i in ProductSpecTable.AsEnumerable()
                    where i.Field<string>("Product") == product && i.Field<int>("PolType") == policyType && i.Field<int>("Gen") == generation
                    select i.Field<dynamic>(specification);
            return r.FirstOrDefault();
        }

        public Policy(DataRow modelPointRow)
        {
            PolicyRow = modelPointRow;
        }

        public string Product()
        {
            return PolicyRow.Field<string>("Product");
        }

        public int PolicyType()
        {
            return PolicyRow.Field<int>("PolicyType");
        }

        public int Gen()
        {
            return PolicyRow.Field<int>("Gen");
        }

        public int Channel()
        {
            return PolicyRow.Field<int>("Product");
        }

        public string Sex()
        {
            return PolicyRow.Field<string>("Sex");
        }

        public int Duration()
        {
            return PolicyRow.Field<int>("Duration");
        }

        public int IssueAge()
        {
            return PolicyRow.Field<int>("IssueAge");
        }

        public int PremFreq()
        {
            return PolicyRow.Field<int>("PremFreq");
        }

        public int PolicyTerm()
        {
            return PolicyRow.Field<int>("PolicyTerm");
        }

        public decimal PolicyCount()
        {
            return PolicyRow.Field<decimal>("PolicyCount");
        }

        public decimal SumAssured()
        {
            return PolicyRow.Field<decimal>("SumAssured");
        }
        /// <summary>
        /// Annualized Premium Rate per Sum Assured
        /// </summary>
        /// <returns></returns>
        public decimal AnnPremRate()
        {
            if (PremFreq()==0) { return GrossPremRate() * 1 / 10; }
            else { return GrossPremRate() * PremFreq(); }
        }
        /// <summary>
        /// Cash Value Rate per Sum Assured
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal CashValueRate(int t)
        {
            return System.Math.Max(ReserveNLP_Rate('PREM', t) - SurrCharge(t), 0);
        }
        /// <summary>
        /// Gross premium table
        /// </summary>
        /// <returns></returns>
        public Nullable<decimal> GrossPremTable()
        {
            return null;
        }
        /// <summary>
        /// Gross Premium Rate per Sum Assured per payment
        /// </summary>
        /// <returns></returns>
        public decimal GrossPremRate()
        {
            return PolicyRow.Field<decimal>("SumAssured");
        }
        /// <summary>
        /// Initial Surrender Charge Rate
        /// </summary>
        /// <returns></returns>
        public decimal InitSurrCharge()
        {
            decimal param1 = AssumtionParameterTableQuery("SurrChargeParam1", Product(), PolicyType(), Gen());
            decimal param2 = AssumtionParameterTableQuery("SurrChargeParam2", Product(), PolicyType(), Gen());
            return param1 + param2 * System.Math.Min(PolicyTerm() / 10, 1);
        }
        /// <summary>
        /// Interest Rate
        /// </summary>
        /// <param name="ratebasis"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public decimal IntRate(string ratebasis)
        {
            string basis;
            if (ratebasis == "PREM")
            { basis = "IntRatePrem"; }
            else if (ratebasis == "VAL")
            { basis = "IntRateVal"; }
            else
            { throw new Exception("Policy.IntRate(string ratebasis) : param ratebasis value error; must be 'PREM' or 'VAL' "); }
            int result = AssumtionParameterTableQuery(basis, Product(), PolicyType(), Gen());
            return result;
        }
        /// <summary>
        /// Acquisition Loading per Sum Assured
        /// </summary>
        /// <returns></returns>
        public decimal LoadAcqSA()
        {
            decimal param1 = AssumtionParameterTableQuery("LoadAcqSAParam1", Product(), PolicyType(), Gen());
            decimal param2 = AssumtionParameterTableQuery("LoadAcqSAParam2", Product(), PolicyType(), Gen());
            return param1 + param2 * System.Math.Min(PolicyTerm() / 10, 1);
        }
        /// <summary>
        /// Maintenance Loading per Gross Premium
        /// </summary>
        /// <returns></returns>
        public decimal LoadMaintPrem()
        {
            return PolicyRow.Field<decimal>("SumAssured");
        }
        /// <summary>
        /// Maintenance Loading per Gross Premium for Premium Waiver
        /// </summary>
        /// <returns></returns>
        public decimal LoadMaintPremWaiverPrem()
        {
            int pt = PolicyTerm();
            if (pt < 5)
            { return 0.0005m; }
            else if (pt < 10)
            { return 0.001m; }
            else { return 0.002m; }
        }
        /// <summary>
        /// Maintenance Loading per Sum Assured during Premium Payment
        /// </summary>
        /// <returns></returns>
        public decimal LoadMaintSA()
        {
            decimal result = AssumtionParameterTableQuery("LoadMaintSA", Product(), PolicyType(), Gen());
            return result;
        }
        /// <summary>
        /// Maintenance Loading per Sum Assured after Premium Payment
        /// </summary>
        /// <returns></returns>
        public decimal LoadMaintSA2()
        {
            decimal result = AssumtionParameterTableQuery("LoadMaintSA2", Product(), PolicyType(), Gen()).value;
            return result;
        }
        /// <summary>
        /// Net Premium Rate
        /// </summary>
        /// <param name="basis"></param>
        /// <returns></returns>
        public decimal NetPremRate(string basis)
        {
            return 0.0m;
        }
        /// <summary>
        /// Net level premium reserve rate
        /// </summary>
        /// <param name="basis"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ReserveNLP_Rate(string basis, int t)
        {
            decimal gamma2 = LoadMaintSA2();
            int x = IssueAge();
            int n = PolicyTerm();
            int m = PolicyTerm();
            string sex = Sex();
            int tableid = TableID(basis);
            if (t <= m)
            {
                return mortalityTable.Axn(x + t, n - t, sex, tableid)
                    + gamma2 * mortalityTable.AnnDuenx(x + t, n - m, sex, tableid, 1, m - t)
                    - NetPremRate(basis) * mortalityTable.AnnDuenx(x + t, m - t, sex, tableid);
            }
            else{ return mortalityTable.Axn(x + t, n - t, sex, tableid) + gamma2 * mortalityTable.AnnDuenx(x + t, m - t, sex, tableid); }
        }
        /// <summary>
        /// Valuation Reserve Rate per Sum Assured
        /// </summary>
        /// <returns></returns>
        public Nullable<decimal> ReserveRate()
        {
            return null;
        }
        /// <summary>
        /// Surrender Charge Rate per Sum Assured
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SurrCharge(int t)
        {
            int m = PolicyTerm();
            return InitSurrCharge() * System.Math.Max((System.Math.Min(m, 10) - t) / System.Math.Min(m, 10), 0);

        }
        /// <summary>
        /// Mortality Table ID
        /// </summary>
        /// <param name="ratebasis"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int TableID(string ratebasis)
        {
            string basis;
            if (ratebasis == "PREM")
            { basis = "MortTablePrem"; }
            else if (ratebasis == "VAL")
            { basis = "MortTableVal"; }
            else
            { throw new Exception("Policy.TableID(string ratebasis) : param ratebasis value error; must be 'PREM' or 'VAL' "); }
            int result = AssumtionParameterTableQuery(basis, Product(), PolicyType(), Gen());
            return result;
        }
        /// <summary>
        /// Unearned Premium Rate
        /// </summary>
        /// <returns></returns>
        public decimal UernPremRate()
        {
            return 0.0m;
        }
    }
}
