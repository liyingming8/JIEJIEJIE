$(document).ready(function () {
    if ($("#TCpID").val() == "no") {
        OpentckNEW({
            mkid: 'tcmark',    //遮罩层ID ，not null
            boxid: 'tcbox',     // tcboxID，not null
            content: '序号不存在哦！', //显示内容，not null    
            url: '../index1.aspx'
        })
    }
    else {
        $.ajax({
            type: "get",
            url: "../yanshi/gettracesourceinfo.ashx",
            data: { cpid: $("#TcpID").val(), compid: $("#TcompID").val() }, 
            success: function (result) {
                //var obj = { "批次": { "生产批次": "XF-SC-20170707-111221", "生产日期": "2017-07-07", "质 检员": "张三" }, "勾兑": { "勾兑批次": "GD-201707-0001", "勾兑日期": "2017-07-06", "质检员": "张三" }, "制曲": { "制曲批次": "ZQ-201707-0002", "制曲日期": "2017-07-07", "质检员": "xxx" }, "原粮": [{ "原粮名称": "大豆", "入库批次": "YL-201707-2-443-0001", "入库日期": "2017-07-06", "供应商": "供应商1", "检测机构": "山西省某某检测机构", "质检员": "张三" }, { "原粮名称": "豌豆", "入库批次": "YL-201707-3-443-0002", "入库日期": "2017-07-07", "供应商": "供应商2", "检测机构": "山西省某某检测机构", "质检员": "徐" }] };
                var obj = { "原料": [{ "名称": "大豆", "产地": "黑龙江省", "供应商": "黑龙江丽水集团1" }, { "名称": "豌豆", "产地": "陕西省", "供应商": "陕西集团" }, { "名称": "小麦", "产地": "陕西省", "供应商": "陕西江天有限责任公司" }], "制曲": [{ "名称": "传统凤型大曲", "时间": "2016年7月12日" }, { "名称": "新凤型大曲", "时间": "2016年7月12日" }, { "名称": "小麦曲", "时间": "2016年7月12日", "质检员": "张三" }], "制酒": { "名称": "绵柔凤香型", "时间": "2017-07-06", "检验员": "张三" }, "灌装": { "车间": "806车间", "灌装小组": "01", "质检员": "张三" ,"时间": "2017-07-06"} };
                try {
                    if (result !== "f") 
                        obj = JSON.parse(result);
                }
                catch (err) {
                }
                 //批次
                var x, tileimg, tilespan, titlebox, contentbox, conx, libox, ulbox = $("#sy_allinfo");
                for (var x in obj) {
                    if (obj.hasOwnProperty(x)) {
                        tileimg = $("<img/>").attr("src", "images/sy/gyadd.png"),
                            tilespan = $("<span></span>").text(x),
                            titlebox = $("<div></div>").addClass("SY_syinfotile").append(tileimg, tilespan),
                            contentbox = $("<div></div>").addClass("SY_syinfocontent"),
                            conobj = obj[x];
                        if ($.isArray(conobj)) {
                            $.each(conobj, function (index, ylobj) {
                                var divbox = $("<div></div>").addClass("SY_syinfoylbox"),
                                    count = 0;
                                for (var ylx in ylobj) {
                                    count++;
                                    if (ylobj.hasOwnProperty(ylx)) {
                                        if (count ===1 ) {
                                            divbox.append("<span>" + ylobj[ylx] + "</span>");
                                        }
                                        else {
                                            divbox.append("<p>" + ylx + "：" + ylobj[ylx] + "</p>");
                                        }
                                    }
                                }
                                contentbox.append(divbox);
                            })
                            libox = $("<li></li>").append(titlebox, contentbox);
                            ulbox.append(libox);
                        }
                        else {
                            for (conx in conobj) {
                                if (conobj.hasOwnProperty(conx)) {
                                    contentbox.append("<p>" + conx + "：" + conobj[conx] + "</p>");
                                }
                            }
                            libox = $("<li></li>").append(titlebox, contentbox);
                            ulbox.append(libox);
                        }
                    }
                }
                var firstli = $("#sy_allinfo  li:eq(0)");
                firstli.addClass("SY_gyfirstinfo");
                firstli.siblings().children(".SY_syinfocontent").addClass("SY_load");
                firstli.find("img").attr("src", "images/sy/gyjian.png");
                $(".SY_infotile").on("click", function () {
                    var me = this;
                    $(me).siblings().slideToggle(function () {
                        var zt = $(this).css("display");
                        if (zt === "none") {
                            $(me).children("img").attr("src", "images/sy/add.png")
                        } else {
                            $(me).children("img").attr("src", "images/sy/jian.png")
                        }
                    });
                    $(me).parent().siblings().children(".SY_allinfobox").slideUp(function () {
                        $(this).prev().children("img").attr("src", "images/sy/add.png")
                    });
                })
                $(".SY_gyinfotile").click(showbox);
                $(".SY_syinfotile").click(showbox);
            },
            error: function () {
                OpentckNEW({
                    mkid: 'tcmark',    //遮罩层ID ，not null
                    boxid: 'tcbox',     // tcboxID，not null
                    content: '暂无溯源信息！', //显示内容，not null  
                    url:'../index1.aspx'
                })
            }
        })               
    }
})
function showbox (e) {
    e.stopPropagation();
    var me = this;
    $(me).siblings().slideToggle(function () {
        var zt = $(this).css("display");
        if (zt === "none") {
            $(me).children("img").attr("src", "images/sy/gyadd.png")
        } else {
            $(me).children("img").attr("src", "images/sy/gyjian.png")
        }
    });
  
    $(me).parent().siblings().children("div:nth-child(2)").slideUp(function () {
      
        $(this).prev().children("img").attr("src", "images/sy/gyadd.png")
    })
};
