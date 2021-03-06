﻿/**
 * 作者：未知
 * 2015年10月10日 马睿杰 去除page静态
 */

using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.FixedAssets.Equipment
{
    public partial class EquipmentsContent : System.Web.UI.Page
    {
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLEquipment bllequipment = new BLHelper.BLLEquipment();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        OperationLog operate = new OperationLog();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {

                btnAddEquipment.OnClientClick = Window_add.GetShowReference("AddEquipment.aspx", "添加设备信息");
                InitData();
            }
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
        //界面初始化
        public void InitData()
        {
            BLHelper.BLLEquipment bllequipment = new BLHelper.BLLEquipment();
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Equipment> list = bllequipment.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();

                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                else
                {
                    return;
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
            
        //分页
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Equipment.PageIndex = 0;
            this.Grid_Equipment.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByEquipmentName();
                    break;
                case 2:
                    FindByPurchase();
                    break;
                case 3:
                    FindByPurchaseTime();
                    break;
                case 4:
                    FindByPrice();
                    break;
                case 5:
                    FindByUsePerson();
                    break;
                case 6:
                    FindByAgencyID();
                    break;
                case 7:
                    FindByEquipNum();
                    break;
                case 8:
                    FindByAgencyName();
                    break;
            }
        }
        protected void People_Info_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_Equipment.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByEquipmentName();
                    break;
                case 2:
                    FindByPurchase();
                    break;
                case 3:
                    FindByPurchaseTime();
                    break;
                case 4:
                    FindByPrice();
                    break;
                case 5:
                    FindByUsePerson();
                    break;
                case 6:
                    FindByAgencyID();
                    break;
                case 7:
                    FindByEquipNum();
                    break;
                case 8:
                    FindByAgencyName();
                    break;
            }
        }

        //将机构ID转化为机构名
        protected string FindAgencyName(int ag)
        {
            try
            {
                return agency.FindAgenName(ag);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            ddl_ch.Reset();
            ddl_ch.Enabled = false;
            ddl_search.Reset();
            tCondition.Reset();
            InitData();
        }
        //删除选择行的点击事件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
            OperationLog operate = new OperationLog();
            BLHelper.BLLUser user = new BLHelper.BLLUser();
            try
            {
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < pm.GridCount(Grid_Equipment, CBoxSelect).Count(); i++)
                    {
                        bllequipment.Delete(Convert.ToInt32(Grid_Equipment.DataKeys[pm.GridCount(Grid_Equipment, CBoxSelect)[i]][0].ToString()));
                    }
                    InitData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < pm.GridCount(Grid_Equipment, CBoxSelect).Count(); i++)
                    {
                        bllequipment.UpdateIsPass(Convert.ToInt32(Grid_Equipment.DataKeys[pm.GridCount(Grid_Equipment, CBoxSelect)[i]][0]), false);
                        operate.LoginName = username;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "Equipments";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Equipment.DataKeys[pm.GridCount(Grid_Equipment, CBoxSelect)[i]][0]);
                        op.Insert(operate);
                    }
                    InitData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("您的数据已提交，请等待确认!");
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
      
        //搜索
        protected void Select_Click(object sender, EventArgs e)
        {
            switch (ddl_search.SelectedIndex)
            {
                case 0:
                    Alert.ShowInTop("请选择查询条件！");
                    return;
                case 1:
                    FindByEquipmentName();
                    break;
                case 2:
                    FindByPurchase();
                    break;
                case 3:
                    FindByPurchaseTime();
                    break;
                case 4:
                    FindByPrice();
                    break;
                case 5:
                    FindByUsePerson();
                    break;
                case 6:
                    FindByAgencyID();
                    break;
                case 7:
                    FindByEquipNum();
                    break;
                case 8:
                    FindByAgencyName();
                    break;
            }
        }

        protected void ddl_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            tCondition.Reset();
            if (ddl_search.SelectedIndex == 4)
                ddl_ch.Enabled = true;
            else
                ddl_ch.Enabled = false;
        }
        //编辑选中行
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_Equipment, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_Equipment, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Equipment.DataKeys[pm.GridCount(Grid_Equipment, CBoxSelect)[0]][0]);
                        Session["EquipmentID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_Update.GetShowReference("UpdateEquipment.aspx", "编辑设备信息"), Target.Top);
                    }
                    else
                    {
                        Alert.Show("一次仅可以对一行进行编辑！");
                    }
                }
                else
                {

                    Alert.Show("请选择一行！");
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //根据机构名查找设备信息
        private void FindByAgencyID()
        {
            try
            {
                BLHelper.BLLAgency blag = new BLHelper.BLLAgency();
                //int agencyid = blag.SelectAgencyID(tCondition.Text.Trim());
                ViewState["page"] = 6;
                List<Common.Entities.Equipment> list = bllequipment.FindByAgencyID(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();//改变分页数
                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //根据使用人查找设备信息
        private void FindByUsePerson()
        {
            try
            {
                ViewState["page"] = 5;
                List<Common.Entities.Equipment> list = bllequipment.FindByUsePerson(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();//改变分页数
                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //根据价格查找设备信息
        private void FindByPrice()
        {
            try
            {
                ViewState["page"] = 4;
                int flag = ddl_ch.SelectedIndex;
                List<Common.Entities.Equipment> list = bllequipment.FindByPrice(flag, tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();//改变分页数
                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //根据购买时间查找设备信息
        private void FindByPurchaseTime()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.Equipment> list = bllequipment.FindByPurchaseTime(Convert.ToInt32(tCondition.Text.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();//改变分页数
                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //根据购买人查找设备信息
        private void FindByPurchase()
        {
            try
            {
                ViewState["page"] = 2;
                List<Common.Entities.Equipment> list = bllequipment.FindByPurchase(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();//改变分页数
                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //根据设备名称查找设备信息
        private void FindByEquipmentName()
        {
            try
            {
                ViewState["page"] = 1;
                List<Common.Entities.Equipment> list = bllequipment.FindByEquipmentName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();//改变分页数
                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //根据资产编号查找设备信息
        private void FindByEquipNum()
        {
            try
            {
                ViewState["page"] = 7;
                List<Common.Entities.Equipment> list = bllequipment.FindByEquipNum(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();//改变分页数
                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //根据所属机构查找设备信息
        private void FindByAgencyName()
        {
            try
            {
                ViewState["page"] = 8;
                List<Common.Entities.Equipment> list = bllequipment.FindByAgencyName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Equipment.RecordCount = list.Count();//改变分页数
                if (list != null)
                {
                    this.Grid_Equipment.DataSource = list.Skip(Grid_Equipment.PageIndex * Grid_Equipment.PageSize).Take(Grid_Equipment.PageSize);
                    this.Grid_Equipment.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //行点击事件
        protected void Grid_Files_RowCommand(object sender, GridCommandEventArgs e)
        {
            BLHelper.BLLUser user = new BLHelper.BLLUser();
            try
            {
                string Person = Grid_Equipment.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (pm.GridCount(Grid_Equipment, CBoxSelect).Count == 0)
                {

                    btnDelete.Enabled = false;
                    return;
                }
                if (pm.GridCount(Grid_Equipment, CBoxSelect).Count != 0)
                {
                    btnDelete.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }

        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Equipment.PageIndex) * Grid_Equipment.PageSize;
        }
        //是否政府采购
        protected string isgor(string isgorver)
        {
            return (isgorver == "True") ? "是" : "否";
        }

        //是否共享
        protected string isshare(string isshare)
        {
            return (isshare == "True") ? "是" : "否";
        }

        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            List<int> selectnum = pm.GridCount(Grid_Equipment, CBoxSelect);
            if (selectnum.Count == 0)
                pm.ExportExcel(3, Grid_Equipment, 0);//选择项目为空，则全部导出
            else
                pm.ExportExcel(3, Grid_Equipment, 0, selectnum);//有选择项，导出所选
        }

        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_Equipment.SelectAllRows();
            int[] select = Grid_Equipment.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Equipment.RecordCount / this.Grid_Equipment.PageSize));

            if (Grid_Equipment.PageIndex == Pages)
                m = (Grid_Equipment.RecordCount - this.Grid_Equipment.PageSize * Grid_Equipment.PageIndex);
            else
                m = this.Grid_Equipment.PageSize;
            bool isCheck = false;
            for (int i = 0; i < m; i++)
            {
                if (CBoxSelect.GetCheckedState(i) == false)
                    isCheck = true;
            }
            if (isCheck)
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, true);
                }
                btnDelete.Enabled = true;
                btnSelect_All.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, false);
                }
                btnDelete.Enabled = false;
                btnSelect_All.Text = "全选";
            }
        }
    }
}