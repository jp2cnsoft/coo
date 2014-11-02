<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrgSortControl.ascx.cs" Inherits="Control_OrgSortControl" %>
	<table class="cm_box" cellspacing="1">
		<tr>
	       <td align="left" style="width:80px;">
               <asp:Label ID="lblCalling" runat="server" meta:resourcekey="R00010"></asp:Label>
           </td>
           <td align="left">
			  <table>
				    <tr>
		               <td align="left" style="width:80px;">
                           <asp:LinkButton ID="lbtnAllCalling" runat="server" meta:resourcekey="R00020" OnClick="lbtnAllCalling_Click"></asp:LinkButton>

                       </td>
                       <td align="left">
                           <asp:DataList ID="callingTitleList" runat="server" OnItemCommand="callingTitleList_ItemCommand" RepeatColumns="8">
                                <ItemTemplate>
                                    > <asp:HiddenField ID="hidCallingID" Value='<%# Eval("ID") %>' runat="server" /><asp:LinkButton ID="lbtnCallingLink" Text='<%# Eval("NAME") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                           </asp:DataList>
                       </td>
                   </tr>
               </table>
           </td>
		</tr>
		<tr>
			<td class="tdButtomLineS">
			</td>
			<td class="tdButtomLineS" align="left">
                <asp:DataList ID="dlistCalling" runat="server" RepeatColumns="10" OnItemDataBound="dlistCalling_ItemDataBound" OnItemCommand="dlistCalling_ItemCommand">
                    <ItemTemplate>
                        <td align="left">
                            <asp:HiddenField ID="hidCallingID" Value='<%# Eval("MA_ORDERID") %>' runat="server" /><asp:LinkButton ID="lbtnCallingLink" Text='<%# Eval("NAME") %>' runat="server"></asp:LinkButton><asp:Label ID="lblCallingCount" Text='<%# Eval("COUNT") %>' Visible="false" runat="server"></asp:Label>
                            <asp:HiddenField ID="hidCallingName" Value='<%# Eval("NAME") %>' runat="server" />
                        </td>
                        <td style="width:20px">
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </td>
		</tr>

		<tr>
			<td align="left" style="width:80px;">
				<asp:Label ID="lblClime" runat="server" meta:resourcekey="R00030"></asp:Label>
			</td>
			<td align="left">
			  <table>
				    <tr>
			            <td align="left" style="width:80px;">
				            <asp:LinkButton ID="lbtnAllCountry" runat="server" meta:resourcekey="R00040" OnClick="lbtnAllCountry_Click"></asp:LinkButton>
		                </td>
		                 <td align="left">
                           <asp:DataList ID="countryTitleList" runat="server" RepeatColumns="8" OnItemCommand="countryTitleList_ItemCommand">
                                <ItemTemplate>
                                    > <asp:HiddenField ID="hidCountryID" Value='<%# Eval("ID") %>' runat="server" /><asp:LinkButton ID="lbtnCountryLink" Text='<%# Eval("NAME") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                           </asp:DataList>
                       </td>
                   </tr>
               </table>
           </td>
		</tr>
		<tr>
			<td class="tdButtomLineS">
			</td>
			<td align="left" colspan="2">
			    <asp:DataList ID="dlistCountry" runat="server" RepeatColumns="10" OnItemDataBound="dlistCountry_ItemDataBound" OnItemCommand="dlistCountry_ItemCommand">
                    <ItemTemplate>
                        <td align="left">
                            <asp:HiddenField ID="hidCountryID" Value='<%# Eval("MA_ORDERID") %>' runat="server" /><asp:LinkButton ID="lbtnCountryLink" Text='<%# Eval("NAME") %>' runat="server"></asp:LinkButton><asp:Label ID="lblCountryCount" Text='<%# Eval("COUNT") %>' Visible="false" runat="server"></asp:Label>
                            <asp:HiddenField ID="hidCountryName" Value='<%# Eval("NAME") %>' runat="server" />
                        </td>
                        <td style="width:20px">
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </td>
		</tr>
	</table>