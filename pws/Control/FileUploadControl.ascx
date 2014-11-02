<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUploadControl.ascx.cs" Inherits="Pages_FileUploadControl" %>

<script type="text/javascript">
var I_FILE_MAX = 100;
var iAttachArray = new Array();
iAttachArray[0] = 0;
for(var i = 1;  i < I_FILE_MAX; i++) iAttachArray[i] = 0;
//取得上传控件索引
function GetFileButtonIndex()
{
	for(var i = 0;  i < I_FILE_MAX && iAttachArray[i] == 1; i++);
	return i;
}
//加入新上传控件
function AddFileTableRow(tableID,hiddenTable,upFileName,lang)
{
	var iFileButtonIndex = GetFileButtonIndex();
	var oTable = document.getElementById(tableID);
	var oRow   = oTable.insertRow(oTable.rows.length); 
	var aCells = oRow.cells; 
	var sFileButtonName  = upFileName + "|upPic" + iFileButtonIndex;
	var sFilePreName = upFileName + "|prePic" + iFileButtonIndex;
	var Cell_1 = oRow.insertCell(aCells.length);
	Cell_1.innerHTML = "<input type=\"file\" class=\"upfile\" name='" + sFileButtonName + "' id='" + sFileButtonName  + "'"
	              //+ " style='width:200px;'"
	                + " onChange=UpFileChange('"+ tableID +"','"+ hiddenTable +"','"+ sFileButtonName +"','"+ sFilePreName +"') "
	                + " onkeydown=\"event.returnValue=false;\" onpaste=\"return false;\" />"
                    + "<span id='"+ sFilePreName +"'></span>"
                    + "<a href=\"javascript:void(0);\""
                    + " onClick=\"DeleteTR('" + tableID + "',document.all ? this.parentElement.parentElement : this.parentNode.parentNode, " + iFileButtonIndex + ", '"+ hiddenTable + "')\">"
                    + " <img src=\"../../Images/<%=this.imgdel %>_delete_mc.png\" border=\"0\" Width=\"80px\" Height=\"22px\" />";
	iAttachArray[iFileButtonIndex] = 1;
	var hidTable = document.getElementById(hiddenTable);
	hidTable.value = oTable.innerHTML;
	return true;
}
//删除tr
function DeleteTR(TableName,oRow,iButtonIndex,hiddenTable)
{
	var oTable = document.getElementById(TableName);
	if(oTable.rows.length > 0){
	    oTable.deleteRow(oRow.rowIndex);
	    iAttachArray[iButtonIndex] = 0;
	    document.getElementById(hiddenTable).value = oTable.innerHTML;
	}
}
//选择文件
function UpFileChange(tableID,hiddenTable,upfile,prePic)
{
    var oTable = document.getElementById(tableID);
    var showPrePic = GetFileName(document.getElementById(upfile));
    document.getElementById(prePic).innerHTML=showPrePic;
	document.getElementById(hiddenTable).value = oTable.innerHTML;
}
//选择单文件
function UpFileSingleChange(upfile,prePic)
{
    var showPrePic = GetFileName(document.getElementById(upfile));
    var picDelete = "<a href=\"javascript:void(0);\" onclick=\"DeleteSingle('" + upfile + "','" + prePic + "')\">"
                  + "<img src=\"../../Images/<%=this.imgdel %>_delete_mc.png\" border=\"0\" Width=\"80px\" Height=\"22px\" /></a>";
    document.getElementById(prePic).innerHTML = showPrePic + picDelete;
}
//清除单个文件上传结果
function DeleteSingle(upfile,prePic)
{
    var file = document.getElementById(upfile);
    //清空上传控件
    file.outerHTML=file.outerHTML;
    //清空显示文件
    document.getElementById(prePic).innerHTML = "";
}
//取得文件名
function GetFileName(file)
{
    var pos = file.value.lastIndexOf("\\");
    var fileName = file.value.substring(pos+1);
    if(fileName.length > 16)
        return fileName.substr(0,16) + "..."
    return fileName;
}
</script>

<asp:Literal ID="ltlAdd" runat="server"></asp:Literal>
<asp:Label ID="lblRemark" Font-Size="12px" CssClass="leaderFont" runat="server"></asp:Label>
<asp:Label ID="errFile" runat="server" CssClass="errFont"></asp:Label>
<table width="100%">
    <tr>
        <td>
        <asp:UpdatePanel ID="updatePanlFile" runat="server">
            <ContentTemplate>
                <asp:DataList ID="dataListFile" runat="server" OnItemCommand="dataListFile_ItemCommand">
                    <ItemTemplate>
                    <tr>
                        <td style="height:22px; width:190px;">
                            <asp:HiddenField ID="hidImgId" Value='<%# Eval("ID") %>' runat="server" /><asp:HyperLink ID="hlinkFile" Text='<%# Eval("FILENAME") %>' NavigateUrl='<%# Eval("FILEURL") %>' Target="_blank" runat="server"></asp:HyperLink>
                            <%--<span style="width:3px;"></span>--%>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgDel" SkinId="SkinImgbtnDeleteMC" runat="server" /><span style="width:5px;"></span>
                        </td>
                    </tr>
                    </ItemTemplate>
                </asp:DataList>
            </ContentTemplate>
        </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:Literal ID="ltlTable" runat="server"></asp:Literal>
<asp:Literal ID="lblReShow" runat="server"></asp:Literal>
<input type="hidden" id="hidReShow" runat="server" />
