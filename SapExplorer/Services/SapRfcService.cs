using SAP.Middleware.Connector;
using SapExplorer.Core;
using SapExplorer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SapExplorer.Services
{
    public class SapRfcService
    {
        private const char DATA_SEPARATOR = ((char)007);
        private string _destinationName;

        public void SetDestination(string destinationName)
        {
            this._destinationName = destinationName;
        }

        public SapResult<List<FunctionModel>> GetFunctions(string functionName)
        {
            var result = new SapResult<List<FunctionModel>>();

            try
            {
                using (var conn = new SapConnection(_destinationName))
                {
                    // NOTE: https://www.sapdatasheet.org/abap/func/rfc_function_search.html
                    var rfcFunction = conn.Repository.CreateFunction("RFC_FUNCTION_SEARCH");
                    rfcFunction.SetValue("FUNCNAME", functionName);
                    rfcFunction.SetValue("LANGUAGE", "KO");
                    rfcFunction.Invoke(conn.Destination);

                    var functions = rfcFunction.GetTable("FUNCTIONS");
                    foreach(var function in functions)
                    {
                        result.Export.Add(new FunctionModel
                        {
                            Name = function.GetString("FUNCNAME"),
                            Group = function.GetString("GROUPNAME"),
                            Application = function.GetString("APPL"),
                            RemoteHost = function.GetString("HOST"),
                            ShortText = function.GetString("STEXT")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            return result;
        }

        public SapResult<List<ParameterModel>> GetFunctionParameters(string functionName)
        {
            var result = new SapResult<List<ParameterModel>>();

            try
            {
                using (var conn = new SapConnection(_destinationName))
                {
                    var parameters = new List<ParameterModel>();
                    var meta = conn.Repository.GetFunctionMetadata(functionName);
                    for (int i = 0; i < meta.ParameterCount; i++)
                    {
                        StructureModel structureModel = null;
                        if (meta[i].DataType == RfcDataType.STRUCTURE)
                        {
                            string structureName = meta[i].ValueMetadataAsStructureMetadata.Name;
                            structureModel = LoadStructureMetadata(conn.Repository, structureName);
                        }
                        else if (meta[i].DataType == RfcDataType.TABLE)
                        {
                            string structureName = meta[i].ValueMetadataAsTableMetadata.LineType.Name;
                            structureModel = LoadStructureMetadata(conn.Repository, structureName);
                        }

                        var parameter = new ParameterModel()
                        {
                            Name = meta[i].Name,
                            DataType = meta[i].DataType.ToString(),
                            Direction = meta[i].Direction.ToString(),
                            DefaultValue = meta[i].DefaultValue,
                            Optional = meta[i].Optional,
                            Documentation = meta[i].Documentation,
                            Structure = structureModel
                        };

                        parameters.Add(parameter);
                    }

                    // 정렬순서
                    var sortOrder = new List<string>() { "IMPORT", "EXPORT", "CHANGING", "TABLES" };

                    result.Export = parameters.OrderBy(x => sortOrder.IndexOf(x.Direction)).ToList();
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            return result;
        }

        public SapResult<DataTable> GetTable(string tableName, int rowSkips = 0, int rowCount = 0)
        {
            var result = new SapResult<DataTable>();

            try
            {
                using (var conn = new SapConnection(_destinationName))
                {
                    // NOTE: https://www.sapdatasheet.org/abap/func/rfc_read_table.html
                    var rfcFunction = conn.Repository.CreateFunction("RFC_READ_TABLE");
                    rfcFunction.SetValue("QUERY_TABLE", tableName);
                    rfcFunction.SetValue("DELIMITER", DATA_SEPARATOR);
                    rfcFunction.SetValue("ROWSKIPS", rowSkips);
                    rfcFunction.SetValue("ROWCOUNT", rowCount);
                    rfcFunction.Invoke(conn.Destination);

                    var fields = rfcFunction.GetTable("FIELDS");
                    var datas = rfcFunction.GetTable("DATA");
                    var table = ToTable(tableName, fields, datas);

                    result.Export = table;
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            return result;
        }

        private DataTable ToTable(string tableName, IRfcTable fields, IRfcTable datas)
        {
            var dt = new DataTable(tableName);
            foreach (var field in fields)
            {
                var column = new DataColumn()
                {
                    ColumnName = field.GetString("FIELDNAME"),
                    Caption = field.GetString("FIELDTEXT"),
                    MaxLength = field.GetInt("LENGTH")
                };

                dt.Columns.Add(column);
            }   

            foreach (var data in datas)
            {
                var values = data.GetString("WA").Split(DATA_SEPARATOR);
                var row = dt.NewRow();
                for (int i = 0; i < values.Length; i++)
                    row[i] = values[i].Trim();

                dt.Rows.Add(row);
            }
            
            return dt;
        }

        private StructureModel LoadStructureMetadata(RfcRepository repository, string structureName)
        {
            var fields = new List<FieldModel>();
            var meta = repository.GetStructureMetadata(structureName);
            
            for (int i = 0; i < meta.FieldCount; i++)
            {
                var field = new FieldModel()
                {
                    Name = meta[i].Name,
                    DataType = meta[i].DataType.ToString(),
                    Length = meta[i].NucLength,
                    Decimals = meta[i].Decimals,
                    Documentation = meta[i].Documentation
                };

                fields.Add(field);
            }

            return new StructureModel(structureName, fields);
        }
    }
}
