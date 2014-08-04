<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApp.manage.user.member.Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>公司编辑</title>
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
    <script type="text/javascript" src="../../../libs/ajaxfileupload.js"></script>
    <script type="text/javascript">
        var editor;

        $(document).ready(function () {
            getTree();
            getOne();
        });

        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="memo"]', {
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
                        'source', '|', 'undo', 'redo', '|', 'preview', 'template', 'cut', 'copy', 'paste',
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

        function uploadPic() {
            var f = $("#imgFile").val();
            var t = isUploadFile(f);
            var fileToUpload = 'imgFile';

            if (t) {
                jQuery.ajaxFileUpload({
                    url: '../../../uploadAction/upload_json.aspx',
                    secureuri: false,
                    fileElementId: fileToUpload,
                    dataType: 'json',
                    success: function (data, status) {
                        if (typeof (data.error) != 'undefined') {
                            if (parseInt(data.error) == 0) {
                                $("#logoUrl").val(data.url);
                                $("#logoPic").html('<img src="' + data.url + '" />');
                            } else {
                                alert(data.message);
                            }
                        }
                    },
                    error: function (data, status, e) {
                        alert(e);
                    }
                });
            } else {
                alert("请选择要导入的图片");
                return;
            }
            /**/
        }

        function save() {
            var userId = $("#userId").val();
            var userName = $("#userName").val();
            var memberId = $("#memberId").val();
            var locationId;

            var t = $("#location").combotree('tree');
            var n = t.tree('getSelected');

            var fullName = $("#fullName").val();
            var shortName = $("#shortName").val();
            var address = $("#address").val();
            var tel = $("#tel").val();
            var cellphone = $("#cellphone").val();
            var fax = $("#fax").val();
            var qq = $("#qq").val();
            var email = $("#email").val();
            var transit = $("#transit").val();
            var logoUrl = $("#logoUrl").val();
            var memo = editor.html();
            var suggestNo = $("#suggestNo").val();
            var keywords = $("#keywords").val();
            var itemIndex = $("#itemIndex").val();

            if (fullName == null) {
                jQuery.messager.alert('错误', '请输入公司全称！', 'error');
                $("#fullName").focus();
                return;
            }
            if (shortName == null) {
                jQuery.messager.alert('错误', '请输入公司简称！', 'error');
                $("#shortName").focus();
                return;
            }
            if (userName == null) {
                jQuery.messager.alert('错误', '请输入用户名！', 'error');
                $("#userName").focus();
                return;
            }
            if (editor.text() == null) {
                jQuery.messager.alert('错误', '请输入公司简介', 'error');
                editor.focus();
                return;
            }
            if (keywords == null) {
                jQuery.messager.alert('错误', '请输入关键词，以逗号分隔！', 'error');
                $("#keywords").focus();
                return;
            }
            if (itemIndex == null) {
                jQuery.messager.alert('错误', '请输入排序值。数值越大越往前！', 'error');
                $("#itemIndex").focus();
                return;
            }
            if (n == null) {
                jQuery.messager.alert('错误', '请选择文章分类！！', 'error');
                $("#process").focus();
                return;
            } else {
                locationId = n.id;
            }

            var param = {
                action: "save",
                paramStr: "memberId,userId,locationId,userName,fullName,shortName,address,tel,cellphone,fax,qq,email,transit,logoUrl,keywords,memo,suggestNo,itemIndex",
                memberId: memberId,
                userName: userName,
                userId: userId,
                locationId: locationId,
                fullName: fullName,
                shortName: shortName,
                address: address,
                tel: tel,
                cellphone: cellphone,
                fax: fax,
                qq: qq,
                email: email,
                transit: transit,
                logoUrl: logoUrl,
                keywords: keywords,
                memo: memo,
                suggestNo: suggestNo,
                itemIndex: itemIndex
            };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    if (parseInt(d.msg) == 1) {
                        jQuery.messager.confirm('保存成功', '你想输入新信息么？', function (r) {
                            if (r) {
                                window.location.href = "?memberId=0";
                            } else {
                                window.location.href = "List.aspx?locationId=" + locationId;
                            }
                        });
                    } else {
                        jQuery.messager.alert('错误', '保存失败！', 'error');
                    }
                },
                'json'
            );
        }

        function getOne() {
            var memberId = $("#memberId").val();
            if (parseInt(memberId) > 0) {
                jQuery.post(
                    "Action.aspx",
                    {
                        action: "one",
                        memberId: memberId
                    },
                    function (data) {
                        var d = eval(data);

                        $("#userId").val(d.userId);
                        $("#userName").val(d.userName);
                        $("#fullName").val(d.fullName);
                        $("#shortName").val(d.shortName);
                        $("#address").val(d.address);
                        $("#tel").val(d.tel);
                        $("#cellphone").val(d.cellphone);
                        $("#fax").val(d.fax);
                        $("#qq").val(d.qq);
                        $("#email").val(d.email);
                        $("#transit").val(d.transit);
                        $("#keywords").val(d.keywords);
                        $("#memo").val(d.memo);
                        $("#suggestNo").val(d.suggestNo);
                        $("#itemIndex").val(d.itemIndex);

                        editor.html(d.memo);

                        $('#location').combotree('setValue', d.locationId);

                        if (d.logoUrl != null && isUploadFile(d.logoUrl)) {
                            $("#logoUrl").val(d.logoUrl);
                            $("#logoPic").html("<img src='" + d.logoUrl + "' />");
                        } else {
                            $("#logoUrl").val("");
                            $("#logoPic").html("");
                        }

                        $("#userName").attr("readonly", "readonly");
                    },
                    'json'
                );
            } else {
                $("#userId").val("0");
            }
        }

        function getTree() {
            $("#location").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            jQuery.post(
                "../../sys/location/Action.aspx",
                {
                    action: "tree",
                    lType: "region"
                },
                function (data) {
                    var d = jQuery.parseJSON(data);
                    $("#location").combotree('loadData', d);
                    $("#location").combotree('tree').tree('expandAll');
                }
            );
        }

        function isUploadFile(filePath) {
            var b = false;
            var extName = filePath.substr(filePath.lastIndexOf(".") + 1);
            var extFiles = "gif,jpg,jpeg,png,bmp,swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb,doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2".split(',');
            for (var i = 0, j = extFiles.length; i < j; i++) {
                if (extFiles[i].toString() == extName.toString()) {
                    b = true;
                }
            }

            return b;
        }
    </script>
</head>
<body>
    <div class="easyui-panel" title="&nbsp;装修知识" fit="true">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="100" class="algR">
                    公司全称：
                </td>
                <td width="">
                    <input class="easyui-validatebox textbox txtInput w400" type="text" id="fullName"
                        required="true" />
            </tr>
            <tr>
                <td class="algR">
                    公司简称：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="shortName" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    网站用户名：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="userName" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    关键词：
                </td>
                <td>
                    <textarea class="txtArea" rows="" cols="" style="height: 50px; margin-bottom: 5px;"
                        id="keywords"></textarea>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    所属地区：
                </td>
                <td>
                    <select class="easyui-combotree txtInput" required="true" panelwidth="200" panelheight="200"
                        id="location">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    联系地址：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="address" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    固定电话：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="tel" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    移动电话：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="cellphone" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    传真：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="fax" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    QQ/微信：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="qq" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    Email：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="email" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    周边公交、地铁：
                </td>
                <td>
                    <textarea class="txtArea" rows="" cols="" style="height: 50px; margin-bottom: 5px;"
                        id="transit"></textarea>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    Logo图片：
                </td>
                <td>
                    <input type="file" class="txtInput w400" name="imgFile" id="imgFile" onchange="uploadPic()" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    <input type="hidden" id="logoUrl" value="" />
                </td>
                <td id="logoPic">
                </td>
            </tr>
            <tr>
                <td class="algR">
                    公司简介：
                </td>
                <td>
                    <textarea id="memo" name="memo" cols="" rows="" class="txtArea"></textarea>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    推荐值：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w80" type="text" id="suggestNo"
                        value="1000" required="true" /><span style="color: Red; margin-left: 10px;">数值越大越前面</span>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    排序值：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w80" type="text" id="itemIndex"
                        value="1000" required="true" /><span style="color: Red; margin-left: 10px;">数值越大越前面</span>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    &nbsp;
                </td>
                <td>
                    &nbsp;&nbsp;<a class="easyui-linkbutton" iconcls="icon-save" href="javascript:void(0)"
                        onclick="save()">&nbsp;保存&nbsp;</a>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    &nbsp;
                </td>
                <td>
                    <input type="hidden" id="memberId" value="<%=memberId %>" />
                    <input type="hidden" id="userId" value="" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
