<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MutilControl.ascx.cs" Inherits="MutilControl" %>
<script type="text/javascript" src="../../Res/jquery/jquery-1.3.1.js"></script>
    <script type="text/javascript">

        var LastKindId = "<%=this.MA_ORDERID %>";

        var lastParentId = "";

        var kindList = Array();

        var max = 0;
        
        var Kinds = function (){
            this.id,
            this.items
        }
        
    
        // 按指定父类ID返回所有子类
        function SelKind(parentId, i) {

            if ("-1" == parentId || (3 == i && -1 != document.getElementById("KindId").value.indexOf(parentId))) {
                return;
            }
            
            clear(i);

            // 判断返回数据结果，如果，返回数据结果的记录数为“0”说明是，叶节点，设置表单元素KindId
            if (max != 0 && max == i) {

                document.getElementById(parentId).style.color = '#d9d9d9';
                
                var kinds0 = document.getElementById($('#Kinds0').val()).text;
                var kinds1 = document.getElementById($('#Kinds1').val()).text;
                var kinds2 = document.getElementById($('#Kinds2').val()).text;
                var kinds3 = null != $('#Kinds3').val() ? document.getElementById($('#Kinds3').val()).text : "";

                kinds3 = null == kinds3 ? "" : kinds3;

                var name = "";

                if ("" == kinds3) {
                    name = kinds0 + '&gt;' + kinds1 + '&gt;' + kinds2;
                } else {
                    name = kinds0 + '&gt;' + kinds1 + '&gt;' + kinds2 + '&gt;' + kinds3;
                }

                // 判断是否是重复添加营经类别
                if (-1 == document.getElementById('SelShowKindList').innerHTML.indexOf(name)) {
    
                    var lineWidth = 40;

                    var content = name;


                    while (-1 != content.indexOf('&gt;')) {
                        content = content.replace('&gt;', '@');
                    }

                    while (-1 != content.indexOf(',')) {
                        content = content.replace(',', '，');
                    }

                    while (-1 != content.indexOf(')')) {
                        content = content.replace(')', '）');
                    }

                    while (-1 != content.indexOf('(')) {
                        content = content.replace('(', '（');
                    }


                    if (content.length < lineWidth) {

                        if (lineWidth - content.length < 4) {
                            for (var x = content.length; x < lineWidth; x++) {
                                content += "…";
                            }
                            content += "<br>　";

                            for (var x = 5; x < lineWidth; x++) {
                                content += "…";
                            }
                        }
                        else {
                            for (var x = content.length + 4; x < lineWidth; x++) {
                                content += "…";
                            }
                            content += "&nbsp;";
                        }
                    } else {

                        var start = "";

                        var index = 0;

                        for (var x = 0; x < content.length / lineWidth; x++) {

                            var sub = content.substring(x * lineWidth, lineWidth) + "<br>";

                            if (0 < x) {
                                sub = "　" + sub;
                            }

                            if (sub.length == lineWidth + 4) {
                                start += sub;
                                index++;
                            }
                        }

                        var end = content.substring(index * lineWidth);


                        if (lineWidth - end.length < 4) {
                            for (var x = end.length; x < lineWidth; x++) {
                                end += "…";
                            }
                            end += "<br>";

                            end = "　" + end;

                            for (var x = 4; x < lineWidth; x++) {
                                end += "…";
                            }
                        }
                        else {
                            end = "　" + end;
                            for (var x = end.length + 4; x < lineWidth; x++) {
                                end += "…";
                            }
                        }

                        content = start + end;
                    }
                    //content = content.replace("TOP", "ＴＯＰ");

                    while (-1 != content.indexOf('@')) {
                        content = content.replace('@', ' &gt;');
                    }

                    // 包函标签，方便删除
                    var item = '<p id=' + parentId + '><img src="../../Images/remove1.png" border="0" align="top" />' + content + '[ <a href="javascript:void(0)" onclick=RemoveItem(this,"' + parentId + '","' + name + '")>删除</a> ]</p>';
                    
                    // 将叶节点添加到选择列表中
                    $('#SelShowKindList').append(item);
                }
                
                if (-1 == $('#KindId').val().indexOf(parentId + "=" + name + '@')) {
                    // 设置表单元素KindId
                    $('#KindId').val($('#KindId').val() + parentId + "=" + name + '@');
                }

                return;
            }

            for (var x = 0; x < kindList.length; x++) {
                var k = kindList[x];
                if (parentId == k.id) {
                    var sel = document.getElementById("Kinds" + (i + 1));
                    sel.style.display = 'inline';
                    sel.options[0] = new Option("", "-1");
                    // 循环返回结果
                    for (var x = 1; x <= k.items.length; x++) {
                        // 获得集合中的一个对象
                        var zm = k.items[x - 1];
                        sel.options[x] = new Option(zm.NAME, zm.MA_ORDERID);
                        sel.options[x].title = zm.NAME;
                        sel.options[x].id = zm.MA_ORDERID;
                        if (-1 != document.getElementById('KindId').value.indexOf(zm.MA_ORDERID)) {
                            sel.options[x].style.color = '#d9d9d9';
                        }
                    }
                    return;
                }
            }

            $.ajax({
                // 设置传送数据
                data: '{parentId : "' + parentId + '" }',
                // 设置返回数据的类型
                dataType: 'json',
                // 请求方式
                type: 'post',
                // 设置请求容器类型
                contentType: 'application/json;UTF-8',
                // 设置请求路径
                url: '<%=this.RequestURL %>',
                // 设置是否缓存
                cache: true,
                // 请求成功回调方法
                success: function(json) {

                    // 清楚列表
                    $('#ShowKindList').html("");

                    if (undefined != json.d && json.d.length != 0) {
                        var sel = document.getElementById("Kinds" + (i + 1));
                        sel.style.display = 'inline';
                        sel.options[0] = new Option("<asp:Literal runat='server' Text='<%$ Resources:GPR, PLASECHOOSE %>'></asp:Literal>", "-1");

                        // 循环返回结果
                        for (var x = 1; x <= json.d.length; x++) {
                            // 获得集合中的一个对象
                            var zm = json.d[x - 1];
                            sel.options[x] = new Option(zm.NAME, zm.MA_ORDERID);
                            sel.options[x].title = zm.NAME;
                            sel.options[x].id = zm.MA_ORDERID;
                            if (-1 != document.getElementById('KindId').value.indexOf(zm.MA_ORDERID)) {
                                sel.options[x].style.color = '#d9d9d9';
                            }
                        }

                        var k = new Kinds();

                        k.id = parentId;
                        k.items = json.d;

                        kindList.push(k);

                    } else {

                        max = i;

                        SelKind(parentId, i);
                    }

                },
                // 设置请求失败回调方法
                error: function(xml, status) {
                    // 打印错误信息
                    $('#ShowKindList').html(xml.responseText);
                },
                // 设置请求前的回调方法
                beforeSend: function(xml) {
                    // 设置容器类型
                    xml.setRequestHeader("Content-Type", "application/json;UTF-8");
                    // 提示用户信息
                    $('#ShowKindList').html("<img src='../../Images/loading.png'>&nbsp;&nbsp;<span style='color:red'><asp:Label runat='server' Text='<%$ Resources:GPR, WAITING %>'></asp:Label></span>");

                    // 保存最后一次请求的经营类别编号
                    lastParentId = LastKindId;
                    LastKindId = parentId;
                }
            });
        }

        // 删除已选择的经营范围
        function RemoveItem(obj, id, name) {

            if (undefined != document.getElementById(id)) {
                document.getElementById(id).style.color = '';
            }
        
            // 获得已选择的经营范围
            var kindId = document.getElementById('KindId').value;
            while (-1 != name.indexOf('>')) {
                name = name.replace('>', '&gt;');
            }

            // 首先处理删除的ID，然后，返回赋值给经营范围
            document.getElementById('KindId').value = kindId.replace(id + '=' + name + '@', '');
            // 返回<span标签，也就是一个经营范围
            var child = obj.parentNode;
            // 返回<span父标签<td
            var parent = obj.parentNode.parentNode;
            // 由<span的父标签删除<span一条经营范围
            parent.removeChild(child);

        }

        function clear(i) {
            i += 1;
            for (var x = i; x <= 3; x++) {
                document.getElementById("Kinds" + x).options.length = 0;
                document.getElementById("Kinds" + x).style.display = 'none';
            }
        }
        
    </script>
<table border='0'>
        <tr>
            <!-- 显示选择的省份与经营类别 -->
            <td>
                <select id='Kinds0' onchange="SelKind(this.value, 0)" style="width : 80px;">
                    <option id="<%=this.MA_ORDERID %>" value="<%=this.MA_ORDERID %>" selected="selected"><%=this.NAME %></option>
                </select>
                <select id='Kinds1' onchange="SelKind(this.value, 1)" style="width : 150px;">
                    <option value="-1" selected="selected"><asp:Literal runat="server" Text="<%$ Resources:GPR, PLASECHOOSE %>"></asp:Literal></option>
                    <%
                        foreach (System.Data.DataRow dr in this.ShowSelectItem.Rows)
                        { 
                            %>
                            <option id="<%=dr["MA_ORDERID"].ToString() %>" value="<%=dr["MA_ORDERID"].ToString() %>" title="<%=dr["NAME"].ToString() %>"><%=dr["NAME"].ToString() %></option>
                            <%
                        }
                     %>
                </select>
                <select id='Kinds2' onchange="SelKind(this.value, 2)" style="width : 150px; display: none;">
                </select>
                <select id='Kinds3' onchange="SelKind(this.value, 3)" style="width : 150px; display: none;">
                </select>
            </td>
        </tr>
        <tr>
            <!-- 显示省份与经营类别 -->
            <td id='ShowKindList' style='LINE-HEIGHT: 150%'>
            </td>
        </tr>
        <tr>
            <td id='SelShowKindList' style='LINE-HEIGHT: 150%'>
                <%
                    if (null != GetSelectTable())
                    {
                        int lineWidth = 40;
                        foreach (System.Data.DataRow dr in GetSelectTable().Rows)
                        {
                            string content = dr["NAME"].ToString();
                            
                            
                            while (-1 != content.IndexOf("&gt;")) {
                                content = content.Replace("&gt;", "@");
                            }

                            while (-1 != content.IndexOf(",")) {
                                content = content.Replace(",", "，");
                            }

                            while (-1 != content.IndexOf(")")) {
                                content = content.Replace(")", "）");
                            }

                            while (-1 != content.IndexOf("(")) {
                                content = content.Replace("(", "（");
                            }
                            

                            if (content.Length < lineWidth) {

                                if (lineWidth - content.Length < 4)
                                {
                                    for (var x = content.Length; x < lineWidth; x++)
                                    {
                                        content += "…";
                                    }
                                    content += "<br>　";

                                    for (var x = 5; x < lineWidth; x++)
                                    {
                                        content += "…";
                                    }
                                }
                                else 
                                {
                                    for (var x = content.Length + 4; x < lineWidth; x++)
                                    {
                                        content += "…";
                                    }
                                    content += "&nbsp;";
                                }
                            } else {

                                var start = "";

                                var index = 0;

                                for (var x = 0; x < content.Length / lineWidth; x++)
                                {

                                    var sub = content.Substring(x * lineWidth, lineWidth) + "<br>";

                                    if (0 < x) {
                                        sub = "　" + sub;
                                    }

                                    if (sub.Length == lineWidth + 4)
                                    {
                                        start += sub;
                                        index++;
                                    }
                                }

                                var end = content.Substring(index * lineWidth);


                                if (lineWidth - end.Length < 4)
                                {
                                    for (var x = end.Length; x < lineWidth; x++)
                                    {
                                        end += "…";
                                    }
                                    end += "<br>";

                                    end = "　" + end;

                                    for (var x = 4; x < lineWidth; x++)
                                    {
                                        end += "…";
                                    }
                                }
                                else
                                {
                                    end = "　" + end;
                                    for (var x = end.Length +4; x < lineWidth; x++)
                                    {
                                        end += "…";
                                    }
                                }

                                content = start + end;
                            }
                            // content = content.Replace("TOP", "ＴＯＰ");
                            while (-1 != content.IndexOf("@")) {
                                content = content.Replace("@", " &gt;");
                            }
                        %>
                            <p id='<%=dr["ID"].ToString() %>'><img src="../../Images/remove1.png" border="0" align="top" /><%=content %>[ <a href="javascript:void(0)" onclick=RemoveItem(this,'<%=dr["ID"].ToString() %>','<%=dr["NAME"].ToString() %>')>删除</a> ]</p>
                        <%
                        }
                    }
                 %>
            </td>
        </tr>
    </table>
    <input type="hidden" id='KindId' name='KindId' value='<%=KindIds %>' />
    <%--<input type='button' value='test' onclick='alert($("#KindId").val())' /><%=this.MA_ORDERID %><br /><%=KindIds %> --%>