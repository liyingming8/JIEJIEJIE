function upImg(bili) {
    $("#imgReplaceBtn").on("click", function () {
        $("body").prepend('<div class="parsetcroBox" >'
            + '<div class="cropperBox">'
            + '<h4 class="boxH4" ><a class="imgBoxBtn">选择<input type="file" class="file_upload" /></a>  </h4>'
            + '<div class="imgBox"><img id="preview" src="" /></div>'
            + '<div class="imgBoxyulan"><img id="previewyulan" src="" /></div>'
            + '<div class="bottomBox">'
            + '<button class="imgBoxBtn queding">确定</button>'
            + '<button class="imgBoxBtn xuanzhuan">旋转</button>'
            + '</div>'
            + ' </button>'
            + '</div>'
            + '</div><div class="mask-container" style="display:none" id="mask"><div class="mask-wrapper"><img src="image/jz2.gif"/><p>上传中</p></div></div>');
    });
    //定义一个变量接受从后台返回的uptoken
    var upImgageToken = ''

    //					$.post($url+'upImgageToken',function(data){   //获取七牛云  token的接口  ，如果需要先上传至七牛云 取消隐藏
    //						var data=jQuery.parseJSON(data)
    //						upImgageToken=data.data
    //选择图片，将box显示出来
    $(".xzBtn").click(function() {
        $(".parsetcroBox").show();
        $(".imgBoxyulan").height($(".imgBoxyulan").width());
        $.getScript("/Admin/getpic/js/cropper.js");
    });
    //将选择的图片在裁剪区域显示出来
    $("body").on("change", ".file_upload", function () {
        var $file = $(this);
        var fileObj = $file[0];
        var type = "image/png";
        var windowUrl = window.URL || window.webkitURL;
        var dataUrl;
        var $img = $("#preview");
        if (fileObj && fileObj.files && fileObj.files[0]) {
            type = fileObj.files[0].type;
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
            crop: function (data) {
                var $imgData = $img.cropper('getCroppedCanvas');
                var dataurl = $imgData.toDataURL(type);
                $("#previewyulan").attr("src", dataurl);
            },
            built: function (e) {
            }
        });
        //console.log(dataUrl)
        $img.cropper('replace', dataUrl)
        $(".xuanzhuan").on("click", function() {
            $img.cropper('rotate', 90);
        });

        $("body").unbind("click").on("click", ".queding", function () {
            $("#mask").show();
            var $imgData = $img.cropper('getCroppedCanvas');
            var dataurl = $imgData.toDataURL(type); //dataurl便是base64图片
            //console.log(dataurl)
            $(".myimg").attr("src", dataurl);
            //imgReplaceBtn = 1;
            $.ajax({
                url: '/Admin/getpic/savepic.ashx',
                type: 'POST',
                data: { 'source': dataurl },
                success: function (res) {
                    $("#savefilepath").val(res);
                    $("#mask").hide();
                    $(".parsetcroBox").remove(); 
                }
            }); 
        }); 
    });
}


