<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.renovation.project.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目管理</title>
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
                    getPager(newValue, 0, 0);
                }
            });

            $("#member").combotree({
                onChange: function (newValue, oldValue) {
                    getDesignerTree();
                    var n = $("#location").combotree("tree").tree("getSelected");
                    if (n != null) {
                        getPager(n.id, newValue, 0);
                    }
                }
            });

            $("#designer").combotree({
                onChange: function (newValue, oldValue) {
                    var n = $("#location").combotree("tree").tree("getSelected");
                    if (n != null) {
                        var m = $("#member").combotree("tree").tree("getSelected");
                        getPager(n.id, m.id, newValue);
                    }
                }
            });

            var locationId = $("#locationId").val();
            var memberId = $("#memberId").val();
            var designerId = $("#designerId").val();
            getPager(locationId, memberId, designerId);
        });

        function getPager(locationId, memberId, designerId) {
            $("#dg").datagrid({
                url: "Action.aspx",
                loadMsg: "数据加载中，请稍后……",
                queryParams: {
                    action: "page",
                    locationId: locationId,
                    memberId: memberId,
                    designerId: designerId
                }
            });
        }

        function add() {
            window.location.href = "Detail.aspx?projectId=0";
        }

        function edit() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要编辑的信息！', 'warning');
            } else {
                window.location.href = "Detail.aspx?projectId=" + $("#dg").datagrid('getSelected').projectId;
            }
        }

        function del() {
            var n = $("#dg").datagrid('getSelected');
            if (n == null) {
                jQuery.messager.alert('注意', '请选择要删除的信息！', 'warning');
            } else {
                jQuery.messager.confirm('删除', '确认删除该信息么？', function (r) {
                    if (r) {
                        var param = { action: "delete", projectId: n.projectId };
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

        function initLocationTree() {
            var param = { action: "tree", lType: "region" };
            jQuery.post(
                "../../sys/location/Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
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
                    "../../user/member/Action.aspx",
                    param,
                    function (data) {
                        var d = eval(data);
                        $("#member").combotree('loadData', d);
                        $("#member").combotree('tree').tree('expandAll');

                        var memberId = $("#memberId").val();
                        if (parseInt(memberId) > 0) {
                            $("#member").combotree('setValue', memberId);
                        }

                        getDesignerTree();
                    },
                    'json'
                );
            }
        }

        function getDesignerTree() {
            var l = $("#location").combotree("tree").tree("getSelected");
            var n = $("#member").combotree("tree").tree("getSelected");
            if (n != null && l != null) {
                var param = { action: "tree", locationId: l.id, memberId: n.id };
                jQuery.post(
                    "../../user/designer/Action.aspx",
                    param,
                    function (data) {
                        var d = eval(data);
                        $("#designer").combotree('loadData', d);
                        $("#designer").combotree('tree').tree('expandAll');

                        var designerId = $("#designerId").val();
                        if (parseInt(designerId) > 0) {
                            $("#designer").combotree('setValue', designerId);
                        }
                    },
                    'json'
                );
            }
        }

        function initDataGrid() {
            $("#dg").datagrid({
                title: "项目管理",
                height: $(window).height(),
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                toolbar: "#tb",
                fitColumns: true,
                columns: [[
                    { field: 'projectName', title: '项目名称', width: 300 },
                    { field: 'member', title: '所属公司', width: 200, align: 'center' },
                    { field: 'designer', title: '所属设计师', width: 200, align: 'center' },
                    { field: 'location', title: '所属区县', width: 100, align: 'center' },
                    { field: 'buildingsName', title: '所属楼盘', width: 100, align: 'center' },
                    { field: 'startTime', title: '开始时间', width: 100, align: 'center' }
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

            $("#designer").combotree({
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
                <td>
                    <select class="easyui-combotree txtInput" style="width: 200px;" id="member">
                    </select>
                </td>
                <td>
                    <select class="easyui-combotree txtInput" style="width: 200px;" id="designer">
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg">
    </table>
    <input id="locationId" type="hidden" value="<%=locationId %>" />
    <input id="memberId" type="hidden" value="<%=memberId %>" />
    <input id="designerId" type="hidden" value="<%=designerId %>" />
</body>
</html>
