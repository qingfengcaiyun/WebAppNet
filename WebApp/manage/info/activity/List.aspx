<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.info.activity.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>活动管理</title>
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
        var pageNo = 1;
        var pageSize = 15;

        $(document).ready(function () {
            initItems();
            getTree();
            initDataGrid();

            $("#location").combotree({
                onChange: function (newValue, oldValue) {
                    getPager(newValue);
                }
            });

            var locationId = $("#locationId").val();
            getPager(locationId);
        });

        function getPager(locationId) {
            $("#dg").datagrid({
                url: "Action.aspx",
                loadMsg: "数据加载中，请稍后……",
                queryParams: {
                    action: "page",
                    locationId: locationId
                }
            });
        }

        function add() {
            window.location.href = "Detail.aspx?actId=0";
        }

        function edit() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要编辑的文章！', 'warning');
            } else {
                window.location.href = "Detail.aspx?actId=" + $("#dg").datagrid('getSelected').actId;
            }
        }

        function del() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要删除的活动！', 'warning');
            } else {
                jQuery.messager.confirm('删除', '确认删除该活动么？', function (r) {
                    if (r) {
                        var param = { action: "delete", newsId: n.newsId };
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
                title: "活动管理",
                height: $(window).height(),
                rownumbers: true,
                singleselect: true,
                pagination: true,
                toolbar: "#tb",
                fitColumns: true,
                columns: [[
                    { field: 'actName', title: '活动名称', width: 400 },
                    { field: 'startTime', title: '开始时间', width: 150, align: 'center' },
                    { field: 'endTime', title: '结束时间', width: 150, align: 'center' },
                    { field: 'location', title: '所属区域', width: 150, align: 'center' },
                    { field: 'checkStr', title: '审核', width: 50, align: 'center' },
                    { field: 'closedStr', title: '活动关闭', width: 150, align: 'center' },
                    { field: 'indexStr', title: '首页显示', width: 150, align: 'center' }
                ]]
            });
        }

        function initItems() {
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

        function getTree() {
            $("#location").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            var param = { action: "tree", lType: "region" };
            jQuery.post(
                "../../sys/location/Action.aspx",
                param,
                function (data) {
                    //alert(data);
                    var d = eval(data);
                    //alert(d);
                    $("#location").combotree('loadData', d);
                    $("#location").combotree('setValue', $("#locationId").val());
                    $("#location").combotree('tree').tree('expandAll');
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
                    <a href="#" id="btnAdd" onclick="add()">添加</a>&nbsp;|&nbsp;<a href="#" id="btnEdit"
                        onclick="edit()"> 编辑</a>&nbsp;|&nbsp;<a href="#" id="btnDel" onclick="del()">删除</a>
                </td>
                <td>
                    <select class="easyui-combotree txtInput" style="width: 200px;" id="location">
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <input id="locationId" type="hidden" value="<%=locationId %>" />
    <table id="dg">
    </table>
</body>
</html>
