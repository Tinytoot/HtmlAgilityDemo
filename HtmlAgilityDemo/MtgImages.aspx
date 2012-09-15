<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MtgImages.aspx.cs" Inherits="HtmlAgilityDemo.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type = "text/javascript">
    function Check_Click(objRef) {
        //Get the Row based on checkbox
        var row = objRef.parentNode.parentNode;
        if (objRef.checked) {
            //If checked change color to Aqua
            row.style.backgroundColor = "aqua";
        }
        else {
            //If not checked change back to original color
            if (row.rowIndex % 2 == 0) {
                //Alternating Row Color
                row.style.backgroundColor = "#C2D69B";
            }
            else {
                row.style.backgroundColor = "white";
            }
        }

        //Get the reference of GridView
        var GridView = row.parentNode;

        //Get all input elements in Gridview
        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {
            //The First element is the Header Checkbox
            var headerCheckBox = inputList[0];

            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;

    }


    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            //Get the Cell To find out ColumnIndex
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    //If the header checkbox is checked
                    //check all checkboxes
                    //and highlight all rows
                    row.style.backgroundColor = "aqua";
                    inputList[i].checked = true;
                }
                else {
                    //If the header checkbox is checked
                    //uncheck all checkboxes
                    //and change rowcolor back to original
                    if (row.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        row.style.backgroundColor = "#C2D69B";
                    }
                    else {
                        row.style.backgroundColor = "white";
                    }
                    inputList[i].checked = false;
                }
            }
        }
    }


    function MouseEvents(objRef, evt) {
        var checkbox = objRef.getElementsByTagName("input")[0];
        if (evt.type == "mouseover") {
            objRef.style.backgroundColor = "orange";
        }
        else {
            if (checkbox.checked) {
                objRef.style.backgroundColor = "aqua";
            }
            else if (evt.type == "mouseout") {
                if (objRef.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    objRef.style.backgroundColor = "#C2D69B";
                }
                else {
                    objRef.style.backgroundColor = "white";
                }
            }
        }
    }
</script> 


    <h2>Get All Links...</h2>
    <p>
        <b>Enter a URL:</b>
        <asp:TextBox ID="txtUrl" runat="server" Columns="50">http://www.wizards.com/magic/magazine/downloads.aspx?x=mtg/daily/downloads/wallpapers</asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvURL" runat="server" 
            ControlToValidate="txtUrl" ErrorMessage="[Required]" 
            SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regexpUrl" runat="server" 
            ErrorMessage="[Invalid]" ControlToValidate="txtUrl" Display="Dynamic" 
            SetFocusOnError="True" 
            ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
        <br />
        <asp:Button ID="btnGetImages" runat="server" Text="Get Images" 
            onclick="btnGetImages_Click" />
   

   <asp:Panel ID="Panel2" runat="server" Visible="False">
   <div class="slidingDiv">
     </p>

            <asp:Button ID="btnSaveResults" runat="server" 
           onclick="btnSaveResults_Click" Text="Save Results" />

            <p>
             <b>Enter a directory to store images:</b>
                <asp:TextBox ID="txtDownloadFolder" runat="server" Columns="50">C:\Images\</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtUrl" ErrorMessage="[Required]" 
                    SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="[Invalid]" ControlToValidate="txtUrl" Display="Dynamic" 
                    SetFocusOnError="True" 
                    ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                <br />
                <asp:Button ID="btnDnload" runat="server" Text="Dnload" onclick="btnDnloadImages_Click" />
       </p>
    </div>
    </asp:Panel>

    
    <p>
        <asp:Label ID="lblSummary" runat="server"></asp:Label>
    </p>
    <div>
        <asp:GridView ID="ImageGrid"  runat="server"  HeaderStyle-CssClass = "header"
            AutoGenerateColumns = "False" Font-Names = "Arial"
            OnRowDataBound = "RowDataBound"
            Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" >
<AlternatingRowStyle BackColor="#C2D69B"></AlternatingRowStyle>
            <Columns>
               <asp:TemplateField>
                  <HeaderTemplate>
                       <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
                  </HeaderTemplate>
                  <ItemTemplate>
                       <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:ImageField DataImageUrlField="ThumbImageUrl" HeaderText="Image" ControlStyle-Width="90" ControlStyle-Height = "80">
<ControlStyle Height="80px" Width="90px"></ControlStyle>
                   </asp:ImageField>
                <asp:TemplateField HeaderText="Url">
                    <EditItemTemplate>
                        <asp:TextBox ID="EditThumbImageUrl" runat="server" Text='<%# Bind("ThumbImageUrl") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ThumbImageUrl" runat="server" Text='<%# Bind("ThumbImageUrl") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <p> There were no images found on the web page...</p>
            </EmptyDataTemplate>

<HeaderStyle CssClass="header"></HeaderStyle>
        </asp:GridView>
    </div>
</asp:Content>



