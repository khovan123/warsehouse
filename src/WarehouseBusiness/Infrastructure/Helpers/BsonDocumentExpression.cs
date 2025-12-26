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

        public static BsonDocument Conditional(BsonValue condition, BsonValue trueValue, BsonValue falseValue)
            => new("$cond", new BsonDocument
            {
            { "if", condition },
            { "then", trueValue },
            { "else", falseValue }
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

        public static BsonDocument Lt(BsonValue ref__value_document, BsonValue value)
            => new("$lt", new BsonArray { ref__value_document, value });      
        public static BsonDocument Gt(BsonValue ref__value_document, BsonValue value)
            => new("$gt", new BsonArray { ref__value_document, value });        
        public static BsonDocument Gte(BsonValue ref__value_document, BsonValue value)
            => new("$gte", new BsonArray { ref__value_document, value });
        public static BsonDocument And(BsonArray array)
            => new("$and", array);   
        public static BsonDocument Expr(BsonDocument document)
            => new("$expr", document);

        public static BsonDocument BuildGroupIdByDateField(SummaryPeriod period, string date_field)
        {
            var dateFormat = MongoDateFormats.GetDateFormat(period);

            if (period == SummaryPeriod.ThisWeek)
            {
                return new BsonDocument
                {
                    {
                        "$concat", new BsonArray
                        {
                            new BsonDocument("$toString",
                                new BsonDocument("$isoWeekYear", $"${date_field}")
                            ),
                            "-W",
                            new BsonDocument("$cond", new BsonArray
                            {
                                new BsonDocument("$lt", new BsonArray
                                {
                                    new BsonDocument("$isoWeek", $"${date_field}"),
                                    10
                                }),
                                new BsonDocument("$concat", new BsonArray
                                {
                                    "0",
                                    new BsonDocument("$toString",
                                        new BsonDocument("$isoWeek", $"${date_field}")
                                    )
                                }),
                                new BsonDocument("$toString",
                                    new BsonDocument("$isoWeek", $"${date_field}")
                                )
                            })
                        }
                    }
                };
            }
            return DateToString(dateFormat, ToDate(date_field));
        }

        public static (DateTime startUtc, DateTime endUtc) RangeUtc(SummaryPeriod period)
        {
            var now = DateTime.UtcNow;

            DateTime start;
            DateTime end;

            switch (period)
            {
                case SummaryPeriod.Today:
                    start = now.Date;
                    end = start.AddDays(1);
                    break;

                case SummaryPeriod.ThisWeek:
                    var today = now.Date;
                    int diff = (7 + (int)today.DayOfWeek - (int)DayOfWeek.Monday) % 7;
                    start = today.AddDays(-diff);
                    end = start.AddDays(7);
                    break;

                case SummaryPeriod.ThisMonth:
                    start = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                    end = start.AddMonths(1);
                    break;

                case SummaryPeriod.ThisYear:
                    start = new DateTime(now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    end = start.AddYears(1);
                    break;

                default:
                    start = now.Date;
                    end = start.AddDays(1);
                    break;
            } 
            return (start, end);
        }
    }
}