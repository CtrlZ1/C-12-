﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="R_Pact_LibraryRecord.aspx.cs" Inherits="WDFramework.Report.R_Pact_LibraryRecord" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" style="margin:auto auto;" Width ="924px" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"  Height ="825px">
            <ServerReport ReportPath="/报表项目2/R_Pact_LibraryRecord" ReportServerUrl="http://win-ja7hpm931cq/ReportServer_SA" />
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
