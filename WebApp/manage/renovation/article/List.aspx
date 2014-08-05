<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.renovation.article.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>装修知识</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/global.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../libs/jquery.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../libs/glibs.js"></script>
    <script type="text/javascript">
        var pageNo = 1;
        var pageSize = 15;
        var d;

        $(document).ready(function () {
            initBtn();
            initDataGrid();
            getProcessTree();

            $("#process").combotree({
                onChange: function (newValue, oldValue) {
                    getPager(newValue);
                }
            });

            var processId = $("#processId").val();

            getPager(processId);

            //getPager(cateId);
        });

        function getPager(processId) {
            $("#dg").datagrid({
                url: "Action.aspx",
                loadMsg: "数据加载中，请稍后……",
                queryParams: {
                    action: "page",
                    processId: processId
                }
            });
        }

        function add() {
            window.location.href = "Detail.aspx?raId=0";
        }

        function edit() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要编辑的文章！', 'warning');
            } else {
                window.location.href = "Detail.aspx?raId=" + $("#dg").datagrid('getSelected').raId;
            }
        }

        function del() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要删除的文章！', 'warning');
            } else {
                jQuery.messager.confirm('删除', '确认删除该文章么？', function (r) {
                    if (r) {
                        var param = { action: "delete", raId: n.raId };
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

        function getProcessTree() {
            $("#process").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            var param = { action: "tree" }
            jQuery.post(
                "../process/Action.aspx",
                param,
                function (data) {
                    //alert(data);
                    d = jQuery.parseJSON(data);
                    $("#process").combotree('loadData', d);
                    $("#process").combotree('setValue', $("#processId").val());
                }
            );
        }

        function getNode(key) {
            for (var i = 0; i < d.length; i++) {
                if (d[i].id.toString() == key.toString()) {
                    return d[i];
                }
            }
        }

        function initDataGrid() {
            $("#dg").datagrid({
                title: "装修知识",
                height: $(window).height(),
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                toolbar: "#tb",
                fitColumns: true,
                columns: [[
                    { field: 'longTitle', title: '标题', width: 750 },
                    { field: 'processName', title: '分类', width: 100, align: 'center' },
                    { field: 'checkStr', title: '审核', width: 50, align: 'center' },
                    { field: 'topStr', title: '置顶', width: 50, align: 'center' },
                    { field: 'insertTime', title: '添加时间', width: 150, align: 'center' },
                    { field: 'updateTime', title: '最后修改', width: 150, align: 'center' }
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
    </script>
</head>
<body>
    <div id="tb" style="padding: 5px;">
        <table>
            <tr>
                <td>
                    <a href="#" id="btnAdd" onclick="add()">添加</a>&nbsp;|&nbsp;<a href="#" id="btnEdit"
                        onclick="edit()"> 编辑</a>&nbsp;|&nbsp;<a href="#" id="btnDel" onclick="del()">删除</a>
                </td>
                <td>
                    <select class="easyui-combotree txtInput" style="width: 200px;" id="process">
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <input id="processId" type="hidden" value="<%=processId %>" />
    <table id="dg">
    </table>
</body>
</html>
