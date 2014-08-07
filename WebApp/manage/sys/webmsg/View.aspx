<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.manage.sys.webmsg.View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>基本信息编辑</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/kindeditor/themes/default/default.css" rel="stylesheet"
        type="text/css" />
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
    <script type="text/javascript" src="../../../libs/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="../../../libs/kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript">
        var editor;

        KindEditor.ready(function (K) {
            editor = K.create('textarea[id="memo"]', {
                resizeType: 2,
                filterMode: false,
                allowFileManager: true,
                allowImageUpload: true,
                fileManagerJson: '../../../uploadAction/file_manager_json.aspx',
                uploadJson: '../../../uploadAction/upload_json.aspx',
                resizeMode: 1,
                allowPreviewEmoticons: true,
                allowUpload: true,
                shadowMode: false,
                basePath: '../../../',
                themesPath: '../../../libs/kindeditor/themes/',
                pluginsPath: '../../../libs/kindeditor/plugins/',
                newlineTag: 'br',
                items: [
                        'source', '|', 'undo', 'redo', '|', 'preview', 'cut', 'copy', 'paste',
                        'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
                        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
                        'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '|',
                        'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
                        'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image',
                        'flash', 'insertfile', 'table', 'hr', 'emoticons', 'pagebreak',
                        'link', 'unlink'
                        ]
            });
        });

        $(document).ready(function () {
            initItems();
            getLocations();
            getOne();
        });

        function save() {
            var t = $("#location").combotree('tree');
            var n = t.tree('getSelected');
            if (t.tree("isLeaf", n.target)) {
                var locationId = n.id;

                var paramStr = "fullName,shortName,keywords,memo,qq,address,phone,cellphone,webUrl,webNo,locationId";

                var fullName = $("#fullName").val();
                var shortName = $("#shortName").val();
                var keywords = $("#keywords").val();
                var memo = editor.html();
                var address = $("#address").val();
                var qq = $("#qq").val();
                var phone = $("#phone").val();
                var cellphone = $("#cellphone").val();
                var webUrl = $("#webUrl").val();
                var webNo = $("#webNo").val();

                var param = {
                    action: "save",
                    locationId: locationId,
                    paramStr: paramStr,
                    fullName: fullName,
                    shortName: shortName,
                    keywords: keywords,
                    qq: qq,
                    memo: memo,
                    address: address,
                    phone: phone,
                    cellphone: cellphone,
                    webUrl: webUrl,
                    webNo: webNo
                };

                jQuery.post(
                    "Action.aspx",
                    param,
                    function (data) {
                        var d = eval(data);
                        if (parseInt(d.msg) == 1) {
                            jQuery.messager.alert('信息', '信息保存成功！', 'info');
                            getOne();
                        } else {
                            jQuery.messager.alert('错误', '保存失败！', 'error');
                        }
                    },
                    "json"
                );
            } else {
                jQuery.messager.alert('错误', '请选择相应城市！', 'error');
            }
        }

        function getOne() {
            var t = $("#location").combotree('tree');
            var n = t.tree('getSelected');

            if (n != null && t.tree('isLeaf', n.target)) {
                var locationId = n.id;
                jQuery.post(
                    "Action.aspx",
                    {
                        action: "one",
                        locationId: locationId
                    },
                    function (data) {
                        if (data != "") {
                            var d = data;
                            $("#fullName").val(d.fullName);
                            $("#shortName").val(d.shortName);
                            $("#keywords").val(d.keywords);
                            editor.html(d.memo);
                            $("#webUrl").val(d.webUrl);
                            $("#qq").val(d.qq);
                            $("#phone").val(d.phone);
                            $("#cellphone").val(d.cellphone);
                            $("#webNo").val(d.webNo);
                            $("#address").val(d.address);
                        }
                    },
                    "json"
                );
            }
        }

        function getLocations() {
            var param = { action: "tree", lType: "city" };
            jQuery.post(
                "../location/Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    $("#location").combotree("loadData", d);
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

        function initItems() {
            $("#mainPanel").panel({
                title: "&nbsp;基本信息编辑",
                fit: true
            });

            $("#location").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200,
                onChange: function (newValue, oldValue) {
                    getOne();
                }
            });

            $("#btnSave").linkbutton({
                iconCls: "icon-save"
            });
        }
    </script>
</head>
<body>
    <div id="mainPanel">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="100" class="algR">
                    所属城市：
                </td>
                <td width="">
                    <select class="txtInput" id="location">
                    </select>
                </td>
            </tr>
            <tr>
                <td width="100" class="algR">
                    网站完整名称：
                </td>
                <td width="">
                    <input class="txtInput w400" type="text" id="fullName" value="" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    网站简化名称：
                </td>
                <td>
                    <input class="txtInput w400" type="text" id="shortName" value="" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    网站链接：
                </td>
                <td>
                    <input class="txtInput w400" type="text" id="webUrl" value="" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    联系电话：
                </td>
                <td>
                    <input class="txtInput w400" type="text" id="phone" value="" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    移动电话：
                </td>
                <td>
                    <input class="txtInput w400" type="text" id="cellphone" value="" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    客服QQ：
                </td>
                <td>
                    <input class="txtInput w400" type="text" id="qq" value="" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    联系地址：
                </td>
                <td>
                    <input class="txtInput w400" type="text" id="address" value="" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    网站备案号：
                </td>
                <td>
                    <input class="txtInput w400" type="text" id="webNo" value="" />
                </td>
            </tr>
            <tr>
                <td class="algR" style="height: 100px">
                    全局关键词：
                </td>
                <td>
                    <textarea class="txtArea" rows="" cols="" style="height: 100px; margin-bottom: 5px;"
                        id="keywords"></textarea>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    网站文字介绍：
                </td>
                <td>
                    <textarea class="txtArea" rows="" cols="" id="memo"></textarea>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    &nbsp;
                </td>
                <td>
                    <a id="btnSave" href="javascript:void(0)" onclick="save()">&nbsp;保存&nbsp;</a>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <input type="hidden" id="locationId" value="<%=locationId %>" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
