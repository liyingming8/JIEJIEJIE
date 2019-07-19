"use strict";
/**
 * 加载中(默认为无背景) loading.open() haveBg为true的时候有背景
 * loading.close()有回调函数
 */
var loading = {
    haveBg: false,
    open: function (obj) {
        var haveBgFlag;
        try { haveBgFlag = obj.hasBg ? true : false }
        catch (err) { haveBgFlag = true }
        if (haveBgFlag) {
            this.haveBg = haveBgFlag;
            if (document.getElementById("loading_bg")) {
                document.getElementById("loading_bg").style.display = "block";
            }
            else {
                var loadingWrap = document.createElement("div");
                loadingWrap.setAttribute("class", "tjloading");
                for (var i = 0; i < 8; i++) {
                    var span = document.createElement("span");
                    span.style.backgroundColor = "white"
                    loadingWrap.appendChild(span);
                }
                var loadingContainer = document.createElement("div");
                loadingContainer.setAttribute("class", "tck_markbox");
                loadingContainer.setAttribute("id", "loading_bg");
                loadingContainer.appendChild(loadingWrap);
                document.body.appendChild(loadingContainer);
            }
        } else {
            if (document.getElementById("loading")) {
                document.getElementById("loading").style.display = "block";
            }
            else {
                var loadingWrap = document.createElement("div");
                loadingWrap.setAttribute("class", "tjloading");
                loadingWrap.setAttribute("id", "loading");
                for (var i = 0; i < 8; i++) {
                    loadingWrap.appendChild(document.createElement("span"));
                }
                document.body.appendChild(loadingWrap);
            }
        }
    },
    close: function (callback) {
        if (this.haveBg) {
            document.getElementById("loading_bg").style.display = "none";
        }
        else {
            document.getElementById("loading").style.display = "none";
        }
        if (typeof callback === "function") {
            callback();
        }
    }
};
/**
 * 弹出框
 * 最简单使用方法： infoBox.open({info:'确定嘛？'})  默认为网络超时
 * eid为elementId, animate为动画CSS默认为flipInX, info为提示内容,btnName为按钮名称,func为点确定执行要执行的函数
 */
var infoBox = {
    open: function (obj) {
        var eid = obj.id ? obj.id : "infoBox",
            animate = obj.animate ? obj.animate : "fadeInDown",
            info = obj.info ? obj.info : "网络超时惹！请刷新或稍后再试~",
            btnName = obj.btnName ? obj.btnName : "知道了";
        if (document.getElementById("infoBox")) {
            document.getElementById("infoBox").style.display = "block";
            document.getElementById("tck_contentwrap_p").innerHTML = info;
        }
        else {
            var ul = document.createElement("ul");
            ul.innerHTML = "<li class='tck_contentwrap' ><p id='tck_contentwrap_p'>" + info + "</p></li>";
            ul.innerHTML += "<li class='tck_choosewrap tck_choosewrapone'><a id='btnYes' class='tck_chooseyes'>" + btnName + "</a></li>";
            var content = document.createElement("div");
            content.setAttribute("class", "tck_contbox animated " + animate + "");
            content.appendChild(ul);
            var wrap = document.createElement("div");
            wrap.setAttribute("class", "tck_markbox");
            wrap.setAttribute("id", eid);
            wrap.appendChild(content);
            document.body.appendChild(wrap);
            document.getElementById("btnYes").addEventListener("click", func)
            function func() {
                document.getElementById("infoBox").style.display = "none";
                if (typeof obj.func === "function") {
                    obj.func()
                }
            }
        }
    }
}
/**
 * cookie操作 默认cookie存放三天
 * @param {any} ele
 */
var cookie = {
    set: function (c_name, value, expiredays) {
        var exdate = new Date();
        if (!expiredays)
            expiredays = 3;
        exdate.setDate(exdate.getDate() + expiredays);
        document.cookie = c_name + "=" + escape(value) +
            ";expires=" + exdate.toGMTString() + ";path=/";
    },
    get: function (c_name) {
        if (document.cookie.length > 0) {
            var c_start = document.cookie.indexOf(c_name + "=")
            if (c_start != -1) {
                c_start = c_start + c_name.length + 1;
                var c_end = document.cookie.indexOf(";", c_start);
                if (c_end == -1) c_end = document.cookie.length;
                return unescape(document.cookie.substring(c_start, c_end));
            }
        }
        return "no"
    }
}
function fadein(ele) {
    ele.style.display = "block";
    ele.style.animation = "fade .5s forwards";
} 
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return null;
    }
    return result[1];
}

 