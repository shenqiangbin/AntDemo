using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntDemoWeb.Enum
{
    /// <summary>
    /// 转换状态枚举
    /// </summary>
    public enum ConvertStatusEnum
    {
        /// <summary>
        /// 未开始
        /// </summary>
        UnStart = 0,

        /// <summary>
        /// 进行中
        /// </summary>
        Converting = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        Finished = 2,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 3
    }


    /// <summary>
    /// 删除标识
    /// </summary>
    public enum DeleteFlagEnum
    {
        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 1,

        /// <summary>
        /// 未删除
        /// </summary>
        UnDeleted = 0,
    }
}