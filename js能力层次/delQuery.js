var mz = mz || {};
mz.string = mz.string || {};
mz.url = mz.url || {};
// 库文件 /mz/string/escapeReg.js
/**
 * 在拼接正则表达式字符串时，消除原字符串中特殊字符对正则表达式的干扰
 * @author:meizz
 * @version: 2010/12/16
 * @param               {String}        str     被正则表达式字符串保护编码的字符串
 * @return              {String}                被保护处理过后的字符串
*/
mz.string.escapeReg = function (str) {
    return str.replace(new RegExp("([.*+?^=!:\x24{}()|[\\]\/\\\\])", "g"), "\\\x241");
}

// 库文件 /mz/url/delQuery.js
/// include mz.string.escapeReg;
/**
 * 删除URL字符串中指定的 Query
 * @author:meizz
 * @version:2010/12/16
 * @param               {String}        url     URL字符串
 * @param               {String}        key     被删除的Query名
 * @return              {String}                被删除指定 query 后的URL字符串
*/
mz.url.delQuery = function (url, key) {
    key = mz.string.escapeReg(key);
    var reg = new RegExp("((\\?)(" + key + "=[^&]*&)+(?!" + key + "=))|(((\\?|&)" + key + "=[^&]*)+$)|(&" + key + "=[^&]*)", "g");
    return url.replace(reg, "\x241")
}