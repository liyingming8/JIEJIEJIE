var oHtml = document.documentElement;
function getSize()
{
    var screenWidth = oHtml.clientWidth;
    console.log(screenWidth);
    if (screenWidth > 640) screenWidth = 640;
    oHtml.style.fontSize = screenWidth / 20 + 'px';
}
getSize();
window.onresize = function ()
{
    getSize();
};