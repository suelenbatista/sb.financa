using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Helper
{
    public static class SqlHelper
    {
        public static string ObterConnectionString(string nomeConexao)
        {
            return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FinancaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
