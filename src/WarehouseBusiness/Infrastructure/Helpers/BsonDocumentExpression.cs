using Domain.Enums;
using Domain.Helpers;
using MongoDB.Bson;

namespace Infrastructure.Helpers
{
    public static class BsonDocumentExpression
    {
        public static BsonDocument ArrayElemAt(string arrayField, int index = 0)
            => new("$arrayElemAt", new BsonArray { $"${arrayField}", index });

        public static BsonDocument GetField(string fieldName, BsonValue inputDoc)
            => new("$getField", new BsonDocument
            {
            { "field", fieldName },
            { "input", inputDoc }
            });

        public static BsonDocument Conditional(BsonValue ifCondition, BsonValue thenValue, BsonValue elseValue)
            => new("$cond", new BsonDocument
            {
            { "if", ifCondition },
            { "then", thenValue },
            { "else", elseValue }
            });

        public static BsonDocument In(BsonValue value, BsonArray array)
            => new("$in", new BsonArray { value, array });

        public static BsonDocument Multiply(params BsonValue[] values)
            => new("$multiply", new BsonArray(values));

        public static BsonDocument DateToString(string format, BsonValue date)
            => new("$dateToString", new BsonDocument
            {
            { "format", format },
            { "date", date }
            });

        public static BsonDocument ToDate(string fieldName)
            => new("$toDate", $"${fieldName}");


        public static BsonDocument Sum(BsonValue expression)
            => new("$sum", expression);

        public static BsonDocument Lt(BsonValue value1, BsonValue value2)
            => new("$lt", new BsonArray { value1, value2 });


        public static BsonDocument Gt(BsonValue value1, BsonValue value2)
            => new("$gt", new BsonArray { value1, value2 });

        public static BsonDocument BuildGroupIdByDateField(SummaryPeriod period, string date_field)
        {
            var dateFormat = MongoDateFormats.GetDateFormat(period);

            if (period == SummaryPeriod.Weekly)
            {
                return new BsonDocument
                {
                    {
                        "year", new BsonDocument ("$isoWeekYear", $"${date_field}")
                    },
                    {
                        "week:", new BsonDocument ("$isoWeek", $"${date_field}")
                    }
                };
                // return new BsonDocument
                // {
                //     {
                //         "$concat", new BsonArray
                //         {
                //             new BsonDocument("$toString",
                //                 new BsonDocument("$isoWeekYear", $"${date_field}")
                //             ),
                //             "-W",
                //             new BsonDocument("$cond", new BsonArray
                //             {
                //                 new BsonDocument("$lt", new BsonArray
                //                 {
                //                     new BsonDocument("$isoWeek", $"${date_field}"),
                //                     10
                //                 }),
                //                 new BsonDocument("$concat", new BsonArray
                //                 {
                //                     "0",
                //                     new BsonDocument("$toString",
                //                         new BsonDocument("$isoWeek", $"${date_field}")
                //                     )
                //                 }),
                //                 new BsonDocument("$toString",
                //                     new BsonDocument("$isoWeek", $"${date_field}")
                //                 )
                //             })
                //         }
                //     }
                // };
            }
            return DateToString(dateFormat, ToDate(date_field));
        }
    }
}
