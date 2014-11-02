<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Category.ascx.cs" Inherits="Control_Category" %>
        <asp:Panel ID="PanType" Width="100%" runat="server" >
<%--            <table width="100%">
                <tr>
                    <td style="width:60px">--%>
                        <asp:LinkButton ID="lbtnAllCalling" runat="server" OnClick="lbtnAllCalling_Click" Visible="false" ></asp:LinkButton>
<%--                    </td>
                    <td align="left">--%>
                        <asp:Repeater ID="callingTitleList" runat="server" 
                OnItemCommand="callingTitleList_ItemCommand" 
                onitemdatabound="callingTitleList_ItemDataBound" >
                            <ItemTemplate>
                                <asp:Label ID="lblCallingLine" Text=">" Visible="false" runat="server"></asp:Label><asp:LinkButton ID="lbtnCallingLink" Text='<%# Eval("NAME") %>' CommandName='<%# Eval("CATEGORYID") %>' runat="server"></asp:LinkButton>
                            </ItemTemplate>
                       </asp:Repeater>
<%--                    </td>
                </tr>
            </table>
           <asp:DataList ID="dlistCalling" runat="server"  OnItemCommand="dlistCalling_ItemCommand">
                <ItemTemplate>
                    <asp:Panel ID="Pancallinglin" runat="server">
                       <td >    
                            <asp:Label ID="lbline" runat="server" CssClass="tdButtomLineR">&nbsp</asp:Label>                                
                        </td>  
                    </asp:Panel> 
                     <td width="5px">                                    
                    </td>                                
                    <td align="left">
                        <asp:LinkButton ID="lbtnCallingLink" Text='<%# Eval("NAME") %>' CommandName='<%# Eval("CATEGORYID") %>' runat="server"></asp:LinkButton>
                    </td>                    
                </ItemTemplate>
            </asp:DataList>--%>
            <table>
                <tr>
                    <td>
                        <asp:Repeater ID="rptCalling" runat="server" 
                            OnItemCommand="rptCalling_ItemCommand" 
                            onitemdatabound="rptCalling_ItemDataBound">
                            <ItemTemplate>
                            <asp:Label ID="lblCallingLine" Text="|" Visible="false" runat="server"></asp:Label>
                            <asp:LinkButton ID="lbtnCallingLink" Text='<%# Eval("NAME") %>' CommandName='<%# Eval("CATEGORYID") %>' runat="server"></asp:LinkButton>
                            
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <asp:Panel ID="PanLine" runat="server">
            <tr>
                <td>
                    <table><tr><td class="headbgSearchL"></td><td class="headbgSearchR"></td></tr></table>
                </td>
            </tr>
        </asp:Panel>
        
        <asp:Panel ID="PanAddress" runat="server">
           <asp:LinkButton ID="lbtnAllCountry" runat="server" CommandName="add" OnClick="lbtnAllCountry_Click" Visible="false" ></asp:LinkButton>
           <asp:Repeater ID="countryTitleList" runat="server" 
                OnItemCommand="countryTitleList_ItemCommand" 
                onitemdatabound="countryTitleList_ItemDataBound" >
                <ItemTemplate>
                    <asp:Label ID="lblAddressLine" Text=">" Visible="false" runat="server"></asp:Label><asp:LinkButton ID="lbtnCountryLink" Text='<%# Eval("NAME") %>'  CommandName='<%# Eval("MA_ORDERID") %>' runat="server" ></asp:LinkButton>
                </ItemTemplate>
           </asp:Repeater>
<%--             <asp:DataList ID="dlistCountry" runat="server" RepeatColumns="6" OnItemCommand="dlistCountry_ItemCommand">
                <ItemTemplate>
                    <td >
                        <asp:Label ID="lbline" runat="server" CssClass="tdButtomLineR">&nbsp</asp:Label>   
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="lbtnCountryLink" Text='<%# Eval("NAME") %>'  CommandName='<%# Eval("MA_ORDERID") %>' runat="server"></asp:LinkButton>
                    </td>                         
                </ItemTemplate>
            </asp:DataList>--%>
            <table>
                <tr>
                    <td>
                        <asp:Repeater ID="rptCountry" runat="server" 
                            OnItemCommand="rptCountry_ItemCommand" 
                            onitemdatabound="rptCountry_ItemDataBound">
                            <ItemTemplate>
                            <asp:Label ID="lblCallingLine" Text="|" Visible="false" runat="server"></asp:Label>
                            <asp:LinkButton ID="lbtnCountryLink" Text='<%# Eval("NAME") %>'  CommandName='<%# Eval("MA_ORDERID") %>' runat="server"></asp:LinkButton>
                            
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <%--<asp:Panel ID="PanLine1" runat="server">
            <tr>
                <td>
                    <table><tr><td class="headbgSearchL"></td><td class="headbgSearchR"></td></tr></table>
                </td>
            </tr>
        </asp:Panel>--%>
        
        <asp:Panel ID="PanPost" runat="server" Visible="false">
           <asp:LinkButton ID="lbtnAllPost" runat="server" CommandName="add" OnClick="lbtnAllPost_Click" Visible="false" ></asp:LinkButton>
           <asp:Repeater ID="postTitleList" runat="server" 
                OnItemCommand="postTitleList_ItemCommand" 
                onitemdatabound="postTitleList_ItemDataBound" >
                <ItemTemplate>
                    <asp:Label ID="lblPostLine" Text=">" Visible="false" runat="server"></asp:Label><asp:LinkButton ID="lbtnPostLink" Text='<%# Eval("NAME") %>'  CommandName='<%# Eval("CATEGORYID") %>' runat="server" ></asp:LinkButton>
                </ItemTemplate>
           </asp:Repeater>
           
            <table>
                <tr>
                    <td>
                        <asp:Repeater ID="rptPost" runat="server" 
                            OnItemCommand="rptPost_ItemCommand" 
                            onitemdatabound="rptPost_ItemDataBound">
                            <ItemTemplate>
                            <asp:Label ID="lblPostLine" Text="|" Visible="false" runat="server"></asp:Label>
                            <asp:LinkButton ID="lbtnPostLink" Text='<%# Eval("NAME") %>'  CommandName='<%# Eval("CATEGORYID") %>' runat="server"></asp:LinkButton>

                            </ItemTemplate>
                            
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <asp:Panel ID="panIndustry" runat="server" Visible="false">
           <asp:LinkButton ID="lbtnAllIndustry" runat="server" CommandName="add" 
                Visible="false" onclick="lbtnAllIndustry_Click" ></asp:LinkButton>
           <asp:Repeater ID="industryTitleList" runat="server" 
                onitemcommand="industryTitleList_ItemCommand" onitemdatabound="industryTitleList_ItemDataBound" 
                 >
                <ItemTemplate>
                    <asp:Label ID="lblIndustryLine" Text=">" Visible="false" runat="server"></asp:Label><asp:LinkButton ID="lbtnIndustryLink" Text='<%# Eval("NAME") %>'  CommandName='<%# Eval("MA_ORDERID") %>' runat="server" ></asp:LinkButton>
                </ItemTemplate>
           </asp:Repeater>
           
            <table>
                <tr>
                    <td>
                        <asp:Repeater ID="rptIndustry" runat="server" 
                            onitemcommand="rptIndustry_ItemCommand" onitemdatabound="rptIndustry_ItemDataBound" 
                            >
                            <ItemTemplate>
                            <asp:Label ID="lblIndustryLine" Text="|" Visible="false" runat="server"></asp:Label>
                            <asp:LinkButton ID="lbtnIndustryLink" Text='<%# Eval("NAME") %>'  CommandName='<%# Eval("CATEGORYID") %>' runat="server"></asp:LinkButton>

                            </ItemTemplate>
                            
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </asp:Panel>