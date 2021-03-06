﻿using AntDemoWeb.Repository;
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

                    --图书表
                    DROP TABLE IF EXISTS Book;
                    CREATE TABLE Book(
                        id integer primary key autoincrement not null,
                        bookName nvarchar(50),
                        bookPath varchar(100),
                        convertStatus int, -- 转换状态 0：未开始 1：进行中 2：已完成 3：失败
                        swfPath varchar(100),
                        uploadTime datetime,
                        deleteFlag int--删除标识，1 已删除
                    );
            ";
        }

    }
}