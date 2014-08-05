<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pictures.aspx.cs" Inherits="WebApp.manage.renovation.project.Pictures" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目图片管理</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td
        {
            height: 30px;
        }
        #previews
        {
            padding: 10px;
        }
        .previewDiv
        {
            width: 200px;
            height: 200px;
            margin-left: 10px;
            margin-top: 10px;
            padding: 10px;
            cursor: pointer;
            border: #CCC 1px solid;
            _display: inline;
        }
    </style>
    <script type="text/javascript" src="../../../libs/jquery.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../libs/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../libs/ajaxfileupload.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initItems();
            getPics();
        });

        function preview() {
            var items = $(".previewDiv");

            if (items.length > 0) {
                $(items).each(function () {
                    $(this).toggle(
                        function () {
                            $(this).css("background-color", "#800000");
                            $("#ppcIds").val($("#ppc").val() + "," + $(this).attr("id"));
                        },
                        function () {
                            $(this).css("background-color", "#ffffff");
                            var ppcIds = $("#ppc").val();
                            var p = ppcIds.split(',')
                            var id = $(this).attr("id");
                            var ids = "0";
                            for (var i = 0; i < p.length; i++) {
                                if (parseInt(p[i]) > 0 && parseInt(p[i]) != id) {
                                    ids = ids + "," + p[i];
                                }
                            }

                            $("#ppcIds").val(ids);
                        }
                    );
                });
            }
        }

        function getPics() {
            var projectId = $("#projectId").val();
            var param = { action: "getPics", projectId: projectId };
            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);

                    if (d.length > 0) {
                        var s = "";
                        for (var i = 0; i < d.length; i++) {
                            s = s + "<div id=\"" + d[i].ppcId + "\" class=\"previewDiv\"><img src=\"" + d[i].picPath + "\" height=\"200\" width=\"200\" alt=\"\" /></div>"
                        }
                        $("#preview").html(s);
                        preview();
                    }
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
                                var param = {
                                    action: "savePic",
                                    projectId: $("#projectId").val(),
                                    picPath: data.url
                                };

                                jQuery.post(
                                    "Action.aspx",
                                    param,
                                    function (data) {
                                        getPics();
                                    }
                                );

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

        function del() {
            var n = $("#ppcIds").val();
            if (n == null || n.toString() == "0") {
                jQuery.messager.alert('注意', '请选择要删除的图片！', 'warning');
            } else {
                jQuery.messager.confirm('删除', '确认删除图片么？', function (r) {
                    if (r) {
                        var param = { action: "delPics", ppcIds: ppcIds };
                        jQuery.post(
                            "Action.aspx",
                            param,
                            function (data) {
                                getPics();
                            },
                            'json'
                        );
                    }
                });
            }
        }

        function initItems() {
            $("#btnDel").linkbutton({
                iconCls: 'icon-cut',
                plain: true
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
    <div class="easyui-panel" title="&nbsp;项目图片管理" fit="true">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="100" class="algR">
                    图片上传：
                </td>
                <td width="">
                    <input type="file" class="txtInput w400" name="imgFile" id="File2" onchange="uploadPic()" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    &nbsp;
                </td>
                <td id="previews">
                    <div id="" class="previewDiv">
                        <img src="" height="200" width="200" alt="" /></div>
                    <div id="" class="previewDiv">
                        <img src="" height="200" width="200" alt="" /></div>
                    <div id="" class="previewDiv">
                        <img src="" height="200" width="200" alt="" /></div>
                    <div id="" class="previewDiv">
                        <img src="" height="200" width="200" alt="" /></div>
                    <div id="" class="previewDiv">
                        <img src="" height="200" width="200" alt="" /></div>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    &nbsp;
                </td>
                <td>
                    <a href="javascript:void(0)" id="btnDel" onclick="del()">删除</a>
                    <input id="projectId" type="hidden" value="<%=projectId %>" />
                    <input id="projectName" type="hidden" value="<%=projectName %>" />
                    <input id="ppcIds" type="hidden" value="0" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
