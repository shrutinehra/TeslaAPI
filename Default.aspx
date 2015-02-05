<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>
<meta />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #result {
            width: 438px;
            height: 131px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%">
        <div   id="login" style="background-color: lightgray;width:40%;float: left;height: 100%;padding: 15px;">
              UserName :       <asp:textbox runat="server" ID="Label3"></asp:textbox>
        <br/>
            <br/>
    Password : &nbsp;    <asp:textbox runat="server" TextMode="Password" ID="Label4"></asp:textbox>
        <br/>
            <br/>
     Mock    :   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox runat="server" ID="mockAPI" AutoPostBack="true"/>
        <br/>
            <br/>
            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="GET RESULT"/>
        </div>
            </div>
        <div style="padding-top: 10%">
    <div  id="result" style="background-color: lightgray;width: 35%;float: left;height: 100%;padding: 15px;">
        <h2>Climate Status Of Vehicle</h2>
   Inside Temp :       <asp:Label runat="server" ID="insideTemp"></asp:Label>
        <br/>
    Outside Temp :     <asp:Label runat="server" ID="outsideTemp"></asp:Label>
        <br/>
     AC Status    :    <asp:Label runat="server" ID="acStatus"></asp:Label>
        <br/>
        Fan Speed :    <asp:Label runat="server" ID="fanSpeed"></asp:Label>
    </div>
            <div  id="Div1" style="background-color: lightgray; float: right;width: 35%;padding: 15px;">
                   <h2>Battery Status Of Vehicle</h2>
                  Charging state :       <asp:Label runat="server" ID="lblCharging"></asp:Label>
        <br/>
    Battery Level :     <asp:Label runat="server" ID="lblBatteryLevel"></asp:Label> %
        <br/>
        Range estimated from recent driving :    <asp:Label runat="server" ID="lblRange"></asp:Label> miles
                </div>
              <div style="background-color: lightgray;width: 30%;float: right;margin-top: 5%;padding: 15px;">
              <h2>GPS Location</h2> 
              Latitude :       <asp:Label runat="server" ID="Label1"></asp:Label>
        <br/>
              Longitude :     <asp:Label runat="server" ID="Label2"></asp:Label>
        </div>
            </div>
      
    </form>
</body>
</html>
