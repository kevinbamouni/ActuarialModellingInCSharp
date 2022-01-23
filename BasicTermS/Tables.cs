using System;
namespace BasicTermS
{
    public static class Tables
    {
        public static string PathDiscountRate = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\BasicTermS\Data\disc_rate_ann.csv";
        public static string SchemasDiscountRate = @"{""year"":""int"",""zero_spot"":""Double""}";

        public static string SchemasMortRate = @"{""Age"":""int"",""0"":""double"",""1"":""Double"",""2"":""Double"",""3"":""Double"",""4"":""Double"",""5"":""Double""}";
        public static string PathMortRate = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\BasicTermS\Data\mort_table.csv";

        public static string PathModelPoint = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\BasicTermS\Data\model_point_table.csv";
        public static string SchemasModelPoint = @"{""point_id"":""int"",""age_at_entry"":""int"",""sex"":""string"",""policy_term"":""int"",""policy_count"":""double"",""sum_assured"":""double""}";
        
        public static string PathModelPointTest = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\BasicTermS\Data\model_point_table_test.csv";

        public static string results = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\BasicTermS\Data\results.csv";
    }
}
