function dyniframesize(down) {
    var pTar = null;
    if (document.getElementById) {
        pTar = document.getElementById(down);
    } else {
        eval('pTar = ' + down + ';');
    }
    if (pTar && !window.opera) {
        pTar.style.display = "block"
        if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
            pTar.height = pTar.contentDocument.body.offsetHeight + 20;
            pTar.width = pTar.contentDocument.body.scrollWidth + 20;
        } else if (pTar.Document && pTar.Document.body.scrollHeight) {
            //ie5+ syntax 
            pTar.height = pTar.Document.body.scrollHeight;
            pTar.width = pTar.Document.body.scrollWidth;
        }
    }
}

function setFrameHeight(obj) {
    var win = obj;
    if (document.getElementById) {
        if (win && !window.opera) {
            if (win.contentDocument && win.contentDocument.body.offsetHeight)
                win.height = win.contentDocument.body.offsetHeight + "px";
            else if (win.Document && win.Document.body.scrollHeight)
                win.height = win.Document.body.scrollHeight + "px";
        }
    }
}

function getObject(id) {
    return (document.getElementById) ? document.getElementById(id) : document.all[id];
}

function txtVert(_obj) {
    _obj.value = _obj.value.replace(/\D/g, '');
}

function CheckedAll(myform) {
    for (var i = 0; i < document.myform.elements.length; i++) {
        var e = document.myform.elements[i];
        if (e.type == 'checkbox' && e.name == 'CheckObject' && e.disabled == false) {
            e.checked = true;
        }
    }
    getObject("myselect").innerHTML = "[<a href='javascript:CancelAll(myform)'>取消</a>]";
}

function CancelAll(myform) {
    for (var i = 0; i < document.myform.elements.length; i++) {
        var e = document.myform.elements[i];
        if (e.type == 'checkbox' && e.name == 'CheckObject' && e.disabled == false) {
            e.checked = false;
        }
    }
    getObject("myselect").innerHTML = "[<a href='javascript:CheckedAll(myform)'>全选</a>]";
}

/*全选取消控件2*/
function CheckedAll2(myform, ControlObj) {
    for (var i = 0; i < document.myform.elements.length; i++) {
        var e = document.myform.elements[i];
        if (e.type == 'checkbox' && e.name == 'CheckObject' && e.disabled == false) {
            e.checked = true;
        }
    }
    getObject(ControlObj).innerHTML = "[<a href=javascript:CancelAll2(myform,'" + ControlObj + "')>取消</a>]";
}

function CancelAll2(myform, ControlObj) {
    for (var i = 0; i < document.myform.elements.length; i++) {
        var e = document.myform.elements[i];
        if (e.type == 'checkbox' && e.name == 'CheckObject' && e.disabled == false) {
            e.checked = false;
        }
    }
    getObject(ControlObj).innerHTML = "[<a href=javascript:CheckedAll2(myform,'" + ControlObj + "')>全选</a>]";
}

/*全选取消控件2*/

function CheckObject(frm) {
    //var frm = getObject("myform");
    var j = 0;
    for (var i = 0; i < frm.elements.length; i++) {
        var e = frm.elements[i];
        if (e.type == 'checkbox' && e.name == 'CheckObject' && e.disabled == false && e.checked == true) {
            j++;
        }
    }
    if (j > 0) {
        return j;
    } else {
        return false;
    }
}

function CheckOnly(frm) {
    if (!CheckObject(frm)) {
        alert("请选择要编辑的记录！");
        return false;
    }
    if (CheckObject(frm) > 1) {
        alert("不能同时编辑多条记录！");
        return false;
    }
    return true;
}

function CheckLeast(frm) {
    if (!CheckObject(frm)) {
        alert("请选择要编辑的记录！");
        return false;
    } else {
        return true;
    }
}

function CheckValue(myform) {
    var ReturnValue = "";
    for (var i = 0; i < document.myform.elements.length; i++) {
        var e = document.myform.elements[i];
        if (e.type == 'checkbox' && e.name == 'CheckObject' && e.disabled == false && e.checked == true) {
            ReturnValue = ReturnValue + "," + e.value;
        }
    }
    //alert(ReturnValue);
    return ReturnValue;
}


//全部选中
function selectAll() {
    var objAll = document.all["chkSelectAll"];
    var objCheckObject = document.all["CheckObject"];

    if (objCheckObject != null) {
        if (objAll.checked) {
            if (objCheckObject.length > 0) {
                for (var i = 0; i < objCheckObject.length; i++) {
                    objCheckObject[i].checked = true;
                    chkSelect();
                }
            } else {
                objCheckObject.checked = true;
                chkSelect();
            }
        } else {
            if (objCheckObject.length > 0) {
                for (var j = 0; j < objCheckObject.length; j++) {
                    objCheckObject[j].checked = false;
                    chkSelect();
                }
            } else {
                objCheckObject.checked = false;
                chkSelect();
            }
        }
    }

}

function chkSelect() {
    var objHidId = document.all["hiddenID"];
    var objCheckObject = document.all["CheckObject"];
    var str = "";

    if (objCheckObject.length > 0) {
        for (var i = 0; i < objCheckObject.length; i++) {
            if (objCheckObject[i].checked) {
                str = str + objCheckObject[i].value + ",";
            }
        }
    } else {
        if (objCheckObject.checked) {
            str = objCheckObject.value + ",";
        }
    }

    if (str.length > 0) {
        var pos = str.length;
        str = str.substring(0, pos - 1);
    }

    objHidId.value = str;
}

//打开景点并且返回值
function OpenViewSpotItem(ControlID) {
    var url = window.showModalDialog("BL_SelectViewSpotItem.aspx", "Journeywindow", "dialogWidth:600px;dialogHeight:500px;help:no;scroll:yes;status:no");
    if (url != null) {
        if (document.getElementById(ControlID).value != "") {
            url = "-" + url;
        }
        document.getElementById(ControlID).value += url;
    }
}

//打开餐并且返回值
function OpenMeal(ControlID) {
    var url = window.showModalDialog("FWD_SelectMeal.aspx", "Journeywindow", "dialogWidth:600px;dialogHeight:500px;help:no;scroll:yes;status:no");
    if (url != null) {
        document.getElementById(ControlID).value = url;
    }
}

//打开酒店并且返回值
function OpenHotel(ControlID) {
    var url = window.showModalDialog("FWD_SelectHouse.aspx", "Journeywindow", "dialogWidth:600px;dialogHeight:500px;help:no;scroll:yes;status:no");
    if (url != null) {
        document.getElementById(ControlID).value = url;
    }
}

function checkNumber() {
    if (!(((window.event.keyCode >= 48) && (window.event.keyCode <= 57)) || (window.event.keyCode == 13))) {
        window.event.keyCode = 0;
    }
}