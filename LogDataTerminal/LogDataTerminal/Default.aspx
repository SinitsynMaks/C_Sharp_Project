<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LogDataTerminal.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
	<style>
		div { margin-bottom: 10px;}
		th, td {text-align: left;}
		td {padding-bottom: 5px;}
		th, table {border-bottom: solid thin black;}
		th:last-child, td:last-child {text-align: right;}
		body {font-family: "Arial Narrow", sans-serif;}
	</style>
</head>
<body>
    <form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManagerl" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div>
		<h1>Статистика работы банковских устройств</h1>
		<p>Используйте фильтр для отображения нужной даты</p>
		<table>
			<tr><th>Name</th><th>Data</th><th>Message</th></tr>
			<asp:Repeater ItemType="LogDataTerminal.Models.log" runat="server"
				SelectMethod="GetLogData">
				<ItemTemplate>
					<tr>
						<td><%#: Item.Name %></td>
						<td><%#: Item.Date %></td>
						<td><%#: Item.Message %></td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
    </div>
	<div>
		Filter:
		<select name="filterSelect">
			<asp:Repeater ItemType="System.String"
				SelectMethod="GetNameLog" runat="server">
				<ItemTemplate>
					<option>
						<%# Item %>
					</option>
				</ItemTemplate>
			</asp:Repeater>
		</select>
		<button type="submit">GetReport</button>
	</div>
    </form>
</body>
</html>
