<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Params.aspx.cs" Inherits="WebApp.manage.renovation.project.Params" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目参数编辑</title>
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
        $(document).ready(function () {
            initItems();
            initDataGrid();
            getParamTypes();
            getData();

            $("#paramTypes").combotree({
                onChange: function (newValue, oldValue) {
                    getParamValues();
                }
            });
        });

        function getData() {
            $("#dg").datagrid({
                url: "Action.aspx",
                loadMsg: "数据加载中，请稍后……",
                queryParams: {
                    action: "getParams",
                    projectId: $("#projectId").val()
                }
            });
        }

        function pic() {
            window.location.href = "Pictures.aspx?projectId=" + $("#projectId").val();
        }

        function add() {
            var n = $("#paramValues").combotree("tree").tree("getSelected");
            if (n != null) {
                var param = { action: "saveParam", projectId: $("#projectId").val(), paramId: n.id };
                jQuery.post(
                    "Action.aspx",
                    param,
                    function (data) {
                        $("#dg").datagrid('clearSelections');
                        $("#dg").datagrid('reload');
                    },
                    'json'
                );
            } else {
                jQuery.messager.alert('注意', '请选择要添加的参数！', 'warning');
            }
        }

        function del() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要删除的参数！', 'warning');
            } else {
                jQuery.messager.confirm('删除', '确认删除该参数么？', function (r) {
                    if (r) {
                        var param = { action: "delParam", pptId: n.pptId };
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

        function initDataGrid() {
            $("#dg").datagrid({
                title: $("#projectName").val() + " 参数设置",
                height: $(window).height(),
                rownumbers: true,
                singleSelect: true,
                toolbar: "#tb",
                fitColumns: true,
                columns: [[
                    { field: 'paramName', title: '参数', width: 100, align: 'right' },
                    { field: 'paramValue', title: '参数值', width: 900, align: 'left' }
                ]]
            });
        }

        function initItems() {
            $("#btnAdd").linkbutton({
                iconCls: 'icon-add',
                plain: true
            });

            $("#btnDel").linkbutton({
                iconCls: 'icon-cut',
                plain: true
            });

            $("#btnPic").linkbutton({
                iconCls: 'icon-ok',
                plain: true
            });

            $("#btnList").linkbutton({
                iconCls: 'icon-undo',
                plain: true
            });

            $("#paramTypes").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            $("#paramValues").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });
        }

        function getParamTypes() {
            var param = { action: "tree" };
            jQuery.post(
                "../parameter/Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    $("#paramTypes").combotree('loadData', d);
                    if (d.length > 0) {
                        $("#paramTypes").combotree('setValue', d[0].id);
                    }

                    getParamValues();
                },
                'json'
            );
        }

        function getParamValues() {
            var n = $("#paramTypes").combotree("tree").tree("getSelected");
            var paramKey = 0;
            if (n != null) {
                paramKey = n.id;
            }
            var param = { action: "treeValue", paramKey: paramKey };
            jQuery.post(
                "../parameter/Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    $("#paramValues").combotree('loadData', d);
                    if (d.length > 0) {
                        $("#paramValues").combotree('setValue', d[0].id);
                    }
                },
                'json'
            );
        }
    </script>
</head>
<body>
    <div id="tb" style="padding: 5px; height: auto">
        <table>
            <tr>
                <td>
                    <select class="easyui-combotree txtInput" style="width: 200px;" id="paramTypes">
                    </select>
                </td>
                <td>
                    <select class="easyui-combotree txtInput" style="width: 200px;" id="paramValues">
                    </select>
                </td>
                <td>
                    <a href="javascript:void(0)" id="btnAdd" onclick="add()">添加</a>&nbsp;|&nbsp;<a href="javascript:void(0)"
                        id="btnDel" onclick="del()">删除</a>&nbsp;|&nbsp;<a href="javascript:void(0)" id="btnPic"
                            onclick="pic()">管理项目图片</a>&nbsp;&nbsp;<a href="#"
                        id="btnList" onclick="javascript:window.location.href='List.aspx'">返回列表</a>
                </td>
            </tr>
        </table>
    </div>
    <input id="projectId" type="hidden" value="<%=projectId %>" />
    <input id="projectName" type="hidden" value="<%=projectName %>" />
    <table id="dg">
    </table>
</body>
</html>
