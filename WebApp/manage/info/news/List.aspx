<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.info.news.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>文章管理</title>
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
        var d;

        $(document).ready(function () {
            initBtn();
            initDataGrid();
            getCateTree();

            $("#cate").combotree({
                onChange: function (newValue, oldValue) {
                    getPager(newValue);
                }
            });

            var cateId = $("#cateId").val();

            getPager(cateId);

            //getPager(cateId);
        });

        function getPager(cateId) {
            $("#dg").datagrid({
                url: "Action.aspx",
                loadMsg: "数据加载中，请稍后……",
                queryParams: {
                    action: "page",
                    cateId: cateId,
                    cityId: $("#cityId").val()
                }
            });
        }

        function add() {
            window.location.href = "Detail.aspx?newsId=0";
        }

        function edit() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要编辑的文章！', 'warning');
            } else {
                window.location.href = "Detail.aspx?newsId=" + $("#dg").datagrid('getSelected').newsId;
            }
        }

        function del() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要删除的文章！', 'warning');
            } else {
                jQuery.messager.confirm('删除', '确认删除该文章么？', function (r) {
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

        function getCateTree() {
            $("#cate").combotree({
                required: true,
                panelWidth: 150,
                panelHeight: 160
            });

            var param = { action: "tree" }
            jQuery.post(
                "../category/Action.aspx",
                param,
                function (data) {
                    //alert(data);
                    d = jQuery.parseJSON(data);
                    $("#cate").combotree('loadData', d);
                    $("#cate").combotree('setValue', $("#cateId").val());
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
                title: "文章管理",
                height: $(window).height(),
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                toolbar: "#tb",
                fitColumns: true,
                columns: [[
                    { field: 'longTitle', title: '标题', width: 750 },
                    { field: 'cateName', title: '分类', width: 100, align: 'center' },
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
                    <select class="easyui-combotree txtInput" style="width: 150px;" id="cate">
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <input id="cityId" type="hidden" value="<%=cityId %>" />
    <input id="cateId" type="hidden" value="<%=cateId %>" />
    <table id="dg">
    </table>
</body>
</html>
