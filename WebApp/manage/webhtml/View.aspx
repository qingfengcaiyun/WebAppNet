<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.manage.webhtml.View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>静态页面管理</title>
    <link href="../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../libs/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        th
        {
            height: 30px;
            font-weight: bolder;
            background-color: #eeeeee;
        }
        td
        {
            height: 30px;
        }
    </style>
    <script type="text/javascript" src="../../libs/jquery.js"></script>
    <script type="text/javascript" src="../../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        function index() {
            var m = $("#indexDiv");
            m.html("操作进行中……");

            var param = { action: "index" };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);

                    if (parseInt(d.msg) == 1) {
                        m.html("成功生成网站首页！！！");
                    } else {
                        m.html("生成网站首页失败。请重试！");
                    }
                },
                'json'
            );
        }

        function process() {
            var m = $("#processDiv");
            m.html("操作进行中……");

            var param = { action: "process" };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);

                    if (parseInt(d.msg) == 1) {
                        m.html("成功生成流程首页！！！");
                    } else {
                        m.html("生成流程首页失败。请重试！");
                    }
                },
                'json'
            );
        }

        function processList() {
            var m = $("#processListDiv");
            m.html("操作进行中……");

            var param = { action: "processList" };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);

                    if (parseInt(d.msg) == 1) {
                        m.html("成功生成流程列表！！！");
                    } else {
                        m.html("生成流程列表失败。请重试！");
                    }
                },
                'json'
            );
        }

        function processItem() {
            var m = $("#processItemDiv");
            m.html("操作进行中……");

            var param = { action: "processItem" };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);

                    if (parseInt(d.msg) == 1) {
                        m.html("成功生成流程信息！！！");
                    } else {
                        m.html("生成流程信息失败。请重试！");
                    }
                },
                'json'
            );
        }
    </script>
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <th width="150">
                操作命令
            </th>
            <th style="text-align: left;">
                &nbsp;信息
            </th>
        </tr>
        <tr>
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="index()">&nbsp;网站首页&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="indexDiv"></span>
            </td>
        </tr>
        <tr style="background-color: #eeeeee;">
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="process()">&nbsp;流程首页&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="processDiv"></span>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="processList()">&nbsp;流程列表&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="processListDiv"></span>
            </td>
        </tr>
        <tr style="background-color: #eeeeee;">
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="processItem()">&nbsp;流程信息&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="processItemDiv"></span>
            </td>
        </tr>
    </table>
</body>
</html>
