<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.user.designer.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>公司管理</title>
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

        $(document).ready(function () {
            initItems();
            initLocationTree();
            initDataGrid();

            $("#location").combotree({
                onChange: function (newValue, oldValue) {
                    getMemberTree();
                    getPager(newValue, 0);
                }
            });

            $("#member").combotree({
                onChange: function (newValue, oldValue) {
                    var n = $("#location").combotree("tree").tree("getSelected");
                    if (n != null) {
                        getPager(n.id, newValue);
                    }
                }
            });

            var locationId = $("#locationId").val();
            var memberId = $("#memberId").val();
            getPager(locationId, memberId);
        });

        function getPager(locationId, memberId) {
            $("#dg").datagrid({
                url: "Action.aspx",
                loadMsg: "数据加载中，请稍后……",
                queryParams: {
                    action: "page",
                    locationId: locationId,
                    memberId: memberId
                }
            });
        }

        function add() {
            window.location.href = "Detail.aspx?designerId=0";
        }

        function edit() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要编辑的信息！', 'warning');
            } else {
                window.location.href = "Detail.aspx?designerId=" + $("#dg").datagrid('getSelected').designerId;
            }
        }

        function del() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要删除的信息！', 'warning');
            } else {
                jQuery.messager.confirm('删除', '确认删除该信息么？', function (r) {
                    if (r) {
                        var param = { action: "delete", designerId: n.designerId };
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

        function project() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要管理项目的公司！', 'warning');
            } else {
                window.location.href = "../../renovation/project/List.aspx?designerId=" + n.designerId;
            }
        }

        function initLocationTree() {
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

                    getMemberTree();
                },
                'json'
            );
        }

        function getMemberTree() {
            var n = $("#location").combotree("tree").tree("getSelected");
            if (n != null) {
                var param = { action: "tree", locationId: n.id };
                jQuery.post(
                    "../member/Action.aspx",
                    param,
                    function (data) {
                        var d = eval(data);
                        $("#member").combotree('loadData', d);
                        $("#member").combotree('tree').tree('expandAll');

                        var memberId = $("#memberId").val();
                        if (parseInt(memberId) > 0) {
                            $("#member").combotree('setValue', memberId);
                        }
                    },
                    'json'
                );
            }
        }

        function initDataGrid() {
            $("#dg").datagrid({
                title: "设计师管理",
                height: $(window).height(),
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                toolbar: "#tb",
                fitColumns: true,
                columns: [[
                    { field: 'fullName', title: '设计师', width: 200 },
                    { field: 'sex', title: '性别', width: 50, align: 'center' },
                    { field: 'member', title: '所属公司', width: 200, align: 'center' },
                    { field: 'location', title: '所属区县', width: 100, align: 'center' },
                    { field: 'suggestNo', title: '推荐值', width: 100, align: 'center' },
                    { field: 'itemIndex', title: '排序', width: 100, align: 'center' },
                    { field: 'delStr', title: '已删除', width: 50, align: 'center' }
                ]]
            });
        }

        function initItems() {
            $("#location").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            $("#member").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

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

            $("#btnProject").linkbutton({
                iconCls: 'icon-redo',
                plain: true
            });
        }
    </script>
</head>
<body>
    <div id="tb" style="padding: 5px;">
        <a href="#" id="btnAdd" onclick="add()">添加</a>&nbsp;|&nbsp;<a href="#" id="btnEdit"
            onclick="edit()"> 编辑</a>&nbsp;|&nbsp;<a href="#" id="btnDel" onclick="del()">删除</a>&nbsp;|&nbsp;
        <select class="easyui-combotree txtInput" style="width: 200px;" id="location">
        </select><select class="easyui-combotree txtInput" style="width: 200px;" id="member">
        </select>
        &nbsp;|&nbsp;<a href="#" id="btnProject" onclick="project()">案例项目</a>
    </div>
    <table id="dg">
    </table>
    <input id="locationId" type="hidden" value="<%=locationId %>" />
    <input id="memberId" type="hidden" value="<%=memberId %>" />
</body>
</html>
