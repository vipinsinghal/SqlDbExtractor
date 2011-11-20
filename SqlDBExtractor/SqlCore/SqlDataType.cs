﻿using System;
//Adapted from http://nmg.codeplex.com/
//This is only for reference purpose and not for commercial use
namespace SqlDBE.Core.SqlCore
{
    public class SqlDataType
    {
        public Type MapFromDbType(string dataType, int? dataLength, int? dataPrecision, int? dataScale)
        {
            if (dataType == "DATE" || dataType == "date" || dataType == "datetime" || dataType == "TIMESTAMP" ||
                dataType == "TIMESTAMP WITH TIME ZONE" || dataType == "TIMESTAMP WITH LOCAL TIME ZONE" ||
                dataType == "smalldatetime")
            {
                return typeof(DateTime);
            }
            if (dataType == "NUMBER" || dataType == "LONG" || dataType == "bigint")
            {
                return typeof(long);
            }
            if (dataType == "smallint")
            {
                return typeof(Int16);
            }
            if (dataType == "tinyint")
            {
                return typeof(Byte);
            }
            if (dataType == "int" || dataType == "INTERVAL YEAR TO MONTH" || dataType == "BINARY_INTEGER" ||
                dataType.Contains("int"))
            {
                return typeof(int);
            }
            if (dataType == "BINARY_DOUBLE" || dataType == "float" || dataType == "numeric")
            {
                return typeof(double);
            }
            if (dataType == "BINARY_FLOAT" || dataType == "FLOAT")
            {
                return typeof(float);
            }
            if (dataType == "BLOB" || dataType == "BFILE *" || dataType == "LONG RAW" || dataType == "binary" ||
                dataType == "image" || dataType == "timestamp" || dataType == "varbinary")
            {
                return typeof(byte[]);
            }
            if (dataType == "INTERVAL DAY TO SECOND")
            {
                return typeof(TimeSpan);
            }
            if (dataType == "bit")
            {
                return typeof(Boolean);
            }
            if (dataType == "decimal" || dataType == "money" || dataType == "smallmoney" || dataType == "numeric")
            {
                return typeof(decimal);
            }
            if (dataType == "real")
            {
                return typeof(Single);
            }
            if (dataType == "uniqueidentifier")
            {
                return typeof(Guid);
            }

            // CHAR, CLOB, NCLOB, NCHAR, XMLType, VARCHAR2, nchar, ntext
            return typeof(string);
        }
    }
}