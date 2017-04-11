using AntDemoWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntDemoWeb.Service
{
    public class SysMgr
    {
        public void InitDB()
        {
            List<string> cmdTexts = new List<string>();
            cmdTexts.Add(GetDbSql());
            SQLiteHelper.ExecuteTransaction(cmdTexts);
        }

        private string GetDbSql()
        {
            return
                @"
                    --人员表
                    DROP TABLE IF EXISTS T_User;
                    CREATE TABLE IF NOT EXISTS T_User(
                        account nvarchar(20) primary key,
                        username varchar(20),
                        password varchar(20),
                        lastTryTime datetime,
                        retryCount integer
                    );
                ";
        }

    }
}