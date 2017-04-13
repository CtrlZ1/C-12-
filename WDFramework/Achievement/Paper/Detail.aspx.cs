﻿/**编写人：方淑云
 * 时间：2014年8月12号
 * 功能:备注界面后台
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Acheievement.Paper
{
    public partial class Detail : System.Web.UI.Page
    {
        BLHelper.BLLPaper paper = new BLHelper.BLLPaper();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                string str = paper.FindRemark(id);
                if (str != "" || str != null)
                {
                    Content.Text = str;
                }
                else
                {
                    Content.Text = " ";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}