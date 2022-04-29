using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLife
{
    public static class Input
    {
        public static string PathAssumptions = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\disc_rate_ann.csv";
        public static string SchemasAssumptions = @"{""year"":""int"",""zero_spot"":""decimal""}";

        public static string PathParamAssumptions = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\disc_rate_ann.csv";
        public static string SchemasParamAssumptions = @"{""year"":""int"",""zero_spot"":""decimal""}";

        public static string SchemasMortRate = @"{""MortTableID"":""int"",""Age"":""int"",""Sex"":""string"",""Qx"":""decimal""}";
        public static string PathMortRate = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\mortality_table.csv";

        public static string SchemasLapse = @"{""Age"":""int"",""0"":""decimal"",""1"":""decimal"",""2"":""decimal"",""3"":""decimal"",""4"":""decimal"",""5"":""decimal""}";
        public static string PathLapse = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\mortality_table.csv";

        public static string SchemasProductSpec = @"{""Age"":""int"",""0"":""decimal"",""1"":""decimal"",""2"":""decimal"",""3"":""decimal"",""4"":""decimal"",""5"":""decimal""}";
        public static string PathProductSpec = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\mortality_table.csv";

        public static string SchemasScenarios = @"{""Age"":""int"",""0"":""decimal"",""1"":""decimal"",""2"":""decimal"",""3"":""decimal"",""4"":""decimal"",""5"":""decimal""}";
        public static string PathScenarios = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\mortality_table.csv";

        public static string PathModelPoint = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\policydata.csv";
        public static string SchemasModelPoint = @"{""point_id"":""int"",""age_at_entry"":""int"",""sex"":""string"",""policy_term"":""int"",""policy_count"":""decimal"",""sum_assured"":""decimal""}";

        public static string PathModelPointTest = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\model_point_table_test.csv";

        public static string results = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\results.json";
    }
}
