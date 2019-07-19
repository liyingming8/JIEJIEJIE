<!--
// flashShow
function flashShow(url, w, h, id, bg, win) {
    var flashStr =
        "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' width='" + w + "' height='" + h + "' id='" + id + "' align='middle'>" +
            "<param name='movie' value='" + url + "' />" +
            "<param name='wmode' value='" + win + "' />" +
            "<param name='menu' value='false' />" +
            "<param name='quality' value='high' />" +
            "<param name='bgcolor' value='" + bg + "' />" +
            "<embed src='" + url + "' wmode='" + win + "' menu='false' quality='high' bgcolor='" + bg + "' width='" + w + "' height='" + h + "' name='" + id + "' align='middle' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' />" +
            "</object>";
    document.write(flashStr);
}

function new_window(page, name, w, h) {
    window.open(page, name, 'width=' + w + ', height=' + h + ',left=0, top=0,toolbar=0, menubar=0, statusbar=0, scrollbar=0, resizable=0');
}

-->