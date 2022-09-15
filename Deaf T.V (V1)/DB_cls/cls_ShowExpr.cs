using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Deaf_T.V__V1_.DB_cls
{
    class cls_ShowExpr
    {
        //Call the data access layer cls to connect with the database
        public DataTable ShowAll()
        {
             cls_DataAccessLayer DAL = new cls_DataAccessLayer();
             DataTable dt = new DataTable();
             dt = DAL.SelectData("Show_All_Expr", null);
            return dt;

        }

    }
}
