﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpeakClasses.aspx.cs" Inherits="WDFramework.People.SpeakClasses.SpeakClasses" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu='return false' ><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
       <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true" 
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true" 
            ShowHeader="false" Title="用户管理"   >
        <Items>
        <x:Grid ID="GridSpeakClass" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
         EnableCheckBoxSelect="false" EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true"
         DataKeyNames="SpeakClassID" AllowSorting="true" SortColumnIndex="0"
         SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand ="GridSpeakClass_RowCommand" OnPageIndexChange ="GridSpeakClass_PageIndexChange"  >
             <Toolbars>
        <x:Toolbar ID="Toolbar1" runat="server"  Width="1300px" >
            <Items>
               <x:Label ID="Label1" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
                <x:Label ID="Label2" runat="server" Label="Label" Text="查询条件："></x:Label>
                <x:Label ID="Label3" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
                <x:TwinTriggerBox runat="server" EmptyText="输入要搜索的人员姓名" MaxLength ="20" ShowLabel="false" ID="TriggerBox" ShowTrigger1="false" TabIndex ="1"
                    ShowTrigger2 ="false" >
                </x:TwinTriggerBox>
                <x:Label ID="Label9" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
                <x:Label ID="Label7" runat="server" Label="Label" Text="教学对象："></x:Label>
               <x:DropDownList ID="DropDownListTeachingDegree" ShowLabel="false" AutoPostBack="true" runat="server" Width ="100px" TabIndex ="2">
                    <x:ListItem Text="全部" Value="全部" Selected ="true" />                  
                </x:DropDownList>
                <x:Button ID="Find" Text="搜索" Icon="SystemSearch" OnClick ="Find_Click" Type="Submit"  runat="server">
                </x:Button>
                 <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick ="btnRefresh_Click"   >  
                                    </x:Button> 
                 <x:Button ID="btnAdd" Text="新增人员主讲课程信息" Icon="Add"  runat="server">
                </x:Button>
                <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" OnClick ="btnDelete_Click"  runat="server" ConfirmText="确定删除？" ConfirmTarget="Top" >
                </x:Button>
                <x:Button ID="Btnchange" Text="编辑选中行" Icon="Pencil" OnClick ="Btnchange_Click"  runat="server">
                </x:Button>
            <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick ="btn_Get_Click"   EnableAjax="false" DisableControlBeforePostBack="false">
                                    </x:Button>
           
            </Items>
        </x:Toolbar>
        </Toolbars>
       <%--AllowPaging这是分页功能--%>
                 <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged ="ddlGridPageSize_SelectedIndexChanged" 
                            runat="server">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>
       <Columns>    
          <x:TemplateField Width="30px" >
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" /> 
           <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
            <x:TemplateField Width="30px" Hidden ="true" >
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
            <x:TemplateField Width="80px" HeaderText="姓名">
                    <ItemTemplate>                        
                        <asp:Label ID="Label5" runat="server" Text='<%# UserName(Convert .ToInt32 (DataBinder.Eval(Container.DataItem, "UserInfoID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
       <x:BoundField DataField="ClassName" HeaderText="课程名称" />
                  <x:BoundField DataField="TeachingTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="教学时间" />
                  <x:BoundField DataField="TeachingDegree" HeaderText="教学对象学历" />
       <x:BoundField DataField="Specialty" HeaderText="专业" />
                  <x:BoundField DataField="Grade" HeaderText="班号" />
       <x:TemplateField Width="80px" HeaderText="保密等级">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>  
       </Columns>
       </x:Grid>
       </Items>
       </x:Panel>
         <x:Window ID="WindowAdd" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="新增人员主讲课程信息">
        </x:Window>]
         <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="编辑人员主讲课程信息">
        </x:Window>

        <x:Window ID="Window4" Title="" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Target="Parent" 
            IsModal="True" Width="750px" Height="450px">
        </x:Window>
    </form>
</body>

</html>



