﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.sys.role.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>角色管理</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        newform td
        {
            height: 30px;
        }
    </style>
    <script type="text/javascript" src="../../../libs/jquery.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../libs/glibs.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initDataGrid();
        });

        function initDataGrid() {
            $("#dg").datagrid({
                title: "角色管理",
                height: $(window).height(),
                url: "Action.aspx",
                rownumbers: true,
                singleSelect: true,
                fitColumns: true,
                columns: [[
                    { field: 'roleName', title: '角色', editor: "text", width: 750 },
                    { field: 'itemIndex', title: '排序', editor: "text", width: 250 }
                ]],
                toolbar: [{
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: addItem
                }, '-', {
                    text: '删除',
                    iconCls: 'icon-cut',
                    handler: delItem
                }, '-', {
                    text: '编辑',
                    iconCls: 'icon-edit',
                    handler: editItem
                }, {
                    text: '保存',
                    iconCls: 'icon-save',
                    handler: updateItem
                }, {
                    text: '取消',
                    iconCls: 'icon-cancel',
                    handler: cancelItem
                }]
            });
        }

        function editItem() {
            var item = $('#dg').datagrid('getSelected');
            if (item) {
                $('#dg').datagrid('beginEdit', $('#dg').datagrid('getRowIndex', item));
            }
        }

        function cancelItem() {
            var item = $('#dg').datagrid('getSelected');
            if (item) {
                $('#dg').datagrid('cancelEdit', $('#dg').datagrid('getRowIndex', item));
            }
        }

        function updateItem() {
            var item = $('#dg').datagrid('getSelected');
            if (item) {
                $('#dg').datagrid('endEdit', $('#dg').datagrid('getRowIndex', item));

                var param = {
                    action: "save",
                    paramStr: "roleId,roleName,itemIndex",
                    roleId: item.roleId,
                    roleName: item.roleName,
                    itemIndex: item.itemIndex
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
                                    $('#dg').datagrid('reload');
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

        function addItem() {
            $("#roleName").val("");
            $("#itemIndex").val("");
            $("#newform").window("open");
        }

        function insertItem() {
            var param = {
                action: "save",
                paramStr: "roleName,itemIndex,roleId",
                roleName: $("#roleName").val(),
                itemIndex: $("#itemIndex").val(),
                roleId: 0
            };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var m = jQuery.parseJSON(data);
                    if (parseInt(m.msg) == 1) {
                        $("#newform").window("close");
                        jQuery.messager.alert('信息', '保存信息成功！', 'info', function () { $('#dg').datagrid('reload'); });
                    } else {
                        $("#newform").window("close");
                        jQuery.messager.alert('错误', '保存信息失败！建议重新登录，再次修改信息！', 'error');
                    }
                }
            );
        }

        function delItem() {
            var item = $('#dg').datagrid('getSelected');
            if (item) {
                var param = { action: "delete", roleId: item.roleId };

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
                                    $('#dg').datagrid('reload');
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
    <table id="dg">
    </table>
    <div id="newform" class="easyui-window" shadow="true" modal="true" minimizable="false"
        maximizable="false" closable="true" closed="true" collapsible="false" resizable="false"
        iconcls="icon-blank" title="角色添加" style="width: 500px; height: 180px;">
        <table width="480" border="0" cellspacing="0" cellpadding="0">
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
                    角色名称：
                </td>
                <td class="algL">
                    <input type="text" id="roleName" class="easyui-validatebox textbox txtInput w200"
                        value="" required="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    角色序号：
                </td>
                <td class="algL">
                    <input type="text" id="itemIndex" class="easyui-validatebox textbox txtInput w100"
                        value="" required="true" />
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
                    <a class="easyui-linkbutton" iconcls="icon-save" href="javascript:void(0)" onclick="insertItem()">
                        &nbsp;保存&nbsp;</a>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
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
