<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.manage.info.category.View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>分类管理</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #newform td
        {
            height: 30px;
        }
    </style>
    <script type="text/javascript" src="../../../libs/jquery.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initTreeGrid();
        });

        function initTreeGrid() {
            $('#dg').treegrid({
                title: "分类管理",
                height: $(window).height(),
                url: 'Action.aspx',
                rownumbers: true,
                animate: true,
                fitColumns: true,
                loadMsg: "数据载入中……稍等！",
                idField: 'cateId',
                treeField: 'cateName',
                columns: [[
                    { title: '分类名称', field: 'cateName', editor: 'text', width: 300 },
                    { title: '分类序号', field: 'cateNo', editor: 'text', width: 100 },
                    { title: '上级序号', field: 'parentNo', editor: 'text', width: 100 }
                ]],
                toolbar: [{
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: addNode
                }, '-', {
                    text: '删除',
                    iconCls: 'icon-cut',
                    handler: delNode
                }, '-', {
                    text: '编辑',
                    iconCls: 'icon-edit',
                    handler: editNode
                }, {
                    text: '保存',
                    iconCls: 'icon-save',
                    handler: updateNode
                }, {
                    text: '取消',
                    iconCls: 'icon-cancel',
                    handler: cancelNode
                }]
            });
        }

        function bindData() {
            var param = { action: 'tree' };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = jQuery.parseJSON(data);
                    $('#dg').treegrid('loadData', d);
                }
            );
        }

        function editNode() {
            var node = $('#dg').treegrid('getSelected');
            if (node) {
                $('#dg').treegrid('beginEdit', node.cateId);
            }
        }

        function cancelNode() {
            var node = $('#dg').treegrid('getSelected');
            if (node) {
                $('#dg').treegrid('cancelEdit', node.cateId);
            }
        }

        function updateNode() {
            var node = $('#dg').treegrid('getSelected');
            if (node) {
                $('#dg').treegrid('endEdit', node.cateId);

                var param = {
                    action: "save",
                    paramStr: "cateId,cateName,cateNo,parentNo,cityId",
                    cateId: node.cateId,
                    cateName: node.cateName,
                    cateNo: node.cateNo,
                    parentNo: node.parentNo,
                    cityId: $("#cityId").val()
                };

                jQuery.post(
                    "Action.aspx",
                    param,
                    function (data) {
                        var d = jQuery.parseJSON(data);

                        if (parseInt(d.msg) == 1) {
                            jQuery.messager.alert(
                                '信息！！！',
                                '操作成功，将刷新数据！！！',
                                'info',
                                function () {
                                    $('#dg').treegrid('reload');
                                }
                            );
                        } else {
                            jQuery.messager.alert(
                                '信息！！！',
                                '操作失败！！！',
                                'error'
                            );
                        }
                    }
                );
            }
        }

        function addNode() {
            var node = $('#dg').treegrid('getSelected');
            if (node) {
                $("#parentName").html(node.cateName);
                $("#parentNo").html(node.cateNo);
                $("#cateName").val("");
                $("#cateNo").val("");
                $("#newform").window("open");
            } else {
                jQuery.messager.alert(
                    '注意！！！',
                    '请选择父级分类！！！',
                    'info'
                );
            }
        }

        function insertNode() {
            var param = {
                action: "save",
                paramStr: "parentNo,cateName,cateNo,cateId,cityId",
                cateName: $("#cateName").val(),
                cateNo: $("#cateNo").val(),
                parentNo: $("#parentNo").html(),
                cateId: 0,
                cityId: $("#cityId").val()
            };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var m = jQuery.parseJSON(data);
                    if (parseInt(m.msg) == 1) {
                        $("#newform").window("close");
                        jQuery.messager.alert('信息', '保存信息成功！', 'info', function () { $('#dg').treegrid('reload'); });
                    } else {
                        $("#newform").window("close");
                        jQuery.messager.alert('错误', '保存信息失败！建议重新登录，再次修改信息！', 'error');
                    }
                }
            );
        }

        function delNode() {
            var node = $('#dg').treegrid('getSelected');
            if (node) {
                var param = { action: "delete", cateNo: node.cateNo };

                jQuery.post(
                    "Action.aspx",
                    param,
                    function (data) {
                        var d = jQuery.parseJSON(data);

                        if (parseInt(d.msg) == 1) {
                            jQuery.messager.alert(
                                '信息！！！',
                                '操作成功，将刷新数据！！！',
                                'info',
                                function () {
                                    $('#dg').treegrid('reload');
                                }
                            );
                        } else {
                            jQuery.messager.alert(
                                '信息！！！',
                                '操作失败！！！',
                                'error'
                            );
                        }
                    }
                );
            }
        }
    </script>
</head>
<body>
    <div id="dg">
    </div>
    <div id="newform" class="easyui-window" shadow="true" modal="true" minimizable="false"
        maximizable="false" closable="true" closed="true" collapsible="false" resizable="false"
        iconcls="icon-blank" title="分类添加" style="width: 370px; height: 260px;">
        <table width="350" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th width="100">
                    &nbsp;
                </th>
                <th width="210">
                    &nbsp;
                </th>
                <th width="40">
                    &nbsp;
                </th>
            </tr>
            <tr>
                <td class="algR">
                    父级分类：
                </td>
                <td class="algL">
                    <span id="parentName"></span>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    父级序号：
                </td>
                <td class="algL">
                    <span id="parentNo"></span>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    分类名称：
                </td>
                <td class="algL">
                    <input type="text" id="cateName" class="easyui-validatebox textbox txtInput w200"
                        value="" required="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    分类序号：
                </td>
                <td class="algL">
                    <input type="text" id="cateNo" class="easyui-validatebox textbox txtInput w100" value=""
                        required="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="algL">
                    <a class="easyui-linkbutton" iconcls="icon-save" href="javascript:void(0)" onclick="insertNode()">
                        &nbsp;保存&nbsp;</a>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" id="cityId" value="<%=cityId %>" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
