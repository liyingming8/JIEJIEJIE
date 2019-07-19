var oHtml = document.documentElement;
function getSize() {
    var screenWidth = oHtml.clientWidth;
    if (screenWidth > 640) {
        screenWidth = 640;
    }
    oHtml.style.fontSize = screenWidth / 20 + 'px';
}
getSize();
// 当窗口发生改变的时候调用
window.onresize = function () {
    getSize();
}
var tjpublic = (function () {
    var wxapi, public = {};
    wxapi = function (obj) {
        this.debug = obj.debug;
        this.appId = obj.appId;
        this.timestamp = obj.timestamp;
        this.nonceStr = obj.nonceStr;
        this.signature = obj.signature;
        this.jsApiList = ["onMenuShareTimeline", "onMenuShareAppMessage", "chooseImage", "previewImage", "uploadImage", "downloadImage", "scanQRCode", "getLocation"]
        console.log(this);
    };
    wxapi.prototype = {
        config: function () {
            wx.config({
                debug: this.debug,
                appId: this.appId,
                timestamp: this.timestamp,
                nonceStr: this.nonceStr,
                signature: this.signature,
                jsApiList: this.jsApiList
            });
        },
        share: function (obj) {
            console.log(this.config);
            this.config();
            wx.ready(function () {
                wx.onMenuShareTimeline({
                    title: obj.tltle,
                    link: obj.link,
                    imgUrl: obj.imgUrl,
                    success: function () {

                        if (obj.success)
                            obj.success()
                    },
                    cancel: function () {

                        if (obj.cancel)
                            obj.cancel()
                    }
                });
                wx.onMenuShareAppMessage({
                    title: obj.tltle,
                    desc: obj.desc,
                    link: obj.link,
                    imgUrl: obj.imgUrl,
                    type: 'link',
                    dataUrl: '',
                    success: function () {
                        if (obj.success)
                            obj.success()
                    },
                    cancel: function () {
                        if (obj.cancel)
                            obj.cancel()
                    }
                });
            });
        }
    }
    public.wxapi = wxapi;
    public.chooseimg = function (obj) {
        var me = this,
            file = obj.efile.files[0],
            imagetype = /image\/\w+/,
            reader = new FileReader(),
            lookimg = obj.eimg;
        if (!imagetype.test(file.type)) {
            obj.efile.value = '';
            alert('请选择图片类型上传');
            return;
        }
        if (file.size / 1024 > 500) {
            obj.efile.value = '';
            alert("上传图片过大，限制不超过500KB");
            return;
        }
        reader.onload = function (aimg) {
            return function (e) {
                aimg.src = e.target.result;
                me.file = file;
            };
        }(lookimg);
        reader.readAsDataURL(file);
    }
    public.bigimg = function (obj) {
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
            tmark.click(function () {
                $("#" + obj.tckid).hide();
            })
        }
    };
    public.showinfobox = function (obj) {
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
            bta.click(function () {
                window.location.href = obj.url;
            })
        }
        else if (obj.back) {
            bta.click(function () {
                history.back();
            })
        }
        else {
            if (obj.closeW) {
                bta.click(function () {
                    $("#" + markid).hide();
                    WeixinJSBridge.call('closeWindow');
                });
            }
            else {
                bta.click(function () {
                    $("#" + markid).hide();
                    clearTimeout(shutdowm);
                });
            }
        }
        if (obj.time) {
            clearTimeout(shutdowm);
            var shutdowm = setTimeout(function () {
                $("#" + markid).hide();
            }, obj.time)
        }
    };
    public.showwxshare = function (obj) {
        if ($("#" + obj.tckid).length > 0) {
            $("#" + obj.tckid).show();
            $("#" + obj.tckid + " img").attr({ "src": obj.src });
        } else {
            var tmark = $("<div></div>").addClass("tck_markbox").attr("id", obj.tckid),
                timg = $("<img></img>").css({ "width": "100%" }).attr({ "src": obj.src });
            tmark.append(timg);
            $("body").append(tmark);
            tmark.click(function () {
                $("#" + obj.tckid).hide();
            })
        }
    }
    public.show_confimbox = function (obj) {
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
        btno.click(function () {
            if (obj.erfunc) { obj.erfunc() }
            else { $("#" + markid).hide(); }
        });
        if (obj.func) {
            btok.click(function () {
                obj.func(obj);
            });
        } else {
            btok.click(function () {
                $("#" + markid).hide();
            });
        }
    };
    public.loading = function (obj) {
        if ($("#" + obj.tckid).length > 0) {
            $("#" + obj.tckid).show();
        } else {
            var tmark = $("<div></div>").addClass("tck_markbox").attr("id", obj.tckid),
                twrap = $("<div><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span></div>").addClass("tjloading");
            tmark.append(twrap);
            $("body").append(tmark);
        }
    };
    public.loading_js = function (obj) {
        if ($("#" + obj.tckid).length > 0) {
            $("#" + obj.tckid).show();
        } else {
            var tmark = $("<div></div>").addClass("tjloading-mark").attr("id", obj.tckid),
                tlbox = $("<div></div>").addClass("tjloading-js-outter"),
                twrap = $("<div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>").addClass("tjloading-js");
            tlbox.append(twrap);
            tmark.append(tlbox);
            $("body").append(tmark);
        }
    };
    public.loadingimg = function (obj) {
        if ($("#" + obj.tckid).length > 0) {
            $("#" + obj.tckid).show();
        } else {
            var tmark = $("<div></div>").addClass("tck_markbox").attr("id", obj.tckid),
                twrap = $("<div><img src='images/timg.gif' /></div>").addClass("tjloadingimg");
            tmark.append(twrap);
            $("body").append(tmark);
        }
    };
    public.loadingwx = function (obj) {
        var tcmarkid = obj.tckid,
            content = obj.content;
        if ($("#" + tcmarkid).length > 0) {
            $("#" + tcmarkid + "  p").text(content);
            $("#" + tcmarkid).show();
        } else {
            var tmark = $("<div></div>").addClass("tck_markbox").attr("id", obj.tckid),
                twrap = $("<div></div>").addClass("tjloading_cbox"),
                timg = $("<img/>").attr("src", obj.img ? obj.img : "../image/jz2.gif"),
                tcontent = $("<span></span>").text(content);
            twrap.append(timg);
            twrap.append(tcontent);
            tmark.append(twrap);
            $("body").append(tmark);
        }
    };
    public.setCookie = function (c_name, value, expiredays) {
        var exdate = new Date()
        exdate.setDate(exdate.getDate() + expiredays)
        document.cookie = c_name + "=" + escape(value) +
            ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
    };
    public.getCookie = function (name) {
        var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
        if (arr = document.cookie.match(reg))
            return unescape(arr[2]);
        else
            return "no";
    };
    public.addtsbox = function (obj) {
        var tcmarkid = obj.tckid ? obj.tckid : "tjaddbox_01",
            content = obj.content;
        if ($("#" + tcmarkid).length > 0) {
            $("#" + tcmarkid + "  span").text(content);
            $("#" + tcmarkid).css("opacity", "1");
        } else {
            var tmark = $("<div></div>").addClass("ts_contbox").attr("id", tcmarkid),
                tcontent = $("<span></span>").text(content);
            tmark.append(tcontent);
            $("body").append(tmark);
        }
        clearTimeout(shutdowm);
        var shutdowm = setTimeout(function () {
            $("#" + tcmarkid).css("opacity", "0");
            if (obj.url)
                window.location.href = obj.url;
        }, obj.time ? obj.time : 1500)

    };
    public.wxsq = function (wxcl, back, home) {
        $.ajax({
            type: "post",
            url: wxcl,
            data: { url: back },
            success: function (result) {
                if (result.substring(0, 5) == "https")
                    window.location.href = result;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                public.showinfobox({ tckid: "tjtck", url: home, content: "登录超时！请稍后再试", btntext: '确定' });
            }
        })
    };
    public.axiosdata = function (data) {
        let ret = ''
        for (let it in data) {
            ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
        }
        return ret
    };
    public.getquerystring = function (name) {
        var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    };
    public.getaddr = function (url) {
        var geolocation = new qq.maps.Geolocation("MWKBZ-DP7A6-IDOSB-MDDUB-JD66S-7QFYW", "myapp");
        geolocation.getLocation(function (res) {
            if (res.addr != "")
                tjpublic.setCookie("tjjs_add", res.addr, 1);
            tjpublic.setCookie("tjjs_lat", res.lat, 1);
            tjpublic.setCookie("tjjs_lng", res.lng, 1);
            tjpublic.setCookie("tjjs_city", res.city, 1);
        },
            function () {
                window.location.hre = url;
            }, { timeout: 8000 });
    };
    public.getaddr_wx = function (adobj) {
        var me = this,
            compid = adobj.compid,
            sid = adobj.sid,
            jssdkurl = adobj.jssdkurl,
            pageurl = adobj.pageurl,
            addrinfo = { jd: "0", wd: "0" };
        $.ajax({
            url: jssdkurl,// '../../ajax/get_wxjsinfo.ashx',
            type: 'post',
            data: {
                url: pageurl, compid: compid, sid: sid
            },
            success: function (result) {
                var res = JSON.parse(result);
                    wx.config({
                        debug: false,
                        appId: res.appId,
                        timestamp: res.timestamp,
                        nonceStr: res.nonceStr,
                        signature: res.signature,
                        jsApiList: ["getLocation"]
                    });

                    wx.ready(function () {
                        wx.getLocation({
                            type: 'wgs84',
                            success: function (res) {
                                addrinfo.wd = res.latitude; //纬度
                                addrinfo.jd = res.longitude;
                                adobj.success(addrinfo);
                            },
                            fail: function () {
                                adobj.success(addrinfo);
                            },
                            cancel: function () {
                                adobj.success(addrinfo);
                            }
                        })
                    });
                    wx.error(function () {
                        adobj.success(addrinfo);
                    });
                    adobj.success(addrinfo);
            },
            error: function () {
                adobj.error();
            }

        })
    };
    public.getinterface_wx = function (adobj) {
        var me = this,
            compid = adobj.compid,
            sid = adobj.sid,
            jssdkurl = adobj.jssdkurl,
            pageurl = adobj.pageurl,
            interface = adobj.interface;
        $.ajax({
            url: jssdkurl,// '../../ajax/get_wxjsinfo.ashx',
            type: 'post',
            data: {
                url: pageurl, compid: compid, sid: sid
            },
            success: function (result) {
                var res = JSON.parse(result);
                wx.config({
                    debug: false,
                    appId: res.appId,
                    timestamp: res.timestamp,
                    nonceStr: res.nonceStr,
                    signature: res.signature,
                    jsApiList: interface
                });
                wx.error(function () {
                    $.ajax({
                        type: "post",
                        url: "AjaxCL/deltoken.ashx",
                        data: { wxbug: "token" },
                        success: function () {
                            alert("对不起，网络不佳，请刷新再试！");
                        }
                    })
                });
                adobj.success();
            },
            error: function () {
                adobj.error();
            }

        })
    }
    return public;

})();
window.tjpublic = tjpublic;
window.public === undefined && (window.public = tjpublic)

