using System.Data;

namespace SimpleLife
{
    public class Economic
    {
        /// <summary>
        /// DataTable : Hold the economic scenarios of the projections
        /// </summary>
        public DataTable EconomicScenarios;
        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="path">path to data file</param>
        /// <param name="schema">schema of data file</param>
        public Economic(string path, string schema)
        {
            EconomicScenarios = DataFromCsv.ReadDataTableFromCsv(path, schema);
        }
        /// <summary>
        /// Discount rate as the IntRate from economic scenarios
        /// </summary>
        /// <param name="ScenID">ID of the scenario</param>
        /// <param name="t">time t of the scenario</param>
        /// <returns>The discount rate from the economic scenario</returns>
        public decimal DiscRate(int ScenID, int t)
        {
            var a = from rw in EconomicScenarios.AsEnumerable()
                    where rw.Field<int>("ScenID") == ScenID && rw.Field<int>("Year") == t
                    select rw.Field<decimal>("IntRate");
            return a.FirstOrDefault<decimal>();
        }
        /// <summary>
        /// Investment return rate wich also equal to the Discount rate function
        /// </summary>
        /// <param name="ScenID"></param>
        /// <param name="t"></param>
        /// <returns>the investment return rate</returns>
        public decimal InvstRetRate(int ScenID, int t)
        {
            return DiscRate(ScenID, t);
        }
        /// <summary>
        /// Count the number of economic scenarios
        /// </summary>
        /// <returns>the number of economic scenarios in the datafile</returns>
        public int NumberOfEconomicScenarios()
        {
            var nb = from i in EconomicScenarios.AsEnumerable()
                     select i.Field<int>("ScenID");
            return nb.Distinct<int>().Count<int>();
        }
    }
}
