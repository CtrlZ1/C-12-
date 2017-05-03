﻿using System;
using FineUI;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WDFramework.ContractAndPact.Contract
{
    public partial class Updata_Contract : System.Web.UI.Page
    {
        BLHelper.BLLContract BLLcontract = new BLHelper.BLLContract();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog BLLOL = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        Common.Entities.Contract NewContract = new Common.Entities.Contract();
        Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
         BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //等级下拉框绑定(可选等级不大于登陆等级)
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                InitData();
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                if (Session["ContractID"].ToString() != "")
                {
                    Common.Entities.Contract contract = new Common.Entities.Contract();
                    contract = BLLcontract.FindByContractID(Convert.ToInt32(Session["ContractID"]));
                    txtContractHeadLine.Text = contract.ContractHeadLine;
                    txtContractAuthors.Text = contract.ContractAuthors;
                    txtContractOriginal.Text = contract.ContractOriginal;


                }
                else
                {
                    return;
                }
               
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        //保存资料信息
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int AttachmentID = BLLcontract.FindAttachmentID(Convert.ToInt32(Session["ContractID"]));
                string path = at.FindPath(AttachmentID);
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                //if (Session["LoginName"].ToString() == "")
                //{
                //    Response.Redirect("login.aspx");
                //    Alert.Show("登录超时！");
                //}
                if (txtContractHeadLine.Text.Trim() == "")
                {
                    Alert.ShowInTop("资料题目不能为空！");
                    txtContractHeadLine.Reset();
                    return;
                }
                if (txtContractAuthors.Text.Trim() == "")
                {
                    Alert.ShowInTop("资料保存人不能为空！");
                    txtContractAuthors.Reset();
                    return;
                }
                if (txtContractOriginal.Text.Trim() == "")
                {
                    Alert.ShowInTop("原始文件保存人不能为空 ! ");
                    txtContractOriginal.Reset();
                    return;
                }
                NewContract.ContractAuthors = txtContractAuthors.Text;
                NewContract.ContractHeadLine = txtContractHeadLine.Text;
                NewContract.ContractOriginal = txtContractOriginal.Text;
                //NewContract.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                NewContract.EntryPerson = BLLcontract.FindByContractID(Convert.ToInt32(Session["ContractID"])).EntryPerson;
                NewContract.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);

                NewContract.ContractID = Convert.ToInt32(Session["ContractID"]);
                //上传附件
                
                int AttachID = pm.UpLoadFile(fileupload).Attachid;
                switch (AttachID)
                {
                    case -1:
                        Alert.ShowInTop("文件类型不符，请重新选择！");
                        return;
                    case 0:
                        Alert.ShowInTop("文件名已经存在！");
                        return;
                    case -2:
                        Alert.ShowInTop("文件不能大于150M");
                        return;
                    case -3:
                        NewContract.AttachmentID = null;
                        break;
                    //Alert.ShowInTop("请上传附件");
                    //return;
                    default:
                        NewContract.AttachmentID = AttachID;
                        break;
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    NewContract.IsPass = true;
                    //向资料表中插入数据
                    //BLLcontract.Insert(NewContract);
                    //Alert.ShowInTop("保存成功");
                    BLLcontract.Update(NewContract);//5级直接更新平台表
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("信息已更新完成！"));
                }
                else
                {
                    NewContract.IsPass = false;
                    //向资料表中插入数据
                    BLLcontract.Insert(NewContract);
                    //向操作日志表中插入信息
                    operationLog.LoginIP = " ";
                    operationLog.LoginName = Session["LoginName"].ToString();
                    operationLog.OperationType = "添加";
                    operationLog.OperationContent = "Contract";
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.OperationDataID = NewContract.ContractID;
                    //向操作日志表中插入数据
                    BLLOL.Insert(operationLog);
                    Alert.ShowInTop("您的数据已提交，请等待确认");
                }

                //PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
               
            }
            catch (Exception ex)
            {
                int ContractID = NewContract.ContractID;
                //删除资料附件
                int AttactID = BLLcontract.FindAttachmentID(ContractID);
                string strPath;
                if (AttactID != 0)
                {
                    strPath = BLLattachment.FindPath(AttactID);
                    if (strPath != "")
                    {
                        //删除附件文件
                        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                        pm.DeleteFile(AttactID, strPath);
                        //在附件表中删除附件数据
                        BLLattachment.Delete(AttactID);
                    }
                }
                //pm.SaveError(ex, this.Request);
            }

        }
        //重置资料信息
        protected void btnReSet_Click(object sender, EventArgs e)
        {
            txtContractHeadLine.Reset();
            DropDownList_SecrecyLevel.SelectedValue = "1";
            txtContractAuthors.Reset();
            txtContractOriginal.Reset();
            PageContext.RegisterStartupScript("clearFile();");
            //filePath.Reset();
        }

       
        //取消添加资料信息
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}