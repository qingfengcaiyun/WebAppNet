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
        function createHtml(tag, msgs) {
            var m = $("#" + tag + "Div");
            m.html("操作进行中……");

            var param = { action: tag };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);

                    if (parseInt(d.msg) == 1) {
                        m.html("成功生成" + msgs + "！！！");
                    } else {
                        m.html("生成" + msgs + "失败。请重试！");
                    }
                },
                'json'
            );
        }
    </script>
</head>
<body>
    <table width="49%" border="0" cellspacing="0" cellpadding="0" style="float: left">
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
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('index', '网站首页')">&nbsp;网站首页&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="indexDiv"></span>
            </td>
        </tr>
        <tr style="background-color: #eeeeee;">
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('processIndex', '流程首页')">&nbsp;流程首页&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="processIndexDiv"></span>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('processList', '流程列表')">&nbsp;流程列表&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="processListDiv"></span>
            </td>
        </tr>
        <tr style="background-color: #eeeeee;">
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('processDetail', '流程信息')">&nbsp;流程信息&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="processDetailDiv"></span>
            </td>
        </tr>
    </table>
    <table width="49%" border="0" cellspacing="0" cellpadding="0" style="float: left;
        margin-left: 1%">
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
            </td>
            <td>
            </td>
        </tr>
        <tr style="background-color: #eeeeee;">
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('newsIndex', '资讯首页')">&nbsp;资讯首页&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="newsIndexDiv"></span>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('newsList', '资讯列表')">&nbsp;资讯列表&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="newsListDiv"></span>
            </td>
        </tr>
        <tr style="background-color: #eeeeee;">
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('newsDetail', '资讯信息')">&nbsp;资讯信息&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="newsDetailDiv"></span>
            </td>
        </tr>
    </table>
</body>
</html>
