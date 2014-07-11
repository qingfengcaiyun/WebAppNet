/*$(document).ready(function() {
						   navItemAct(); 
						   inputBg("#EED69A", "#ffffff");
						   $("#Container").setCenter();
						   $("#BottonDiv").setFullSize();
						   });
*/
(function($) {
    $.fn.flashSlider = function(options) {
        var options = $.extend({}, $.fn.flashSlider.defaults, options);
        this.each(function() {
            var obj = $(this);
            var curr = 1; //当前索引
            var $img = obj.find("div.ImgTab");
            var s = $img.length;
            var w = $img.eq(0).width();
            var h = $img.eq(0).height();
            var $flashelement = $("ul", obj);
            options.height == 0 ? obj.height(h) : obj.height(options.height);
            options.width == 0 ? obj.width(w) : obj.width(options.width);
            obj.css("overflow", "hidden");
            obj.css("position", "relative");
            $flashelement.css('width', s * w);
            if (!options.vertical) {
                $("li", obj).css('float', 'left')
            }
            else {
                $img.css('display', 'block')
            };
            if (options.controlsShow) {
                var navbtnhtml = '<div id="flashnvanum">';
                for (var i = 0; i < s; i++) {
                    navbtnhtml += '<span>' + (i + 1) + '</span>';
                }
                navbtnhtml += '</div>';
                obj.append(navbtnhtml);
                obj.find("#flashnvanum span").hover(function() {
                    var num = $(this).html();
                    flash(num, true);
                }, function() {
                    timeout = setTimeout(function() {
                        flash((curr / 1 + 1), false);
                    }, options.pause / 4);
                });
            };
            function setcurrnum(index) {
                obj.find("#flashnvanum span").eq(index).addClass('on').siblings().removeClass('on');
            }
            function flash(index, clicked) {
                $flashelement.stop();
                var next = index == s ? 1 : index + 1;
                curr = index - 1;
                setcurrnum((index - 1));
                if (!options.vertical) {
                    p = ((index - 1) * w * -1);
                    $flashelement.animate(
						{ marginLeft: p },
						options.speed, options.easing
					);
                } else {
                    p = ((index - 1) * h * -1);
                    $flashelement.animate(
						{ marginTop: p },
						options.speed, options.easing
					);
                };
                if (clicked) {
                    clearTimeout(timeout);
                };
                if (options.auto && !clicked) {
                    timeout = setTimeout(function() {
                        flash(next, false);
                    }, options.speed + options.pause);
                };
            }
            var timeout;
            //初始化
            setcurrnum(0);
            if (options.auto) {
                timeout = setTimeout(function() {
                    flash(2, false);
                }, options.pause);
            };
        });
    };
    //默认值
    $.fn.flashSlider.defaults = {
        controlsShow: true, //是否显示数字导航
        vertical: false, //纵向还是横向滑动
        speed: 800, //滑动速度
        auto: true, //是否自定滑动
        pause: 3000, //两次滑动暂停时间
        easing: "swing", //easing效果，默认swing，更多效果需要easing插件支持
        height: 0, //容器高度，不设置自动获取图片高度
        width: 0//容器宽度，不设置自动获取图片宽度
    }
})(jQuery);

function navItemAct() {
    var navItems = $(".NavItem");
    if (navItems.length > 0) {
        navItems.each(function(i, navItem) {
            $(navItem).hover(
												function() {
												    $(navItem).css({ "background-image": "url(imgs/bg_navOn.jpg)", "background-repeat": "repeat-x", "color": "#4a2a19" });
												},
												function() {
												    $(navItem).css({ "background-image": "", "background-repeat": "", "color": "#fff" });
												}
												);
        });
    }
}

//全角转换为半角
function DBC2SBC(str, flag) {
    var result = '';
    str = str.replace(/。/g, "．");
    for (var i = 0; i < str.length; i++) {
        code = str.charCodeAt(i);
        if (flag) {
            if (code >= 65281 && code <= 65373) result += String.fromCharCode(str.charCodeAt(i) - 65248);
            else if (code == 12288) result += String.fromCharCode(str.charCodeAt(i) - 12288 + 32);
            else result += str.charAt(i);
        }
        else {
            if (code >= 33 && code <= 126) result += String.fromCharCode(str.charCodeAt(i) + 65248);
            else if (code == 32) result += String.fromCharCode(str.charCodeAt(i) - 32 + 12288);
            else result += str.charAt(i);
        }
    }
    return result;
}

//漂浮DIV
/*
FloatDiv = function(){
this.floatDo = function(){
$("body").prepend("<div id='floatDiv'></div><div></div>");		
var floatDiv = $("#floatDiv");
floatDiv.css({"line-height":"30px","background-color":"#666","padding":"10px","border":"1px solid #000","z-index":"1000","font-size":"14px","font-weight":"bolder","color":"#FF0","text-align":"center","width":"80px","height":"80px","position":"relative","top":"150px","left":"50px","word-wrap":"break-word"});
floatDiv.html("<a href='/zsbm'><img src='../imgs/1242230245389.jpg' border='0' title='' /></a>");
}
}
*/

/*
* 使用说明：
*   speed //元素移动速度
*   xPos  //元素一开始左距离
*   yPos  //元素一开始上距离
*   $('#div1').imgFloat({speed:30,xPos:10,yPos:10});  
*   $('#div2').imgFloat();   //不给参数默认（speed:10,xPos:0,yPos:0）                      
*/
(function($) {
    jQuery.fn.imgFloat = function(options) {
        var own = this;
        var xD = 0;
        var yD = 0;
        var i = 1;
        var settings = {
            speed: 10,
            xPos: 0,
            yPos: 0
        };        
        jQuery.extend(settings, options);
        var ownTop = settings.xPos;
        var ownLeft = settings.yPos;
        own.css({ position: "absolute", cursor: "pointer" });

        function imgPosition() {
            var winWidth = $(window).width() - own.width();
            var winHeight = $(window).height() - own.height();
            if (xD == 0) {
                ownLeft += i;
                own.css({
                    left: ownLeft
                });
                if (ownLeft >= winWidth) {
                    ownLeft = winWidth;
                    xD = 1;
                }
            }
            if (xD == 1) {
                ownLeft -= i;
                own.css({
                    left: ownLeft
                });
                if (ownLeft <= 0) xD = 0;
            }
            if (yD == 0) {
                ownTop += i;
                own.css({
                    top: ownTop
                });
                if (ownTop >= winHeight) {
                    ownTop = winHeight;
                    yD = 1;
                }
            }
            if (yD == 1) {
                ownTop -= i;
                own.css({
                    top: ownTop
                });
                if (ownTop <= 0) yD = 0;
            }
        }
        var imgHover = setInterval(imgPosition, settings.speed);
        own.hover(
			function() {
			    clearInterval(imgHover);
			},
        	function() {
        	    imgHover = setInterval(imgPosition, settings.speed);
        	}
		);
    }
})(jQuery);

//测试控件或HTML标签的类型
function checkHTMLType(id) {
    var htmlTag = $(id);
    var tags = new Array("textarea", "select", "input:text", "input:password", "input:hidden", "input:checkbox", "input:radio", "input:button", "input:reset", "div", "span", "img", "table");
    var i = 0;
    var rs = "";
    for (i = 0; i < tags.length; i++) {
        if (htmlTag.is(tags[i])) {
            rs = tags[i];
            break;
        }
    }
    return rs;
}

//正则表达式
RegExpDo = function() {
    this.patrn = "";

    //英文字母，大小写
    this.isEnglish = function(str) {
        patrn = /^[A-Za-z]+$/;
        return patrn.test(str);
    }

    //英文字母，大写
    this.isUpperEnglish = function(str) {
        patrn = /^[A-Z]+$/;
        return patrn.test(str);
    }

    //英文字母，小写
    this.isLowerEnglish = function(str) {
        patrn = /^[a-z]+$/;
        return patrn.test(str);
    }

    //校验用户名：只能输入6-20个字母、数字、下划线	
    this.isUserName = function(str) {
        patrn = /^[A-Za-z0-9_]{6,20}$/;
        return patrn.test(str);
    }

    //身份证
    this.isIDCard = function(str) {
        patrn = /(^\d{15}$)|(^\d{17}([0-9]|X)$)/;
        return patrn.test(str.toUpperCase());
    }

    //IP
    this.isIP = function(str) {
        patrn = /(\d+)\.(\d+)\.(\d+)\.(\d+)/;
        if (patrn.test(str)) {
            var ss = str.split(".");
            var vs;
            for (var i = 0; i < ss.length; i++) {
                vs = parseInt(ss[i].toString());
                if (vs > 255 || vs < 0) {
                    return false;
                }
            }
            return true;
        }
        else {
            return false;
        }
    }

    //Email地址
    this.isEmail = function(str) {
        patrn = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/;
        return patrn.test(str);
    }

    //电话号码
    this.isPhoneNum = function(str) {
        patrn = /^[+86-]?((\d){3,4}[-])?(\d){5,9}$/;
        return patrn.test(str);
    }

    //校验手机号码：必须以数字开头，除数字外，可含有“-”	
    this.isCellphoneNum = function(str) {
        patrn = /^[+]?(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/;
        return patrn.test(str);
    }

    //校验URL
    this.isUrl = function(str) {
        //var patrn = /^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$/;
        return patrn.test(str);
    }

    //校验邮政编码 
    this.isZipcode = function(str) {
        patrn = /^[0-9]+$/;
        return patrn.test(str);
    }

    //纯数字的字符串
    this.isNumeric = function(str) {
        patrn = /^[0-9]+$/;
        return patrn.test(str);
    }

    //校验密码：只能输入6-20个字母、数字、下划线	
    this.isPassword = function(str) {
        patrn = /^[A-Za-z0-9_]{6,20}$/;
        return patrn.test(str);
    }
}

//插入div显示信息，在左上角
MsgBox = function() {
    this.left = 10;
    this.boxIndex = 0;
    this.serialIndex = 0;
    this.winWidth = $(window).width();

    this.showMsg = function(msg) {
        var winWidth = $(window).width();
        var divWidth = winWidth / 9;
        $("body").prepend("<div id='msgShow" + this.serialIndex + "'></div><div></div>");
        var msgShow = $("#msgShow" + this.serialIndex);
        msgShow.addClass("msgShow");
        //msgShow.css({"line-height":"30px","background-color":"#666","padding":"30px","border":"1px solid #000","z-index":"1000","font-size":"14px","font-weight":"bolder","color":"#FF0","text-align":"center","width":divWidth,"position":"fixed","display":"none","top":"0px","word-wrap":"break-word"});
        if (this.boxIndex > 0) {
            this.left = 70 + this.left + divWidth;
        }
        if (this.left + divWidth > $(window).width()) {
            this.left = 20;
            this.boxIndex = 0;
            this.width = 0;
        }
        msgShow.css("left", this.left);
        msgShow.css("z-index", 1000 + this.serialIndex);
        this.boxIndex += 1;
        this.serialIndex += 1;
        msgShow.html(msg).show("blind", {}, 600, this.callback(msgShow));
    }

    this.callback = function(m) {
        setTimeout(function() {
            //m.removeClass("msgShow").hide().fadeOut();
            m.hide("blind", {}, 500);
        }, 5000);
    }

    this.alertMsg = function(msg) {
        $("body").prepend("<div id='alertMsg' title='消息提示！！！'></div><div></div>");
        var alertMsg = $("#alertMsg");
        alertMsg.html(msg).dialog({
            modal: true,
            buttons: {
                "我知道了！": function() {
                    $(this).dialog('close').remove();
                }
            }
        }); ;
    }
}

//文本框，文本域，密码框背景颜色的变化。调用：inputBg("#cccccc","#ffffff")
function inputBg(blurColor, focusColor) {
    var i;
    var inputTypes = new Array()

    inputTypes[0] = ":text";
    inputTypes[1] = ":password";
    inputTypes[2] = ":file";
    inputTypes[3] = "textarea";

    var objs;

    for (i in inputTypes) {
        objs = $(inputTypes[i]);
        if (objs.length > 0) {
            objs.each(function(i, obj) {
                $(obj).css("background-color", blurColor);
                $(obj).focus(function() {
                    $(obj).css("background-color", focusColor);
                });
                $(obj).blur(function() {
                    $(obj).css("background-color", blurColor);
                });
            });
        }
    }
}

//清空表单。调用：clearForm()
function clearForm() {
    //文本框
    var txtInput = $(":text");
    if (txtInput.length > 0) {
        txtInput.each(function(i, obj) {
            $(obj).val("");
        });
    }
    //密码框
    var pwdInput = $(":password");
    if (pwdInput.length > 0) {
        pwdInput.each(function(i, obj) {
            $(obj).val("");
        });
    }
    //多选框
    var checkInput = $(":checkbox");
    if (checkInput.length > 0) {
        checkInput.each(function(i, obj) {
            $(obj).attr("checked", false);
        });
    }
    //单选框
    var radioInput = $(":radio");
    if (radioInput.length > 0) {
        radioInput.each(function(i, obj) {
            $(obj).attr("checked", false);
        });
    }
    //下拉列表框
    var selectItem = $("select");
    if (selectItem.length > 0) {
        selectItem.each(function(i, obj) {
            obj.selectedIndex = 0;
        });
    }
    //文本域
    var txtAreas = $("textarea");
    if (txtAreas.length > 0) {
        txtAreas.each(function(i, obj) {
            $(obj).html("");
        });
    }
}

//将页面居中
/*
* 使用说明：
*   w //水平方向
*   h //垂直方向
*   $('#div1').setCenter({w:true,h:fasle});  
*   $('#div2').setCenter();   //不给参数默认水平方向居中                     
*/
(function($) {
    jQuery.fn.setCenter = function(options) {
        var own = this;
        var param = {
            w:true,
            h:false
        };

        jQuery.extend(param,options);
        function setPosition() {
            if (param.h) {
                var lh = own.height();
                var wh = $(window).height();

                if (wh > lh) {
                    var hh = (wh - lh) / 3;
                    own.css("margin-top", hh);
                }
                else {
                    own.css("margin-top", 0);
                }
            }

            if (param.w) {
                var lw = own.width();
                var ww = $(window).width();

                if (ww > lw) {
                    var hw = (ww - lw) / 2;
                    own.css("margin-left", hw);
                }
                else {
                    own.css("margin-left", 0);
                }
            }
        }

        $(document).ready(function() { setPosition(); });
        $(window).resize(function() { setPosition(); });
    }
})(jQuery);

/*
* 使用说明：
*   w //水平方向
*   h //垂直方向
*   $('#div1').setFullSize({w:"1000",h:"1000"});  
*   $('#div2').setFullSize();   //不给参数默认水平方向居中                     
*/
(function($) {
    jQuery.fn.setFullSize = function(options) {
        var own = this;
        var param = {
            w:1000,
            h:0
        };

        jQuery.extend(param,options);
        function setSize() {
            if (param.h!=0) {
                var lh = own.height();
                var wh = $(window).height();

                if (wh > lh) {
                    own.css("height", wh);
                }
                else {
                    own.css("height", param.h);
                }
            }

            if (param.w!=0) {
                var lw = own.width();
                var ww = $(window).width();

                if (ww < param.w) {
                    own.css("width", param.w);
                }
                else {
                    own.css("width", ww);
                }
            }
        }

        $(document).ready(function() { setSize(); });
        $(window).resize(function() { setSize(); });
    }
})(jQuery);

//获取 URL 中传递的参数 
$.urlParam = function(name){
    var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (!results) { return 0; }
    return results[1] || 0;
} 


//Hashtable类
/*调用的方法
var hashtable = new jQuery.Hashtable();
$(function(){
$('#btnAdd').click(function(){
hashtable.add($('#txtAddKey').val(),$('#txtAddValue').val());
});
$('#btnGet').click(function(){
alert(hashtable.get($('#txtGetKey').val()))
});
})*/
jQuery.Hashtable = function() {
    this.items = newArray();
    this.itemsCount = 0;

    //添加add(key,value) void
    this.add = function(key, value) {
        if (!this.containsKey(key)) {
            this.items[key] = value;
            this.itemsCount++;
        }
        else
            throw "key '" + key + "' already exists.";
    }
    //读取get(key) string
    this.get = function(key) {
        if (this.containsKey(key))
            return this.items[key];
        else
            return null;
    }
    // 删除remove(key) void
    this.remove = function(key) {
        if (this.containsKey(key)) {
            delete this.items[key];
            this.itemsCount--;
        }
        else
            throw "key'" + key + "'doesnotexists.";
    }
    //存在键containsKey(key) bool
    this.containsKey = function(key) {
        return typeof (this.items[key]) != "undefined";
    }
    //存在值containsValue(value) bool
    this.containsValue = function(value) {
        for (var i in this.items) {
            if (this.items[i] == value)
                return true;
        }
        return false;
    }
    //存在键值contains(keyOrValue) bool
    this.contains = function(keyOrValue) {
        return this.containsKey(keyOrValue) || this.containsValue(keyOrValue);
    }
    //清空clear() void
    this.clear = function() {
        this.items = newArray();
        itemsCount = 0;
    }
    //键值对总数size() int
    this.size = function() {
        return this.itemsCount;
    }
    //是否为空isEmpty() bool
    this.isEmpty = function() {
        return this.size() == 0;
    }
};

//滚动图片
(function($) {
    $.fn.marquee = function(o) {
        //获取滚动内容内各元素相关信息
        o = $.extend({
            speed: parseInt($(this).attr('speed')) || 30, // 滚动速度
            step: parseInt($(this).attr('step')) || 1, // 滚动步长
            direction: $(this).attr('direction') || 'up', // 滚动方向
            pause: parseInt($(this).attr('pause')) || 1000 // 停顿时长
        }, o || {});
        var dIndex = jQuery.inArray(o.direction, ['right', 'down']);
        if (dIndex > -1) {
            o.direction = ['left', 'up'][dIndex];
            o.step = -o.step;
        }
        var mid;
        var div = $(this); // 容器对象
        var divWidth = div.innerWidth(); // 容器宽
        var divHeight = div.innerHeight(); // 容器高
        var ul = $("ul", div);
        var li = $("li", ul);
        var liSize = li.size(); // 初始元素个数
        var liWidth = li.width(); // 元素宽
        var liHeight = li.height(); // 元素高
        var width = liWidth * liSize;
        var height = liHeight * liSize;
        if ((o.direction == 'left' && width > divWidth) ||
			(o.direction == 'up' && height > divHeight)) {
            // 元素超出可显示范围才滚动
            if (o.direction == 'left') {
                ul.width(2 * liSize * liWidth);
                if (o.step < 0) div.scrollLeft(width);
            } else {
                ul.height(2 * liSize * liHeight);
                if (o.step < 0) div.scrollTop(height);
            }
            ul.append(li.clone()); // 复制元素
            mid = setInterval(_marquee, o.speed);
            div.hover(
				function() { clearInterval(mid); },
				function() { mid = setInterval(_marquee, o.speed); }
			);
        }
        function _marquee() {
            // 滚动
            if (o.direction == 'left') {
                var l = div.scrollLeft();
                if (o.step < 0) {
                    div.scrollLeft((l <= 0 ? width : l) + o.step);
                } else {
                    div.scrollLeft((l >= width ? 0 : l) + o.step);
                }
                if (l % liWidth == 0) _pause();
            } else {
                var t = div.scrollTop();
                if (o.step < 0) {
                    div.scrollTop((t <= 0 ? height : t) + o.step);
                } else {
                    div.scrollTop((t >= height ? 0 : t) + o.step);
                }
                if (t % liHeight == 0) _pause();
            }
        }
        function _pause() {
            // 停顿
            if (o.pause > 0) {
                var tempStep = o.step;
                o.step = 0;
                setTimeout(function() {
                    o.step = tempStep;
                }, o.pause);
            }
        }
    };
})(jQuery);
$(document).ready(function() {
    $(".marquee").each(function() {
        $(this).marquee();
    });
});