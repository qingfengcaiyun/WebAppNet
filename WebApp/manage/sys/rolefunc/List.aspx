<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.manage.sys.rolefunc.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>角色功能分配</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/global.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../libs/jquery.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../libs/glibs.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initRoleTree();
            initFuncTree();
        });

        function initRoleTree() {
            var param = {
                action: "tree"
            };
            jQuery.post(
                "../role/Action.aspx",
                param,
                function (data) {
                    var m = jQuery.parseJSON(data);
                    $("#roleTree").tree({
                        animate: true,
                        data: m,
                        onClick: function (node) {
                            if ($('#roleTree').tree('isLeaf', node.target)) {
                                var roleId = node.id;

                                jQuery.post(
                                    "Action.aspx",
                                    { action: "list", roleId: roleId },
                                    function (data) {
                                        var nodes = $("#funcTree").tree("getChecked");

                                        if (nodes.length > 0) {
                                            for (var i = 0; i < nodes.length; i++) {
                                                $('#funcTree').tree('uncheck', nodes[i].target);
                                            }
                                        }

                                        if (data != "") {
                                            var ids = data.split(',');
                                            for (var i = 0; i < ids.length; i++) {
                                                $('#funcTree').tree('check', $('#funcTree').tree('find', ids[i]).target);
                                            }
                                        }
                                    }
                                );
                            }
                        }
                    });
                }
            );
        }

        function initFuncTree() {
            var param = {
                action: "tree"
            };
            jQuery.post(
                "../function/Action.aspx",
                param,
                function (data) {
                    var m = jQuery.parseJSON(data);
                    $("#funcTree").tree({
                        animate: true,
                        data: m,
                        checkbox: true,
                        onClick: function (node) {
                            if (!$('#funcTree').tree('isLeaf', node.target)) {
                                $('#funcTree').tree('toggle', node.target);
                            }
                        }
                    });
                }
            );
        }

        function save() {
            var nodes = $("#funcTree").tree("getChecked");

            if (nodes.length > 0) {
                var ids = "0";
                for (var i = 0; i < nodes.length; i++) {
                    ids = ids + "," + nodes[i].id;
                }

                var role = $("#roleTree").tree("getSelected");

                if (role == null) {
                    jQuery.messager.alert("注意", "请选择角色！！", "warning");
                    return;
                } else {
                    var param = { action: "save", ids: ids, roleId: role.id };
                    jQuery.post(
                        "Action.aspx",
                        param,
                        function (data) {
                            if (parseInt(eval(data).msg) == 1) {
                                jQuery.messager.alert("成功", "为该角色分配功能成功！！", "info");
                            } else {
                                jQuery.messager.alert("失败", "为该角色分配功能失败！！", "error");
                            }
                        },
                        'json'
                    );
                }
            } else {
                jQuery.messager.alert("注意", "请为该角色分配功能！！", "warning");
            }
        }
    </script>
</head>
<body class="easyui-layout">
    <div region="north" split="true" style="height: 40px; line-height: 40px; padding: 5px;
        _display: inline;">
        <a class="easyui-linkbutton" iconcls="icon-save" href="javascript:void(0)" onclick="save()">
            &nbsp;保存&nbsp;</a>
    </div>
    <div region="west" split="true" style="width: 300px; padding: 1px; overflow: hidden;">
        <ul id="roleTree" class="easyui-tree">
        </ul>
    </div>
    <div region="center" style="overflow: hidden;">
        <ul id="funcTree" class="easyui-tree">
        </ul>
    </div>
</body>
</html>
