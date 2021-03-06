﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyGetIn.aspx.cs" Inherits="WDFramework.MoneyGetIn" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel4" runat="server" />
        <%--  --%>
        <x:Panel ID="Panel4" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true"
            ShowHeader="false" Title="用户管理">
            <Items>

                <x:Panel ID="Panel7" runat="server" ShowBorder="True" EnableCollapse="true" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="320px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="False">
                    <Items>

                        <x:Panel ID="Panel9" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="40px"
                            ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="lb_UserInfoID" Width="110px" runat="server" ShowLabel="false" Text="提取人：">
                                        </x:Label>
                                        <x:TextBox ID="tb_UserInfoID" ShowLabel="true" Label="提取人" Width="200px" runat="server" Required="true" TabIndex="2" AutoPostBack="true" OnTextChanged="tb_UserInfoID_TextChanged">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>


                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="LabBudgetDirector" Width="110px" runat="server" ShowLabel="false" Text="负责人：">
                                        </x:Label>
                                        <x:TextBox ID="tb_BudgetDirector" ShowLabel="true" Label="负责人" Width="200px" runat="server" TabIndex="3">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="lb_SourceWork" Width="110px" runat="server" ShowLabel="false" Text="所属项目：">
                                        </x:Label>
                                        <x:TextBox ID="tb_SourceWork" ShowLabel="true" Label="所属项目" Width="200px" runat="server" AutoPostBack="true" Required="true" TabIndex="4" OnTextChanged="tb_SourceWork_TextChanged">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                              
                                <%--这是空行--%>

                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="lb_MoneyNum" Width="110px" runat="server" ShowLabel="false" Text="金额(元)：">
                                        </x:Label>
                                        <x:TextBox ID="tb_MoneyNum" AutoPostBack="true" ShowLabel="true" Label="金额" Width="200px" runat="server" Required="true" TabIndex="5" OnTextChanged="tb_MoneyNum_TextChanged">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="lb_Time" Width="110px" runat="server" ShowLabel="false" Text="提取时间：">
                                        </x:Label>
                                        <x:DatePicker ID="dp_Time" EnableEdit="false" runat="server" ShowLabel="true" Label="提取时间" Width="195px" Required="true" TabIndex="6">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="lb_FundingPurposeSortID" Width="110px" runat="server" ShowLabel="false" Text="用途：">
                                        </x:Label>
                                        <x:DropDownList ID="ddl_FundingPurposeSortID" ShowLabel="true" Label="用途" AutoPostBack="true" runat="server" Width="195px" EnableEdit="false" ForceSelection="false" Required="true" TabIndex="7">
                                            
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                  <%--这是空行--%>
                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="LabelSecrecyLevel" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" ShowRedStar="true" Text="保密等级：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="true" Required="true" EnableSimulateTree="true"
                                            ShowRedStar="true" runat="server" ID="DropDownListLevel" Width="195px" EnableEdit="false" Label="保密等级" TabIndex="8">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label126" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>

                            </Items>
                        </x:Panel>

                    </Items>
                </x:Panel>
                <x:Panel ID="Panel18" runat="server" Height="40px" ShowBorder="True" EnableCollapse="true"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false" Width="750px">
                    <Items>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="150"></x:Label>
                                <x:Button ID="Save" Text="保存" runat="server" Icon="Add" Size="Medium" ConfirmText="确定保存？" Type="Submit" ConfirmTarget="Top" ValidateForms="Panel2" OnClick="btn_Save_Click">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Icon="Delete" Size="Medium" ConfirmText="确定重置？" ConfirmTarget="Top" Type="Submit" OnClick="btn_Delete_Click">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
    </form>
</body>
</html>



