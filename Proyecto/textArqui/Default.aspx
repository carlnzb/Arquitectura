<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="textAnalizer._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div  style="margin-left:40%; margin-top:5%;" class="col-md-9 space-up">
            <asp:Panel ID="panelMsjExito" runat="server" CssClass="exito" Visible="false">
                <asp:Image ID="imgInd" runat="server" ImageUrl="~/img/ok.png"/>
                <asp:Label ID="lbInd" runat="server" Text="" Font-Bold="true"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="panelMsjError" runat="server" CssClass="error" Visible="false">
                <asp:Image ID="imgIndError" runat="server" ImageUrl="~/img/error_icon.png"/>
                <asp:Label ID="lbIndError" runat="server" Text="" Font-Bold="true"></asp:Label>
            </asp:Panel>
           
            <asp:Panel ID="panelMsjLoader" runat="server" CssClass="error" Visible="false">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/img/loader.png"/>
                <asp:Label ID="lbIndLoader" runat="server" Text="" Font-Bold="true"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="panelMsjAdv" runat="server" CssClass="alerta" Visible="false">
                <asp:Image ID="imgIndAdv" runat="server" ImageUrl="~/img/warning_icon.png"/>
                <asp:Label ID="lbIndAdv" runat="server" Text="" Font-Bold="true"></asp:Label>
            </asp:Panel>
        </div>

    <div style="margin-left:3%; text-align:center " class="jumbotron">
        <h1>Bienvenidos al Sistema </h1>
            
        <div class="row">
             <asp:RadioButton id="rd_secuencial" style="margin-left:26%; margin-top:+4%;" class="col-md-4" Text="Secuencial" Checked="True" GroupName="RadioGroup1" runat="server" /><br />
             
             <asp:RadioButton id="rd_paralelo" style="margin-left:40%; margin-top:-5%;" class="col-md-4" Text="Paralelo" GroupName="RadioGroup1" runat="server"/><br />
         
        </div>
        <div  style="">
                    <asp:DropDownList id="kindOperation_DD"
                    AutoPostBack="True"
                    runat="server">

                  <asp:ListItem Selected="True" Value=1> Encriptar Texto</asp:ListItem>
                  <asp:ListItem Value=0> Desencriptar Texto </asp:ListItem>

               </asp:DropDownList>
        </div>
    </div>

    <div style="height:40%; margin-top:10%;" class="row">
        <div style="margin-left:15%; margin-top:-10%;" class="col-md-4">
            <asp:TextBox id="initialData_txt" placeholder="Inicial data here..." style="width:100%; margin-left=100%;" TextMode="multiline" Columns="50" Rows="5" runat="server" />            
        </div>
        <div style="margin-left:50%; margin-top:-10%;" class="col-md-4">
            <asp:TextBox id="resultData_txt" placeholder="Results" Enabled="false" style="width:100%; margin-left=100%;" TextMode="multiline" Columns="50" Rows="5" runat="server" />            
        </div>
        <asp:LinkButton id="linkClean" style="margin-left:16.5%;" runat="server" Text="<span class='glyphicon glyphicon-erase'></span> Limpiar" OnClick="cleanData_Click" CssClass="btn btn-success"/>
        <asp:LinkButton id="linkConsultar" style="margin-left:23%;" runat="server" Text="<span class='glyphicon glyphicon-search'></span> Obtener" OnClick="startProcess_Click" CssClass="btn btn-success"/>
        
    </div>

</asp:Content>
