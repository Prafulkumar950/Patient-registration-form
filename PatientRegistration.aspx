<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientRegistration.aspx.cs" Inherits="PatientRegistration" MaintainScrollPositionOnPostback="true"   %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patient Registration By Praful</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Container">
        
         <div class="img">
             <img src="icon.png"/>
         </div>

        <asp:Label ID="lblName" runat="server" Text="Name :"  />
        <asp:TextBox ID="txtName" runat="server" placeholder="Enter your Name" style="width: 60%;  padding: 5px; border: 2px solid black; border-radius: 10px; background: transparent; font-size: 16px; color:black; margin: 5px 38px;  " ></asp:TextBox>
        <br/>

        <asp:Label ID="lblAge" runat="server" Text="Age :" />
        <asp:TextBox ID="txtAge" runat="server" placeholder="Enter your Age" style="width: 60%; padding: 5px; border: 2px solid black ; border-radius: 10px; background: transparent; font-size: 16px; color:black; margin: 5px 52px;  " ></asp:TextBox>
        <br/>

        <asp:Label ID="lblAddress" runat="server" Text="Address :" />
        <asp:TextBox ID="txtAddress" runat="server" placeholder="Enter your Address" style="width: 60%; padding: 5px; border: 2px solid black ; border-radius: 10px; background: transparent; font-size: 16px; color:black; margin: 5px 22px;  "></asp:TextBox>
        <br/>

        <asp:Label ID="lblContact" runat="server" Text="Contact No :" />
        <asp:TextBox ID="txtContact" runat="server" placeholder="Enter your Contact Number" style="width: 60%; padding: 5px; border: 2px solid black;  border-radius: 10px; background: transparent; font-size: 16px; color:black; margin: 5px 0px;  " ></asp:TextBox>
        <br/>

        <asp:Label ID="lblDoctor" runat="server" Text="Doctor :"  ></asp:Label>
        <asp:DropDownList ID="ddlDoctor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged" style="width: 63%; padding: 5px; border: 2px solid black; cursor:pointer; border-radius: 10px; background: transparent; font-size: 16px; color:black; margin: 5px 34px; "> </asp:DropDownList>
        <br/>

        <asp:Label ID="lblTreatment" runat="server" Text="Treatment :"  ></asp:Label>
        <asp:DropDownList ID="ddlTreatment" runat="server"  style="width: 63%; padding: 5px; border: 2px solid black; cursor:pointer; border-radius: 10px; background: transparent; font-size: 16px; color:black; margin: 5px 9px; "> </asp:DropDownList>
        <br/>

        <%--<asp:Label ID="lblTreatment" runat="server" Text="Treatment :"  ></asp:Label>
        <asp:DropDownList ID="ddlTreatment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTreatment_SelectedIndexChanged"  style="width: 63%; padding: 5px; border: 2px solid black; border-radius: 10px; background: transparent; font-size: 16px; color:black; margin: 5px 9px; "> </asp:DropDownList>
        <br/>--%>

        <asp:Label ID="lblHospital" runat="server" Text="Hospital :"  ></asp:Label>
        <asp:DropDownList ID="ddlHospital" runat="server"  style="width: 63%; padding: 5px; border: 2px solid black; cursor:pointer; border-radius: 10px; background: transparent; font-size: 16px; color:black; margin: 5px 23px;  "> </asp:DropDownList>
        <br/>
       
        <asp:HiddenField ID="HiddenF" runat="server" />
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <asp:Button ID="btnRegister" runat="server" Text="SUBMIT" OnClick="btnRegister_Click"  style="width: 40%; padding: 8px; letter-spacing:4px;  border-radius: 10px;  font-weight:bold; transition:.5s;   font-size: 16px; color:black; margin: 5px  120px; margin-top:40px;   " />        
       
        <br/>
        </div>
        <br />
         <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" style=" background-color:red; font-weight:bold; color:black;   border: 1px solid black; border-radius: 3px; text-decoration:none;  margin-left:83%; padding:7px;" />
       
       <%-- <div style="margin-left:82%; margin-bottom:1%;">--%>
            <asp:Button ID="btnDltSelected" runat="server" Text="DELETE " OnClick="btnDltSelected_Click"   style=" background-color:red; font-weight:bold; color:black;   border: 1px solid black; border-radius: 3px; text-decoration:none;  margin-left:92%; padding:7px;" />
        


         <asp:GridView ID="griddetail" runat="server" AutoGenerateColumns="false"  RowStyle-HorizontalAlign="Center" CellPadding="10"  BackColor="LightGrey" AlternatingRowStyle-BackColor="#00ccff"  AllowPaging="true" PageSize="5" OnPageIndexChanging="griddetail_PageIndexChanging" HeaderStyle-BackColor="#00ff00" Width="100%" DataKeyNames="Name, Age, Address, Contact,Id"  >
             <Columns>
               <asp:TemplateField HeaderText="S.No." HeaderStyle-CssClass="Header2">
                   <ItemTemplate>
                       <asp:Label ID="labelSNo" runat="server" Text="<%#Container.DataItemIndex+1%>"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Name" >
                        <ItemTemplate>
                            <asp:Label ID="labelName" runat="server"  Text='<%# Eval("Name") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
               <asp:TemplateField HeaderText="Age"  >
                        <ItemTemplate>
                            <asp:Label ID="labelAge" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
               <asp:TemplateField HeaderText="Address"  >
                        <ItemTemplate>
                            <asp:Label ID="labelAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                <asp:TemplateField HeaderText="Contact" >
                        <ItemTemplate>
                            <asp:Label ID="labelContact" runat="server"  Text='<%# Eval("Contact") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

               <asp:TemplateField HeaderText="Doctor" >
                        <ItemTemplate>
                            <asp:Label ID="labelDoctor" runat="server"  Text='<%# Eval("Doctor") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
               <asp:TemplateField HeaderText="Treatment"  >
                        <ItemTemplate>
                            <asp:Label ID="labelTreatment" runat="server" Text='<%# Eval("Treatment") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

               <asp:TemplateField HeaderText="Hospital"  >
                        <ItemTemplate>
                            <asp:Label ID="labelHospital" runat="server" Text='<%# Eval("Hospital") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

              <asp:TemplateField HeaderText="Operation">
                   <ItemTemplate>
                       <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  style=" background-color:red; font-weight:bold; color:black; letter-spacing:1px;  border: 1px solid black; border-radius: 3px; text-decoration:none;" />
                       <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"  style=" background-color:blue; font-weight:bold; color:black; letter-spacing:1px;  border: 1px solid black; border-radius: 3px; text-decoration:none;"  />
                   </ItemTemplate>
               </asp:TemplateField>

                 <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged"  AutoPostBack="true" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server"  OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
          
           </Columns>  
       </asp:GridView>     
    </form>

        <style>
            body {
                font-family: sans-serif;
                background:url(BCKPAT.jpg)no-repeat;
                background-position: center;
                background-size:cover;
            }

            .Container {
                width: 100%;
                max-width: 400px;
                margin: 80px 200px;
                padding: 40px;
                height:100%;
                background: linear-gradient(255, 255, 255, 0.5);
                border: 2px solid black;
                border-radius: 20px;
                backdrop-filter: blur(50px);
                box-shadow: 0px 4px 20px black;  
            }

            .container-img {
                border-radius: 20px;
                width: 100%;
                padding: 5px 0px;
                margin: 5px 0px;
            }

            .img {
                display: flex;
                justify-content: center;
                margin-bottom: 30px;
                margin-top:-10px;
            }
             
            .img img {
                max-width: 100%;
                height: 50px;
                border-radius: 10px;
            }

            #btnRegister:hover {
                background-color:lightgreen;
                cursor:pointer;
            }
        </style>
</body>
</html>
