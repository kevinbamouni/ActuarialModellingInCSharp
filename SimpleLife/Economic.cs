using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace SimpleLife
{
    internal class Economic
    {
        /// <summary>
        /// Hold the economic scenarios of the projections
        /// </summary>
        public DataTable EconomicScenarios;
        public Economic(string path, string schema)
        {
            EconomicScenarios = DataFromCsv.ReadDataTableFromCsv(path, schema);
        }
        /// <summary>
        /// Discount rate as the IntRate from economic scenarios
        /// </summary>
        /// <param name="ScenID"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal DiscRate(int ScenID, int t)
        {
            var a = from rw in EconomicScenarios.AsEnumerable()
                    where rw.Field<int>("ScenID") == ScenID && rw.Field<int>("Year") == t
                    select rw.Field<decimal>("IntRate");
            return a.FirstOrDefault<decimal>();
        }
        /// <summary>
        /// Investment return rate equal to the Discount rate function
        /// </summary>
        /// <param name="ScenID"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal InvstRetRate(int ScenID, int t)
        {
            return DiscRate(ScenID, t);
        }
        /// <summary>
        /// Count the number of economic scenarios
        /// </summary>
        /// <returns></returns>
        public int NumberOfEconomicScenarios()
        {
            var nb = from i in EconomicScenarios.AsEnumerable()
                     select i.Field<int>("ScenID");
            return nb.Distinct<int>().Count<int>();
        }
    }
}
