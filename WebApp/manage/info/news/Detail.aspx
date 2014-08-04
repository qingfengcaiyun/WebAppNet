<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApp.manage.info.news.Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>文章编辑</title>
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
    <script type="text/javascript">
        var editor;

        $(document).ready(function () {

            switchTopOption(0);
            getCateTree();
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
                        'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'cut', 'copy', 'paste',
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
            var b = $("#isTop").val(v);
            if (parseInt(v) == 1) {
                $("#topTimeDiv").show();
                $("#topYes").show();
                $("#topNo").hide();
            } else {
                $("#topTimeDiv").hide();
                $("#topYes").hide();
                $("#topNo").show();
            }
        }

        function preview() {
            jQuery.messager.alert('嘻嘻', '模板还未做完！');
        }

        function save() {
            //cateList
            var t = $("#cate").combotree('tree');
            var n = t.tree('getSelected');
            var cateId;

            var longTitle = $("#longTitle").val();

            var titleColor = $("#titleColor").val();
            if (titleColor == "标题颜色") {
                titleColor = "#000000";
            }

            var shortTitle = $("#shortTitle").val();
            var isTop = $("#isTop").val();
            var topTime = $("#topTime").datetimebox('getValue');
            var content = editor.html();
            var keywords = $("#keywords").val();
            var itemIndex = $("#itemIndex").val();
            var outLink = $("#outLink").val();

            var newsId = $("#newsId").val();

            if (longTitle == null) {
                jQuery.messager.alert('错误', '请输入长标题！', 'error');
                $("#longTitle").focus();
                return;
            }
            if (n == null) {
                jQuery.messager.alert('错误', '请选择文章分类！！', 'error');
                $("#cate").focus();
                return;
            } else {
                cateId = n.id;
            }
            if (editor.text() == null) {
                jQuery.messager.alert('错误', '请输入内容', 'error');
                return;
            }
            if (keywords == null) {
                jQuery.messager.alert('错误', '请输入关键词，以逗号分隔！', 'error');
                $("#keywords").focus();
                return;
            }
            if (itemIndex == null) {
                jQuery.messager.alert('错误', '请输入文章排序值。数值越大越往前！', 'error');
                $("#itemIndex").focus();
                return;
            }


            var picUrl = "";
            var txt = editor.html();
            $(txt).each(function () {
                if ($(this).attr("src") != undefined && isUploadFile($(this).attr("src"))) {
                    picUrl = picUrl + "," + $(this).attr("src");
                }
            });

            var param = {
                action: "save",
                paramStr: "newsId,longTitle,titleColor,shortTitle,isTop,topTime,content,keywords,itemIndex,cateId,outLink",
                newsId: newsId,
                longTitle: longTitle,
                titleColor: titleColor,
                shortTitle: shortTitle,
                isTop: isTop,
                topTime: topTime,
                content: content,
                keywords: keywords,
                picUrl: picUrl.length > 0 ? picUrl.substr(1) : "",
                itemIndex: itemIndex,
                cateId: cateId,
                outLink: outLink
            };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    if (parseInt(d.msg) == 1) {
                        jQuery.messager.confirm('保存成功', '你想输入新信息么？', function (r) {
                            if (r) {
                                window.location.href = "?action=edit&newsId=0";
                            } else {
                                window.location.href = "List.aspx?cateId=" + cateId;
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
            var newsId = $("#newsId").val();
            if (parseInt(newsId) > 0) {
                jQuery.post(
                    "Action.aspx",
                    {
                        action: "one",
                        newsId: newsId
                    },
                    function (data) {
                        var d = eval(data);
                        $("#longTitle").val(d.longTitle);
                        $("#shortTitle").val(d.shortTitle);
                        $("#keywords").val(d.keywords);
                        $("#itemIndex").val(d.itemIndex);
                        $("#outLink").val(d.outLink);
                        $("#isTop").val(d.isTop);

                        editor.html(d.content);

                        if (d.isTop.toString() == "1") {
                            $("#topTime").datetimebox("setValue", d.topTime.toString());
                        }

                        switchTopOption(d.isTop);

                        $('#cate').combotree('setValue', d.cateId);
                    },
                    'json'
                );
            }
        }

        function getCateTree() {
            $("#cate").combotree({
                required: true,
                panelWidth: 150,
                panelHeight: auto
            });

            jQuery.post(
                "../category/Action.aspx",
                {
                    action: "tree"
                },
                function (data) {
                    var d = jQuery.parseJSON(data);
                    $("#cate").combotree('loadData', d);
                }
            );
        }

        function isUploadFile(filePath) {
            var b = false;
            var extName = filePath.substr(filePath.lastIndexOf(".") + 1);
            //alert(extName);
            var extFiles = "gif,jpg,jpeg,png,bmp,swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb,doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2".split(',');
            for (var i = 0, j = extFiles.length; i < j; i++) {
                //alert(extFiles[i]);
                if (extFiles[i].toString() == extName.toString()) {
                    b = true;
                }
            }

            return b;
        }
    </script>
</head>
<body>
    <div class="easyui-panel" title="&nbsp;文章编辑" fit="true">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="100" class="algR">
                    完整标题：
                </td>
                <td width="">
                    <input class="easyui-validatebox textbox txtInput w400" type="text" id="longTitle"
                        required="true" />
                    &nbsp;
                    <input class="txtInput w80 algC" type="text" id="titleColor" value="标题颜色" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    简化标题：
                </td>
                <td>
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="shortTitle" />
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
                    文章分类：
                </td>
                <td>
                    <select class="easyui-combotree txtInput" style="width: 150px" id="cate">
                    </select>
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
                    置顶：
                </td>
                <td>
                    <span id="topNo" class="top"><a class="easyui-linkbutton" iconcls="icon-no" href="javascript:void(0)"
                        onclick="switchTopOption(1)">&nbsp;不置顶</a></span><span id="topYes" class="top"><a
                            class="easyui-linkbutton" iconcls="icon-ok" href="javascript:void(0)" onclick="switchTopOption(0)">&nbsp;置顶</a></span>
                    <span id="topTimeDiv" class="top">该文章置顶截止：
                        <input class="easyui-datetimebox" value="10/11/2012 2:3:56" style="width: 200px;"
                            id="topTime" />
                    </span>
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
                    外部连接：
                </td>
                <td>
                    <input class="txtInput w400" type="text" id="outLink" value="" />
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
                    <input type="hidden" id="isTop" value="" />
                    <input type="hidden" id="newsId" value="<%=newsId %>" />
                    <input type="hidden" id="cateList" value="" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
