﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebManage.aspx.cs" Inherits="WebApp.manage.WebManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>欢迎使用装修人人通管理系统！</title>
    <link href="../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../libs/global.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../libs/jquery.js"></script>
    <script type="text/javascript" src="../libs/jquery.md5.js"></script>
    <script type="text/javascript" src="../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setInterval(keepSession, 1000000);
            doFuncTree();
        });

        function keepSession() {
            jQuery.post(
                "Action.aspx",
                { action: "keepSession" },
                function (data) {
                    if (parseInt(data) == 0) {
                        jQuery.messager.alert('错误！！！', '与服务器链接断开！请退出重新登录！', 'error', function () {
                            logout();
                        });
                    }
                }
            );
        }

        function logout() {
            var param = { action: "logout" };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) { }
            );
            window.location.href = "/manage/";
        }

        function modpwd() {
            $("#oldpwd").val("");
            $("#newpwd").val("");
            $("#repwd").val("");
            $("#code").val("");
            $("#codeImg").attr("src", "/VerifyCodeAction.aspx?id=" + Math.random().toString());
            $("#pwd").window("open");
        }

        function savepwd() {
            var oldpwd = $("#oldpwd").val();
            var newpwd = $("#newpwd").val();
            var code = $("#code").val();
            var repwd = $("#repwd").val();

            if (oldpwd == "") { jQuery.messager.alert('错误！！！', '请输入原先密码！！！', 'error'); $("#oldpwd").focus(); }
            if (newpwd == "") { jQuery.messager.alert('错误！！！', '请输入新密码！！！', 'error'); $("#newpwd").focus(); }
            if (repwd == "") { jQuery.messager.alert('错误！！！', '请输入重复新密码！！！', 'error'); $("#repwd").focus(); }
            if (code == "") { jQuery.messager.alert('错误！！！', '请输入验证码！！！', 'error'); $("#code").focus(); }

            var param = { action: "modPwd", oldPwd: jQuery.md5(oldpwd), newPwd: jQuery.md5(newpwd), xcode: code }

            if (newpwd == repwd) {
                jQuery.post(
                    "Action.aspx",
                    param,
                    function (data) {
                        var m = jQuery.parseJSON(data);
                        switch (parseInt(m.msg)) {
                            case 0:
                                jQuery.messager.alert('错误！！！', '验证码输入错误！！！', 'error');
                                $("#codeImg").attr("src", $("#codeImg").attr("src") + "?");
                                $("#code").val("");
                                $("#code").focus();
                                break;
                            case 1:
                                jQuery.messager.alert('错误！！！', '原始密码输入错误！！！', 'error');
                                $("#oldpwd").val("");
                                $("#oldpwd").focus();
                                break;
                            case 2:
                                jQuery.messager.alert('错误！！！', '新密码修改失败！！！', 'error');
                                $("#newpwd").val("");
                                $("#repwd").val("");
                                $("#newpwd").focus();
                                break;
                            case 3:
                                jQuery.messager.alert('信息！！！', '新密码修改成功！请重新登录！', 'info', function () { logout(); });
                                break;
                        }
                    }
                );
            } else {
                jQuery.messager.alert('错误：', '两次密码输入不一致！！！', 'error');
            }
        }

        function privateInfo() {
            var param = { action: "getAdmin" };
            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var u = jQuery.parseJSON(data);
                    $("#userName").text(u.userName);
                    $("#fullName").val(u.fullName);
                    $("#location").text(u.location);
                    $("#phone").val(u.phone);
                    $("#email").val(u.email);
                    $("#qq").val(u.qq);
                    $("#insertTime").text(u.insertTime);
                    $("#updateTime").text(u.updateTime);
                }
           );

            $("#privateInfo").window("open");
        }

        function savePrivate() {
            var param = {
                action: "saveAdmin",
                paramStr: "fullName,phone,email,qq",
                fullName: $("#fullName").val(),
                phone: $("#phone").val(),
                email: $("#email").val(),
                qq: $("#qq").val()
            };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var m = jQuery.parseJSON(data);
                    if (parseInt(m.msg) == 1) {
                        $("#privateInfo").window("close");
                        jQuery.messager.alert('信息', '保存信息成功！');
                    } else {
                        $("#privateInfo").window("close");
                        jQuery.messager.alert('错误', '保存信息失败！建议重新登录，再次修改信息！', 'error');
                    }
                }
            );
        }

        function doFuncTree() {
            var param = {
                action: "tree"
            };
            jQuery.post(
                "sys/function/Action.aspx",
                param,
                function (data) {
                    var m = jQuery.parseJSON(data);
                    $("#funcTree").tree({
                        animate: true,
                        //dnd: true,
                        data: m,
                        onClick: function (node) {
                            if ($('#funcTree').tree('isLeaf', node.target)) {
                                newTab(node.text, node.attributes.url);
                            } else {
                                $('#funcTree').tree('toggle', node.target);
                            }
                        }
                    });
                }
            );
        }

        function newTab(tabName, url) {
            if ($("#tabs").tabs("exists", tabName)) {
                $("#tabs").tabs("close", tabName);
            }
            $("#tabs").tabs("add", {
                title: tabName,
                content: '<iframe scrolling="yes" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>',
                closable: true
            });
        }
    </script>
</head>
<body class="easyui-layout">
    <div region="north" split="true" title="欢迎使用装修人人通管理系统" style="height: 100px;">
        <div style="float: left; width: 240px; height: 60px;">
            <img src="imgs/logo.jpg" width="240" height="60" border="0" alt="欢迎使用装修人人通管理系统" title="欢迎使用装修人人通管理系统" /></div>
        <div style="float: right; width: auto; height: 30px; line-height: 30px; font-weight: bold;">
            账号：<a href="javascript:void(0);" onclick="privateInfo()"><%=userName %></a>&nbsp;&nbsp;姓名：<%=fullName %>&nbsp;&nbsp;上次登录：<%=lastLogin %>&nbsp;&nbsp;<a
                href="javascript:void(0);" onclick="modpwd()">修改密码</a>&nbsp;&nbsp;<a href="javascript:void(0);"
                    onclick="logout()">退出</a>&nbsp;&nbsp;</div>
    </div>
    <div region="west" split="true" title="装修人人通管理系统" style="width: 200px; padding1: 1px;
        overflow: hidden;">
        <ul id="funcTree" class="easyui-tree">
        </ul>
    </div>
    <div region="center" style="overflow: hidden;">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="管理首页" style="padding: 20px; overflow: hidden; _display: inline">
            </div>
        </div>
    </div>
    <div id="pwd" class="easyui-window" shadow="true" modal="true" minimizable="false"
        maximizable="false" closable="true" closed="true" collapsible="false" resizable="false"
        iconcls="icon-blank" title="密码修改" style="width: 370px; height: 280px;">
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
                    原密码：
                </td>
                <td class="algL">
                    <input type="password" id="oldpwd" class="easyui-validatebox textbox txtInput w200"
                        value="" required="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    新密码：
                </td>
                <td class="algL">
                    <input type="password" id="newpwd" class="easyui-validatebox textbox txtInput w200"
                        value="" required="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    重复新密码：
                </td>
                <td class="algL">
                    <input type="password" id="repwd" class="easyui-validatebox textbox txtInput w200"
                        value="" required="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    验证码：
                </td>
                <td class="algL">
                    <input type="text" id="code" class="easyui-validatebox textbox txtInput w80" value=""
                        required="true" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    &nbsp;
                </td>
                <td class="algL" style="height: 40px">
                    <img id="codeImg" src="" width="60" height="30" />
                </td>
                <td class="algL">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="algL">
                    <a class="easyui-linkbutton" iconcls="icon-save" href="javascript:void(0)" onclick="savepwd()">
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
    <div id="privateInfo" class="easyui-window" shadow="true" modal="true" minimizable="false"
        maximizable="false" closable="true" closed="true" collapsible="false" resizable="false"
        iconcls="icon-blank" title="个人信息设置" style="width: 400px; height: 360px;">
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
                    账号：
                </td>
                <td class="algL">
                    <span id="userName"></span>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    姓名：
                </td>
                <td class="algL">
                    <input type="text" id="fullName" class="txtInput w200" value="" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    所属：
                </td>
                <td class="algL">
                    <span id="location"></span>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    联系电话：
                </td>
                <td class="algL">
                    <input type="text" id="phone" class="txtInput w200" value="" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    邮箱：
                </td>
                <td class="algL">
                    <input type="text" id="email" class="txtInput w200" value="" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    QQ/微信：
                </td>
                <td class="algL">
                    <input type="text" id="qq" class="txtInput w200" value="" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    账号创建：
                </td>
                <td class="algL">
                    <span id="insertTime"></span>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="algR">
                    上次修改：
                </td>
                <td class="algL">
                    <span id="updateTime"></span>
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
                    <a class="easyui-linkbutton" iconcls="icon-save" href="javascript:void(0)" onclick="savePrivate()">
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
