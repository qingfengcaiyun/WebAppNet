<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApp.manage.renovation.building.Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>楼盘信息</title>
    <link href="../../../libs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../libs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" src="../../../libs/ajaxfileupload.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#panel").panel({
                width: $(window).width(),
                height: $(window).height(),
                title: "&nbsp;楼盘信息"
            });

            getRegionList();
            getOne();
        });

        function save() {
            var t = $("#location").combotree('tree');
            var n = t.tree('getSelected');
            var locationId;

            var buildingsId = $("#buildingsId").val();
            var buildingsName = $("#buildingsName").val();
            var address = $("#address").val();
            var picUrl = $("#picUrl").val();
            var itemIndex = $("#itemIndex").val();

            if (buildingsName == null) {
                jQuery.messager.alert('错误', '请输入楼盘名称！', 'error');
                $("#buildingsName").focus();
                return;
            }

            if (n == null) {
                jQuery.messager.alert('错误', '请选择楼盘所属区县！！', 'error');
                $("#location").focus();
                return;
            } else {
                locationId = n.id;
            }

            if (picUrl == null) {
                jQuery.messager.alert('错误', '请上传楼盘图片！！', 'error');
                $("#fileToUpload").focus();
                return;
            }

            var param = {
                action: "save",
                paramStr: "buildingsId,buildingsName,locationId,address,picUrl,itemIndex",
                buildingsId: buildingsId,
                buildingsName: buildingsName,
                locationId: locationId,
                address: address,
                picUrl: picUrl,
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
                                window.location.href = "?buildingsId=0";
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
            var buildingsId = $("#buildingsId").val();
            if (parseInt(buildingsId) > 0) {
                jQuery.post(
                    "Action.aspx",
                    {
                        action: "one",
                        buildingsId: buildingsId
                    },
                    function (data) {
                        var d = eval(data);
                        $("#buildingsName").val(d.buildingsName);
                        $("#address").val(d.address);
                        $("#picUrl").val(d.picUrl);
                        $("#itemIndex").val(d.itemIndex);
                        $('#location').combotree('setValue', d.locationId);

                        if ($("#picUrl").val() != null) {
                            $("#preview").html('<img src="' + d.picUrl + '" />');
                        }
                    },
                    'json'
                );
            } else {
                $('#location').combotree('setValue', $("#locationId").val());
            }
        }

        function getRegionList() {
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
                                $("#picUrl").val(data.url);
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
    <div id="panel">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="100" class="algR">
                    区县：
                </td>
                <td width="">
                    <select class="easyui-combotree txtInput" style="width: 200px;" id="location">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    楼盘名称：
                </td>
                <td>
                    <input type="text" id="buildingsName" class="easyui-validatebox textbox txtInput w200"
                        value="" required="true" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    序号：
                </td>
                <td>
                    <input type="text" id="itemIndex" class="easyui-validatebox textbox txtInput w100"
                        value="1000" required="true" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    详细地址：
                </td>
                <td>
                    <input type="text" id="address" class="easyui-validatebox textbox txtInput w400"
                        value="" required="true" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    楼盘图片：
                </td>
                <td>
                    <input type="file" class="txtInput w400" name="imgFile" id="imgFile" onchange="uploadPic()" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    <input type="hidden" id="picUrl" value="" />
                </td>
                <td id="preview">
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
                    <input type="hidden" id="buildingsId" value="<%=buildingsId %>" />
                    <input type="hidden" id="locationId" value="<%=locationId %>" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
