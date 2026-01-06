using Domain.Enums;
using Domain.Helpers;
using MongoDB.Bson;

namespace Infrastructure.Helpers
{
    public static class BsonDocumentExpression
    {
        public static string AddReferenceSymbol(string field)
        {
            return $"${field.Replace("$", "")}";
        }
        public static string RefField(string source, string field)
            => $"{AddReferenceSymbol(source)}.{field}";

        public static BsonDocument ArrayElemAt(string arrayField, int index = 0)
            => new("$arrayElemAt", new BsonArray { AddReferenceSymbol(arrayField), index });

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
            => new("$toDate", AddReferenceSymbol(fieldName));
        public static BsonDocument Sum(BsonValue expression)
            => new("$sum", expression);
        public static BsonDocument Abs(BsonValue expression)
            => new("$abs", expression);
        public static BsonDocument Add(BsonArray array)
            => new("$add", array);
        public static BsonDocument Lt(BsonValue ref__value_document, BsonValue value)
            => new("$lt", new BsonArray { ref__value_document, value });
        public static BsonDocument Gt(BsonValue ref__value_document, BsonValue value)
            => new("$gt", new BsonArray { ref__value_document, value });
        public static BsonDocument Gte(BsonValue ref__value_document, BsonValue value)
            => new("$gte", new BsonArray { ref__value_document, value });
        public static BsonDocument Eq(BsonValue ref__value_document, BsonValue value)
            => new("$eq", new BsonArray { ref__value_document, value });

        public static BsonDocument Let(string name, BsonValue value)
            => new() { { name, value } };

        public static BsonArray LookupMatchById(string varName, bool is_variable = false)
            => new BsonArray
            {
                new BsonDocument("$match", new BsonDocument("$expr",
                    new BsonDocument("$eq", new BsonArray
                    {
                        "$_id",
                        $"{varName.Replace("$",(is_variable ? "$$" : "$"))}"
                    })
                ))
            };


        public static BsonDocument? MatchInArrayField<TValue>(
                    TValue? value,
                    string arrayField,
                    string fieldName,
                    int arrayIndex = 0) where TValue : struct, Enum
        {
            if (!value.HasValue)
                return null;

            var enumString = value.Value.ToString();
            var targetField = GetField(fieldName, ArrayElemAt(arrayField, arrayIndex));

            return Match(Expr(Eq(targetField, enumString)));
        }

        public static BsonDocument? MatchEnumField<TEnum>(
            TEnum? enumValue,
            string fieldName) where TEnum : struct, Enum
        {
            if (!enumValue.HasValue)
                return null;

            var enumString = enumValue.Value.ToString();

            return new BsonDocument
            {
                { fieldName, enumString }
            };
        }

        public static BsonDocument? MatchObjectIdField(
            string? objectIdValue,
            string fieldName)
        {
            if (string.IsNullOrEmpty(objectIdValue))
                return null;

            if (!ObjectId.TryParse(objectIdValue, out var objectId))
                return null;

            return Match(new BsonDocument
            {
                { fieldName, new BsonObjectId(objectId) }
            });
        }

        public static BsonDocument Match(BsonDocument matchSpec)
            => new("$match", matchSpec);

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
                                new BsonDocument("$isoWeekYear", AddReferenceSymbol(date_field))
                            ),
                            "-W",
                            new BsonDocument("$cond", new BsonArray
                            {
                                new BsonDocument("$lt", new BsonArray
                                {
                                    new BsonDocument("$isoWeek", AddReferenceSymbol(date_field)),
                                    10
                                }),
                                new BsonDocument("$concat", new BsonArray
                                {
                                    "0",
                                    new BsonDocument("$toString",
                                        new BsonDocument("$isoWeek", AddReferenceSymbol(date_field))
                                    )
                                }),
                                new BsonDocument("$toString",
                                    new BsonDocument("$isoWeek", AddReferenceSymbol(date_field))
                                )
                            })
                        }
                    }
                };
            }
            return DateToString(dateFormat, ToDate(date_field));
        }
    }
}