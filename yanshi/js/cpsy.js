$(".SY_infotile").on("click", function () {
    var me = this;
    $(me).siblings().slideToggle(function () {
        var zt = $(this).css("display");
        if (zt === "none") {
            $(me).children("img").attr("src", "img/sy/add.png")
        } else {
            $(me).children("img").attr("src", "img/sy/jian.png")
        }
    });
    $(me).parent().siblings().children(".SY_allinfobox").slideUp(function () {
        $(this).prev().children("img").attr("src", "img/sy/add.png")
    });
})
$(".SY_gyinfotile").click(showbox);
$(".SY_syinfotile").click(showbox);
function showbox(e) {
    e.stopPropagation();
    var me = this;
    $(me).siblings().slideToggle(function () {
        var zt = $(this).css("display");
        if (zt === "none") {
            $(me).children("img").attr("src", "img/sy/gyadd.png")
        } else {
            $(me).children("img").attr("src", "img/sy/gyjian.png")
        }
    });

    $(me).parent().siblings().children("div:nth-child(2)").slideUp(function () {

        $(this).prev().children("img").attr("src", "img/sy/gyadd.png")
    })
};
