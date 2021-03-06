﻿//===============================================================================
// OracleHelper based on Microsoft Data Access Application Block (DAAB) for .NET
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//
// OracleHelper.cs
//
// This file contains the implementations of the OracleHelper and OracleHelperParameterCache
// classes.
//
// The DAAB for MS .NET Provider for Oracle has been tested in the context of this Nile implementation,
// but has not undergone the generic functional testing that the SQL version has gone through.
// You can use it in other .NET applications using Oracle databases.  For complete docs explaining how to use
// and how it's built go to the originl appblock link. 
// For this sample, the code resides in the Nile namespaces not the Microsoft.ApplicationBlocks namespace
//==============================================================================

using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Microsoft.ApplicationBlocks.Data
{
    internal class OracleCommand
    {
        public object Parameters { get; internal set; }
        public OracleConnection Connection { get; internal set; }
        public string CommandText { get; internal set; }
        public OracleTransaction Transaction { get; internal set; }
        public CommandType CommandType { get; internal set; }
    }
}