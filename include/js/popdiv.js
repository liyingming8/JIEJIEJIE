function ShowNo(idbg, id, ifreload) //隐藏两个层
{
    document.getElementById(idbg).style.display = "none";
    document.getElementById(id).style.display = "none";
    if (ifreload == true) {
        window.location.reload();
    } else if (ifreload != false && ifreload != "") {
        window.location.href = ifreload;
    }
}


function ShowNoCtrl(id, ifreload) {
    if (ifreload == false) {
        $(id).innerHTML = "<a href=javascript:ShowNo('doing','divUpload',false)><img src='../skin/01/images/close_01.gif' alt='关闭'></a>"
    } else {
        $(id).innerHTML = "<a href=javascript:ShowNo('doing','divUpload',true)><img src='../skin/01/images/close_01.gif' alt='关闭'></a>"
    }

}

function CloseChildWin(divUpload) {
    parent.document.all.doing.style.display = 'none';
    parent.document.all.divUpload.style.display = 'none';

}

function $(id) {
    return (document.getElementById) ? document.getElementById(id) : document.all[id];
}

function showFloat(idbg, id, url, twidth, theight, mstitle) //根据屏幕的大小显示两个层(遮罩层ID，弹窗ID，弹窗URL，弹窗宽度，弹窗高度)
{
    var range = getRange();
    $(idbg).style.width = range.width + "px";
    $(idbg).style.height = range.height + "px";
    $(idbg).style.display = "block";
    document.getElementById(id).style.display = "";
    document.getElementById("Showiframe").src = url;

    document.getElementById("openwintitle").innerHTML = mstitle;
    document.getElementById(id).style.width = twidth;
    document.getElementById("Showiframe").style.height = theight; //iframe高度调节
    document.getElementById(id).style.left = ((document.body.offsetWidth - twidth) / 2) + document.body.scrollLeft;
    document.getElementById(id).style.top = "0";
//	if(theight=="" || theight==0){
//		document.getElementById(id).style.top ="0";
//        }
//		else{
//		var totop=(document.body.offsetHeight-theight)/2+document.body.scrollTop;
//		if(totop<60){ totop=60; }
//		document.getElementById(id).style.top =totop;	
//	}	
}

function getRange() //得到屏幕的大小
{
    var top = document.body.scrollTop;
    var left = document.body.scrollLeft;
    var height = document.body.clientHeight;
    var width = document.body.clientWidth;

    if (top == 0 && left == 0 && height == 0 && width == 0) {
        top = document.documentElement.scrollTop;
        left = document.documentElement.scrollLeft;
        height = document.documentElement.clientHeight;
        width = document.documentElement.clientWidth;
    }
    return { top: top, left: left, height: height, width: width };
}

/*------------------宽度高度自适应------------------*/
function frameStyleResize(targObj) {
    var targWin = targObj.parent.document.all[targObj.name];
    if (targWin != null) {
        var HeightValue = targObj.document.body.scrollHeight
        //if(HeightValue < 200){ HeightValue = 200; } //不小于200
        targWin.style.pixelHeight = HeightValue;
    }
}

/*------------------层的拖动------------------*/
var isdown = false
var beginx, beginy

function down() {
    isdown = true;
}

function move(divname) {
    if (isdown) {
        var endx = event.clientX;
        var endy = event.clientY;
        divname = getObject(divname);
        divname.style.left = parseInt(divname.style.left) + endx - beginx;
        divname.style.top = parseInt(divname.style.top) + endy - beginy;
    }
    beginx = event.clientX;
    beginy = event.clientY;
}

function up() {
    isdown = false;
}
/*------------------层的拖动------------------*/