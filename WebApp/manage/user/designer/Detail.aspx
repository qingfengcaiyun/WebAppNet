<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApp.manage.user.designer.Detail" %>

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
    <script type="text/javascript" src="../../../libs/ajaxfileupload.js"></script>
    <script type="text/javascript">
        var editor;

        $(document).ready(function () {
            initItems();
            initLocationTree();
            getMemberTree();

            $("#location").combotree({
                onChange: function (newValue, oldValue) {
                    getMemberTree();
                }
            });

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

        function switchOption(v) {
            var b = $("#sex").val(v);
            if (v == "男") {
                $("#topYes").show();
                $("#topNo").hide();
            } else {
                $("#topYes").hide();
                $("#topNo").show();
            }
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
                                $("#photoUrl").val(data.url);
                                $("#photoPic").html('<img src="' + data.url + '" />');
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
            var designerId = $("#designerId").val();
            var userName = $("#userName").val();

            var m = $("#member").combotree('tree').tree('getSelected');
            var l = $("#location").combotree('tree').tree('getSelected');

            var memberId;
            var locationId;

            var fullName = $("#fullName").val();
            var sex = $("#sex").val();
            var job = $("#job").val();
            var tel = $("#tel").val();
            var cellphone = $("#cellphone").val();
            var qq = $("#qq").val();
            var email = $("#email").val();
            var photoUrl = $("#photoUrl").val();
            var memo = editor.html();
            var suggestNo = $("#suggestNo").val();
            var itemIndex = $("#itemIndex").val();

            if (fullName == null) {
                jQuery.messager.alert('错误', '请输入全名！', 'error');
                $("#fullName").focus();
                return;
            }
            if (userName == null) {
                jQuery.messager.alert('错误', '请输入用户名！', 'error');
                $("#userName").focus();
                return;
            }
            if (l == null) {
                jQuery.messager.alert('错误', '请选择所属地区！！', 'error');
                $("#location").focus();
                return;
            } else {
                locationId = l.id;
            }
            if (m == null) {
                jQuery.messager.alert('错误', '请选择所属公司！！', 'error');
                $("#member").focus();
                return;
            } else {
                memberId = m.id;
            }
            if (editor.text() == null) {
                jQuery.messager.alert('错误', '请输入设计师简介', 'error');
                editor.focus();
                return;
            }
            if (suggestNo == null) {
                jQuery.messager.alert('错误', '请输入推荐值。数值越大越往前！', 'error');
                $("#suggestNo").focus();
                return;
            }
            if (itemIndex == null) {
                jQuery.messager.alert('错误', '请输入排序值。数值越大越往前！', 'error');
                $("#itemIndex").focus();
                return;
            }


            var param = {
                action: "save",
                paramStr: "designerId,userName,userId,locationId,fullName,sex,memberId,job,tel,cellphone,qq,email,photoUrl,memo,suggestNo,itemIndex",
                designerId: designerId,
                userName: userName,
                userId: userId,
                locationId: locationId,
                fullName: fullName,
                sex: sex,
                memberId: memberId,
                job: job,
                tel: tel,
                cellphone: cellphone,
                qq: qq,
                email: email,
                photoUrl: photoUrl,
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
                                window.location.href = "Detail.aspx?designerId=0";
                            } else {
                                window.location.href = "List.aspx?locationId=" + locationId + "&memberId=" + memberId;
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
            var designerId = $("#designerId").val();
            if (parseInt(designerId) > 0) {
                jQuery.post(
                    "Action.aspx",
                    {
                        action: "one",
                        designerId: designerId
                    },
                    function (data) {
                        var d = eval(data);

                        $("#designerId").val(d.designerId);
                        $("#userId").val(d.userId);
                        $("#fullName").val(d.fullName);
                        $("#sex").val(d.sex);
                        switchOption(d.sex);
                        $("#userName").val(d.userName);

                        $("#job").val(d.job);
                        $("#tel").val(d.tel);
                        $("#cellphone").val(d.cellphone);
                        $("#qq").val(d.qq);
                        $("#email").val(d.email);
                        $("#suggestNo").val(d.suggestNo);
                        $("#itemIndex").val(d.itemIndex);

                        editor.html(d.memo);

                        $('#location').combotree('setValue', d.locationId);
                        getMemberTree();
                        $('#member').combotree('setValue', d.memberId);

                        if (d.photoUrl != null && isUploadFile(d.photoUrl)) {
                            $("#photoUrl").val(d.photoUrl);
                            $("#photoPic").html("<img src='" + d.photoUrl + "' />");
                        } else {
                            $("#photoUrl").val("");
                            $("#photoPic").html("");
                        }

                        $("#userName").attr("readonly", "readonly");
                    },
                    'json'
                );
            } else {
                $("#userId").val("0");
                $("#sex").val(1);
                switchOption(1);
            }
        }

        function initLocationTree() {
            var param = { action: "tree", lType: "region" };
            jQuery.post(
                "../../sys/location/Action.aspx",
                param,
                function (data) {
                    //alert(data);
                    var d = eval(data);
                    //alert(d);
                    $("#location").combotree('loadData', d);
                    $("#location").combotree('setValue', $("#locationId").val());
                    $("#location").combotree('tree').tree('expandAll');
                },
                'json'
            );
        }

        function getMemberTree() {
            var n = $("#location").combotree("tree").tree("getSelected");
            if (n != null) {
                var param = { action: "tree", locationId: n.id };
                jQuery.post(
                    "../member/Action.aspx",
                    param,
                    function (data) {
                        //alert(data);
                        var d = eval(data);
                        //alert(d);
                        $("#member").combotree('loadData', d);
                        $("#member").combotree('tree').tree('expandAll');
                    },
                    'json'
                );
            }
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
                    全名：
                </td>
                <td width="">
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="fullName"
                        required="true" />
            </tr>
            <tr>
                <td class="algR">
                    性别：
                </td>
                <td>
                    <span id="topNo" class="top"><a class="easyui-linkbutton" iconcls="icon-add" href="javascript:void(0)"
                        onclick="switchOption('男')">&nbsp;女</a></span><span id="topYes" class="top"><a class="easyui-linkbutton"
                            iconcls="icon-back" href="javascript:void(0)" onclick="switchOption('女')">&nbsp;男</a></span>
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
                    所属地区：
                </td>
                <td>
                    <select class="txtInput" id="location">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    所属公司：
                </td>
                <td>
                    <select class="txtInput" id="member">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    职位：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="job" />
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
                    个人照片：
                </td>
                <td>
                    <input type="file" class="txtInput w400" name="imgFile" id="imgFile" onchange="uploadPic()" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    <input type="hidden" id="photoUrl" value="" />
                </td>
                <td id="photoPic">
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
                    <input type="hidden" id="designerId" value="<%=designerId %>" />
                    <input type="hidden" id="userId" value="" />
                    <input type="hidden" id="sex" value="" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
