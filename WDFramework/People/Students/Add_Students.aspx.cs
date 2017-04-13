﻿/**编写人：王会会
 * 时间：2014年8月12号
 * 功能：添加学生情况基本信息的相关操作
 * 修改履历： 1.修改人：吕博扬
 *             修改时间：2015年10月14日
 *             修改内容：取消了入学、毕业时间的限制
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People
{
    public partial class Add_Students : System.Web.UI.Page
    {
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLStudent bllStudent = new BLHelper.BLLStudent();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownListDocumentType();
                InitDropDownListType();
                InitDropDownList_Agency();
                //DatePickerEnterTime.MaxDate = DateTime.Now;
                //DatePickerGraduationTime.MaxDate = DateTime.Now;
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (T_UserInfoID.Text.Trim() != "")
                {
                    if (bllUser.IsUser(T_UserInfoID.Text.Trim()) != null)
                    {
                        if (bllUser.IsUser(T_UserInfoID.Text.Trim()).IsPass == true)
                        {
                            if (T_Sno.Text.Trim() == "")
                            {
                                Alert.ShowInTop("学号不能为空！");
                                T_Sno.Text = "";
                                return;
                            }
                            if (T_SName.Text.Trim() == "")
                            {
                                Alert.ShowInTop("姓名不能为空！");
                                T_SName.Text = "";
                                return;
                            }
                            if (T_Specialty.Text.Trim() == "")
                            {
                                Alert.ShowInTop("专业不能为空！");
                                T_Specialty.Text = "";
                                return;
                            }
                            if (T_SResearch.Text.Trim() == "")
                            {
                                Alert.ShowInTop("研究方向不能为空！");
                                T_SResearch.Text = "";
                                return;
                            }
                            if (bllStudent.IsSnoAndTeacher(T_Sno.Text.Trim(), Convert.ToInt32(bllUser.FindID(T_UserInfoID.Text.Trim().ToString()))) == null)
                            {
                                Student newstudent = new Student();
                                newstudent.Sno = T_Sno.Text.Trim();
                                newstudent.Sname = T_SName.Text.Trim();
                                if (rbtnBoy.Checked == true)
                                {
                                    newstudent.Sex = true;
                                }
                                else
                                    newstudent.Sex = false;
                                newstudent.DocumentType = DropDownListDocumentType.SelectedItem.Text;
                                newstudent.DocumentNumber = T_DocumentNumber.Text.Trim();
                                newstudent.Contact = T_Contact.Text.Trim();
                                if (IsGraduation.Checked == true)
                                {
                                    newstudent.IsGraduation = true;
                                }
                                else
                                    newstudent.IsGraduation = false;
                                newstudent.Specialty = T_Specialty.Text.Trim();
                                newstudent.SResearch = T_SResearch.Text.Trim();
                                newstudent.SGraduationDirection = T_SGraduationDirection.Text.Trim();
                                newstudent.Type = DropDownListType.SelectedItem.Text;
                                newstudent.UserInfoID = bllUser.FindID(T_UserInfoID.Text);
                                newstudent.EnterTime = DatePickerEnterTime.SelectedDate;
                                newstudent.AgencyID = BLLAgency.SelectAgencyID(DropDownList_Agency.SelectedText);
                                if (DatePickerGraduationTime.SelectedDate.HasValue)
                                {
                                    if (DatePickerGraduationTime.SelectedDate < DatePickerEnterTime.SelectedDate)
                                    {
                                        Alert.ShowInTop("毕业时间不能小于入学时间！");
                                        return;
                                    }
                                    else
                                        newstudent.GraduationTime = DatePickerGraduationTime.SelectedDate;
                                }
                                newstudent.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                                newstudent.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                                {
                                    newstudent.IsPass = false;
                                    bllStudent.InsertForPeople(newstudent);//插入学生情况表
                                    OperationLog operate = new OperationLog();
                                    operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    operate.LoginIP = "";
                                    operate.OperationType = "添加";
                                    operate.OperationContent = "Student";
                                    operate.OperationDataID = bllStudent.SelectByStudentID(newstudent.Sno);
                                    operate.OperationTime = System.DateTime.Now;
                                    operate.Remark = "";
                                    bllOperate.Insert(operate);//插入操作表
                                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("学生情况信息已提交审核！"));
                                }
                                else
                                {
                                    newstudent.IsPass = true;
                                    bllStudent.InsertForPeople(newstudent);//插入学生情况表
                                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("学生情况信息已添加完成！"));
                                }
                            }
                            else
                            {
                                Alert.ShowInTop("已存在学号为" + T_Sno.Text + ", 授课老师为" + T_UserInfoID.Text + "的信息");
                                T_Sno.Text = "";
                            }
                        }
                        else
                            Alert.ShowInTop("授课老师尚未通过审核！");
                    }
                    else
                    {
                        Alert.ShowInTop("授课老师不存在！");
                        T_UserInfoID.Text = "";
                    }
                }
                else
                {
                    Alert.ShowInTop("授课老师不能为空！");
                    T_UserInfoID.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }      
        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                T_Sno.Reset();
                T_SName.Reset();
                rbtnBoy.Reset();
                rbtnGril.Reset();
                DropDownListDocumentType.Reset();
                T_DocumentNumber.Reset();
                T_Contact.Reset();
                IsGraduation.Reset();
                NotGraduation.Reset();
                T_Specialty.Reset();
                T_SResearch.Reset();
                T_SGraduationDirection.Reset();
                DropDownListType.Reset();
                T_UserInfoID.Reset();
                DatePickerEnterTime.Reset();
                DatePickerGraduationTime.Reset();
                DropDownListSecrecyLevel.Reset();
                DropDownList_Agency.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }       
        //初始化证件类型下拉框
        public void InitDropDownListDocumentType()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("证件类型");
                for (int i = 0; i < list.Count(); i++)
                {

                    DropDownListDocumentType.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化所属机构下拉框
        public void InitDropDownList_Agency()
        {
            try
            {
                List<Common.Entities.Agency> list = BLLAgency.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownList_Agency.Items.Add(list[i].AgencyName.ToString(), (i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化学生类型下拉框
        public void InitDropDownListType()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("学生类型");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListType.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}