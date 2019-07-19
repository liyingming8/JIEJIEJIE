function ShowImg(FilePath, imgId) {
    var pointCount = FilePath.lastIndexOf(".");
    var LaseName = FilePath.substring(pointCount + 1).toLowerCase();

    if (FilePath != "") {
        if (LaseName == "gif" ||
			LaseName == "bmp" ||
			LaseName == "jpg" ||
			LaseName == "jpeg" ||
            LaseName == "png" ||
            LaseName == "css" ||
            LaseName == "mp3" ||
            LaseName == "wav" ||
            LaseName == "mp4" ||
            LaseName == "ogg" ||
            LaseName == "webm"
				) {
            if (LaseName != "css" && LaseName != "mp3" && LaseName != "wav" && LaseName != "mp4" && LaseName != "ogg" && LaseName != "webm")
            {
                DrawImage(imgId, FilePath, 120, 90);
            }
        }
        else {
            alert("文件格式错误(图片必须为：gif,bmp,jpg,jpeg,png 格式！；音频必须为：mp3,wav格式！；视频必须为:mp4,ogg,webm格式！)");
        }
    }
    else {
        //alert("请选择图片！)");
    }
}

function DrawImage(imgId, FilePath, MaxWidth, MaxHeight) {
    //图片按比例缩放，且限制最大宽度和高度
    if (FilePath != "") {
        document.all[imgId].src = FilePath;
        //document.all[imgId].width = 10;
        //document.all[imgId].height = 10;
        var image = new Image();
        image.src = document.all[imgId].src;
        if (MaxHeight != "" && image.height > MaxHeight) {
            document.all[imgId].height = MaxHeight;
            document.all[imgId].width = MaxHeight * image.width / image.height;
            //alert("1");
        }
        else if (MaxWidth != "" && image.width > MaxWidth) {
            document.all[imgId].width = MaxWidth;
            document.all[imgId].height = MaxWidth * image.height / image.width;
            //alert("2");
        }
        else {
            document.all[imgId].width = image.width;
            document.all[imgId].height = image.height;
            //alert(document.all[imgId].width + "," + document.all[imgId].height);
        }
    }
}

function callBackForCss(hdObj, fileName) {   
    document.all[hdObj].value = fileName;
    alert("上传成功！");
}

function callBack(imgObj, hdObj, fileName) {
    document.all[imgObj].src = fileName.replace("~", ""); 
    document.all[hdObj].value = fileName;
    //alert("上传成功！");
}

function callBack2(imgObj, hdBigObj, hdSmallObj, BigfileName, SmallfileName) {
    document.all[imgObj].src = SmallfileName.replace("~", "");
    document.all[hdBigObj].value = BigfileName;
    document.all[hdSmallObj].value = SmallfileName;
    alert("上传成功！");
}



function SelectFile(ImageLogo, HiddenLogo, PicUrl) {
    if (PicUrl == null || PicUrl == "") {
        alert("非法操作！");
    }
    else {
        var strRetrun = showModalDialog('../SelectPicture.aspx?PicUrl=' + PicUrl + '&t=' + new Date().getTime(), '', 'dialogWidth:650px; dialogHeight:530px; help: no; scroll: yes; status: no');
        if (strRetrun != "" && strRetrun != undefined) {
            document.all[ImageLogo].src = strRetrun;
            document.all[HiddenLogo].value = strRetrun;
            DrawImage(ImageLogo, strRetrun, 600, 110);
        }
    }
}

function ReturnLogoImage(LogoUrl) {
    window.returnValue = LogoUrl; 
    window.close();
}

function openWinCenter(u, w, h) {
    var l = (screen.width - w) / 2;
    var t = (screen.height - h) / 2;
    var s = 'width=' + w + ',height=' + h + ',top=' + t + ',left=' + l;
    s += ',center=yes,menubar=no,scroll=no,resizable=no,location=no, status=no';
    window.open(u, 'oWin', s);
}

//function openWinCenter(u, w, h,title) {
//    var l = (screen.width - w) / 2;
//    var t = (screen.height - h) / 2;
//    var s = 'dialogWidth=' + w + ', dialogHeight=' + h + ', dialogTop=' + t + ', dialogLeft=' + l;
//    s += ',center=yes,menubar=no,scroll=no,resizable=no,location=no, status=no';
//    $.nsWindow.open({ title: title, width: w, height: h, dataUrl: u, theme: 'yellow' });
//} 

function showMyWindow(title, href, width, height, modal, minimizable,
        maximizable) {
    if($("#myWindow").length == 0) {
        $('body').append('<div id="myWindow" class="easyui-dialog" closed="true"></div>');
    }  
    $('#myWindow').window(
                    {
                        title: title,
                        width: width === undefined ? 600 : width,
                        height: height === undefined ? 400 : height+20,
                        content: '<iframe scrolling="no" frameborder="0"  src="'
                                + href
                                + '" style="width:100%;height:100%;"></iframe>',
                        modal: modal === undefined ? true : modal,
                        minimizable: minimizable === undefined ? false
                                : minimizable,
                        maximizable: maximizable === undefined ? false
                                : maximizable,
                        shadow: false,
                        cache: false,
                        closed: false,
                        collapsible: false,
                        resizable: true,
                        loadingMessage: '正在加载数据，请稍等片刻......'
                    });
}

function openWinCenter(u, w, h, title) { 
    showMessageDialog(u, title, w, h, true);
}

function closemyWindow() {
    this.parent.location.reload();
    this.parent.$("#msgwindow").dialog("close");
}

function closemyWindowReloadNewhref(newhref) {
    this.parent.location.href=newhref;
    this.parent.$("#msgwindow").dialog("close");
}

//url：窗口调用地址，title：窗口标题，width：宽度，height：高度，shadow：是否显示背景阴影罩层
function showMessageDialog(url, title, width, height, shadow) {
    var content = '<iframe src="' + url + '" width="100%" height="99%" frameborder="0" scrolling="auto"></iframe>';
    var boarddiv = '<div id="msgwindow" style="border-left:1px solid #fff" title="' + title + '"></div>';//style="overflow:hidden;"可以去掉滚动条
    $(document.body).append(boarddiv);
    var win = $('#msgwindow').dialog({
        content: content,
        width: width,
        height: height,
        modal: shadow, 
        title: title,
        onClose: function () {
            $(this).dialog('destroy');//后面可以关闭后的事件
        }
    });
    win.dialog('open');
}