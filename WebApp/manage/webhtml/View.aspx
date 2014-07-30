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
                        m.html("成功生成首页！！！");
                    } else {
                        m.html("生成首页失败。请重试！");
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
            <td width="50">
                &nbsp;
            </td>
            <td width="100">
                操作命令
            </td>
            <td>
                &nbsp;信息
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="index()">&nbsp;生成首页&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="indexDiv"></span>
            </td>
        </tr>
    </table>
</body>
</html>
