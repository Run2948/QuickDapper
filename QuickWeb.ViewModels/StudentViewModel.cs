﻿/* ==============================================================================
* 命名空间：QuickWeb.ViewModels 
* 类 名 称：StudentViewModel
* 创 建 者：Qing
* 创建时间：2019/06/23 20:33:17
* CLR 版本：4.0.30319.42000
* 保存的文件名：StudentViewModel
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWeb.ViewModels
{
    public class StudentViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }
}
