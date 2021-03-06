﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.manage.webhtml.View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>静态页面管理</title>
    <link href="../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../libs/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background-color: #eeeeee;
        }
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
        $(document).ready(function () {
            getLocations();
        });

        function createHtml(tag, msgs) {
            var t = $("#location").combotree("tree");
            var n = t.tree("getSelected");

            if (t.tree('isLeaf', n.target)) {
                var m = $("#" + tag + "Div");
                m.html("操作进行中……");

                var param = { action: tag, locationId: n.id };

                jQuery.post(
                    "Action.aspx",
                    param,
                    function (data) {
                        var d = eval(data);

                        if (parseInt(d.msg) == 1) {
                            m.html("成功生成【" + msgs + "】！！！");
                        } else {
                            m.html("生成【" + msgs + "】失败。请重试！");
                        }
                    },
                    'json'
                );
            } else {
                jQuery.messager.alert('错误', '请选择操作的城市！', 'error');
            }
        }

        function getLocations() {
            $("#location").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            var param = { action: "tree", lType: "city" };
            jQuery.post(
                "../sys/location/Action.aspx",
                param,
                function (data) {
                    var l = eval(data);
                    $("#location").combotree("loadData", l);
                    $("#location").combotree("setValue", $("#locationId").val());
                    var nodes = $("#location").combotree("tree").tree('getChildren');
                    if (nodes.length == 1) {
                        $("#location").combotree('disable');
                    } else {
                        $("#location").combotree('tree').tree('expandAll');
                    }
                },
                "json"
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
        <tr style="background-color: #fff;">
            <td style="text-align: center;">
                选择城市：
            </td>
            <td>
                &nbsp;<select class="txtInput" id="location">
                </select><input type="hidden" id="locationId" value="<%=locationId %>" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('index', '网站首页')">&nbsp;网站首页&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="indexDiv"></span>
            </td>
        </tr>
        <tr style="background-color: #fff;">
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
        <tr style="background-color: #fff;">
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
        <tr style="background-color: #fff;">
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('activityList', '活动列表')">&nbsp;活动列表&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="activityListDiv"></span>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                &nbsp;<a class="easyui-linkbutton" href="javascript:void(0)" onclick="createHtml('activityDetail', '活动详情')">&nbsp;活动详情&nbsp;</a>
            </td>
            <td>
                &nbsp;<span id="activityDetailDiv"></span>
            </td>
        </tr>
        <tr style="background-color: #fff;">
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
        <tr style="background-color: #fff;">
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
