<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Viewer.aspx.cs" Inherits="HtmlAgilityDemo.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%@ Import Namespace="System.Data" %>
	<script type="text/javascript">
	    $(document).ready(function () {
	        $('#coin-slider').coinslider({ width: 900, navigation: true, delay: 5000,effect: 'random'});
	    });
</script>

<asp:Repeater ID="Repeater1" runat="server">
  <HeaderTemplate>
     <div id='coin-slider'>
  </HeaderTemplate>
  <ItemTemplate>
     <a href='<%#Eval("ThumbImageUrl")%>'>
        <img src='<%#Eval("ThumbImageUrl")%>'  width="900" height="400" />
        <span>
           <%#Eval("ImageName")%>
          </span>
      </a>
   </ItemTemplate>
   <FooterTemplate>
     </div>
   </FooterTemplate>
</asp:Repeater>
</asp:Content>
