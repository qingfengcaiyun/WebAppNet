<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApp.manage.info.activity.Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>活动编辑</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/kindeditor/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../libs/jquery.bigcolorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td
        {
            height: 30px;
        }
        .top
        {
            float: left;
            margin-left: 15px;
        }
    </style>
    <script type="text/javascript" src="../../../libs/jquery.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../libs/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="../../../libs/kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../../../libs/jquery.bigcolorpicker.js"></script>
    <script type="text/javascript" src="../../../libs/ajaxfileupload.js"></script>
    <script type="text/javascript">
        var editor;

        $(document).ready(function () {
            getTree();
            switchTopOption(0);
            getOne();

            $("#titleColor").bigColorpicker(function (el, color) {
                $(el).css("backgroundColor", color);
                $(el).val(color);
            });
        });

        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="content"]', {
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

        function switchTopOption(v) {
            var b = $("#onIndex").val(v);
            if (parseInt(v) == 1) {
                $("#topYes").show();
                $("#topNo").hide();
            } else {
                $("#topYes").hide();
                $("#topNo").show();
            }
        }

        function save() {
            var n = $("#location").combotree('tree').tree('getSelected');

            var locationId;

            var actName = $("#actName").val();

            var titleColor = $("#titleColor").val();
            if (titleColor == "标题颜色") {
                titleColor = "#000000";
            }

            var startTime = $("#startTime").datetimebox('getValue');
            var endTime = $("#endTime").datetimebox('getValue');
            var content = editor.html();
            var keywords = $("#keywords").val();
            var itemIndex = $("#itemIndex").val();
            var address = $("#address").val();
            var phone = $("#phone").val();
            var qq = $("#qq").val();
            var onIndex = $("#onIndex").val();
            var actId = $("#actId").val();
            var publicAdpic = $("#publicAdpic").val();

            if (actName == null) {
                jQuery.messager.alert('错误', '请输入活动名称！', 'error');
                $("#actName").focus();
                return;
            }
            if (editor.text() == null) {
                jQuery.messager.alert('错误', '请输入内容', 'error');
                editor.focus();
                return;
            }
            if (keywords == null) {
                jQuery.messager.alert('错误', '请输入关键词，以逗号分隔！', 'error');
                $("#keywords").focus();
                return;
            }
            if (publicAdpic == null) {
                jQuery.messager.alert('错误', '请上传首页显示的图片！', 'error');
                $("#imgFile").focus();
                return;
            }
            if (itemIndex == null) {
                jQuery.messager.alert('错误', '请输入文章排序值。数值越大越往前！', 'error');
                $("#itemIndex").focus();
                return;
            }
            if (n == null) {
                jQuery.messager.alert('错误', '请选择所属区域！！', 'error');
                $("#location").focus();
                return;
            } else {
                locationId = n.id;
            }



            var param = {
                action: "save",
                paramStr: "actId,cityId,actName,titleColor,startTime,endTime,publicAdpic,content,address,phone,qq,keywords,itemIndex,onIndex",
                actId: actId,
                actName: actName,
                titleColor: titleColor,
                cityId: locationId,
                startTime: startTime,
                endTime: endTime,
                publicAdpic: publicAdpic,
                content: content,
                address: address,
                phone: phone,
                qq: qq,
                keywords: keywords,
                itemIndex: itemIndex,
                onIndex: onIndex
            };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    if (parseInt(d.msg) == 1) {
                        jQuery.messager.confirm('保存成功', '你想输入新信息么？', function (r) {
                            if (r) {
                                window.location.href = "Detail.aspx?actId=0";
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
            var actId = $("#actId").val();
            if (parseInt(actId) > 0) {
                jQuery.post(
                    "Action.aspx",
                    {
                        action: "one",
                        actId: actId
                    },
                    function (data) {
                        var d = jQuery.parseJSON(data);

                        $("#actName").val(d.actName);

                        $("#startTime").datetimebox("setValue", d.startTime.toString());
                        $("#endTime").datetimebox("setValue", d.endTime.toString());

                        $("#keywords").val(d.keywords);
                        $("#itemIndex").val(d.itemIndex);
                        $("#address").val(d.address);
                        $("#phone").val(d.phone);
                        $("#qq").val(d.qq);

                        editor.html(d.content);

                        if (d.onIndex.toString().toLowerCase() == "true") {
                            switchTopOption(1);
                        } else {
                            switchTopOption(0);
                        }

                        $('#location').combotree('setValue', d.cityId);
                    }
                );
            }
        }

        function getTree() {
            $("#location").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            var param = { action: "tree", lType: "region" };
            jQuery.post(
                "../../sys/location/Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    $("#location").combotree('loadData', d);
                    $("#location").combotree('setValue', $("#locationId").val());
                    $("#location").combotree('tree').tree('expandAll');
                },
                'json'
            );
        }

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
                                $("#publicAdpic").val(data.url);
                                $("#preview").html('<img src="' + data.url + '" />');
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
    <div class="easyui-panel" title="&nbsp;活动编辑" fit="true">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="100" class="algR">
                    活动名称：
                </td>
                <td width="">
                    <input class="easyui-validatebox textbox txtInput w400" type="text" id="actName"
                        required="true" />
                    &nbsp;
                    <input class="txtInput w80 algC" type="text" id="titleColor" value="标题颜色" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    开始时间：
                </td>
                <td>
                    <input class="easyui-datetimebox" value="10/11/2012 12:34:56" style="width: 200px;"
                        id="startTime" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    结束时间：
                </td>
                <td>
                    <input class="easyui-datetimebox" value="10/11/2012 12:34:56" style="width: 200px;"
                        id="endTime" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    关键词：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="keywords"
                        value="" required="true" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    所属区域：
                </td>
                <td>
                    <select class="easyui-combotree txtInput" required="true" panelwidth="200" panelheight="200"
                        id="location">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    首页图片：
                </td>
                <td>
                    <input type="file" class="txtInput w400" name="imgFile" id="imgFile" onchange="uploadPic()" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    <input type="hidden" id="publicAdpic" value="" />
                </td>
                <td id="preview">
                </td>
            </tr>
            <tr>
                <td class="algR">
                    文章内容：
                </td>
                <td>
                    <textarea id="content" name="content" cols="" rows="" class="txtArea"></textarea>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    首页显示：
                </td>
                <td>
                    <span id="topNo" class="top"><a class="easyui-linkbutton" iconcls="icon-no" href="javascript:void(0)"
                        onclick="switchTopOption(1)">&nbsp;不显示</a></span><span id="topYes" class="top"><a
                            class="easyui-linkbutton" iconcls="icon-ok" href="javascript:void(0)" onclick="switchTopOption(0)">&nbsp;显示</a></span>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    活动地址：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="address"
                        value="" required="true" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    活动电话：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="phone" value=""
                        required="true" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    客服QQ：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="qq" value=""
                        required="true" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    排序值：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w80" type="text" id="itemIndex"
                        value="1000" required="true" />
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
                    <input type="hidden" id="onIndex" value="" />
                    <input type="hidden" id="actId" value="<%=actId %>" />
                    <input type="hidden" id="locationId" value="<%=locationId %>" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
