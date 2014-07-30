<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.renovation.parameter.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>参数管理</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td
        {
            height: 30px;
        }
    </style>
    <script type="text/javascript" src="../../../libs/jquery.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../libs/glibs.js"></script>
    <script type="text/javascript">
        var d;

        $(document).ready(function () {
            initForm();

            initBtn();
            initDataGrid();
            getParamType();

            $("#paramType").combotree({
                onChange: function (newValue, oldValue) {
                    getList(newValue);
                }
            });
        });

        function getList(paramKey) {
            $("#dg").datagrid({
                url: "Action.aspx",
                loadMsg: "数据加载中，请稍后……",
                queryParams: {
                    action: "list",
                    paramKey: paramKey
                }
            });
        }

        function add() {
            $("#paramValue").val("");
            $("#itemIndex").val("");
            $("#paramId").val(0);

            $("#newform").window("open");
        }

        function edit() {
            var n = $("#dg").datagrid('getSelected');

            if (n == null) {
                jQuery.messager.alert('注意', '请选择要编辑的信息！', 'warning');
            } else {
                $("#paramValue").val(n.paramValue);
                $("#itemIndex").val(n.itemIndex);
                $("#paramTypes").combotree('setValue', n.paramKey);
                $("#paramId").val(n.paramId);

                $("#newform").window("open");
            }
        }

        function save() {
            var t = $("#paramTypes").combotree('tree');
            var n = t.tree('getSelected');

            var paramKey = n.id;
            var paramName = n.text;
            var paramValue = $("#paramValue").val();
            var itemIndex = $("#itemIndex").val();
            var paramId = $("#paramId").val();

            var param = {
                action: "save",
                paramStr: "paramId,paramName,paramKey,paramValue,itemIndex",
                paramName: paramName,
                paramKey: paramKey,
                paramValue: paramValue,
                itemIndex: itemIndex,
                paramId: paramId
            };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);

                    if (parseInt(d.msg) == 1) {
                        jQuery.messager.alert('信息', '操作成功！', 'info');
                        $("#newform").window("close");
                        $("#dg").datagrid('clearSelections');
                        $("#dg").datagrid('reload');
                    } else {
                        jQuery.messager.alert('错误', '操作失败！', 'error');
                    }
                },
                'json'
            );
        }

        function del() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要删除信息！', 'warning');
            } else {
                jQuery.messager.confirm('删除', '确认删除该信息么？', function (r) {
                    if (r) {
                        var param = { action: "delete", paramId: n.paramId };
                        jQuery.post(
                            "Action.aspx",
                            param,
                            function (data) {
                                $("#dg").datagrid('clearSelections');
                                $("#dg").datagrid('reload');
                            }
                        );
                    }
                });
            }
        }

        function getParamType() {
            $("#paramType").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            $("#paramTypes").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            var param = { action: "tree" }
            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    $("#paramType").combotree('loadData', d);
                    $("#paramTypes").combotree('loadData', d);
                },
                'json'
            );
        }

        function initDataGrid() {
            $("#dg").datagrid({
                title: "参数管理",
                height: $(window).height(),
                rownumbers: true,
                singleSelect: true,
                toolbar: "#tb",
                fitColumns: true,
                columns: [[
                    { field: 'paramValue', title: '参数', width: 750 },
                    { field: 'itemIndex', title: '序号', width: 250 }
                ]]
            });
        }

        function initBtn() {
            $("#btnAdd").linkbutton({
                iconCls: 'icon-add',
                plain: true
            });

            $("#btnEdit").linkbutton({
                iconCls: 'icon-edit',
                plain: true
            });

            $("#btnDel").linkbutton({
                iconCls: 'icon-cut',
                plain: true
            });
        }

        function initForm() {
            $("#newform").window({
                title: "分类添加",
                iconCls: 'icon-blank',
                shadow: true,
                modal: true,
                minimizable: false,
                maximizable: false,
                closable: true,
                closed: true,
                collapsible: true,
                resizable: true
            });
        }

    </script>
</head>
<body>
    <div id="tb" style="padding: 5px; height: auto">
        <div style="margin-bottom: 5px">
            <a href="#" id="btnAdd" onclick="add()">添加</a> <a href="#" id="btnEdit" onclick="edit()">
                编辑</a> <a href="#" id="btnDel" onclick="del()">删除</a>
            <select class="easyui-combotree txtInput" style="width: 200px;" id="paramType">
            </select>
        </div>
    </div>
    <table id="dg">
    </table>
    <div id="newform" style="width: 370px; height: 260px;">
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
                    参数分类：
                </td>
                <td class="algL">
                    <select class="easyui-combotree txtInput" style="width: 200px;" id="paramTypes">
                    </select>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    参数值：
                </td>
                <td class="algL">
                    <input type="text" id="paramValue" class="easyui-validatebox textbox txtInput w200"
                        value="" required="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    排序：
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
                    <a class="easyui-linkbutton" iconcls="icon-save" href="javascript:void(0)" onclick="save()">
                        &nbsp;保存&nbsp;</a>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" id="paramId" value="" />
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
