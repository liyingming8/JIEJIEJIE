'use strict';
//处理图片
//如果要处理IOS图像被旋转90度的bug，请在使用之前引入exif.js，并将file写入obj中
//使用方法：dealImage({file:file,width:100,height:100,quality:0.7}, function (base) { });
/**
 * 压缩图片 解决IOS和部分安卓端图片被旋转90度 
 * @param {any} obj  
 * @param {any} callback 返回一个base64
 */
function dealImage(obj, callback) {
    var file = obj.file; 
    if (!/image\/\w+/.test(file.type)) {
        alert("请确保文件为图像类型");
        return false;
    }
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function (e) { 
        var img = new Image();
         //生成canvas
        var canvas = document.createElement('canvas');
        var ctx = canvas.getContext('2d');
        img.src = this.result;
        img.onload = function () {
            var that = this;
            // 默认按比例压缩
            var w = that.width,
                h = that.height,
                scale = w / h;
            w = obj.width || w;
            h = obj.height || (w / scale);
            var quality = obj.quality || 0.7;  // 默认图片质量为0.7 
            // 创建属性节点
            var anw = document.createAttribute("width");
            var anh = document.createAttribute("height"); 
                anw.nodeValue = w;
                anh.nodeValue = h;
                canvas.setAttributeNode(anw);
                canvas.setAttributeNode(anh);
                ctx.fillStyle = '#fff'; //将背景色调成白色 防止png背景变黑
                ctx.fillRect(0, 0, canvas.width, canvas.height);
                ctx.drawImage(that, 0, 0, w, h); 
            // 图像质量
            if (obj.quality && obj.quality <= 1 && obj.quality > 0) {
                quality = obj.quality;
            }
            // quality值越小，所绘制出的图像越模糊
            var base64 = canvas.toDataURL('image/jpeg', quality);
            // 回调函数返回base64的值 
            callback(base64);
        }
    }
} 

/**
 * 将base64转换为二进制 好上传
 * @param {any} dataurl base64
 */
function dataURLtoBlob(dataurl) {
    var arr = dataurl.split(','),
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]),
        n = bstr.length,
        u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new Blob([u8arr], {
        type: mime
    });
}