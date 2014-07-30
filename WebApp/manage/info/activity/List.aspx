<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.info.activity.List" %>

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
            //$('#dg').setFullSize({ w: "1000", h: "1000" });
            initDataGrid();
            getCateTree();

            $("#cate").combotree({
                onChange: function (newValue, oldValue) {
                    //var n = getNode(newValue);
                    //alert(n.attributes.cateId);
                    //$("#cate").combotree("disable");
                    //getPager(n.attributes.cateId);
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

        function getNode(key) {
            for (var i = 0; i < d.length; i++) {
                if (d[i].id.toString() == key.toString()) {
                    return d[i];
                }
            }
        }

        function setFullSize() {
            var dg = $("#dg");

            var w = $(window);
            alert(w.height());
            alert(dg.height());
            dg.css("height", w.height());
        }

        function initDataGrid() {
            $("#dg").datagrid({
                title: "文章管理",
                height: $(window).height(),
                rownumbers: true,
                singleselect: true,
                pagination: true,
                toolbar: "#tb",
                fitColumns: true,
                columns: [[
                    { field: 'longTitle', title: '标题', width: 750 },
                    { field: 'isTop', title: '置顶', width: 50, align: 'center' },
                    { field: 'insertTime', title: '添加时间', width: 150, align: 'center' },
                    { field: 'updateTime', title: '最后修改', width: 150, align: 'center' }
                ]]
            });
        }
    </script>
</head>
<body>
    <div id="tb" style="padding: 5px; height: auto">
        <div style="margin-bottom: 5px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="add()">
                添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="edit()">
                    编辑</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="true" onclick="del()">
                        删除</a>
            <select class="easyui-combotree txtInput" panelheight="140" required="true" id="cate">
            </select>
        </div>
    </div>
    <input id="cityId" type="hidden" value="<%=cityId %>" />
    <table id="dg">
    </table>
</body>
</html>
