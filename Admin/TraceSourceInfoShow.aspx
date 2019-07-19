<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TraceSourceInfoShow.aspx.cs" Inherits="Admin_TraceSourceInfoShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
    <link href="include/MasterPage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">
        <span id="content"></span>
    </div>
    </form>
    <script src="js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript">
        window.onload= function () { 
            var result = format('<%=JsonString%>',false);
            document.getElementById('content').innerText = result;
        }
        var formatJson = function (json, options) {
            var reg = null,
                formatted = '',
                pad = 0,
                padding = ''; // one can also use '\t' or a different number of spaces

            // optional settings
            options = options || {};
            // remove newline where '{' or '[' follows ':'
            options.newlineAfterColonIfBeforeBraceOrBracket = (options.newlineAfterColonIfBeforeBraceOrBracket === true) ? true : false;
            // use a space after a colon
            options.spaceAfterColon = (options.spaceAfterColon === false) ? false : true;

            // begin formatting...
            if (typeof json !== 'string') {
                // make sure we start with the JSON as a string
                json = JSON.stringify(json);
            } else {
                // is already a string, so parse and re-stringify in order to remove extra whitespace
                json = JSON.parse(json);
                json = JSON.stringify(json);
            }

            // add newline before and after curly braces
            reg = /([\{\}])/g;
            json = json.replace(reg, '\r\n$1\r\n');

            // add newline before and after square brackets
            reg = /([\[\]])/g;
            json = json.replace(reg, '\r\n$1\r\n');

            // add newline after comma
            reg = /(\,)/g;
            json = json.replace(reg, '$1\r\n');

            // remove multiple newlines
            reg = /(\r\n\r\n)/g;
            json = json.replace(reg, '\r\n');

            // remove newlines before commas
            reg = /\r\n\,/g;
            json = json.replace(reg, ',');

            // optional formatting...
            if (!options.newlineAfterColonIfBeforeBraceOrBracket) {
                reg = /\:\r\n\{/g;
                json = json.replace(reg, ':{');
                reg = /\:\r\n\[/g;
                json = json.replace(reg, ':[');
            }
            if (options.spaceAfterColon) {
                reg = /\:/g;
                json = json.replace(reg, ': ');
            }

            $.each(json.split('\r\n'), function (index, node) {
                var i = 0,
                    indent = 0,
                    padding = ' ';

                if (node.match(/\{$/) || node.match(/\[$/)) {
                    indent = 1;
                } else if (node.match(/\}/) || node.match(/\]/)) {
                    if (pad !== 0) {
                        pad -= 1;
                    }
                } else {
                    indent = 0;
                }

                for (i = 0; i < pad; i++) {
                    padding += padding;
                }

                formatted += padding + node + '\r\n';
                pad += indent;
            });

            return formatted;
        };

        function format(txt, compress/*是否为压缩模式*/) {/* 格式化JSON源码(对象转换为JSON文本) */
            var indentChar = '    ';
            if (/^\s*$/.test(txt)) {
                alert('数据为空,无法格式化! ');
                return;
            }
            try { var data = eval('(' + txt + ')'); }
            catch (e) {
                alert('数据源语法错误,格式化失败! 错误信息: ' + e.description, 'err');
                return;
            };
            var draw = [], last = false, This = this, line = compress ? '' : '\n', nodeCount = 0, maxDepth = 0;

            var notify = function (name, value, isLast, indent/*缩进*/, formObj) {
                nodeCount++;/*节点计数*/
                for (var i = 0, tab = ''; i < indent; i++) tab += indentChar;/* 缩进HTML */
                tab = compress ? '' : tab;/*压缩模式忽略缩进*/
                maxDepth = ++indent;/*缩进递增并记录*/
                if (value && value.constructor == Array) {/*处理数组*/
                    draw.push(tab + (formObj ? ('"' + name + '":') : '') + '[' + line);/*缩进'[' 然后换行*/
                    for (var i = 0; i < value.length; i++)
                        notify(i, value[i], i == value.length - 1, indent, false);
                    draw.push(tab + ']' + (isLast ? line : (',' + line)));/*缩进']'换行,若非尾元素则添加逗号*/
                } else if (value && typeof value == 'object') {/*处理对象*/
                    draw.push(tab + (formObj ? ('"' + name + '":') : '') + '{' + line);/*缩进'{' 然后换行*/
                    var len = 0, i = 0;
                    for (var key in value) len++;
                    for (var key in value) notify(key, value[key], ++i == len, indent, true);
                    draw.push(tab + '}' + (isLast ? line : (',' + line)));/*缩进'}'换行,若非尾元素则添加逗号*/
                } else {
                    if (typeof value == 'string') value = '"' + value + '"';
                    draw.push(tab + (formObj ? ('"' + name + '":') : '') + value + (isLast ? '' : ',') + line);
                };
            };
            var isLast = true, indent = 0;
            notify('', data, isLast, indent, false);
            return draw.join('');
        }
    </script>
</body>
</html>
