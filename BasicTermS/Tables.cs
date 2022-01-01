using System;
namespace BasicTermS
{
    public static class Tables
    {
        public static string PathDiscountRate = "/Users/kevinbamouni/OneDrive/8-PROJETS/ActuarialModellingInCSharp/BasicTermS/Data/disc_rate_ann.csv";
        public static string SchemasDiscountRate = @"{""year"":""int"",""zero_spot"":""double""}";

        public static string SchemasMortRate = @"{""Age"":""int"",""0"":""float"",""1"":""float"",""2"":""float"",""3"":""float"",""4"":""float"",""5"":""float""}";
        public static string PathMortRate = "/Users/kevinbamouni/OneDrive/8-PROJETS/ActuarialModellingInCSharp/BasicTermS/Data/mort_table.csv";

        public static string PathModelPoint = "/Users/kevinbamouni/OneDrive/8-PROJETS/ActuarialModellingInCSharp/BasicTermS/Data/model_point_table.csv";
        public static string SchemasModelPoint = @"{""point_id"":""int"",""age_at_entry"":""int"",""sex"":""string"",""policy_term"":""double"",""policy_count"":""double"",""sum_assured"":""double""}";
    }
}

