<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApp.manage.renovation.project.Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目管理</title>
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
            initItems();
            initLocationTree();

            $("#blocation").combotree({
                onChange: function (newValue, oldValue) {
                    getBuildingsTree();
                }
            });

            $("#location").combotree({
                onChange: function (newValue, oldValue) {
                    getMemberTree();
                }
            });

            $("#member").combotree({
                onChange: function (newValue, oldValue) {
                    getDesignerTree();
                }
            });

            getOne();
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
                                $("#picSnap").val(data.url);
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
            var projectId = $("#projectId").val();

            var b = $("#buildings").combotree('tree').tree('getSelected');
            var l = $("#blocation").combotree('tree').tree('getSelected');

            var m = $("#member").combotree('tree').tree('getSelected');
            var d = $("#designer").combotree('tree').tree('getSelected');

            var memberId;
            var locationId;
            var buildingsId;
            var designerId;

            var projectName = $("#projectName").val();
            var memo = $("#memo").val();
            var picSnap = $("#picSnap").val();
            var startTime = $("#startTime").datetimebox("getValue");
            var itemIndex = $("#itemIndex").val();

            if (projectName == null) {
                jQuery.messager.alert('错误', '请输入项目名称！', 'error');
                $("#projectName").focus();
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
            if (d == null) {
                jQuery.messager.alert('错误', '请选择所属设计师！！', 'error');
                $("#designer").focus();
                return;
            } else {
                designerId = d.id;
            }
            if (b == null) {
                jQuery.messager.alert('错误', '请选择所属楼盘！！', 'error');
                $("#buildings").focus();
                return;
            } else {
                buildingsId = b.id;
            }
            if (memo == null) {
                jQuery.messager.alert('错误', '请输入设计理念或项目简介', 'error');
                $("#memo").focus();
                return;
            }
            if (itemIndex == null) {
                jQuery.messager.alert('错误', '请输入排序值。数值越大越往前！', 'error');
                $("#itemIndex").focus();
                return;
            }

            var param = {
                action: "save",
                paramStr: "projectId,memberId,designerId,locationId,buildingsId,projectName,memo,picSnap,startTime,itemIndex",
                projectId: projectId,
                memberId: memberId,
                designerId: designerId,
                locationId: locationId,
                buildingsId: buildingsId,
                projectName: projectName,
                memo: memo,
                picSnap: picSnap,
                startTime: startTime,
                itemIndex: itemIndex
            };

            jQuery.post(
                "Action.aspx",
                param,
                function (data) {
                    var d = eval(data);
                    if (parseInt(d.msg) > 0) {
                        jQuery.messager.confirm('保存成功', '继续设置案例参数么？', function (r) {
                            if (r) {
                                window.location.href = "Params.aspx?projectId=" + (projectId > 0 ? projectId : d.msg);
                            } else {
                                window.location.href = "List.aspx?locationId=" + locationId + "&memberId=" + memberId + "&designerId=" + designerId;
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
            var projectId = $("#projectId").val();
            if (parseInt(projectId) > 0) {
                jQuery.post(
                    "Action.aspx",
                    {
                        action: "one",
                        projectId: projectId
                    },
                    function (data) {
                        var d = eval(data);

                        $("#projectName").val(d.projectName);
                        $("#memo").val(d.memo);
                        $("#picSnap").val(d.picSnap);
                        $("#itemIndex").val(d.itemIndex);
                        $("#startTime").datetimebox("setValue", d.startTime.toString());

                        $('#blocation').combotree('setValue', d.locationId);
                        getBuildingsTree();
                        $('#buildings').combotree('setValue', d.buildingsId);

                        if (d.picSnap != null && isUploadFile(d.picSnap)) {
                            $("#picSnap").val(d.picSnap);
                            $("#photoPic").html("<img src='" + d.picSnap + "' />");
                        } else {
                            $("#picSnap").val("");
                            $("#photoPic").html("");
                        }

                        param = { action: "one", memberId: d.memberId };

                        jQuery.post(
                            "../../user/member/Action.aspx",
                            param,
                            function (data) {
                                var m = eval(data);

                                $('#location').combotree('setValue', m.locationId);
                                getMemberTree();
                                $('#member').combotree('setValue', d.memberId);
                                getDesignerTree();
                                $('#designer').combotree('setValue', d.designerId);
                            },
                            'json'
                        );
                    },
                    'json'
                );
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

                    $("#blocation").combotree('loadData', d);
                    $("#blocation").combotree('setValue', $("#blocationId").val());
                    $("#blocation").combotree('tree').tree('expandAll');

                    getBuildingsTree();
                    getMemberTree();
                },
                'json'
            );
        }

        function getMemberTree() {
            var n = $("#location").combotree("tree").tree("getSelected");
            if (n != null) {
                var param = { action: "tree", locationId: n.id };
                jQuery.post(
                    "../../user/member/Action.aspx",
                    param,
                    function (data) {
                        var d = eval(data);
                        $("#member").combotree('loadData', d);
                        $("#member").combotree('tree').tree('expandAll');

                        if (d.length > 0) {
                            $("#member").combotree("setValue", d[0].id);

                            getDesignerTree();
                        }
                    },
                    'json'
                );
            }
        }

        function getDesignerTree() {
            var n = $("#member").combotree("tree").tree("getSelected");
            if (n != null) {
                var param = { action: "tree", memberId: n.id };
                jQuery.post(
                    "../../user/designer/Action.aspx",
                    param,
                    function (data) {
                        var d = eval(data);
                        $("#designer").combotree('loadData', d);
                        $("#designer").combotree('tree').tree('expandAll');

                        if (d.length > 0) {
                            $("#designer").combotree("setValue", d[0].id);
                        }
                    },
                    'json'
                );
            }
        }

        function getBuildingsTree() {
            var n = $("#blocation").combotree("tree").tree("getSelected");
            if (n != null) {
                var param = { action: "tree", locationId: n.id };
                jQuery.post(
                    "../building/Action.aspx",
                    param,
                    function (data) {
                        var d = eval(data);
                        $("#buildings").combotree('loadData', d);
                        $("#buildings").combotree('tree').tree('expandAll');

                        if (d.length > 0) {
                            $("#buildings").combotree("setValue", d[0].id);
                        }
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

            $("#designer").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            $("#blocation").combotree({
                required: true,
                panelWidth: 200,
                panelHeight: 200
            });

            $("#buildings").combotree({
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
    <div class="easyui-panel" title="&nbsp;项目管理" fit="true">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="200" class="algR">
                    项目全名：
                </td>
                <td width="">
                    <input class="easyui-validatebox textbox txtInput w300" type="text" id="projectName"
                        required="true" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    所属地区和楼盘：
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <select class="txtInput" id="blocation">
                                </select>
                            </td>
                            <td>
                                <select class="txtInput" id="buildings">
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    所属公司和设计师：
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <select class="txtInput" id="location">
                                </select>
                            </td>
                            <td>
                                <select class="txtInput" id="member">
                                </select>
                            </td>
                            <td>
                                <select class="txtInput" id="designer">
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="algR">
                    起始时间：
                </td>
                <td>
                    <input class="easyui-datetimebox" value="10/11/2012 2:3:56" style="width: 200px;"
                        id="startTime" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    预览图片：
                </td>
                <td>
                    <input type="file" class="txtInput w400" name="imgFile" id="imgFile" onchange="uploadPic()" />
                </td>
            </tr>
            <tr>
                <td class="algR">
                    <input type="hidden" id="picSnap" value="" />
                </td>
                <td id="photoPic">
                </td>
            </tr>
            <tr>
                <td class="algC">
                    设计理念<br />
                    或<br />
                    项目简介
                </td>
                <td>
                    <textarea id="memo" name="memo" cols="" rows="" class="txtArea"></textarea>
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
                    <input type="hidden" id="projectId" value="<%=projectId %>" />
                    <input type="hidden" id="locationId" value="<%=locationId %>" />
                    <input type="hidden" id="blocationId" value="<%=blocationId %>" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
