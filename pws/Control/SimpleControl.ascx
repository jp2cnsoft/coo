<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SimpleControl.ascx.cs" Inherits="SimpleControl" %>
<script type="text/javascript" src="../../Res/jquery/jquery.js"></script>
    <script type="text/javascript">

        var LastCityId = "<%=CountryId %>";
    
        // 按指定父类ID返回所有子类
        function SelCity(parentId) {

            // 判断节点下是否还有子节点，如果，有子节点继续，如果，没有子节点停止
            if (1 == $('#CitySpan' + parentId).children().size()) {
                return;
            }

            $.ajax({
                // 设置是否是全域
                global: true,
                // 设置超时时间
                timeout: 10000,
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

                    // 判断返回数据结果，如果，返回数据结果的记录数为“0”说明是，叶节点，设置表单元素CityId
                    if (0 == json.d.length) {
                        // 设置表单元素CityId
                        var CitySpanMain = document.getElementById('ShowSelCity');
                        //alert(CitySpanMain.innerHTML);
                        // 设置选择的国家、省份、城市、地区
                        for (var x = 0; x < $('span', CitySpanMain).length; x++) {
                            // alert($('span', CitySpanMain)[x].innerHTML);
                            var span = $('span', CitySpanMain)[x];
                            if (x == 0) {
                                document.getElementById('CountryId').value = span.id.replace('CitySpan', '');
                                document.getElementById('CountryName').value = $('a', CitySpanMain)[x].innerHTML;
                            } else if (x == 1) {
                                document.getElementById('ProvinceId').value = span.id.replace('CitySpan', '');
                                document.getElementById('ProvinceName').value = $('a', CitySpanMain)[x].innerHTML;
                            } else if (x == 2) {
                                document.getElementById('CityId').value = span.id.replace('CitySpan', '');
                                document.getElementById('CityName').value = $('a', CitySpanMain)[x].innerHTML;
                            } else if (x == 3) {
                                document.getElementById('BoroughId').value = span.id.replace('CitySpan', '');
                                document.getElementById('BoroughName').value = $('a', CitySpanMain)[x].innerHTML;
                            }
                        }
                    } else {
                        document.getElementById('CountryId').value = "";
                        document.getElementById('CountryName').value = "";
                        document.getElementById('ProvinceId').value = "";
                        document.getElementById('ProvinceName').value = "";
                        document.getElementById('CityId').value = "";
                        document.getElementById('CityName').value = "";
                        document.getElementById('BoroughId').value = "";
                        document.getElementById('BoroughName').value = "";
                    }

                    // 清楚列表
                    $('#ShowCityList').html("");
                    // 循环返回结果
                    for (var x = 0; x < json.d.length; x++) {
                        // 获得集合中的一个对象
                        var zm = json.d[x];
                        // 将新的地区连接，追加到列表中
                        $('<a id=City' + zm.MA_ORDERID + ' href=\'javascript:void(0)\' onclick=\'SelCity(\"' + zm.MA_ORDERID + '\")\'>' + zm.NAME + '</a>').appendTo('#ShowCityList');
                        // 添加分隔符
                        $('#ShowCityList').append('&nbsp;|&nbsp;');
                    }
                },
                // 设置请求失败回调方法
                error: function(xml, status) {
                    // 打印错误信息
                    $('#ShowCityList').html(xml.responseText);
                },
                // 设置请求前的回调方法
                beforeSend: function(xml) {
                    // 判断单击的是否是
                    if ($('#City' + parentId).parent().attr('id') == 'ShowCityList') {
                        // 生成一个文本容器
                        $('<span id=CitySpan' + parentId + '>&gt;</span>').appendTo('#CitySpan' + LastCityId);
                        
                        // 添加选择的城市
                        $('#City' + parentId).appendTo('#CitySpan' + parentId);

                        //alert($('#CitySpan' + parentId).html());
                    } else {
                        // 判断是否有子节点，如果，有子节点进行删除操作，如果，没有子节点不进行删除操作
                        if (1 < $('#CitySpan' + parentId).children().size()) {
                            // 返回地区Span文本域中的最好后一个元素，也就是下一级子节点元素
                            var index = $('#CitySpan' + parentId).children().size() - 1;
                            // 通过索引返回最后一个子节点
                            var lastNode = $('#CitySpan' + parentId).children().get(index);
                            // 通过父节点删除最后一个子节点
                            document.getElementById('CitySpan' + parentId).removeChild(lastNode);
                        }
                    }

                    // 设置容器类型
                    xml.setRequestHeader("Content-Type", "application/json;UTF-8");
                    // 提示用户信息
                    $('#ShowCityList').html("<img src='../../Images/loading.png'><span style='color:red'><asp:Label runat='server' Text='<%$ Resources:GPR, WAITING %>'></asp:Label></span>");

                    // 保存最后一次请求的城市编号
                    LastCityId = parentId;
                }
            });
        }
  
    </script>
    
    <table border='0'>
        <tr><td id="ShowSelCity">
                        <%
                            if (null != CountryId && !"".Equals(CountryId))
                            {
                                %><span id='CitySpan<%=CountryId %>'><a id='City<%=CountryId %>' href='javascript:void(0)' onclick='SelCity("<%=CountryId %>")'><%=CountryName%></a>
                                        <%
                                            if (null != ProvinceId && !"".Equals(ProvinceId))
                                             {
                                            %><span id='CitySpan<%=ProvinceId %>'>&gt;<a id='City<%=ProvinceId %>' href='javascript:void(0)' onclick='SelCity("<%=ProvinceId %>")'><%=ProvinceName %></a>
                                                    <%
                                                        if (null != CityId && !"".Equals(CityId))
                                                        { 
                                                            %><span id='CitySpan<%=CityId %>'>&gt;<a id='City<%=CityId %>' href='javascript:void(0)' onclick='SelCity("<%=CityId %>")'><%=CityName %></a>
                                                                    <%
                                                                        if (null != BoroughId && !"".Equals(BoroughId))
                                                                        { 
                                                                            %><span id='CitySpan<%=BoroughId %>'>&gt;<a id='City<%=BoroughId %>' href='javascript:void(0)' onclick='SelCity("<%=BoroughId %>")'><%=BoroughName %></a>
                                                                            </span>
                                                                            <%
                                                                        }
                                                             %></span>
                                                            <%
                                                        }
                                                     %>
                                             </span>
                                            <%
                                             }
                                     %>
                                 </span>
                                <%
                            }
                         %>
            </td>
        </tr>
        <tr>
            <!-- 显示省份与城市 -->
            <td id='ShowCityList' style='LINE-HEIGHT: 150%'>
                <%
                    if (null != this.ShowSelectItem && !IsSelAddress())
                    {
                        foreach (System.Data.DataRow dr in this.ShowSelectItem.Rows)
                        { 
                        %><a id='City<%=dr["MA_ORDERID"].ToString() %>' href='javascript:void(0)' onclick='SelCity("<%=dr["MA_ORDERID"].ToString() %>")'><%=dr["NAME"].ToString()%></a>&nbsp;|&nbsp;<%
                        }
                    }
                 %>
            </td>
        </tr>
    </table>
    <input type="hidden" id='CountryId' name='CountryId' value='<%=CountryId %>' />
    <input type="hidden" id='CountryName' name='CountryName' value='<%=CountryName %>' />
    
    <input type="hidden" id='ProvinceId' name='ProvinceId' value='<%=ProvinceId %>' />
    <input type="hidden" id='ProvinceName' name='ProvinceName' value='<%=ProvinceName %>' />
    
    <input type="hidden" id='CityId' name='CityId' value='<%=CityId %>' />
    <input type="hidden" id='CityName' name='CityName' value='<%=CityName %>' />
    
    <input type="hidden" id='BoroughId' name='BoroughId' value='<%=BoroughId %>' />
    <input type="hidden" id='BoroughName' name='BoroughName' value='<%=BoroughName %>' />