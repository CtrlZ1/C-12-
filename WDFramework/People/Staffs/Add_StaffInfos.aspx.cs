﻿/**编写人：王会会
 * 时间：2014年8月12号
 * 功能：添加人员基本信息的相关操作
 * 修改履历：(**2014年10月17号**刘诚中**Save_Click()函数中添加是否添加机构的判断**记为change_1)
 * 修改人：高琪 修改时间2015/11/30 修改履历：增加保存人员照片功能
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
    public partial class Add_StaffInfos : System.Web.UI.Page
    {
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLAgency bllAgency = new BLHelper.BLLAgency();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.Encrypt encrypt = new BLCommon.Encrypt();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        Common.Entities.UserInfo userInfo = new UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownListAgencyP();
                InitDropDownListEducation();
                InitDropDownListDegree();
                InitDropDownListDocumentsType();
                InitDropDownListPoliticalStatus();
                InitDropDownListAdmin();
                InitDropDownListSubjectSortP();
                InitDropDownListNation();
                InitDropListSecrecyLevel();
                InitDropDownListStaffType();
                //初始化学缘下拉框
                InitDropDownListStudySource();
                DatePickerBirth.MaxDate = DateTime.Now;
                DatePickerDoctorTeacherTime.MaxDate = DateTime.Now;
                DatePickerJobTitleTime.MaxDate = DateTime.Now;
                DatePickerPoliticalStatusTime.MaxDate = DateTime.Now;
                DatePickerMasterTeacherTime.MaxDate = DateTime.Now;
                DatePickerEnterSchoolTime.MaxDate = DateTime.Now;//入校时间
            }

        }      
        //初始化项目涉密等级等级
        public void InitDropListSecrecyLevel()
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                DropDownListSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }
      
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (T_UserName.Text.Trim () != "")
                {
                    //if (T_UserInfoBH.Text.Trim() == "")
                    //{
                    //    Alert.ShowInTop("用户编号不能为空！");
                    //    T_UserInfoBH.Text = "";
                    //    return;
                    //}
                    if (T_LoginName.Text.Trim() == "")
                    {
                        Alert.ShowInTop("用户登录名不能为空！");
                        T_LoginName.Text = "";
                        return;
                    }
                    if (bllUser.IsUser(T_UserName.Text.Trim()) == null)
                    {
                        //if (bllUser.IsUserInfoBH(T_UserInfoBH.Text.Trim()) == null)
                        //{
                            if (bllUser.IsLoginName(T_LoginName.Text.Trim()) == null)
                            {
                                UserInfo NewUser = new UserInfo();
                                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                                    NewUser.IsPass = false;
                                else
                                    NewUser.IsPass = true;
                                NewUser.UserName = T_UserName.Text.Trim ();
                                if (rbtnBoy.Checked == true)
                                {
                                    NewUser.Sex = true;
                                }
                                else
                                    NewUser.Sex = false;
                                NewUser.Nation = DropDownListNation.SelectedItem.Text;
                                NewUser.StaffType = DropDownListStaffType.SelectedItem.Text;
                                NewUser.Hometown = T_Hometown.Text.Trim ();
                                if (DatePickerBirth.SelectedDate.HasValue)
                                {
                                    NewUser.Birth = DatePickerBirth.SelectedDate;
                                }
                                NewUser.JobTitle = T_JobTitle.Text.Trim ();
                                NewUser.AgencyID = bllAgency.SelectAgencyID(DropDownListAgencyP.SelectedText);
                                NewUser.TeleNum = T_Telenum.Text.Trim ();
                                NewUser.HomeNum = T_HomeTetlum.Text.Trim ();
                                NewUser.OfficeNum = T_Officenum.Text.Trim ();
                                NewUser.DocumentsNum = T_DocumentsNum.Text.Trim();
                                NewUser.DocumentsType = DropDownListDocumentsType.SelectedItem.Text;
                                NewUser.PoliticalStatus = DropDownListPoliticalStatus.SelectedItem.Text;
                                NewUser.Profile = TextAreaProfile.Text.Trim();
                                NewUser.Education = DropDownListEducation.SelectedItem.Text;
                                NewUser.Degree = DropDownListDegree.SelectedItem.Text;
                                NewUser.StaffType = DropDownListStaffType.SelectedItem.Text;
                                NewUser.Specialty = T_Specilty.Text.Trim();
                                if (ISMarriage.Checked == true)
                                {
                                    NewUser.Marriage = true;
                                }
                                else
                                    NewUser.Marriage = false;

                                NewUser.Fax = T_Fax.Text.Trim();
                                NewUser.HomeAddress = T_HomeAddress.Text.Trim();
                                NewUser.PostalCode = T_PostalCode.Text.Trim();
                                NewUser.qqNum = T_QQnum.Text.Trim();
                                NewUser.Remark = T_Remark.Text.Trim();
                                NewUser.UnitName = T_UnitName.Text.Trim();
                                //NewUser.StaffType = T_StaffType.Text.Trim();
                                //NewUser.UserInfoBH = T_UserInfoBH.Text.Trim();
                                NewUser.LoginName = T_LoginName.Text.Trim();

                                if (IsPWD.Text.Trim() != T_LoginPWD.Text.Trim())
                                {
                                    Alert.ShowInTop("密码不一致！");
                                    IsPWD.Text = "";
                                    return;
                                }
                                else
                                    NewUser.LoginPWD = encrypt.MD5(T_LoginPWD.Text.Trim());
                                NewUser.Email = T_Email.Text.Trim();
                                NewUser.TeleNum = T_Telenum.Text.Trim();
                                NewUser.AdministrativeLevelName = DropDownListAdmin.SelectedItem.Text;
                                NewUser.Domicile = T_Domicile.Text.Trim();
                                NewUser.SubjectSortName = DropDownListSubjectSortP.SelectedItem.Text;
                                if (DatePickerJobTitleTime.SelectedDate.HasValue)
                                {
                                    NewUser.JobTitleTime = DatePickerJobTitleTime.SelectedDate;
                                }
                                if (DatePickerPoliticalStatusTime.SelectedDate.HasValue)
                                {
                                    NewUser.PoliticalStatusTime = DatePickerPoliticalStatusTime.SelectedDate;
                                }
                                if (IsMasterTeacher.Checked == true)
                                {
                                    NewUser.IsMasteTeacher = true;
                                }
                                else
                                {
                                    NewUser.IsMasteTeacher = false;
                                }
                                if (IsDoctorTeacher.Checked == true)
                                {
                                    NewUser.IsDocdorTeacher = true;
                                }
                                else
                                {
                                    NewUser.IsDocdorTeacher = false;
                                }
                                if (DatePickerMasterTeacherTime.SelectedDate.HasValue)
                                {
                                    NewUser.MasterTeacherTime = DatePickerMasterTeacherTime.SelectedDate;
                                }
                                if (DatePickerDoctorTeacherTime.SelectedDate.HasValue)
                                {
                                    NewUser.DoctorTeacherTime = DatePickerDoctorTeacherTime.SelectedDate;
                                }
                                NewUser.SecrecyLevel = Convert.ToInt32(DropDownListSecrecyLevel.SelectedIndex + 1);
                                NewUser.ResearchDirection = T_Reserch.Text.Trim();
                                NewUser.LastSchool = LastSchool.Text.Trim();
                                NewUser.EnterSchoolTime = DatePickerEnterSchoolTime.SelectedDate;//入校时间；
                                NewUser.StudySource = DropDownListStudySource.SelectedItem.Text;//学缘
                                NewUser.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                                if(Session["AttachID"]!=null)
                                {
                                    NewUser.PhotoID = int.Parse(Session["AttachID"].ToString());
                                } 
                                else
                                {
                                    NewUser.PhotoID = null;
                                }
                                //switch (AttachID)
                                //{
                                //    case -1:
                                //        Alert.ShowInTop("照片类型不符，请重新选择！");
                                //        return;
                                //    case 0:
                                //        Alert.ShowInTop("文件名已经存在！");
                                //        return;
                                //    case -2:
                                //        Alert.ShowInTop("照片不能大于150M");
                                //        return;
                                //    case -3:
                                //        NewUser.PhotoID = null;
                                //        break;
                                //    //Alert.ShowInTop("请上传附件");
                                //    //return;
                                //    default:
                                //        NewUser.PhotoID = AttachID;
                                //        break;
                                //}
                                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                                {
                                    bllUser.Insert(NewUser);//插入人员基本信息表
                                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员基本信息添加完成！"));
                                }
                                else
                                {
                                    bllUser.Insert(NewUser);//插入人员基本信息表
                                    //向操作日志表中插入
                                    OperationLog operate = new OperationLog();
                                    operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    operate.LoginIP = "";
                                    operate.OperationType = "添加";
                                    operate.OperationContent = "UserInfo";
                                    operate.OperationDataID = NewUser.UserInfoID;
                                    operate.OperationTime = System.DateTime.Now;
                                    operate.Remark = "";
                                    bllOperate.Insert(operate);
                                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员基本信息已提交审核！"));
                                }
                            }
                            else
                            {
                                if (bllUser.IsLoginName(T_LoginName.Text.Trim()).IsPass == false)
                                {
                                    Alert.ShowInTop("该用户登录名正在审核中，请等待！");
                                    T_LoginName.Text = "";
                                    return;
                                }
                                if (bllUser.IsLoginName(T_LoginName.Text.Trim()).IsPass == true)
                                {
                                    Alert.ShowInTop("用户登录名已存在！");
                                    T_LoginName.Text = "";
                                    return;
                                }
                            }
                        //}
                        //else
                        //{
                        //    if (bllUser.IsUserInfoBH(T_UserInfoBH.Text.Trim()).IsPass == false)
                        //    {
                        //        Alert.ShowInTop("该用户编号正在审核中，请等待！");
                        //        T_UserInfoBH.Text = "";
                        //        return;
                        //    }
                        //    if (bllUser.IsUserInfoBH(T_UserInfoBH.Text.Trim()).IsPass == true)
                        //    {
                        //        Alert.ShowInTop("用户编号已存在！");
                        //        T_UserInfoBH.Text = "";
                        //        return;
                        //    }
                        //}
                    }
                    else
                    {
                        if (bllUser.IsUser(T_UserName.Text.Trim()).IsPass == false)
                        {
                            Alert.ShowInTop("该用户姓名正在审核中，请等待！");
                            T_UserName.Text = "";
                            return;
                        }
                        if (bllUser.IsUser(T_UserName.Text.Trim()).IsPass == true)
                        {
                            Alert.ShowInTop("用户姓名已存在！");
                            T_UserName.Text = "";
                            return;
                        }
                    }
                }
                else
                {
                    Alert.ShowInTop("人员名称不能为空！");
                    T_UserName.Text = "";
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
                T_UserName.Reset();
                rbtnBoy.Reset();
                rbtnGril.Reset();
                DropDownListNation.Reset();
                DropDownListStaffType.Reset();
                T_Hometown.Reset();
                DatePickerBirth.Reset();
                T_JobTitle.Reset();
                DropDownListAgencyP.Reset();
                T_Telenum.Reset();
                T_HomeTetlum.Reset();
                T_Officenum.Reset();
                T_DocumentsNum.Reset();
                DropDownListDocumentsType.Reset();
                DropDownListPoliticalStatus.Reset();
                TextAreaProfile.Reset();
                DropDownListEducation.Reset();
                DropDownListDegree.Reset();
                T_Specilty.Reset();
                ISMarriage.Reset();
                NotMarriage.Reset();
                T_Fax.Reset();
                T_HomeAddress.Reset();
                T_PostalCode.Reset();
                T_QQnum.Reset();
                T_Remark.Reset();
                T_UnitName.Reset();
                //T_UserInfoBH.Reset();
                T_LoginName.Reset();
                T_LoginPWD.Reset();
                IsPWD.Reset();
                T_Email.Reset();
                T_Telenum.Reset();
                DropDownListAdmin.Reset();
                T_Domicile.Reset();
                DropDownListSubjectSortP.Reset();
                DatePickerJobTitleTime.Reset();
                DatePickerPoliticalStatusTime.Reset();
                IsMasterTeacher.Reset();
                NotMasterTeacher.Reset();
                IsDoctorTeacher.Reset();
                NotDoctorTeacher.Reset();
                DatePickerMasterTeacherTime.Reset();
                DatePickerDoctorTeacherTime.Reset();
                DropDownListSecrecyLevel.Reset();
                T_Reserch.Reset();
                LastSchool.Reset();
                DatePickerEnterSchoolTime.Reset();//入校时间；
                DropDownListStudySource.Reset();//学缘
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
      
        //初始化机构名称下拉框
        public void InitDropDownListAgencyP()
        {
            try
            {
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                List<Common.Entities.Agency> list = agency.FindAllAgencyName();
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListAgencyP.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化学历下拉框
        public void InitDropDownListEducation()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("学历");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListEducation.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化学位下拉框
        public void InitDropDownListDegree()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("学位");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListDegree.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        
        //初始化证件类型下拉框
        public void InitDropDownListDocumentsType()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("证件类型");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListDocumentsType.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }       
        //初始化人员行政级别名称下拉框
        public void InitDropDownListAdmin()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("人员行政级别名称");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListAdmin.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化学科分类名称下拉框
        public void InitDropDownListSubjectSortP()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("学科分类名称");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListSubjectSortP.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化民族下拉框
        public void InitDropDownListNation()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("民族");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListNation.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化员工类型下拉框
        public void InitDropDownListStaffType()
        {
            string[] StaffTypes = new string[] { "兼职人员", "专职人员"};
         
            for (int i = 0; i < 2; i++)
            {
                DropDownListStaffType.Items.Add(StaffTypes[i], i.ToString());
            }
        }
        //初始化专业技术职务下拉框
        public void InitDropDownListPoliticalStatus()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("政治面貌");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListPoliticalStatus.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化学缘下拉框
        public void InitDropDownListStudySource()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("学缘");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListStudySource.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        protected void photoupload_FileSelected(object sender, EventArgs e)
        
        {
            int AttachID = publicmethod.UpLoadPhoto(photoupload);
            switch (AttachID)
            {
                case -1:
                    Alert.ShowInTop("照片类型不符，请重新选择！");
                    return;
                case 0:
                    Alert.ShowInTop("文件名已经存在！");
                    return;
                case -2:
                    Alert.ShowInTop("照片不能大于150M");
                    return;
                case -3:
                    Session["AttachID"] = null;
                    break;
                //Alert.ShowInTop("请上传附件");
                //return;
                default:
                    {
                        Session["AttachID"] = AttachID;
                        BLHelper.BLLAttachment att = new BLHelper.BLLAttachment();
                        Image_show.ImageUrl = att.FindPath(AttachID);
                        break;
                    }
            }
        }
    }
}