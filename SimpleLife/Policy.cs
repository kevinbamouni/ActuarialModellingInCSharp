using System.Data;

namespace SimpleLife
{
    internal class Policy
    {
        /// <summary>
        /// Policy Data
        /// </summary>
        public DataRow PolicyRow { get; set; }
        /// <summary>
        /// Mortality Data Table
        /// </summary>
        public static LifeTable mortalityTable = new LifeTable(Input.PathMortRate, Input.SchemasMortRate, 0.0m);
        /// <summary>
        /// Product specification Data Object
        /// </summary>
        public static ProductSpecifications ProductSpecfication = new ProductSpecifications(Input.PathProductSpec, Input.SchemasProductSpec);
        /// <summary>
        /// General Static Assumption Data Object
        /// </summary>
        public static GeneralStaticAssumptions generalStaticAssumptions = new GeneralStaticAssumptions(Input.PathAssumptions,
            Input.SchemasAssumptions,
            Input.PathParamAssumptions,
            Input.SchemasParamAssumptions);
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="modelPointRow"></param>
        public Policy(DataRow modelPointRow)
        {
            PolicyRow = modelPointRow;
        }
        /// <summary>
        /// Model point Product
        /// </summary>
        /// <returns></returns>
        public string? Product()
        {
            return PolicyRow.Field<string>("Product");
        }
        /// <summary>
        /// Model point Policy Type
        /// </summary>
        /// <returns></returns>
        public int PolicyType()
        {
            return PolicyRow.Field<int>("PolicyType");
        }
        /// <summary>
        /// Model point generation
        /// </summary>
        /// <returns></returns>
        public int Gen()
        {
            return PolicyRow.Field<int>("Gen");
        }
        /// <summary>
        /// Model point Channel
        /// </summary>
        /// <returns></returns>
        public int Channel()
        {
            return PolicyRow.Field<int>("Product");
        }
        /// <summary>
        /// Model point Sex
        /// </summary>
        /// <returns></returns>
        public string Sex()
        {
            return PolicyRow.Field<string>("Sex");
        }
        /// <summary>
        /// Model point DUration
        /// </summary>
        /// <returns></returns>
        public int Duration()
        {
            return PolicyRow.Field<int>("Duration");
        }
        /// <summary>
        /// Model point Issue Age of policy
        /// </summary>
        /// <returns></returns>
        public int IssueAge()
        {
            return PolicyRow.Field<int>("IssueAge");
        }
        /// <summary>
        /// Model point premium frequency
        /// </summary>
        /// <returns></returns>
        public int PremFreq()
        {
            return PolicyRow.Field<int>("PremFreq");
        }
        /// <summary>
        /// Model point Policy term
        /// </summary>
        /// <returns></returns>
        public int PolicyTerm()
        {
            return PolicyRow.Field<int>("PolicyTerm");
        }
        /// <summary>
        /// Model point Number of policy
        /// </summary>
        /// <returns></returns>
        public decimal PolicyCount()
        {
            return PolicyRow.Field<decimal>("PolicyCount");
        }
        /// <summary>
        /// Model point Total Amount insured
        /// </summary>
        /// <returns></returns>
        public decimal SumAssured()
        {
            return PolicyRow.Field<decimal>("SumAssured");
        }
        /// <summary>
        /// Discount rate of technical cashflow (i guest)
        /// Can be parametrize using an assumption input file
        /// </summary>
        /// <returns>the technical Discount Rate</returns>
        public decimal DiscountRate()
        {
            decimal sm = SumAssured();
            if (sm < 10_000_000m)
            {
                return 0.0m;
            }
            else if (sm <= 30_000_000m && sm > 10_000_000m)
            {
                return 0.05m;
            }
            else
            {
                return 0.1m;
            }
        }
        /// <summary>
        /// 
        /// Can be parametrize using an assumption input file
        /// </summary>
        /// <returns>PremWaiverCost</returns>
        public decimal PremWaiverCost()
        {
            int sm = PolicyTerm();
            if (sm < 5)
            {
                return 0.0005m;
            }
            else if (sm <= 10 && sm > 5)
            {
                return 0.0010m;
            }
            else
            {
                return 0.002m;
            }
        }

        /// <summary>
        /// Annualized Premium Rate per Sum Assured
        /// </summary>
        /// <returns></returns>
        public decimal AnnPremRate()
        {
            if (PremFreq() == 0) { return GrossPremRate() * 1 / 10; }
            else { return GrossPremRate() * PremFreq(); }
        }
        /// <summary>
        /// Cash Value Rate per Sum Assured
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal CashValueRate(int t)
        {
            return System.Math.Max(ReserveNLP_Rate("PREM", t) - SurrCharge(t), 0);
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
            decimal alpha = ProductSpecfication.LoadAcqSA(Product(), PolicyType(), Gen(), PolicyTerm());
            decimal beta = ProductSpecfication.LoadMaintPrem(Product(), PolicyType(), Gen());
            decimal gamma = ProductSpecfication.LoadMaintSA(Product(), PolicyType(), Gen());
            decimal gamma2 = ProductSpecfication.LoadMaintSA2(Product(), PolicyType(), Gen());
            decimal delta = LoadMaintPremWaiverPrem();
            int x = IssueAge();
            int n = PolicyTerm();
            int m = PolicyTerm();
            string sex = Sex();
            int tabid = ProductSpecfication.TableID("PREM", Product(), PolicyType(), Gen());
            if (Product() == "TERM" || Product() == "WL")
            {
                return (mortalityTable.Axn(x, n, sex, tabid)
                    + alpha
                    + gamma
                    * mortalityTable.AnnDuenx(x, n, sex, tabid, PremFreq())
                    + gamma2
                    * mortalityTable.AnnDuenx(x, n - m, sex, tabid, 1, m)) / (1 - beta - delta) / PremFreq() / mortalityTable.AnnDuenx(x, m, sex, tabid, PremFreq());
            }
            else if (Product() == "ENDW")
            {
                return (mortalityTable.Exn(x, n, sex, tabid) + mortalityTable.Axn(x, n, sex, tabid)
                    + alpha
                    + gamma
                    * mortalityTable.AnnDuenx(x, n, sex, tabid, PremFreq())
                    + gamma2
                    * mortalityTable.AnnDuenx(x, n - m, sex, tabid, 1, m))
                    / (1 - beta - delta) / PremFreq() / mortalityTable.AnnDuenx(x, m, sex, tabid, PremFreq());
            }
            else throw new Exception("Policy.GrossPremRate() : Product() must be TERM || WL || ENDW ");
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
        /// Net Premium Rate
        /// </summary>
        /// <param name="basis"></param>
        /// <returns></returns>
        public decimal NetPremRate(string basis)
        {
            return 0.0m;
        }
        /// <summary>
        /// Net level premium reserve rate : Net Percentage of premium to be reserved
        /// </summary>
        /// <param name="basis"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ReserveNLP_Rate(string basis, int t)
        {
            decimal gamma2 = ProductSpecfication.LoadMaintSA2(Product(), PolicyType(), Gen());
            int x = IssueAge();
            int n = PolicyTerm();
            int m = PolicyTerm();
            string sex = Sex();
            int tableid = ProductSpecfication.TableID("PREM", Product(), PolicyType(), Gen());
            if (t <= m)
            {
                return mortalityTable.Axn(x + t, n - t, sex, tableid)
                    + gamma2 * mortalityTable.AnnDuenx(x + t, n - m, sex, tableid, 1, m - t)
                    - NetPremRate(basis) * mortalityTable.AnnDuenx(x + t, m - t, sex, tableid);
            }
            else { return mortalityTable.Axn(x + t, n - t, sex, tableid) + gamma2 * mortalityTable.AnnDuenx(x + t, m - t, sex, tableid); }
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
            return ProductSpecfication.InitSurrCharge(Product(), PolicyType(), Gen(), PolicyTerm())
                * System.Math.Max((System.Math.Min(m, 10) - t)
                / System.Math.Min(m, 10), 0);

        }
        /// <summary>
        /// Unearned Premium Rate
        /// </summary>
        /// <returns></returns>
        public decimal UernPremRate()
        {
            return 0.0m;
        }
        /// <summary>
        /// BaseMortRate Qx of the policy at Age age
        /// </summary>
        /// <returns></returns>
        public decimal BaseMortRate(int age)
        {
            return mortalityTable.qx(age,
                Sex(),
                generalStaticAssumptions.BaseMort(Product(), PolicyType(), Gen()));
        }
    }
}