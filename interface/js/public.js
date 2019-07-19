var tjpublic = (function() {
    var public = {};
    public.bigimg = function(obj) {
        if ($("#" + obj.tckid).length > 0) {
            $("#" + obj.tckid).show();
            $("#" + obj.tckid + " img").attr({ "src": obj.src });
        } else {
            var tmark = $("<div></div>").addClass("tck_markbox").attr("id", obj.tckid),
                twrap = $("<div></div>").addClass("TJ_bigimgWarp"),
                timg = $("<img></img>").css({ "width": "100%" }).attr({ "src": obj.src });
            twrap.append(timg);
            tmark.append(twrap);
            $("body").append(tmark);
            tmark.click(function() {
                $("#" + obj.tckid).hide();
            })
        }
    };
    public.showinfobox = function(obj) {
        var content = obj.content,
            btntext = obj.btntext,
            markid = obj.tckid;
        if ($("#" + markid).length > 0) {
            $("#" + markid + " .tck_contentwrap>p").text(content);
            $("#" + markid + " .tck_chooseyes").text(btntext);
            $("#" + markid).show();
        } else {
            var tmark = $("<div></div>").addClass("tck_markbox").attr("id", markid),
                tcbox = $("<div></div>").addClass("tck_contbox"),
                tcul = $("<ul></ul>"),
                content = $("<li></li>").addClass("tck_contentwrap").html("<p>" + content + "</p>"),
                choose = $("<li></li>").addClass("tck_choosewrap tck_choosewrapone"),
                aok = $("<a></a>").addClass("tck_chooseyes").text(btntext);
            choose.append(aok);
            tcul.append(content, choose);
            tcbox.append(tcul);
            tmark.append(tcbox);
            $("body").append(tmark);
            if (obj.animate) {
                $("#" + markid + " .tck_contbox").addClass(obj.animate);
            }
        }
        var bta = $("#" + markid + " .tck_chooseyes");
        if (obj.url) {
            bta.click(function() {
                window.location.href = obj.url;
            })
        } else {
            if (obj.closeW) {
                bta.click(function() {
                    $("#" + markid).hide();
                    WeixinJSBridge.call('closeWindow');
                });
            }
            else {
                bta.click(function() {
                    $("#" + markid).hide();
                    clearTimeout(shutdowm);
                });
            }
        }
        if (obj.time) {
            clearTimeout(shutdowm);
            var shutdowm = setTimeout(function() {
                $("#" + markid).hide();
            }, obj.time)
        }
    };
    public.show_confimbox = function(obj) {
        var content = obj.content,
            markid = obj.tckid;
        if ($("#" + markid).length > 0) {
            $("#" + markid + " .tck_contentwrap>p").text(content);
            $("#" + markid + " .tck_choosewraptwo>a").off();
            if (obj.inputPL) {
                $("#" + markid + " input").attr("placeholder", obj.inputPL);
            }
            $("#" + markid).show();
        } else {
            var tmark = $("<div></div>").addClass("tck_markbox").attr("id", markid),
                tcbox = $("<div></div>").addClass("tck_contbox"),
                tcul = $("<ul></ul>"),
                content = $("<li></li>").addClass("tck_contentwrap").html("<p>" + content + "</p>"),
                choose = $("<li></li>").addClass("tck_choosewrap tck_choosewraptwo"),
                aok = $("<a>确定</a>").addClass("tck_chooseyes"),
                ano = $("<a>取消</a>").addClass("tck_chooseno");
            if (obj.inputPL) {
                content.append($("<input type=\"text\" placeholder='" + obj.inputPL + "' />"));
            }
            choose.append(ano);
            choose.append(aok);
            tcul.append(content, choose);
            tcbox.append(tcul);
            tmark.append(tcbox);
            $("body").append(tmark);
            if (obj.animate) {
                $("#" + markid + "  .tck_contbox").addClass(obj.animate);
            }
        }
        var btok = $("#" + markid + "  .tck_chooseyes"),
            btno = $("#" + markid + "  .tck_chooseno");
        btno.click(function() {
            if (obj.erfunc) { obj.erfunc() }
            else { $("#" + markid).hide(); }
        });
        if (obj.func) {
            btok.click(function() {
                obj.func(this);
            });
        } else {
            btok.click(function() {
                $("#" + markid).hide();
            });
        }
    };
    public.loading = function(obj) {
        if ($("#" + obj.tckid).length > 0) {
            $("#" + obj.tckid).show();
            if (obj.info)
                $(".tjloadingInfo").text(obj.info);
        } else {
            var tmark, twrap;
            if (obj.noBg) {
                tmark = $("<div></div>").addClass("tck_markbox_noBg").attr("id", obj.tckid);
            }
            else {
                tmark = $("<div></div>").addClass("tck_markbox").attr("id", obj.tckid);
            }
            twrap = $("<div><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span></div>").addClass("tjloading");
            tmark.append(twrap);
            if (obj.info) {
                var tinfo = $("<div>" + obj.info + "</div>").addClass("tjloadingInfo");
                tmark.append(tinfo);
            }
            $("body").append(tmark);
        }
    };
    public.loadingimg = function(obj) {
        if ($("#" + obj.tckid).length > 0) {
            $("#" + obj.tckid).show();
        } else {
            var tmark = $("<div></div>").addClass("tck_markbox").attr("id", obj.tckid),
                twrap = $("<div><img src='images/loading/loading.gif' /></div>").addClass("tjloadingimg");
            tmark.append(twrap);
            $("body").append(tmark);
        }
    }
    public.setCookie = function(c_name, value, expiredays) {
        var exdate = new Date();
        if (!expiredays)
            expiredays = 3;
        exdate.setDate(exdate.getDate() + expiredays);
        document.cookie = c_name + "=" + escape(value) +
            ";expires=" + exdate.toGMTString() + ";path=/";

    };
    public.getCookie = function(name) {
        var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
        if (arr = document.cookie.match(reg))
            return unescape(arr[2]);
        else
            return "no";
    };
    public.wxsq = function(wxcl, back, home) {
        $.ajax({
            type: "post",
            url: wxcl,
            data: { url: back },
            success: function(result) {
                if (result.substring(0, 5) == "https")
                    window.location.href = result;
            },
            error: function(jqXHR, textStatus, errorThrown) {
                public.showinfobox({ tckid: "tjtck", url: home, content: "登录超时！请稍后再试", btntext: '确定' });
            }
        })
    };
  
    public.sq = function(compid, sid, backurl, callback) {
        if (public.getCookie("TJWXuserid_" + compid + "_" + sid + "") == "no") {
            $.ajax({
                url: 'http://tjfnew.china315net.com/ajax/wxlogin.ashx',
                type: 'post',
                data: { url: backurl },
                success: function(result) {
                    if (result != 'has' && result != 'fail') //如果cookie存在 并且数据没有丢失 则授权
                        window.location.href = result;
                    else if (result == 'fail')
                        alert("数据丢失")
                    else if (callback)
                        callback();
                }
            })
        }
        else if (callback) {
            callback();
        }
    }
    public.getJs = function(url, apiList, bug, type) {
        var toUrl = "http://tjfnew.china315net.com/ajax/wxjsinfo.ashx";
        if (type) {
            if (type == "swm")
                toUrl = "http://tjfnew.china315net.com/ajax/wxjsinfoSwm.ashx";
            else if (type == "https")
                toUrl = "../ajax/wxjsinfoHttps.ashx";
        }
        $.ajax({
            url: toUrl,
            type: 'post',
            data: { url: url, type: type },
            success: function(result) {
                if (result != 'fail') {
                    var res = JSON.parse(result);
                    wx.config({
                        debug: bug ? true : false,
                        appId: res.appId,
                        timestamp: res.timestamp,
                        nonceStr: res.nonceStr,
                        signature: res.signature,
                        jsApiList: apiList
                    });
                }
                else {
                    alert("网络超时");
                }
            }
        })
    }
    public.locationPlace = {
        weidu: 0,
        jingdu: 0,
        get: function(callback) {
            var that = this;
            if (this.weidu == 0 && this.jingdu == 0) {
                wx.getLocation({
                    type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                    success: function(res) {
                        that.weidu = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                        that.jingdu = res.longitude; // 经度，浮点数，范围为180 ~ -180。 
                        if (callback) //如果回调
                            callback();
                    },
                    cancel: function() {
                    if (callback) //如果回调
                        callback();
                    },
                    fail: function() {
                        public.showinfobox({
                            content: "获取地理信息失败~",
                            btntext: "知道了",
                            tckid: "tck",
                            animate: " flipInX"
                        })
                        if (callback) //如果回调
                            callback();
                    }
                });
            }
            else {
                if (callback) //如果回调
                    callback();
            }
        }
    }
    public.loadingNew = {
        id: "",
        width: 1,
        open: function(obj) {
            if ($("#loading").length > 0) {
                this.width = 1;
                $("#loading").show();
            } else {
                var tmark, twrap;
                tmark = $("<div class='tck_markbox_noBg' id='loading'></div>");
                if (obj.src) {
                    var img = "<div class='loading-img'><img src='" + obj.src + "'/></div>";
                    tmark.append(img)
                }
                twrap = '<div class="loading-wrapper"><div class="loading-inner" id="loading_inner"></div></div>';
                tmark.append(twrap);
                if (obj.info) {
                    var tinfo = "<p class='loading-info'>" + obj.info + " <span id='loading_info'>1%</span><br/>" + obj.info2 + "</p>";
                    tmark.append(tinfo);
                }
                $("body").append(tmark);
                $("#loading_inner").css("background-color", obj.backgroundColor || "orange");
            }
            var that = this,
                num = 0;
            var timer = setInterval(function() {
                that.width += num;
                if (that.width > 100) {
                    that.width = 100;
                    clearInterval(timer);
                }
                $("#loading_inner").width(that.width + "%");
                $("#loading_info").text(that.width + "%");
                num = Math.floor(Math.random() * obj.num || 8);
            }, 200);
        },
        close: function(callback) {
            this.width = 100;
            setTimeout(function() {
            $("#loading").fadeOut();
            if (callback)
                callback();
            }, 700); 
        }
    }
    return public;
})();
window.tjpublic = tjpublic;
window.public === undefined && (window.public = tjpublic)

