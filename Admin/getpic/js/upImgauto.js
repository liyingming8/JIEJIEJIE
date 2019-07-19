function upImgauto(bili, simg,hd) {
    //定义一个变量接受从后台返回的uptoken  
    $("body").prepend('<div id="picbox"><div class="parsetcroBox" >'
            + '<div class="cropperBox">'
            + '<h4 class="boxH4" ><a class="imgBoxBtn">选择<input type="file" id="fileup" class="file_upload" /></a>  </h4>'
            + '<div class="imgBox"><img id="preview" src="" /></div>'
            + '<div class="imgBoxyulan"><img id="previewyulan" src="" /></div>'
            + '<div class="bottomBox">'
            + '<button class="imgBoxBtn queding">确定</button>'
            + '<button class="imgBoxBtn xuanzhuan">旋转</button>'
            + '</div>'
            + ' </button>'
            + '</div>'
            + '</div><div class="mask-container" style="display:none" id="mask"><div class="mask-wrapper"><img src="/Admin/image/jz2.gif"/><p>上传中</p></div></div></div>'); 
    $("#picbox").show();
    $(".parsetcroBox").show();
    $.getScript("/Admin/getpic/js/cropper.js");
    $(".xzBtn").click(function () {  
        $(".imgBoxyulan").height($(".imgBoxyulan").width()); 
    });
    //将选择的图片在裁剪区域显示出来
    $("body").on("change", "#fileup", function () {
        var $file = $(this);
        var fileObj = $file[0];
        var type = "image/png";
        if (bili != 0) {
            var windowUrl = window.URL || window.webkitURL;
            var dataUrl;
            var $img = $("#preview");
            if (fileObj && fileObj.files && fileObj.files[0]) {
                type=fileObj.files[0].type;
                dataUrl = windowUrl.createObjectURL(fileObj.files[0]);
                $img.attr('src', dataUrl);
            } else {
                dataUrl = $file.val();
                var imgObj = document.getElementById("preview");
                imgObj.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                imgObj.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = dataUrl;
            } 
            $img.cropper({
                aspectRatio: bili,         //1 / 1,  //图片比例,1:1
                crop: function() {
                    var $imgData = $img.cropper('getCroppedCanvas');
                    //var dataurl = $imgData.toDataURL('image/png');
                    var dataurl = $imgData.toDataURL(type);
                    $("#previewyulan").attr("src", dataurl);
                },
                built: function (e) {

                }
            });
            $img.cropper('replace', dataUrl);
            $(".xuanzhuan").on("click", function () {
                $img.cropper('rotate', 90);
            });

            $("body").unbind("click").on("click", ".queding", function () {
                $("#mask").show();
                var $imgData = $img.cropper('getCroppedCanvas');
                //var dataurl = $imgData.toDataURL('image/png'); //dataurl便是base64图片 
                var dataurl = $imgData.toDataURL(type); 
                $("#" + simg).attr("src", dataurl);
                $.ajax({
                    url: './savepic.ashx',
                    type: 'POST',
                    data: { 'source': dataurl },
                    success: function (res) { 
                        $("#" + hd).val(res);
                        $("#picbox").remove();
                        bili = 0;
                    },
                    error: function (res) {
                        console.log(res);
                    }
                });
            });
        } 
    });
}


